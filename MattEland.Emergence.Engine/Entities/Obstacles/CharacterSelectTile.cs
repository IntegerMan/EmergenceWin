using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class CharacterSelectTile : GameObjectBase
    {
        private bool _isHidden;
        public PlayerType PlayerType { get; }

        public CharacterSelectTile(Pos2D pos, PlayerType playerType) : base(pos)
        {
            PlayerType = playerType;
        }

        public override GameObjectType ObjectType => GameObjectType.CharacterSelect;
        public override bool IsInvulnerable => true;
        public override bool IsTargetable => false;
        public override char AsciiChar => '@';

        public override string Name => "Select a Character";

        public override void MaintainActiveEffects(GameContext context)
        {
            base.MaintainActiveEffects(context);

            UpdateIsHidden(context);
        }

        public override bool IsHidden => _isHidden;

        private void UpdateIsHidden(GameContext context)
        {
            var isHidden = context.Player.PlayerType == PlayerType;

            if (_isHidden != isHidden)
            {
                _isHidden = isHidden;
                context.UpdateObject(this);
            }
        }

        public override void ApplyActiveEffects(GameContext context)
        {
            base.ApplyActiveEffects(context);

            UpdateIsHidden(context);
        }

        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
        {
            if (!actor.IsPlayer)
            {
                return false;
            }

            // Swap the player with the new actor and position it at the tile's location
            if (!IsHidden)
            {
                context.ReplacePlayer(GameObjectFactory.CreatePlayer(Pos, PlayerType));
            }

            return true;
        }

        public override string ForegroundColor => IsHidden ? GameColors.Gray : GameColors.LightGreen;

        public override int ZIndex => 50;

        public override bool IsCorruptable => false;
    }
}