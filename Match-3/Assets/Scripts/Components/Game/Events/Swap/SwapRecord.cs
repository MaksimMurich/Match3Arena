using System;

namespace Match3.Components.Game.Events {
    [Serializable]
    public struct SwapRecord {
        public float SwapRating;
        public SwapPossibility SelectedSwap;
    }
}
