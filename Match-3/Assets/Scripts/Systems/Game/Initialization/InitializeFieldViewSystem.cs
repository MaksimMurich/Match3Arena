using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Assets.Scripts.Services.Pool;
using Match3.Components.Game;
using Match3.Configurations;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class InitializeFieldViewSystem : IEcsInitSystem
    {
        private readonly InGameConfiguration _configuration = null;
        private readonly GameField _gameField = null;
        private readonly ObjectPool _objectPool = null;

        public void Init()
        {
            int backTypesCount = _configuration.CellViewBackgrounds.Count;
            int startCellType = 0;

            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                startCellType += 1;
                startCellType = startCellType % backTypesCount;

                int currentCellType = startCellType;

                for (int row = 0; row < _configuration.LevelHeight; row++)
                {
                    Vector2Int position = new Vector2Int(column, row);
                    EcsEntity entity = _gameField.Cells[position];
                    ref Cell cell = ref entity.Ref<Cell>().Unref();
                    cell.View = _objectPool.Get(cell.Configuration.ViewExample);
                    cell.View.transform.position = new Vector3(position.x, _configuration.LevelHeight + 1);
                    cell.View.Entity = entity;

                    CellBackground cellBackground = _objectPool.Get(_configuration.CellViewBackgrounds[currentCellType]);
                    currentCellType = (currentCellType + 1) % backTypesCount;
                    cellBackground.transform.position = new Vector3(column, row, 50);
                }
            }
        }
    }
}