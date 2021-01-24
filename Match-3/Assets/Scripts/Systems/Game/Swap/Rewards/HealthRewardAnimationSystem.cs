using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards {
    public sealed class HealthRewardAnimationSystem : IEcsRunSystem {
        private readonly EcsFilter<HealthRewardRequest> _filter = null;

        public void Run() {
            foreach (int index in _filter) {
                HealthRewardRequest request = _filter.Get1(index);

                if (request.Value == 0) {
                    continue;
                }

                var views = Global.Views.InGame;
                RectTransform canvasRect = views.RewardsContainer.GetComponent<RectTransform>();

                float widthProportion = canvasRect.sizeDelta.x / Screen.width;
                float heightProportion = canvasRect.sizeDelta.y / Screen.height;

                Vector3 position = new Vector3(request.Position.x, request.Position.y, 0);
                Vector2 screenPosition = views.Camera.WorldToScreenPoint(position);
                Vector2 canvasPosition = new Vector2(screenPosition.x * widthProportion, screenPosition.y * heightProportion);

                PlayerInGameDataView dataView = Global.Data.Common.PlayerState.Active ? views.PlayerDataView : views.BotDataView;
                CellRewardView rewardView = Global.Services.Pool.Get(request.View);
                rewardView.SetValue(request.Value);
                rewardView.transform.SetParent(views.RewardsContainer.transform);
                rewardView.transform.localScale = Vector3.one;
                RectTransform rectTrancform = rewardView.GetComponent<RectTransform>();
                rectTrancform.anchoredPosition = canvasPosition;

                Sequence sequence = DOTween.Sequence();
                sequence.Append(rectTrancform.DOAnchorPosY(rectTrancform.anchoredPosition.y + 100, Global.Config.InGame.Animation.ExplodedRewardUpAnimatingDuration));
                sequence.AppendCallback(() => { Global.Services.Pool.Stash(rewardView); });
            }
        }

        public Vector3 ScreenToCanvasPosition(Canvas canvas, Vector3 screenPosition) {
            var viewportPosition = new Vector3(screenPosition.x / Screen.width,
                                               screenPosition.y / Screen.height,
                                               0);
            return ViewportToCanvasPosition(canvas, viewportPosition);
        }

        public Vector3 ViewportToCanvasPosition(Canvas canvas, Vector3 viewportPosition) {
            var centerBasedViewPortPosition = viewportPosition - new Vector3(0.5f, 0.5f, 0);
            var canvasRect = canvas.GetComponent<RectTransform>();
            var scale = canvasRect.sizeDelta;
            return Vector3.Scale(centerBasedViewPortPosition, scale);
        }
    }
}
