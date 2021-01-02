using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;
using System;

namespace Match3.Assets.Scripts.Systems.Game.UI
{
    public sealed class UnrewardPlayerOnStartRoundSystem : IEcsInitSystem
    {
        public void Init()
        {
            Global.Data.Player.Coins -= Global.Data.InGame.PlayerState.CurrentBet;

            float ratingsProportion = Global.Data.Player.Rating <= 0 ? .1f : Global.Data.Player.Rating / (float)OpponentState.Rating;

            int deltaRatingReward = Math.Abs((int)((OpponentState.Rating - Global.Data.Player.Rating) * Global.Config.InGame.DeltaRatingRewardMultiplayer / ratingsProportion));
            deltaRatingReward = Math.Max(deltaRatingReward, Global.Config.InGame.MinDeltaRating);
            deltaRatingReward = OpponentState.Rating < Global.Data.Player.Rating ? Global.Config.InGame.MinDeltaRating : deltaRatingReward;
            Global.Data.InGame.PlayerState.DeltaRatingReward = deltaRatingReward;

            int deltaRatingUnreward = Math.Abs((int)((OpponentState.Rating - Global.Data.Player.Rating) * Global.Config.InGame.DeltaRatingRewardMultiplayer * ratingsProportion));
            deltaRatingUnreward = Math.Max(deltaRatingUnreward, Global.Config.InGame.MinDeltaRating);
            deltaRatingUnreward = OpponentState.Rating > Global.Data.Player.Rating ? Global.Config.InGame.MinDeltaRating : deltaRatingUnreward;
            Global.Data.InGame.PlayerState.DeltaRatingUnreward = deltaRatingUnreward;

            Global.Data.Player.Rating -= Global.Data.InGame.PlayerState.DeltaRatingUnreward;

            LocalSaveLoad<PlayerData>.Save(Global.Data.Player);
        }
    }
}
