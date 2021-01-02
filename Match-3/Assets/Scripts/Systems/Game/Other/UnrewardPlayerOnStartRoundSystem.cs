using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Configurations;
using System;

namespace Match3.Assets.Scripts.Systems.Game.UI
{
    public sealed class UnrewardPlayerOnStartRoundSystem : IEcsInitSystem
    {
        private readonly PlayerData _playerData = null;
        private readonly PlayerState _playerState = null;
        private readonly InGameConfiguration _configuration = null;

        public void Init()
        {
            _playerData.Coins -= _playerState.CurrentBet;

            float ratingsProportion = _playerData.Rating <= 0 ? .1f : _playerData.Rating / (float)OpponentState.Rating;

            int deltaRatingReward = Math.Abs((int)((OpponentState.Rating - _playerData.Rating) * _configuration.DeltaRatingRewardMultiplayer / ratingsProportion));
            deltaRatingReward = Math.Max(deltaRatingReward, _configuration.MinDeltaRating);
            deltaRatingReward = OpponentState.Rating < _playerData.Rating ? _configuration.MinDeltaRating : deltaRatingReward;
            _playerState.DeltaRatingReward = deltaRatingReward;

            int deltaRatingUnreward = Math.Abs((int)((OpponentState.Rating - _playerData.Rating) * _configuration.DeltaRatingRewardMultiplayer * ratingsProportion));
            deltaRatingUnreward = Math.Max(deltaRatingUnreward, _configuration.MinDeltaRating);
            deltaRatingUnreward = OpponentState.Rating > _playerData.Rating ? _configuration.MinDeltaRating : deltaRatingUnreward;
            _playerState.DeltaRatingUnreward = deltaRatingUnreward;

            _playerData.Rating -= _playerState.DeltaRatingUnreward;

            LocalSaveLoad<PlayerData>.Save(_playerData);
        }
    }
}
