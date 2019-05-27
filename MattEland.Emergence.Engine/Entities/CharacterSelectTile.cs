using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities
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

        public override string Name => "Select a Character";

        public override void MaintainActiveEffects(CommandContext context)
        {
            base.MaintainActiveEffects(context);

            UpdateIsHidden(context);
        }

        private void UpdateIsHidden(CommandContext context)
        {
            var isHidden = context.Player.ObjectId == ObjectId;

            if (IsHidden != isHidden)
            {
                IsHidden = isHidden;
                context.UpdateObject(this);
            }
        }

        public override void ApplyActiveEffects(CommandContext context)
        {
            base.ApplyActiveEffects(context);

            UpdateIsHidden(context);
        }

        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (!actor.IsPlayer)
            {
                return false;
            }

            // Swap the player with the new actor and position it at the tile's location
            if (!IsHidden)
            {
                context.ReplacePlayer(CreationService.CreatePlayer(ObjectId));
            }

            return true;
        }

        public override string ForegroundColor => IsHidden ? GameColors.Gray : GameColors.LightGreen;

        public override int ZIndex => 50;

        public override bool IsCorruptable => false;
    }
}