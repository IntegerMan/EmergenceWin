using System;
using System.Collections.Generic;
using GeneticSharp.Domain.Randomizations;

namespace MattEland.Emergence.Tests
{
    public class TestRandomizer : IRandomization
    {
        private readonly Random _random;

        public TestRandomizer(int seed = 42)
        {
            _random = new Random(seed);
        }

        public int GetInt(int min, int max) => _random.Next(min, max);

        public int[] GetInts(int length, int min, int max)
        {
            var values = new List<int>(length);

            while (length-- > 0)
            {
                values.Add( GetInt(min, max));
            }

            return values.ToArray();
        }

        public int[] GetUniqueInts(int length, int min, int max)
        {
            var values = new List<int>(length);

            int val;
            while (length > 0)
            {
                do
                {
                    val = GetInt(min, max);
                } while (values.Contains(val));

                values.Add( val);
                length--;
            }

            return values.ToArray();
        }

        public float GetFloat() => (float) _random.NextDouble();

        public float GetFloat(float min, float max) => (float) (_random.NextDouble() * max + min);

        public double GetDouble() => _random.NextDouble();

        public double GetDouble(double min, double max) => _random.NextDouble() * max + min;
    }
}