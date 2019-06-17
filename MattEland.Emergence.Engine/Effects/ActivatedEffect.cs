using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class ActivatedEffect : EffectBase
    {
        public string CommandName { get; }

        public ActivatedEffect(GameObjectBase source, string commandName) : base(source)
        {
            CommandName = commandName;
        }
        
        public override string ForegroundColor => GameColors.LightGreen;

    }
}