using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Configurations;
using System;

namespace Match3.Assets.Scripts.Systems.Game.UI {
    public sealed class UnrewardPlayerOnStartRoundSystem : IEcsInitSystem {
        public void Init() {
            Global.Data.Player.Coins -= Global.Data.InGame.PlayerState.CurrentBet;
            int playerRating = Global.Data.Player.Rating;
            InGameConfiguration inGame = Global.Config.InGame;

            int opponentRating = OpponentState.Rating;
            float ratingsProportion = playerRating <= 0 ? .1f : playerRating / (float)opponentRating;

            int deltaRatingReward = Math.Abs((int)((opponentRating - playerRating) * inGame.DeltaRatingRewardMultiplayer / ratingsProportion));
            deltaRatingReward = Math.Max(deltaRatingReward, inGame.MinDeltaRating);
            deltaRatingReward = opponentRating < playerRating ? inGame.MinDeltaRating : deltaRatingReward;
            Global.Data.InGame.PlayerState.DeltaRatingReward = deltaRatingReward;

            int deltaRatingUnreward = Math.Abs((int)((opponentRating - playerRating) * inGame.DeltaRatingRewardMultiplayer * ratingsProportion));
            deltaRatingUnreward = Math.Max(deltaRatingUnreward, inGame.MinDeltaRating);
            deltaRatingUnreward = opponentRating > playerRating ? inGame.MinDeltaRating : deltaRatingUnreward;
            Global.Data.InGame.PlayerState.DeltaRatingUnreward = deltaRatingUnreward;
            playerRating -= deltaRatingUnreward;


            LocalSaveLoad<PlayerData>.Save(Global.Data.Player);
        }
    }
}
