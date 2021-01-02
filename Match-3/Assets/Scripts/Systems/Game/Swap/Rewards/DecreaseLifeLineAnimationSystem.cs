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
        private readonly PlayerState _playerState = null;
        private readonly InGameViews _sceneData = null;
        private readonly InGameConfiguration _configuration = null;
        private readonly EcsFilter<DemageRewardRequest> _filter = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0)
            {
                return;
            }

            PlayerInGameDataView dataView = _playerState.Active == false ? _sceneData.PlayerDataView : _sceneData.BotDataView;

            float playerHealthPart = _playerState.CurrentLife / _playerState.MaxLife;
            float opponentHealthPart = OpponentState.CurrentLife / OpponentState.MaxLife;
            float healthPart = _playerState.Active == false ? playerHealthPart : opponentHealthPart;

            Vector3 scale = dataView.DecreaseLifeLine.localScale;

            dataView.DecreaseLifeLine.gameObject.SetActive(true);
            dataView.IncreaseLifeLine.gameObject.SetActive(false);
            dataView.LifeLine.localScale = new Vector3(healthPart, scale.y, scale.z);
            dataView.DecreaseLifeLine.DOScaleX(healthPart,  Global.Config.InGame.Animation.UpdateLifeTime);
        }
    }
}
