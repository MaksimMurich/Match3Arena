using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Common;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using Match3.UnityComponents;

namespace Match3.Assets.Scripts.Systems.Game.CellsExplosion
{
    public sealed class AnimateCellsExplosionSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly InGameConfiguration _configuration = null;
        private readonly EcsFilter<Cell, AnimateExplosionRequest> _filter = null;

        public void Run()
        {
            if(_filter.GetEntitiesCount() == 0)
            {
                return;
            }

            _world.NewEntity().Set<PlaySoundRequest>() = new PlaySoundRequest(_configuration.Sounds.CellsExplosion);

            foreach (var index in _filter)
            {
                CellView view = _filter.Get1(index).View;
                view.Entity.Set<AnimateExplosion>();

                var tween = view.transform.DOScale(_configuration.Animation.ExplosionScale, _configuration.Animation.ExplosionSeconds).OnComplete(() =>
                {
                    Hide(view);
                });
            }
        }

        private void Hide(CellView cell)
        {
            cell.Entity.Unset<AnimateExplosion>();
        }
    }
}
