using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Actions
{
    public class CreateActorAction : GameActionBase
    {
        private readonly Pos2D _pos;
        private readonly ActorType _actorType;

        public CreateActorAction(ActorType actorType, Pos2D pos)
        {
            _pos = pos;
            _actorType = actorType;
        }

        public override void Execute(GameContext context) => 
            context.AddObject(GameObjectFactory.CreateActor(_actorType, _pos));
    }
}