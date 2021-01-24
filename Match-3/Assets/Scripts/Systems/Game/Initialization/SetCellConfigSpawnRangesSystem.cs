using Leopotam.Ecs;
using Match3.Configurations;
using System.Linq;

namespace Match3.Systems.Game.Initialization {
    public sealed class SetCellConfigSpawnRangesSystem : IEcsInitSystem {
        public void Init() {
            float sumSpawnWeights = Global.Config.InGame.CellConfigurations.Sum(c => c.Weight);

            float max = 0;

            foreach (CellConfiguration cellConfiguration in Global.Config.InGame.CellConfigurations) {
                float min = max;
                max = min + 100 * cellConfiguration.Weight / sumSpawnWeights;
                cellConfiguration.SetSpawnRange(min, max);
            }
        }
    }
}