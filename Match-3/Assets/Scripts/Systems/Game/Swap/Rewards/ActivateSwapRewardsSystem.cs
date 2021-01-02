using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Configurations;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards
{
    public sealed class ActivateSwapRewardsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RewardRequest> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                RewardRequest reward = _filter.Get1(index);
                CellConfiguration config = reward.Cell.Configuration;

                 Global.Data.InGame.World.NewEntity().Set<HealthRewardRequest>() = new HealthRewardRequest()
                {
                    Value = config.Health,
                    Position = reward.Position,
                    View = reward.Cell.Configuration.HealthRewardView,
                };

                 Global.Data.InGame.World.NewEntity().Set<DemageRewardRequest>() = new DemageRewardRequest()
                {
                    Value = config.Demage,
                    Position = reward.Position,
                    View = reward.Cell.Configuration.DemageRewardView,
                };
            }
        }
    }
}
