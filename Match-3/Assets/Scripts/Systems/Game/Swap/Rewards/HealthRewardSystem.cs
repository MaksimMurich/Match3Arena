using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards
{
    public sealed class HealthRewardSystem : IEcsRunSystem
    {
        private readonly PlayerState _playerState = null;
        private readonly EcsFilter<HealthRewardRequest> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                HealthRewardRequest reward = _filter.Get1(index);

                if (_playerState.Active)
                {
                    _playerState.SumHealseRestored += reward.Value;

                    _playerState.CurrentLife += reward.Value;
                    _playerState.CurrentLife = Mathf.Min(_playerState.CurrentLife, _playerState.MaxLife);
                }
                else
                {
                    OpponentState.CurrentLife += reward.Value;
                    OpponentState.CurrentLife = Mathf.Min(OpponentState.CurrentLife, OpponentState.MaxLife);
                }
            }
        }
    }
}
