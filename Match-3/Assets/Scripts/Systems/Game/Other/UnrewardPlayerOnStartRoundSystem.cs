using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Configurations;
using System;

namespace Match3.Assets.Scripts.Systems.Game.UI {
    public sealed class UnrewardPlayerOnStartRoundSystem : IEcsInitSystem {
        public void Init() {
            Global.Data.Player.Coins -= Global.Data.Common.PlayerState.CurrentBet;
            int playerRating = Global.Data.Player.Rating;
            InGameConfiguration inGame = Global.Config.InGame;
            CommonConfiguration common = Global.Config.Common;

            int opponentRating = OpponentState.Rating;
            float ratingsProportion = playerRating <= 0 ? .1f : playerRating / (float)opponentRating;

            int deltaRatingReward = Math.Abs((int)((opponentRating - playerRating) * common.DeltaRatingRewardMultiplayer / ratingsProportion));
            deltaRatingReward = Math.Max(deltaRatingReward, common.MinDeltaRating);
            deltaRatingReward = opponentRating < playerRating ? common.MinDeltaRating : deltaRatingReward;
            Global.Data.Common.PlayerState.DeltaRatingReward = deltaRatingReward;

            int deltaRatingUnreward = Math.Abs((int)((opponentRating - playerRating) * common.DeltaRatingRewardMultiplayer * ratingsProportion));
            deltaRatingUnreward = Math.Max(deltaRatingUnreward, common.MinDeltaRating);
            deltaRatingUnreward = opponentRating > playerRating ? common.MinDeltaRating : deltaRatingUnreward;
            Global.Data.Common.PlayerState.DeltaRatingUnreward = deltaRatingUnreward;
            playerRating -= deltaRatingUnreward;


            LocalSaveLoad<PlayerData>.Save(Global.Data.Player);
        }
    }
}
