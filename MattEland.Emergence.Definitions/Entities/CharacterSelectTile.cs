using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class CharacterSelectTile : GameObjectBase
    {
        public CharacterSelectTile(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true;
        public override bool IsTargetable => false;
        public override bool IsInteractive => true;
        public override char AsciiChar => '@';

        protected override string CustomName => "Select a Character";

        public override void MaintainActiveEffects(ICommandContext context)
        {
            base.MaintainActiveEffects(context);

            UpdateIsHidden(context);
        }

        private void UpdateIsHidden(ICommandContext context)
        {
            // Don't show this if the player is currently that object type
            IsHidden = context.Player.ObjectId == ObjectId;
        }

        public override void ApplyActiveEffects(ICommandContext context)
        {
            base.ApplyActiveEffects(context);

            UpdateIsHidden(context);
        }

        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (!actor.IsPlayer)
            {
                return false;
            }

            UpdateIsHidden(context);

            // Swap the player with the new actor and position it at the tile's location
            if (!IsHidden)
            {
                context.ReplacePlayer(CreationService.CreatePlayer(ObjectId), Pos);
            }

            UpdateIsHidden(context);

            return true;
        }

        public override bool IsCorruptable => false;
    }
}