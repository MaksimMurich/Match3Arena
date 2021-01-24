using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Components.Game;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.CellsExplosion {
    public sealed class DestroyExplodedCellsSystem : IEcsRunSystem {
        private readonly EcsFilter<Cell, ChargedToExplosion, Vector2Int>.Exclude<AnimateExplosion> _filter = null;

        public void Run() {
            foreach (var index in _filter) {
                EcsEntity cell = _filter.GetEntity(index);
                cell.Unset<ChargedToExplosion>();
                cell.Set<EmptySpace>();

                Global.Data.InGame.World.NewEntity().Set<RewardRequest>() = new RewardRequest() {
                    Cell = _filter.Get1(index),
                    Position = _filter.Get3(index)
                };
            }
        }
    }
}
