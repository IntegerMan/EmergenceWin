using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using System;

namespace MattEland.Emergence.AI.Brains
{
    public class LegacyBrainProvider : IBrainProvider
    {
        public IBrain GetBrainForActor(IActor actor)
        {
            IBrain brain;

            switch (actor.Team)
            {
                case Alignment.SystemCore:
                    brain = new PreyBrain(Alignment.Player, Alignment.Bug);
                    break;
                case Alignment.SystemAntiVirus:
                    brain = new HunterBrain(Alignment.Virus, Alignment.Bug, Alignment.Player);
                    break;
                case Alignment.SystemSecurity:
                    brain = new HunterBrain(Alignment.Bug, Alignment.Player);
                    break;
                case Alignment.Virus:
                case Alignment.Bug:
                    if (actor.ObjectId == "ACTOR_LOGIC_BOMB")
                    {
                        brain = new LogicBombBrain(Alignment.SystemCore, Alignment.SystemAntiVirus, Alignment.SystemSecurity, Alignment.Player);
                    }
                    else
                    {
                        brain = new HunterBrain(Alignment.SystemCore, Alignment.SystemAntiVirus,
                                               Alignment.SystemSecurity, Alignment.Player);
                    }

                    break;
                case Alignment.Player:
                    brain = new HunterBrain(Alignment.Bug, Alignment.Virus, Alignment.Neutral, Alignment.SystemAntiVirus, Alignment.SystemCore, Alignment.SystemSecurity);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(actor), $"Team {actor.Team} is not supported for grabbing a brain");
            }

            return brain;

        }
    }
}
