using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Systems.Game
{
    public sealed class CreateCellsViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Cell, CreateCellViewRequest, Vector2Int> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                ref Cell cell = ref _filter.Get1(index);
                CellView view = Global.Services.Pool.Get(cell.Configuration.ViewExample);
                view.Entity = _filter.GetEntity(index);

                cell.View = view;
                view.transform.position = new Vector2(_filter.Get3(index).x, Global.Config.InGame.LevelHeight);
                view.Entity.Set<AnimateCreatedViewRequest>();
            }
        }
    }
}