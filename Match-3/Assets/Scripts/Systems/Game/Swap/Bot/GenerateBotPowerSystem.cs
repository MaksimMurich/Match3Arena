using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Components.Game.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Bot
{
    public sealed class GenerateBotSwapPowerRangesSystem : IEcsInitSystem
    {
        private readonly PlayerData _playerData = null;

        public void Init()
        {
            List<SwapPowerRange> swapPowerRanges = new List<SwapPowerRange>();
            Dictionary<float, int> userSwapsByPoewr = new Dictionary<float, int>(); // rating 3 of 10 swaps = 70% power

            // calculate swaps count by rating
            for (int i = 0; i < Global.Data.Player.UserSwaps.Count; i++)
            {
                SwapRecord swap = Global.Data.Player.UserSwaps[i];

                if (!userSwapsByPoewr.ContainsKey(swap.SwapRating))
                {
                    userSwapsByPoewr.Add(swap.SwapRating, 0);
                }

                userSwapsByPoewr[swap.SwapRating]++;
            }

            float rangeMinValue = 0;

            float totalSwaps = Global.Data.Player.UserSwaps.Count;

            foreach (KeyValuePair<float, int> powerSwapsCount in userSwapsByPoewr)
            {
                //powerSwapsCount.Key, rangeMinValue / keysCount
                swapPowerRanges.Add(new SwapPowerRange()
                {
                    Power = powerSwapsCount.Key,
                    RangeMin = rangeMinValue / totalSwaps,
                    RangeMax = (rangeMinValue + powerSwapsCount.Value) / totalSwaps
                });

                rangeMinValue += powerSwapsCount.Value;
            }

            if (swapPowerRanges.Count == 0)
            {
                swapPowerRanges = GenerateDefultSwapRanges();
            }

            float swapsPowerMultiplayer = 0.5f + OpponentState.Difficult;

            for (int i = 0; i < swapPowerRanges.Count; i++)
            {
                float power = swapPowerRanges[i].Power * swapsPowerMultiplayer;
                power = Mathf.Min(1, power);
                swapPowerRanges[i].SetPower(power);
            }

            OpponentState.SwapPowerRanges = swapPowerRanges;
        }

        private List<SwapPowerRange> GenerateDefultSwapRanges()
        {
            List<SwapPowerRange> result = new List<SwapPowerRange>();
            result.Add(new SwapPowerRange() { Power = .1f, RangeMin = 0.0f, RangeMax = 0.2f, });
            result.Add(new SwapPowerRange() { Power = .3f, RangeMin = 0.2f, RangeMax = 0.4f, });
            result.Add(new SwapPowerRange() { Power = .4f, RangeMin = 0.4f, RangeMax = 0.6f, });
            result.Add(new SwapPowerRange() { Power = .6f, RangeMin = 0.6f, RangeMax = 0.8f, });
            result.Add(new SwapPowerRange() { Power = .7f, RangeMin = 0.8f, RangeMax = 1f, });

            return result;
        }
    }
}
