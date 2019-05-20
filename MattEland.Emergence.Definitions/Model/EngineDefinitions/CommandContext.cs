using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Entities;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.Messages;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Definitions.Model.EngineDefinitions
{
    public class CommandContext : ICommandContext
    {
        [NotNull, ItemNotNull]
        private readonly IEnumerable<IGameObject> _objects;

        [NotNull, ItemNotNull]
        private readonly List<GameMessage> _messages = new List<GameMessage>();

        public CommandContext([NotNull] IGameManager gameManager, [NotNull] Player player, [NotNull] IEnumerable<IGameObject> objects)
        {
            _objects = objects ?? throw new ArgumentNullException(nameof(objects));
            GameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }

        [NotNull]
        public IGameManager GameManager { get; }
        
        [NotNull] public Player Player { get; }
        
        public void MoveObject([NotNull] IGameObject obj, Pos2D newPos)
        {            
            var oldPos = obj.Pos;
        
            obj.Pos = newPos;
        
            AddMessage(new MovedMessage(obj, oldPos, newPos));
        }

        private void AddMessage([NotNull] GameMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            
            _messages.Add(message);
        }
        
        private void AddMessages([NotNull, ItemNotNull] IEnumerable<GameMessage> messages)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            
            _messages.AddRange(messages);
        }

        public void MoveExecutingActor(Pos2D newPos) => MoveObject(Player, newPos);
        public void UpdateObject(IGameObject gameObject) => AddMessage(new ObjectUpdatedMessage(gameObject));

        public void UpdateCapturedCores()
        {
            /* TODO
            // Check to see if all cores are captured
            var cores = _objects.OfType<Core>().ToList();
            int totalCores = cores.Count;
            int capturedCores = cores.Count(c => c.IsCaptured);
            var isOpen = capturedCores >= totalCores;

            // Display messages to the user
            DisplayText($"{capturedCores} of {totalCores} are now under your control.");
            if (isOpen)
            {
                DisplayText("You can now breach the firewall.");
            }

            // Firewalls may have opened / closed
            _objects.OfType<Firewall>().Each(firewall =>
            {
                firewall.IsOpen = isOpen;
                UpdateObject(firewall);
            });
            */
        }

        public void DisplayText(string text) => AddMessage(new DisplayTextMessage(text));

        [NotNull, ItemNotNull]
        public IEnumerable<GameMessage> Messages => _messages;
        public void AdvanceToNextLevel()
        {
            AddMessages(_objects.Where(o => o != Player).Select(o => new DestroyedMessage(o)));
            AddMessages(GameManager.GenerateLevel());
        }

    }
}