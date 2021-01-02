using DG.Tweening;
using Leopotam.Ecs;
using Match3.Configurations;

namespace Match3.Systems.Game.Initialization.Bet
{
    public sealed class BetPulseScalesOnStartAnimationSystem : IEcsInitSystem
    {
        private readonly InGameConfiguration _configuration = null;
        private readonly InGameViews _inGameSceneData = null;

        public void Init()
        {
            float oneScaleDuration = .5f;
            int loopsCount = (int)( Global.Config.InGame.Animation.StartGameBetAccumulationDurationSec / oneScaleDuration);
            Global.Views.InGame.BetView.Bet.DOScale(1.5f, oneScaleDuration).SetLoops(loopsCount, LoopType.Yoyo);
        }
    }
}