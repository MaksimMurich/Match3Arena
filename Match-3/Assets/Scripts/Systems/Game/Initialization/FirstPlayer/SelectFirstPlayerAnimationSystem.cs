using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Initialization.FirstPlaer {
    public sealed class SelectFirstPlayerAnimationSystem : IEcsInitSystem {
        private EcsEntity _animatingState;

        public void Init() {
            Global.Views.InGame.FirstPlayerSelectionView.gameObject.SetActive(true);
            _animatingState = Global.Data.InGame.World.NewEntity();
            _animatingState.Set<FirstPlayerSelectionAnimating>();
            List<Transform> avatars = new List<Transform>();
            avatars.Add(Global.Views.InGame.FirstPlayerSelectionView.BotAvatarItemExample);
            avatars.Add(Global.Views.InGame.FirstPlayerSelectionView.PlayerAvatarItemExample);

            for (int i = 0; i < Global.Config.InGame.Animation.SelectFirstPlayerAvatarsCount - 1; i++) {
                int avatarId = Random.Range(0, 2);
                GenerateItem(avatars[avatarId], i.ToString());
            }

            if (Global.Data.InGame.PlayerState.Active) {
                GenerateItem(Global.Views.InGame.FirstPlayerSelectionView.PlayerAvatarItemExample, " Origin");
            }
            else {
                GenerateItem(Global.Views.InGame.FirstPlayerSelectionView.BotAvatarItemExample, " Origin");
            }

            Global.Views.InGame.FirstPlayerSelectionView.VerticalLayoutGroup.enabled = true;
            RectTransform container = Global.Views.InGame.FirstPlayerSelectionView.ItemsContainer;
            Sequence sequence = DOTween.Sequence();
            float targetY = container.rect.height * (Global.Config.InGame.Animation.SelectFirstPlayerAvatarsCount - 1);
            sequence.Append(container.DOAnchorPos(new Vector2(0, targetY * 1), Global.Config.InGame.Animation.SelectFirstPlayerDuration)).SetEase(Ease.OutExpo);
            sequence.SetDelay(.2f);
            sequence.onComplete += OnFirstPlayerSelected;
            sequence.Play();
        }

        private void OnFirstPlayerSelected() {
            Global.Views.InGame.FirstPlayerSelectionView.gameObject.SetActive(false);
            Global.Data.InGame.World.NewEntity().Unset<FirstPlayerSelectionAnimating>();

            if (!Global.Data.InGame.PlayerState.Active) {
                EcsEntity makeSwapRequest = Global.Data.InGame.World.NewEntity();
                makeSwapRequest.Set<BotMakeSwapDecisionRequest>();
            }
        }

        private void GenerateItem(Transform example, string namePostfix) {
            Transform avatar = Object.Instantiate(example);
            avatar.SetParent(Global.Views.InGame.FirstPlayerSelectionView.ItemsContainer);
            avatar.localScale = Vector3.one;
            avatar.name += namePostfix;
        }
    }

}
