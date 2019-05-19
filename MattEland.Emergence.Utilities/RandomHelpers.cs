using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Randomizations;

namespace MattEland.Emergence.Utilities
{
    public static class RandomHelpers
    {

        public static T GetRandomElement<T>(this IEnumerable<T> items, IRandomization randomization)
        {
            if (items == null)
            {
                return default(T);
            }

            var list = items.ToList();

            if (!list.Any())
            {
                return default(T);
            }

            int index = randomization.GetInt(0, list.Count - 1);
            
            return list[index];
        }

    }
}
