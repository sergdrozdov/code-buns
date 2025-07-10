using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuns.Dotnet.Common
{
    public static class Randomizer
    {
        public static void Randomize<T>(T[] items)
        {
            var rand = new Random();

            // For each spot in the array, pick
            // a random item to swap into that spot.
            for (var i = 0; i < items.Length - 1; i++)
            {
                var j = rand.Next(i, items.Length);
                (items[i], items[j]) = (items[j], items[i]);
            }
        }

        public static void Randomize<T>(List<T> items)
        {
            var rand = new Random();

            // For each spot in the array, pick
            // a random item to swap into that spot.
            for (var i = 0; i < items.Count - 1; i++)
            {
                var j = rand.Next(i, items.Count);
                (items[i], items[j]) = (items[j], items[i]);
            }
        }

        public static List<int> Randomize(int[] items)
        {
            var randomized = new List<int>();
            var original = new List<int>(items);
            var rnd = new Random();
            while (original.Count > 0)
            {
                var index = rnd.Next(original.Count);
                randomized.Add(original[index]);
                original.RemoveAt(index);
            }

            return randomized;
        }

        public static int Get(int minValue, int maxValue)
        {
            var rnd = new Random();
            return rnd.Next(minValue, maxValue);
        }

        public static int Get(int maxValue)
        {
            var rnd = new Random();
            return rnd.Next(maxValue);
        }
    }
}
