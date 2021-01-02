﻿using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Common;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Systems.Game.Swap
{
    public sealed class AnimateSwapBackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Cell, Vector2Int, AnimateSwapRequest, AnimateSwapBackRequest> _filter = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() > 0)
            {
                Global.Data.InGame.World.NewEntity().Set<PlaySoundRequest>() = new PlaySoundRequest(Global.Config.InGame.Sounds.SwapBack);
            }

            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                entity.Set<ChangeFieldAnimating>();
                float zPosition = _filter.Get3(index).MainCell ? -1 : 0;

                Vector2Int cellPosition = _filter.Get2(index);
                Vector2Int targetPosition = _filter.Get4(index).TargetPosition;
                Vector3 targetMovePosition = new Vector3(targetPosition.x, targetPosition.y, zPosition);
                Vector3 startPosition = new Vector3(cellPosition.x, cellPosition.y, zPosition);

                Transform view = _filter.Get1(index).View.transform;
                view.position += new Vector3(0, 0, zPosition - view.position.z);
                view.DOMove(targetMovePosition, Global.Config.InGame.Animation.SwapDuration)
                    .OnComplete(() => view.DOMove(startPosition, Global.Config.InGame.Animation.SwapDuration)
                    .OnComplete(() => OnSwapBackCompleate(entity, view)));
            }
        }

        private void OnSwapBackCompleate(EcsEntity entity, Transform view)
        {
            entity.Unset<ChangeFieldAnimating>();
            view.transform.position -= new Vector3(0, 0, view.transform.position.z);
        }
    }
}