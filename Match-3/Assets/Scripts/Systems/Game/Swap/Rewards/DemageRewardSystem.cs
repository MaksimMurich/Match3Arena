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
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                DemageRewardRequest reward = _filter.Get1(index);

                if (Global.Data.InGame.PlayerState.Active)
                {
                    Global.Data.InGame.PlayerState.SumOpponentDemage += reward.Value;
                    OpponentState.CurrentLife -= reward.Value;
                    OpponentState.CurrentLife = Mathf.Max(OpponentState.CurrentLife, 0);
                }
                else
                {
                    Global.Data.InGame.PlayerState.CurrentLife -= reward.Value;
                    Global.Data.InGame.PlayerState.CurrentLife = Mathf.Max(Global.Data.InGame.PlayerState.CurrentLife, 0);
                }
            }

            if (Global.Data.InGame.PlayerState.CurrentLife <= 0 || OpponentState.CurrentLife <= 0)
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
