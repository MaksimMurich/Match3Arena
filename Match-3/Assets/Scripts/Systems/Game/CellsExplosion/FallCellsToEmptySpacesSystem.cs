using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.CellsExplosion
{
    public sealed class FallCellsToEmptySpacesSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly InGameConfiguration _configuration = null;
        private readonly EcsFilter<EmptySpace> _filter = null;

        public void Run()
        {
            bool fullField = _filter.GetEntitiesCount() == 0;

            if (fullField)
            {
                return;
            }

            for (int column = 0; column <  Global.Config.InGame.LevelWidth; column++)
            {
                for (int row = 0; row <  Global.Config.InGame.LevelHeight; row++)
                {
                    Vector2Int position = new Vector2Int(column, row);
                    EcsEntity cell = Global.Data.InGame.GameField.Cells[position];

                    if (!cell.Has<EmptySpace>())
                    {
                        continue;
                    }

                    bool emptySpace = true;

                    Vector2Int extenderPosition = position;

                    while (emptySpace)
                    {
                        extenderPosition = extenderPosition + Vector2Int.up;
                        bool hasCellID = Global.Data.InGame.GameField.Cells.ContainsKey(extenderPosition);

                        if (!hasCellID)
                        {
                            break;
                        }

                        emptySpace = Global.Data.InGame.GameField.Cells[extenderPosition].Has<EmptySpace>();
                    }

                    SwapCells(position, extenderPosition);
                }
            }
        }

        private void SwapCells(Vector2Int position, Vector2Int extenderPosition)
        {
            bool hasCellID = Global.Data.InGame.GameField.Cells.ContainsKey(extenderPosition);

            if (!hasCellID)
            {
                return;
            }

            EcsEntity emptyEntity = Global.Data.InGame.GameField.Cells[position];
            EcsEntity extenderEntity = Global.Data.InGame.GameField.Cells[extenderPosition];
            Global.Data.InGame.GameField.Cells.Remove(position);
            Global.Data.InGame.GameField.Cells.Remove(extenderPosition);
            Global.Data.InGame.GameField.Cells.Add(position, extenderEntity);
            Global.Data.InGame.GameField.Cells.Add(extenderPosition, emptyEntity);
            emptyEntity.Set<Vector2Int>() = extenderPosition;
            extenderEntity.Set<Vector2Int>() = position;

            extenderEntity.Set<AnimateFallDownRequest>();
        }
    }
}
