using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards
{
    public sealed class HealthRewardSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthRewardRequest> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                HealthRewardRequest reward = _filter.Get1(index);

                if (Global.Data.InGame.PlayerState.Active)
                {
                    Global.Data.InGame.PlayerState.SumHealseRestored += reward.Value;

                    Global.Data.InGame.PlayerState.CurrentLife += reward.Value;
                    Global.Data.InGame.PlayerState.CurrentLife = Mathf.Min(Global.Data.InGame.PlayerState.CurrentLife, Global.Data.InGame.PlayerState.MaxLife);
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
