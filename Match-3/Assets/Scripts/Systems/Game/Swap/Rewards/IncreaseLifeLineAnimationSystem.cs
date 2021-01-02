using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards
{
    public sealed class IncreaseLifeLineAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthRewardRequest> _filter = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0)
            {
                return;
            }

            var state = Global.Data.InGame.PlayerState;

            PlayerInGameDataView dataView = state.Active ? Global.Views.InGame.PlayerDataView : Global.Views.InGame.BotDataView;

            float playerHealthPart = state.CurrentLife / state.MaxLife;
            float opponentHealthPart = OpponentState.CurrentLife / OpponentState.MaxLife;
            float healthPart = state.Active ? playerHealthPart : opponentHealthPart;

            Vector3 scale = dataView.IncreaseLifeLine.localScale;

            dataView.DecreaseLifeLine.gameObject.SetActive(false);
            dataView.IncreaseLifeLine.gameObject.SetActive(true);
            dataView.IncreaseLifeLine.localScale = new Vector3(healthPart, scale.y, scale.z);
            dataView.LifeLine.DOScaleX(healthPart, Global.Config.InGame.Animation.UpdateLifeTime);
        }
    }
}
