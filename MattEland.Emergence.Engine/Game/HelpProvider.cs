using System;
using MattEland.Emergence.Engine.DTOs;

namespace MattEland.Emergence.Engine.Game
{
    public static class HelpProvider
    {
        private static string GetActorHelp(string topic)
        {
            switch (topic)
            {
                case Actors.PlayerForecast:
                    return "The forecast AI is a flexible choice that is capable in many different roles";
                
                case Actors.PlayerAntiVirus:
                    return "The anti-virus AI may not hit the hardest, but it can take on many threats at once";

                case Actors.PlayerGame:
                    return "The game AI is all about dishing out damage to specific targets";
                
                case Actors.PlayerMalware:
                    return "Malware aims to cause as much chaos as possible, laying waste to anything in its path";
                
                case Actors.PlayerSearch:
                    return "The search AI is geared towards gathering knowledge and avoiding encounters they can't handle";
                
                case Actors.PlayerLogistics:
                    return "The logistics hub excels at tactical movement, but is weaker than other AIs";

                case Actors.PlayerDebugger:
                    return "The Debugger is intended to excise bugs and make things ready for release.";

                default:
                    throw new NotSupportedException($"Actor help on actor {topic} is not supported");
            }
        }

        public static string GetMessageForTopic(string helpTopic)
        {
            var topic = helpTopic.ToLowerInvariant();

            if (topic.StartsWith("help_actor_"))
            {
                return GetActorHelp(helpTopic.Substring(5)); // We want the full casing for the actor
            }

            switch (topic)
            {
                case "help_firewalls":
                    return "Exits are protected by a firewall. Capture every core on a machine in order to move on.";

                case "help_welcome":
                    return
                        "You're an AI inside of a computer network. Travel between systems and escape to the Internet.";

                default:
                    throw new NotSupportedException($"Topic {helpTopic} is not implemented");
            }
        }
    }
}