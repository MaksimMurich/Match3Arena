using DG.Tweening;
using Leopotam.Ecs;

namespace Match3.Systems.Game.Initialization.Bet {
    public sealed class BetPulseScalesOnStartAnimationSystem : IEcsInitSystem {
        public void Init() {
            float oneScaleDuration = .5f;
            int loopsCount = (int)(Global.Config.InGame.Animation.StartGameBetAccumulationDurationSec / oneScaleDuration);
            Global.Views.InGame.BetView.Bet.DOScale(1.5f, oneScaleDuration).SetLoops(loopsCount, LoopType.Yoyo);
        }
    }
}