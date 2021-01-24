using System;

namespace Match3.Components.Game.Events {
    [Serializable]
    public struct SwapRewards {
        public int HealthReward;
        public int DemageReward;

        public int CalculateTotal() {
            return HealthReward + DemageReward;
        }
    }
}
