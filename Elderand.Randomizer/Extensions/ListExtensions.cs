using System;
using System.Collections.Generic;

namespace Elderand.Randomizer.Extensions
{
    public static class ListExtensions
    {
        public static int GetLastIndex<T>(this List<T> list)
        {
            return list.Count - 1;
        }

        public static int GetRandomIndex<T>(this List<T> list, Random rng)
        {
            return rng.Next(list.Count);
        }

        public static void Shuffle<T>(this List<T> list, Random rng)
        {
            int upperIdx = list.Count;
            while (upperIdx > 1)
            {
                upperIdx--;
                int randIdx = rng.Next(upperIdx + 1);
                T value = list[randIdx];
                list[randIdx] = list[upperIdx];
                list[upperIdx] = value;
            }
        }
    }
}
