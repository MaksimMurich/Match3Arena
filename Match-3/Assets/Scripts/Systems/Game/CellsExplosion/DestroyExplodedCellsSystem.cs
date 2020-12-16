using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Components.Game;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.CellsExplosion
{
    public sealed class DestroyExplodedCellsSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<Cell, ChargedToExplosion, Vector2Int>.Exclude<AnimateExplosion> _filter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                EcsEntity cell = _filter.GetEntity(index);
                cell.Unset<ChargedToExplosion>();
                cell.Set<EmptySpace>();

                _world.NewEntity().Set<RewardRequest>() = new RewardRequest()
                {
                    Cell = _filter.Get1(index),
                    Position = _filter.Get3(index)
                };
            }
        }
    }
}
