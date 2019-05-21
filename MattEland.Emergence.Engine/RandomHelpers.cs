using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;

namespace MattEland.Emergence.Engine
{
    public static class RandomHelpers
    {

        public static T GetRandomElement<T>([CanBeNull] this IEnumerable<T> items, [NotNull] IRandomization randomization)
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
