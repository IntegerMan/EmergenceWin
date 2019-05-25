using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Engine.Entities
{
    public class LevelCore : Actor
    {
        public LevelCore(ActorDto dto) : base(dto)
        {
        }

        public override bool IsCapturable => true;

        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            context.CombatManager.HandleCapture(context, this, actor);

            return true;
        }

        public override string Name => "System Core";

        public override void OnCaptured(CommandContext context, [CanBeNull] GameObjectBase executor, Alignment oldTeam)
        {
            var isPlayerAction = executor != null && executor.IsPlayer;

            if (isPlayerAction)
            {
                context.Player.CoresCaptured++;
            }
            else if (oldTeam == Alignment.Player)
            {
                context.Player.CoresCaptured--;
            }

            int numRemaining = context.Level.Cores.Count(c => c.Team == Alignment.SystemCore || c.Team == Alignment.SystemAntiVirus || c.Team == Alignment.SystemSecurity);

            context.Level.HasAdminAccess = numRemaining <= 0;

            if (isPlayerAction)
            {
                var message = context.Level.HasAdminAccess
                    ? $"You have captured a {Name}. You now have administrative access and can pass through the firewall."
                    : $"You have captured a {Name}. {numRemaining} more must be captured before the firewall opens.";

                context.AddMessage(message, ClientMessageType.Success);
            }
            else
            {
                var executorName = executor?.Name;
                if (string.IsNullOrEmpty(executorName))
                {
                    executorName = "Corruption";
                }

                var message = numRemaining > 0
                    ? $"{executorName} has claimed a {Name}. {numRemaining} more must be captured before the firewall opens."
                    : $"{executorName} has claimed the last {Name}. The firewall has been compromised and the exit is open.";

                context.AddMessage(message, ClientMessageType.Success);
            }

            context.UpdateObject(this);
            context.Level.Objects.OfType<Firewall>().Each(context.UpdateObject);
        }

        public override void ApplyCorruptionDamage(CommandContext context, [CanBeNull] GameObjectBase source, int damage)
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

        public bool IsCaptured => ActualTeam == Alignment.Player;

        public override string ForegroundColor => IsCaptured ? GameColors.Green : GameColors.Yellow;
    }
}