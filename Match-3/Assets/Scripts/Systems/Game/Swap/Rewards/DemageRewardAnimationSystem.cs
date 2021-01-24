using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards {
    public sealed class DemageRewardAnimationSystem : IEcsRunSystem {
        private readonly EcsFilter<DemageRewardRequest> _filter = null;

        public void Run() {
            var views = Global.Views.InGame;

            foreach (int index in _filter) {
                DemageRewardRequest request = _filter.Get1(index);

                if (request.Value == 0) {
                    continue;
                }

                RectTransform canvasRect = views.RewardsContainer.GetComponent<RectTransform>();

                float widthProportion = canvasRect.sizeDelta.x / Screen.width;
                float heightProportion = canvasRect.sizeDelta.y / Screen.height;

                PlayerInGameDataView dataView = Global.Data.Common.PlayerState.Active ? views.PlayerDataView : views.BotDataView;
                CellRewardView rewardView = Global.Services.Pool.Get(request.View);
                rewardView.SetValue(request.Value);
                rewardView.transform.SetParent(views.RewardsContainer.transform);
                rewardView.transform.localScale = Vector3.one;
                RectTransform rectTrancform = rewardView.GetComponent<RectTransform>();

                Vector3 position = new Vector3(request.Position.x, request.Position.y, 0);
                Vector2 screenPosition = views.Camera.WorldToScreenPoint(position);
                Vector2 canvasPosition = new Vector2(screenPosition.x * widthProportion, screenPosition.y * heightProportion);
                rectTrancform.anchoredPosition = canvasPosition;

                Sequence sequence = DOTween.Sequence();
                sequence.Append(rectTrancform.DOAnchorPosY(rectTrancform.anchoredPosition.y + 150, Global.Config.InGame.Animation.ExplodedRewardUpAnimatingDuration));
                sequence.AppendCallback(() => { Global.Services.Pool.Stash(rewardView); });
            }
        }
    }
}
