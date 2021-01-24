using Match3.Components.Game.Events;
using System;
using System.Collections.Generic;

namespace Match3.Assets.Scripts.Services.SaveLoad {
    [Serializable]
    public class PlayerData {
        public int Rating = 100;
        public long Coins = 500;
        public int WinsCount = 0;
        internal int RoundsCount = 0;
        public List<SwapRecord> UserSwaps = new List<SwapRecord>();

        public PlayerData(int rating, long coins) {
            Rating = rating;
            Coins = coins;
        }
    }
}
