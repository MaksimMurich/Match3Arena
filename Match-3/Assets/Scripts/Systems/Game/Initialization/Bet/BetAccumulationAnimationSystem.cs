using DG.Tweening;
using Leopotam.Ecs;
using Match3.Configurations;

namespace Match3.Systems.Game.Initialization.Bet
{
    public sealed class BetAccumulationAnimationSystem : IEcsInitSystem
    {
        private long _bet = 0;

        private readonly PlayerState _playerState = null;
        private readonly InGameConfiguration _configuration = null;
        private readonly InGameSceneData _inGameSceneData = null;

        public void Init()
        {
            DOTween.To(() => _bet, x => _bet = x, _playerState.CurrentBet * 2, _configuration.Animation.StartGameBetAccumulationDurationSec)
                .OnUpdate(() => _inGameSceneData.BetView.Set(_bet));
        }
    }
}