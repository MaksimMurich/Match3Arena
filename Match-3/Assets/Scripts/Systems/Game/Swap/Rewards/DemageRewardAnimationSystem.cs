using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Assets.Scripts.Services.Pool;
using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards
{
    public sealed class DemageRewardAnimationSystem : IEcsRunSystem
    {
        private readonly ObjectPool  _objectPool = null;
        private readonly PlayerState _playerState = null;
        private readonly InGameSceneData _sceneData = null;
        private readonly InGameConfiguration _configuration = null;
        private readonly EcsFilter<DemageRewardRequest> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                DemageRewardRequest request = _filter.Get1(index);

                if(request.Value == 0)
                {
                    continue;
                }

                RectTransform canvasRect = _sceneData.RewardsContainer.GetComponent<RectTransform>();

                float widthProportion = canvasRect.sizeDelta.x / Screen.width;
                float heightProportion = canvasRect.sizeDelta.y / Screen.height;

                PlayerInGameDataView dataView = _playerState.Active ? _sceneData.PlayerDataView : _sceneData.BotDataView;
                CellRewardView rewardView = _objectPool.Get(request.View);
                rewardView.SetValue(request.Value);
                rewardView.transform.SetParent(_sceneData.RewardsContainer.transform);
                rewardView.transform.localScale = Vector3.one;
                RectTransform rectTrancform = rewardView.GetComponent<RectTransform>();

                Vector3 position = new Vector3(request.Position.x, request.Position.y, 0);
                Vector2 screenPosition = _sceneData.Camera.WorldToScreenPoint(position);
                Vector2 canvasPosition = new Vector2(screenPosition.x * widthProportion, screenPosition.y * heightProportion);
                rectTrancform.anchoredPosition = canvasPosition;

                Sequence sequence = DOTween.Sequence();
                sequence.Append(rectTrancform.DOAnchorPosY(rectTrancform.anchoredPosition.y + 150, _configuration.Animation.ExplodedRewardUpAnimatingDuration));
                sequence.AppendCallback(() => { _objectPool.Stash(rewardView); });
            }
        }
    }
}
