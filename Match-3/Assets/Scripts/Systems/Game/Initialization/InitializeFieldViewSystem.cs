using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Systems.Game.Initialization {
    public sealed class InitializeFieldViewSystem : IEcsInitSystem {
        public void Init() {
            int backTypesCount = Global.Config.InGame.CellViewBackgrounds.Count;
            int startCellType = 0;

            for (int column = 0; column < Global.Config.InGame.LevelWidth; column++) {
                startCellType += 1;
                startCellType = startCellType % backTypesCount;

                int currentCellType = startCellType;

                for (int row = 0; row < Global.Config.InGame.LevelHeight; row++) {
                    Vector2Int position = new Vector2Int(column, row);
                    EcsEntity entity = Global.Data.InGame.GameField.Cells[position];
                    ref Cell cell = ref entity.Ref<Cell>().Unref();
                    cell.View = Global.Services.Pool.Get(cell.Configuration.ViewExample);
                    cell.View.transform.position = new Vector3(position.x, Global.Config.InGame.LevelHeight + 1);
                    cell.View.Entity = entity;

                    CellBackground cellBackground = Global.Services.Pool.Get(Global.Config.InGame.CellViewBackgrounds[currentCellType]);
                    currentCellType = (currentCellType + 1) % backTypesCount;
                    cellBackground.transform.position = new Vector3(column, row, 50);
                }
            }
        }
    }
}