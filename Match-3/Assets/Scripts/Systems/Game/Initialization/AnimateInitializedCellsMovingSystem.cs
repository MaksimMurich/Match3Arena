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
            for (int column = 0; column <  Global.Config.InGame.LevelWidth; column++)
            {
                for (int row = 0; row <  Global.Config.InGame.LevelHeight; row++)
                {
                    EcsEntity entity = Global.Data.InGame.GameField.Cells[new Vector2Int(column, row)];
                    ref Cell cell = ref entity.Ref<Cell>().Unref();
                    Global.Data.InGame.GameField.Cells[new Vector2Int(column, row)].Set<ChangeFieldAnimating>();

                    Vector3 targetPosition = new Vector3(column, row);
                    cell.View.transform
                        .DOMove(targetPosition,  Global.Config.InGame.Animation.CellMovingSeconds)
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