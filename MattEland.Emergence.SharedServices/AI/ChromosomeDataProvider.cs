using System.Collections.Generic;

namespace MattEland.Emergence.Services.AI
{
    public class ChromosomeDataProvider
    {
        public IEnumerable<string> ChromosomeIds
        {
            get
            {
                yield return "ACTOR_BIT";
                yield return "ACTOR_WORM";
                yield return "ACTOR_VIRUS";
                yield return "ACTOR_ANTI_VIRUS";
                yield return "ACTOR_KERNEL_WORKER";
                yield return "ACTOR_DEFENDER";
                yield return "ACTOR_DAEMON";
                yield return "ACTOR_GARBAGE_COLLECTOR";
                yield return "ACTOR_GLITCH";
                yield return "ACTOR_BUG";
                yield return "ACTOR_FEATURE";
                yield return "ACTOR_SEC_AGENT";
                yield return "ACTOR_INSPECTOR";
                yield return "ACTOR_SEARCH";
                yield return "ACTOR_HELP";
                yield return "ACTOR_LOGIC_BOMB";
            }
        }
    }
}