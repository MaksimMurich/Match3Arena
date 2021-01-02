using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards
{
    public sealed class DemageRewardSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly PlayerState _playerState = null;
        private readonly EcsFilter<DemageRewardRequest> _filter = null;

        private bool gameIsEneded = false;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                DemageRewardRequest reward = _filter.Get1(index);

                if (_playerState.Active)
                {
                    _playerState.SumOpponentDemage += reward.Value;
                    OpponentState.CurrentLife -= reward.Value;
                    OpponentState.CurrentLife = Mathf.Max(OpponentState.CurrentLife, 0);
                }
                else
                {
                    _playerState.CurrentLife -= reward.Value;
                    _playerState.CurrentLife = Mathf.Max(_playerState.CurrentLife, 0);
                }
            }

            if (_playerState.CurrentLife <= 0 || OpponentState.CurrentLife <= 0)
            {
                if (gameIsEneded)
                {
                    return;
                }
                else
                {
                    gameIsEneded = true;
                     Global.Data.InGame.World.NewEntity().Set<EndRoundRequest>();
                }
            }
        }
    }
}
