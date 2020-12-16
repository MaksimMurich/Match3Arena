﻿using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class AnimateInitializedCellsMovingSystem : IEcsInitSystem
    {
        private readonly GameField _gameField = null;
        private readonly InGameConfiguration _configuration = null;

        public void Init()
        {
            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                for (int row = 0; row < _configuration.LevelHeight; row++)
                {
                    EcsEntity entity = _gameField.Cells[new Vector2Int(column, row)];
                    ref Cell cell = ref entity.Ref<Cell>().Unref();
                    _gameField.Cells[new Vector2Int(column, row)].Set<ChangeFieldAnimating>();

                    Vector3 targetPosition = new Vector3(column, row);
                    cell.View.transform
                        .DOMove(targetPosition, _configuration.Animation.CellMovingSeconds)
                        .OnComplete(() => RemoveFieldChangingState(entity));
                }
            }
        }

        private void RemoveFieldChangingState(EcsEntity entity)
        {
            entity.Unset<ChangeFieldAnimating>();
        }
    }
}