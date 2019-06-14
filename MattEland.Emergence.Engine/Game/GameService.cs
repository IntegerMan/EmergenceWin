using System;
using System.Linq;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Level.Generation;
using MattEland.Emergence.Engine.Level.Generation.Encounters;
using MattEland.Emergence.Engine.Level.Generation.Prefabs;
using MattEland.Emergence.Engine.Loot;
using MattEland.Emergence.Engine.Messages;
using MattEland.Emergence.Engine.Model;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Engine.Game
{
    /// <summary>
    /// A service for dealing with high-level game operations such as new games or player moves.
    /// </summary>
    public sealed class GameService
    {
        [NotNull] private readonly LevelGenerationService _levelService;
        [NotNull] private readonly LootProvider _lootProvider;
        [NotNull] private readonly CombatManager _combatManager;

        public GameStatus State { get; private set; } = GameStatus.NotStarted;

        public LevelData Level
        {
            get => _level;
            private set
            {
                _level = value;
                Context?.SetLevel(value);
            }
        }

        [NotNull]
        private readonly IRandomization _randomizer;

        private readonly GameCommand _moveCommand;
        private readonly GameCommand _waitCommand;
        private LevelData _level;

        [CanBeNull]
        public GameContext Context { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        public GameService([CanBeNull] IRandomization randomizer = null)
        {
            _combatManager = new CombatManager();
            _lootProvider = new LootProvider();
            _levelService = new LevelGenerationService(new PrefabService(), new EncountersService(), new BasicRandomization());
            _randomizer = randomizer ?? new BasicRandomization();
            _moveCommand = new MoveCommand();
            _waitCommand = new WaitCommand();
        }

        public GameContext StartNewGame(PlayerType playerType = PlayerType.Logistics)
        {
            if (State != GameStatus.NotStarted && State != GameStatus.GameOver) throw new InvalidOperationException("The game has already been started");

            State = GameStatus.Executing;


            // Set up the basic parameters
            var levelParameters = new LevelGenerationParameters { LevelType = LevelType.Tutorial };
            Player = GameObjectFactory.CreatePlayer(new Pos2D(0,0), playerType);
            Level = _levelService.GenerateLevel(levelParameters, Player);
            Context = new GameContext(Level, this, _combatManager, _lootProvider, _randomizer);
            
            Level.Objects.Each(o => Context.CreatedObject(o));

            // Ensure line of sight is calculated. This message should come after new creation events
            UpdatePlayerLineOfSight();
            
            State = GameStatus.Ready;

            return Context;
        }

        public GameContext HandleCommand([NotNull] CommandSlot slot, Pos2D pos)
        {
            if (slot.Command == null) throw new ArgumentNullException(nameof(slot));

            return HandleCommand(slot.Command, pos, slot.IsActive);
        }

        public GameContext HandleCommand([NotNull] GameCommand command, Pos2D pos, bool isActive = false)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (State == GameStatus.GameOver) throw new InvalidOperationException("The game is over. Start a new game to play again.");
            if (State != GameStatus.Ready) throw new InvalidOperationException("The game is not ready for input");

            Context.ClearMessages();

            State = GameStatus.Executing;

            NumMoves++;

            // Give objects and actors a chance to react to the current game state
            foreach (var obj in Context.Level.Objects.ToList())
            {
                obj.ApplyActiveEffects(Context);
            }

            command.Execute(Context, Player, pos, isActive);

            if (command.ActivationType == CommandActivationType.Active)
            {
                var activeState = !isActive; // TODO: This should really come from context or be set by context
                Player.SetCommandActiveState(command, activeState);
            }

            var nonDead = Context.Level.Objects.Where(o => !o.IsDead).ToList();

            nonDead.OfType<Actor>().Each(a => ProcessActorTurn(a, Context));

            // Give objects and actors a chance to react to the changed state
            nonDead.Each(o => o.MaintainActiveEffects(Context));

            // Ensure that vision is accurate for the player
            UpdatePlayerLineOfSight();

            // The player can change so make sure we keep a reference to the correct player object
            Player = Context.Player;

            // Death should end the game
            if (Player.IsDead && !Context.IsGameOver)
            {
                Context.EndGame();
            }
            
            // Update the game state
            State = Context.IsGameOver ? GameStatus.GameOver : GameStatus.Ready;
            
            return Context;
        }

        private void UpdatePlayerLineOfSight()
        {
            var cells = Context.CalculateLineOfSight(Context.Player);
            Context.AddMessage(new VisibleCellsMessage(cells));
        }

        private static void ProcessActorTurn([NotNull] Actor actor, [NotNull] GameContext context)
        {
            // Allow AI to do their thing
            if (!actor.IsPlayer && !actor.IsDead)
            {
                context.AI.ProcessActorTurn(actor);
            }

            // Regenerate operations
            actor.AdjustOperationsPoints(1);
        }

        public int NumMoves { get; set; }
        public Player Player { get; private set; }

        public GameContext MovePlayer(MoveDirection direction) => HandleCommand(_moveCommand, Player.Pos.GetNeighbor(direction));

        internal LevelData GenerateLevel(LevelGenerationParameters levelParams, Player player) => Level = _levelService.GenerateLevel(levelParams, player);

        public GameContext Wait() => HandleCommand(_waitCommand, Player.Pos);
    }
}