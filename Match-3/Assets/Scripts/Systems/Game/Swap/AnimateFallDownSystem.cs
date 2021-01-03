using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using UnityEngine;

namespace Match3.Systems.Game.Swap
{
    public sealed class AnimateFallDownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Cell, Vector2Int, AnimateFallDownRequest> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                entity.Set<ChangeFieldAnimating>();
                float zPosition = 1;
                Vector2Int cellPosition = _filter.Get2(index);
                Vector3 target = new Vector3(cellPosition.x, cellPosition.y, zPosition);

                Transform view = _filter.Get1(index).View.transform;
                view.position += new Vector3(0, 0, zPosition - view.position.z);
                view.DOMove(target, Global.Config.InGame.Animation.CellMovingSeconds)
                    .OnComplete(() => OnFallenDown(entity, view, index == 0));
            }
        }

        private void OnFallenDown(EcsEntity entity, Transform view, bool playAudio)
        {
            entity.Unset<ChangeFieldAnimating>();
            view.transform.position -= new Vector3(0, 0, view.transform.position.z);
        }
    }
}