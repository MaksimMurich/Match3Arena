using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Assets.Scripts.Services {
    public static class GameFieldAnalyst {
        public static bool HasChain(Dictionary<Vector2Int, EcsEntity> cells) {
            List<ChainEvent> chains = GetChains(cells);

            return chains.Count > 0;
        }

        public static List<ChainEvent> GetChains(Dictionary<Vector2Int, EcsEntity> cells) {
            List<ChainEvent> result = new List<ChainEvent>();

            for (int column = 0; column < Global.Config.InGame.LevelWidth; column++) {
                for (int row = 0; row < Global.Config.InGame.LevelHeight; row++) {
                    Vector2Int position = new Vector2Int(column, row);

                    if (!cells.ContainsKey(position)) {
                        continue;
                    }

                    result.AddRange(GetCellChains(cells, position));
                }
            }

            return result;
        }

        public static bool CheckCellInChain(Dictionary<Vector2Int, EcsEntity> cells, Vector2Int position) {
            Vector2Int direction = new Vector2Int(0, 1);
            ChainEvent horisontalRight = GetChain(position, direction, cells);

            if (horisontalRight.Size >= Global.Config.InGame.MinRewardableChain) {
                return true;
            }

            direction = new Vector2Int(0, -1);
            ChainEvent horisontalLeft = GetChain(position, direction, cells);

            if (horisontalLeft.Size >= Global.Config.InGame.MinRewardableChain) {
                return true;
            }

            direction = new Vector2Int(1, 0);
            ChainEvent verticalUp = GetChain(position, direction, cells);

            if (verticalUp.Size >= Global.Config.InGame.MinRewardableChain) {
                return true;
            }

            direction = new Vector2Int(-1, 0);
            ChainEvent verticalDown = GetChain(position, direction, cells);

            if (verticalDown.Size >= Global.Config.InGame.MinRewardableChain) {
                return true;
            }

            return false;
        }

        public static List<SwapPossibility> GetAllSwapPossibilities(int maxHealthReward, GameField gameField) {
            List<SwapPossibility> possibilities = new List<SwapPossibility>();

            for (int column = 0; column < Global.Config.InGame.LevelWidth; column++) {
                for (int row = 0; row < Global.Config.InGame.LevelHeight; row++) {
                    Vector2Int position = new Vector2Int(column, row);

                    if (!gameField.Cells.ContainsKey(position)) {
                        continue;
                    }

                    AddSwapIfPossible(gameField, possibilities, position, Vector2Int.up);
                    AddSwapIfPossible(gameField, possibilities, position, Vector2Int.right);
                }
            }

            possibilities.ForEach(p => p.SwapRewards.HealthReward = Math.Min(maxHealthReward, p.SwapRewards.HealthReward));

            return possibilities;
        }

        private static void AddSwapIfPossible(GameField gameField, List<SwapPossibility> possibilities, Vector2Int position, Vector2Int direction) {
            bool swapIsPossible = CheckSwapCreateChains(position, direction, gameField.Cells);

            if (swapIsPossible) {
                SwapPossibility possibility = CalculatePossibility(position, direction, gameField);
                possibilities.Add(possibility);
            }
        }

        private static bool CheckSwapCreateChains(Vector2Int position, Vector2Int direction, Dictionary<Vector2Int, EcsEntity> cells) {
            GameFieldModifier.SwapCellsWithoutChangeComponents(position, position + direction, cells);

            bool result = CheckCellInChain(cells, position);
            result = result || CheckCellInChain(cells, position + direction);

            GameFieldModifier.SwapCellsWithoutChangeComponents(position, position + direction, cells); // swap cells back

            return result;
        }

        private static SwapPossibility CalculatePossibility(Vector2Int position, Vector2Int direction, GameField field) {
            SwapPossibility result = new SwapPossibility() { FromX = position.x, FromY = position.y, ToX = position.x + direction.x, ToY = position.y + direction.y };

            GameField fieldClone = GameFieldModifier.Clone(field);
            GameFieldModifier.SwapCellsWithoutChangeComponents(position, position + direction, fieldClone.Cells);

            result.SwapRewards = CalculateRewards(fieldClone.Cells);

            return result;
        }

        private static SwapRewards CalculateRewards(Dictionary<Vector2Int, EcsEntity> cells) {
            SwapRewards result = new SwapRewards();

            List<ChainEvent> chains = GetChains(cells);


            while (chains.Count > 0) {
                AddChainsRewards(cells, ref result, chains);

                // move cells down to empty spaces
                MoveCellsDownToEmptySpaces(cells);
                chains = GetChains(cells);
            }

            return result;
        }

        private static void MoveCellsDownToEmptySpaces(Dictionary<Vector2Int, EcsEntity> cells) {
            for (int column = 0; column < Global.Config.InGame.LevelWidth; column++) {
                for (int row = 0; row < Global.Config.InGame.LevelHeight; row++) {
                    Vector2Int position = new Vector2Int(column, row);
                    EcsEntity cell = cells[position];

                    if (!cell.Equals(EcsEntity.Null)) {
                        continue;
                    }

                    bool emptySpace = true;

                    Vector2Int extenderPosition = position;

                    while (emptySpace) {
                        extenderPosition += Vector2Int.up;
                        bool hasCellID = cells.ContainsKey(extenderPosition);

                        if (!hasCellID) {
                            break;
                        }

                        emptySpace = cells[extenderPosition].Equals(EcsEntity.Null);
                    }
                    GameFieldModifier.SwapCellsWithoutChangeComponents(position, extenderPosition, cells);
                }
            }
        }

        private static void AddChainsRewards(Dictionary<Vector2Int, EcsEntity> cells, ref SwapRewards result, List<ChainEvent> chains) {
            for (int i = 0; i < chains.Count; i++) {
                for (int cellNum = 0; cellNum < chains[i].Size; cellNum++) {
                    Vector2Int position = chains[i].Position + chains[i].Direction * cellNum;

                    if (cells[position].Equals(EcsEntity.Null)) {
                        continue;
                    }

                    Cell cell = cells[position].Ref<Cell>().Unref();

                    result.HealthReward += cell.Configuration.Health;
                    result.DemageReward += cell.Configuration.Demage;

                    cells[position] = EcsEntity.Null;
                }
            }
        }

        public static bool CheckIsCorrectSwap(Vector2Int cellId, Vector2Int direction, Dictionary<Vector2Int, EcsEntity> cells) {
            // swap
            Vector2Int targetCellId = cellId + direction;
            EcsEntity targetCell = cells[targetCellId];
            cells[targetCellId] = cells[cellId];
            cells[cellId] = targetCell;

            bool hasChains = HasChain(cells);

            // swap back
            targetCell = cells[targetCellId];
            cells[targetCellId] = cells[cellId];
            cells[cellId] = targetCell;

            return hasChains;
        }

        private static List<ChainEvent> GetCellChains(Dictionary<Vector2Int, EcsEntity> cells, Vector2Int position) {
            List<ChainEvent> result = new List<ChainEvent>();

            Vector2Int direction = new Vector2Int(0, 1);
            ChainEvent horisontal = GetChain(position, direction, cells);

            if (horisontal.Size >= Global.Config.InGame.MinRewardableChain) {
                if (!result.Contains(horisontal)) {
                    result.Add(horisontal);
                }
            }

            direction = new Vector2Int(1, 0);
            ChainEvent vertical = GetChain(position, direction, cells);

            if (vertical.Size >= Global.Config.InGame.MinRewardableChain) {
                if (!result.Contains(vertical)) {
                    result.Add(vertical);
                }
            }

            return result;
        }

        private static ChainEvent GetChain(Vector2Int startPosition, Vector2Int direction, Dictionary<Vector2Int, EcsEntity> cells) {

            Vector2Int position = startPosition;

            if (!cells.ContainsKey(position)) {
                return new ChainEvent();
            }

            Vector2Int cellBefore = position - direction;

            while (cells.ContainsKey(cellBefore) && GetCellType(cellBefore, cells) == GetCellType(position, cells)) {
                startPosition = cellBefore;
                position = cellBefore;
                cellBefore -= direction;
            }

            CellType cellType = GetCellType(position, cells);

            if (cellType == CellType.Unknown) {
                return new ChainEvent();
            }

            int chainSize = 0;

            while (cells.ContainsKey(position) && GetCellType(position, cells) == cellType) {
                chainSize++;
                position += direction;
            }

            return new ChainEvent() { Position = startPosition, Direction = direction, Size = chainSize };
        }

        private static bool CheckCellChainedBefore(Vector2Int direction, Vector2Int position, CellType cellType, Dictionary<Vector2Int, EcsEntity> cells) {
            bool result = false;
            Vector2Int previousCell = position - direction;
            bool hasCellBefore = cells.ContainsKey(previousCell);

            if (hasCellBefore) {
                CellType typeBefore = GetCellType(previousCell, cells);
                result = typeBefore == cellType;
            }

            return result;
        }

        private static CellType GetCellType(Vector2Int position, Dictionary<Vector2Int, EcsEntity> cells) {
            if (cells[position].Equals(null) || cells[position].Owner == null || cells[position].Has<EmptySpace>()) {
                return CellType.Unknown;
            }
            else {
                return cells[position].Ref<Cell>().Unref().Configuration.Type;
            }
        }
    }
}
