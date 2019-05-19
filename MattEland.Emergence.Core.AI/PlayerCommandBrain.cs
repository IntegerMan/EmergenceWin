using System;
using System.Collections.Generic;
using MattEland.Emergence.AI.Brains;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.AI
{
    public class PlayerCommandBrain : ActorBrainBase
    {
        private readonly Pos2D _desiredPos;
        private readonly GameCommandDTO _command;

        public override bool HandleSpecialCommand(ICommandContext context, IActor actor)
        {
            if (_command.CommandType == CommandType.Command)
            {
                HandleActorCommand(context, context.Player, _command.CommandData, Pos2D.FromString(_command.CommandPosition));

                return false;
            }

            return base.HandleSpecialCommand(context, actor);
        }

        public override string Id => "STATIC_BRAIN_PLAYER";

        public PlayerCommandBrain(GameCommandDTO command, IGameObject actor)
        {


            if (command.CommandType == CommandType.Pathfind)
            {
                var pos = Pos2D.FromString(command.CommandPosition);
                int xDelta = pos.X - actor.Position.X;
                int yDelta = pos.Y - actor.Position.Y;

                if (Math.Abs(xDelta) < Math.Abs(yDelta))
                {
                    if (yDelta < 0)
                    {
                        command.CommandType = CommandType.MoveUp;
                    }
                    else if (yDelta > 0)
                    {
                        command.CommandType = CommandType.MoveDown;
                    }
                    else
                    {
                        command.CommandType = CommandType.Wait;
                    }
                }
                else if (xDelta < 0)
                {
                    command.CommandType = CommandType.MoveLeft;
                }
                else if (xDelta > 0)
                {
                    command.CommandType = CommandType.MoveRight;
                }
                else
                {
                    command.CommandType = CommandType.Wait;
                }
            }

            _desiredPos = command.CalculateRequestedNewPosition(actor);
            _command = command;
        }

        protected override decimal CalculateCellScore(IGameCell choice, IActor actor, IEnumerable<IGameCell> otherCells, ICommandContext context)
        {
            return choice.Pos == _desiredPos ? 1 : 0;
        }
    }
}