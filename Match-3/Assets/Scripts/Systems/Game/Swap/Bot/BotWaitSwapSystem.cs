using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Configurations;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Bot {

    public sealed class BotWaitSwapSystem : IEcsInitSystem, IEcsRunSystem {
        private readonly EcsFilter<PlayerChangedEvent> _playerChangeRequestsFilter = null;

        private float _minBotThinkingTime;
        private float _maxBotThinkingTime;

        // generate concrete bot behaviour
        public void Init() {
            BotBehaviourConfiguration botBehaviour = Global.Config.InGame.BotBehaviour;
            float middleWhaitTime = Random.Range(botBehaviour.MinThinkingTime, botBehaviour.MaxThinkingTime);

            _minBotThinkingTime = middleWhaitTime / botBehaviour.ThinkingTimeDeviationProportion;
            _minBotThinkingTime = Mathf.Max(_minBotThinkingTime, botBehaviour.MinThinkingTime);
            _maxBotThinkingTime = middleWhaitTime * botBehaviour.ThinkingTimeDeviationProportion;
            _maxBotThinkingTime = Mathf.Min(_maxBotThinkingTime, botBehaviour.MaxThinkingTime);
        }

        public void Run() {
            bool botIsActive = !Global.Data.InGame.PlayerState.Active;
            bool roundEnded = Global.Data.InGame.PlayerState.CurrentLife <= 0 || OpponentState.CurrentLife <= 0;

            if (roundEnded || _playerChangeRequestsFilter.GetEntitiesCount() == 0 || !botIsActive) {
                return;
            }

            float swapDelay = Random.Range(_minBotThinkingTime, _maxBotThinkingTime);
            swapDelay = Mathf.Max(swapDelay, Global.Config.InGame.BotBehaviour.MinThinkingTime);
            swapDelay = Mathf.Min(swapDelay, Global.Config.InGame.BotBehaviour.MaxThinkingTime);

            Sequence sequence = DOTween.Sequence();
            sequence.SetDelay(swapDelay);
            sequence.OnComplete(() => {
                Global.Data.InGame.World.NewEntity().Set<BotMakeSwapDecisionRequest>();
            });
        }
    }
}
