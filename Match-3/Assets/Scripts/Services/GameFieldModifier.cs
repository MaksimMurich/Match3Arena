using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Assets.Scripts.Services {
    public static class GameFieldModifier {
        public static GameField Clone(GameField gameField) {
            GameField result = new GameField();

            foreach (KeyValuePair<Vector2Int, EcsEntity> cell in gameField.Cells) {
                result.Cells.Add(cell.Key, cell.Value);
            }

            return result;
        }

        public static void SwapCellsWithoutChangeComponents(Vector2Int position, Vector2Int extenderPosition, Dictionary<Vector2Int, EcsEntity> cells) {
            if (!cells.ContainsKey(extenderPosition)) {
                return;
            }

            Vector2Int target = extenderPosition;
            EcsEntity targetCell = cells[target];
            cells[target] = cells[position];
            cells[position] = targetCell;
        }
    }
}
