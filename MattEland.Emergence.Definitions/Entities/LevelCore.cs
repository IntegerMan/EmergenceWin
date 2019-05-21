using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

namespace MattEland.Emergence.Definitions.Entities
{
    public class LevelCore : Actor
    {
        public LevelCore(ActorDto dto) : base(dto)
        {
        }

        public override bool IsCapturable => true;

        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor)
        {
            context.CombatManager.HandleCapture(context, this, actor);

            return false;
        }

        protected override string CustomName => "System Core";

        public override void OnCaptured(ICommandContext context, [CanBeNull] IGameObject executor, Alignment oldTeam)
        {
            var isPlayerAction = executor != null && executor.IsPlayer;

            if (isPlayerAction)
            {
                context.Player.CoresCaptured++;
            }
            else
            {
                if (oldTeam == Alignment.Player)
                {
                    context.Player.CoresCaptured--;

                }
            }

            int numRemaining = context.Level.Cores.Count(c => c.Team == Alignment.SystemCore || c.Team == Alignment.SystemAntiVirus || c.Team == Alignment.SystemSecurity);

            context.Level.HasAdminAccess = numRemaining <= 0;

            if (isPlayerAction)
            {
                context.AddMessage(
                    context.Level.HasAdminAccess
                        ? $"You have captured a {Name}. You now have administrative access and can pass through the firewall."
                        : $"You have captured a {Name}. {numRemaining} more must be captured before the firewall opens.",
                    ClientMessageType.Success);
            }
            else
            {
                var executorName = executor?.Name;
                if (string.IsNullOrEmpty(executorName))
                {
                    executorName = "Corruption";
                }

                if (numRemaining > 0)
                {
                    context.AddMessage(
                        $"{executorName} has claimed a {Name}. {numRemaining} more must be captured before the firewall opens.",
                        ClientMessageType.Success);
                }
                else
                {
                    context.AddMessage(
                        $"{executorName} has claimed the last {Name}. The firewall has been compromised and the exit is open.",
                        ClientMessageType.Success);
                }
            }
        }

        public override void ApplyCorruptionDamage(ICommandContext context, [CanBeNull] IGameObject source, int damage)
        {
            base.ApplyCorruptionDamage(context, source, damage);

            var currentTeam = ActualTeam;

            if (IsCorrupted && currentTeam != Team)
            {
                ActualTeam = Team;
                OnCaptured(context, source, currentTeam);
            }
        }

        public override char AsciiChar => 'C';
        public override void OnInteract(CommandContext context, IActor actor)
        {
            context.DisplayNotImplemented();
        }

        public override string ForegroundColor => GameColors.Yellow;
    }
}