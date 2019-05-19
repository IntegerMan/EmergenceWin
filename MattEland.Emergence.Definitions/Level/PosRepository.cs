using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MattEland.Emergence.Definitions.Level
{
    public class PosRepository
    {
        private static IDictionary<string, Pos2D> ValuesByString;

        public static Pos2D FromString(string input)
        {
            Pos2D pos;

            if (input == null)
            {
                input = string.Empty;
            }

            // Try to grab the input from the cache
            if (ValuesByString == null)
            {
                ValuesByString = new ConcurrentDictionary<string, Pos2D>();
            }
            else if (ValuesByString.TryGetValue(input, out pos))
            {
                return pos;
            }

            // Create the object
            pos = CreatePos2DFromString(input);

            // Store the new value for next time
            ValuesByString[input] = pos;

            return pos;
        }

        private static Pos2D CreatePos2DFromString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new Pos2D();
            }

            var strings = input.Split(',');

            var pos = new Pos2D(int.Parse(strings[0]), int.Parse(strings[1]));

            return pos;
        }
    }
}