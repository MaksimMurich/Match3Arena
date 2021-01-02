using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Rewards
{
    public sealed class DecreaseLifeLineAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DemageRewardRequest> _filter = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0)
            {
                return;
            }

            PlayerInGameDataView dataView = Global.Data.InGame.PlayerState.Active == false ? Global.Views.InGame.PlayerDataView : Global.Views.InGame.BotDataView;

            float playerHealthPart = Global.Data.InGame.PlayerState.CurrentLife / Global.Data.InGame.PlayerState.MaxLife;
            float opponentHealthPart = OpponentState.CurrentLife / OpponentState.MaxLife;
            float healthPart = Global.Data.InGame.PlayerState.Active == false ? playerHealthPart : opponentHealthPart;

            Vector3 scale = dataView.DecreaseLifeLine.localScale;

            dataView.DecreaseLifeLine.gameObject.SetActive(true);
            dataView.IncreaseLifeLine.gameObject.SetActive(false);
            dataView.LifeLine.localScale = new Vector3(healthPart, scale.y, scale.z);
            dataView.DecreaseLifeLine.DOScaleX(healthPart,  Global.Config.InGame.Animation.UpdateLifeTime);
        }
    }
}
