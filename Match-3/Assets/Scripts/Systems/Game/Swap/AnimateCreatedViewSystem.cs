﻿using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Common;
using Match3.Components.Game;
using UnityEngine;

namespace Match3.Systems.Game.Swap {
    public sealed class AnimateCreatedViewSystem : IEcsRunSystem {
        private readonly EcsFilter<Cell, Vector2Int, AnimateCreatedViewRequest> _filter = null;

        public void Run() {
            if (_filter.GetEntitiesCount() == 0) {
                return;
            }

            Global.Data.InGame.World.NewEntity().Set<PlaySoundRequest>() = new PlaySoundRequest(Global.Config.InGame.Sounds.DropDownCells);

            foreach (int index in _filter) {
                EcsEntity entity = _filter.GetEntity(index);
                entity.Set<ChangeFieldAnimating>();
                float zPosition = 1;
                Vector2Int cellPosition = _filter.Get2(index);
                Vector3 target = new Vector3(cellPosition.x, cellPosition.y, zPosition);

                Transform view = _filter.Get1(index).View.transform;
                view.position += new Vector3(0, 0, zPosition - view.position.z);
                view.DOMove(target, Global.Config.InGame.Animation.CellMovingSeconds)
                    .OnComplete(() => OnFallenDown(entity, view));
            }
        }

        private void OnFallenDown(EcsEntity entity, Transform view) {
            entity.Unset<ChangeFieldAnimating>();
            view.transform.position -= new Vector3(0, 0, view.transform.position.z);
        }
    }
}