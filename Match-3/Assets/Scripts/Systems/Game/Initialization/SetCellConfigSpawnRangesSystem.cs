﻿using Leopotam.Ecs;
using Match3.Configurations;
using System.Linq;

namespace Match3.Systems.Game.Initialization
{
    public sealed class SetCellConfigSpawnRangesSystem : IEcsInitSystem
    {
        private readonly InGameConfiguration _configuration = null;

        public void Init()
        {
            float sumSpawnWeights = _configuration.CellConfigurations.Sum(c => c.Weight);

            float max = 0;

            foreach (CellConfiguration cellConfiguration in _configuration.CellConfigurations)
            {
                float min = max;
                max = min + 100 * cellConfiguration.Weight / sumSpawnWeights;
                cellConfiguration.SetSpawnRange(min, max);
            }
        }
    }
}