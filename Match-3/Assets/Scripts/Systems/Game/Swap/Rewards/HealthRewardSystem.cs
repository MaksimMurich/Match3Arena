using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards {
    public sealed class HealthRewardSystem : IEcsRunSystem {
        private readonly EcsFilter<HealthRewardRequest> _filter = null;

        public void Run() {
            foreach (int index in _filter) {
                var state = Global.Data.InGame.PlayerState;
                EcsEntity entity = _filter.GetEntity(index);
                HealthRewardRequest reward = _filter.Get1(index);

                if (state.Active) {
                    state.SumHealseRestored += reward.Value;

                    state.CurrentLife += reward.Value;
                    state.CurrentLife = Mathf.Min(state.CurrentLife, state.MaxLife);
                }
                else {
                    OpponentState.CurrentLife += reward.Value;
                    OpponentState.CurrentLife = Mathf.Min(OpponentState.CurrentLife, OpponentState.MaxLife);
                }
            }
        }
    }
}
