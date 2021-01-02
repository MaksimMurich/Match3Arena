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
            Global.Data.Player.Coins -= _playerState.CurrentBet;

            float ratingsProportion = Global.Data.Player.Rating <= 0 ? .1f : Global.Data.Player.Rating / (float)OpponentState.Rating;

            int deltaRatingReward = Math.Abs((int)((OpponentState.Rating - Global.Data.Player.Rating) *  Global.Config.InGame.DeltaRatingRewardMultiplayer / ratingsProportion));
            deltaRatingReward = Math.Max(deltaRatingReward,  Global.Config.InGame.MinDeltaRating);
            deltaRatingReward = OpponentState.Rating < Global.Data.Player.Rating ?  Global.Config.InGame.MinDeltaRating : deltaRatingReward;
            _playerState.DeltaRatingReward = deltaRatingReward;

            int deltaRatingUnreward = Math.Abs((int)((OpponentState.Rating - Global.Data.Player.Rating) *  Global.Config.InGame.DeltaRatingRewardMultiplayer * ratingsProportion));
            deltaRatingUnreward = Math.Max(deltaRatingUnreward,  Global.Config.InGame.MinDeltaRating);
            deltaRatingUnreward = OpponentState.Rating > Global.Data.Player.Rating ?  Global.Config.InGame.MinDeltaRating : deltaRatingUnreward;
            _playerState.DeltaRatingUnreward = deltaRatingUnreward;

            Global.Data.Player.Rating -= _playerState.DeltaRatingUnreward;

            LocalSaveLoad<PlayerData>.Save(_playerData);
        }
    }
}
