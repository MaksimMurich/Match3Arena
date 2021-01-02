using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Animations
{
    public sealed class ScaleSelectedCellAnimationSystem : IEcsRunSystem
    {
        private readonly InGameConfiguration _configuration = null;
        private readonly EcsFilter<Cell, SelectCellAnimationRequest> _filter = null;

        public void Run()
        {
            AnimationsConfiguration configuration =  Global.Config.InGame.Animation;

            foreach (int index in _filter)
            {
                Transform view = _filter.Get1(index).View.transform;
                view.transform.position += configuration.UpCellOnAnimate;

                Sequence sequence = DOTween.Sequence();

                sequence.Append(view.DOScaleX(configuration.SetectedCellScale, configuration.SetectedCellScaleSeconds));
                sequence.Join(view.DOScaleY(configuration.SetectedCellScale, configuration.SetectedCellScaleSeconds));

                sequence.Play();
            }
        }
    }
}
