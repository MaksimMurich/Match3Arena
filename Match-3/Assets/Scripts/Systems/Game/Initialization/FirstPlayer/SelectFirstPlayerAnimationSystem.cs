using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Initialization.FirstPlaer
{
    public sealed class SelectFirstPlayerAnimationSystem : IEcsInitSystem
    {
        private EcsEntity _animatingState;

        private readonly EcsWorld _world = null;
        private readonly PlayerState _playerState = null;
        private readonly InGameViews _inGameSceneData = null;
        private readonly InGameConfiguration _configuration = null;

        public void Init()
        {
            _inGameSceneData.FirstPlayerSelectionView.gameObject.SetActive(true);
            _animatingState =  Global.Data.InGame.World.NewEntity();
            _animatingState.Set<FirstPlayerSelectionAnimating>();
            List<Transform> avatars = new List<Transform>();
            avatars.Add(_inGameSceneData.FirstPlayerSelectionView.BotAvatarItemExample);
            avatars.Add(_inGameSceneData.FirstPlayerSelectionView.PlayerAvatarItemExample);

            for (int i = 0; i <  Global.Config.InGame.Animation.SelectFirstPlayerAvatarsCount - 1; i++)
            {
                int avatarId = Random.Range(0, 2);
                GenerateItem(avatars[avatarId], i.ToString());
            }

            if (_playerState.Active)
            {
                GenerateItem(_inGameSceneData.FirstPlayerSelectionView.PlayerAvatarItemExample, " Origin");
            }
            else
            {
                GenerateItem(_inGameSceneData.FirstPlayerSelectionView.BotAvatarItemExample, " Origin");
            }

            _inGameSceneData.FirstPlayerSelectionView.VerticalLayoutGroup.enabled = true;
            RectTransform container = _inGameSceneData.FirstPlayerSelectionView.ItemsContainer;
            Sequence sequence = DOTween.Sequence();
            float targetY = container.rect.height * ( Global.Config.InGame.Animation.SelectFirstPlayerAvatarsCount - 1);
            sequence.Append(container.DOAnchorPos(new Vector2(0, targetY * 1),  Global.Config.InGame.Animation.SelectFirstPlayerDuration)).SetEase(Ease.OutExpo);
            sequence.SetDelay(.2f);
            sequence.onComplete += OnFirstPlayerSelected;
            sequence.Play();
        }

        private void OnFirstPlayerSelected()
        {
            _inGameSceneData.FirstPlayerSelectionView.gameObject.SetActive(false);
             Global.Data.InGame.World.NewEntity().Unset<FirstPlayerSelectionAnimating>();

            if (!_playerState.Active)
            {
                EcsEntity makeSwapRequest =  Global.Data.InGame.World.NewEntity();
                makeSwapRequest.Set<BotMakeSwapDecisionRequest>();
            }
        }

        private void GenerateItem(Transform example, string namePostfix)
        {
            Transform avatar = Object.Instantiate(example);
            avatar.SetParent(_inGameSceneData.FirstPlayerSelectionView.ItemsContainer);
            avatar.localScale = Vector3.one;
            avatar.name += namePostfix;
        }
    }

}
