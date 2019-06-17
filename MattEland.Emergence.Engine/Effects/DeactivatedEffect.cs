using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class DeactivatedEffect : EffectBase
    {
        public string CommandName { get; }

        public DeactivatedEffect(GameObjectBase source, string commandName) : base(source)
        {
            CommandName = commandName;
        }
        
        public override string ForegroundColor => GameColors.DarkGray;
    }
}