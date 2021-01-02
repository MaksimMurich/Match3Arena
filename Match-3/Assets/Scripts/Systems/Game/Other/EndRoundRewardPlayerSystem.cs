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

            bool win = _playerState.CurrentLife > 0;

            if (win)
            {
                _playerData.Coins += _playerState.CurrentBet * 2;

                _playerData.Rating += _playerState.DeltaRatingReward + _playerState.DeltaRatingUnreward;

                _playerData.WinsCount++;
            }

            _playerData.RoundsCount++;

            LocalSaveLoad<PlayerData>.Save(_playerData);
        }
    }
}
