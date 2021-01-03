using Match3.Components.Game.Events;
using System.Collections.Generic;

namespace Match3
{
    public static class OpponentState
    {
        public static string Nick = "Guest64952";

        public static float MaxLife;
        public static float CurrentLife;
        public static float Difficult;

        public static int Rating = 1000;
        public static List<SwapPowerRange> SwapPowerRanges = new List<SwapPowerRange>();
    }
}
