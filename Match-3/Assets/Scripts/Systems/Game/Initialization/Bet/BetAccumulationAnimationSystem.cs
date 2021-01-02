using DG.Tweening;
using Leopotam.Ecs;
using Match3.Configurations;

namespace Match3.Systems.Game.Initialization.Bet
{
    public sealed class BetAccumulationAnimationSystem : IEcsInitSystem
    {
        private long _bet = 0;

        public void Init()
        {
            DOTween.To(() => _bet, x => _bet = x, Global.Data.InGame.PlayerState.CurrentBet * 2,  Global.Config.InGame.Animation.StartGameBetAccumulationDurationSec)
                .OnUpdate(() => Global.Views.InGame.BetView.Set(_bet));
        }
    }
}