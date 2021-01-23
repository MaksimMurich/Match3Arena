using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Components.Game.Events;

namespace Match3.Assets.Scripts.Systems.Game.UI {
    public sealed class EndRoundRewardPlayerSystem : IEcsRunSystem {
        private readonly EcsFilter<EndRoundRequest> _filter = null;

        public void Run() {
            if (_filter.GetEntitiesCount() == 0) {
                return;
            }

            bool win = Global.Data.Common.PlayerState.CurrentLife > 0;

            if (win) {
                Global.Data.Player.Coins += Global.Data.Common.PlayerState.CurrentBet * 2;

                Global.Data.Player.Rating += Global.Data.Common.PlayerState.DeltaRatingReward + Global.Data.Common.PlayerState.DeltaRatingUnreward;

                Global.Data.Player.WinsCount++;
            }

            Global.Data.Player.RoundsCount++;

            LocalSaveLoad<PlayerData>.Save(Global.Data.Player);
        }
    }
}
