using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards
{
    public sealed class DemageRewardSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DemageRewardRequest> _filter = null;

        private bool gameIsEneded = false;

        public void Run()
        {
            if(_filter.GetEntitiesCount() <= 0)
            {
                return;
            }

            var state = Global.Data.InGame.PlayerState;

            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                DemageRewardRequest reward = _filter.Get1(index);

                if (state.Active)
                {
                    state.SumOpponentDemage += reward.Value;
                    OpponentState.CurrentLife -= reward.Value;
                    OpponentState.CurrentLife = Mathf.Max(OpponentState.CurrentLife, 0);
                }
                else
                {
                    state.CurrentLife -= reward.Value;
                    state.CurrentLife = Mathf.Max(state.CurrentLife, 0);
                }
            }

            if (state.CurrentLife <= 0 || OpponentState.CurrentLife <= 0)
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
