using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Components.Game.Events;

namespace Match3.Assets.Scripts.Systems.Game.UI
{
    public sealed class EndRoundRewardPlayerSystem : IEcsRunSystem
    {
        private readonly PlayerData _playerData = null;
        private readonly PlayerState _playerState = null;

        private readonly EcsFilter<EndRoundRequest> _filter = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0)
            {
                return;
            }

            bool win = Global.Data.InGame.PlayerState.CurrentLife > 0;

            if (win)
            {
                Global.Data.Player.Coins += Global.Data.InGame.PlayerState.CurrentBet * 2;

                Global.Data.Player.Rating += Global.Data.InGame.PlayerState.DeltaRatingReward + Global.Data.InGame.PlayerState.DeltaRatingUnreward;

                Global.Data.Player.WinsCount++;
            }

            Global.Data.Player.RoundsCount++;

            LocalSaveLoad<PlayerData>.Save(_playerData);
        }
    }
}
