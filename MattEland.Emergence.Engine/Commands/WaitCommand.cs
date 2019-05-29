using System.Linq;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
{
    public class WaitCommand : GameCommand
    {
        public override string Id => "WAIT";
        public override string Name => "Wait";
        public override string Description => "Does literally nothing";
        public override int ActivationCost => 0;
        public override string IconId => string.Empty;
        public override Rarity Rarity => Rarity.None;

        public override bool IsSilent => true;

        public override CommandActivationType ActivationType => CommandActivationType.Simple;
    }
}