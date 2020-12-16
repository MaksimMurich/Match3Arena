using System;

namespace Match3.Components.Game.Events
{
    [Serializable]
    public struct SwapPossibility
    {
        public int FromX;
        public int FromY;
        public int ToX;
        public int ToY;
        public SwapRewards SwapRewards;
    }
}
