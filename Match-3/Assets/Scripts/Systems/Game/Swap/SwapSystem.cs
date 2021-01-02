using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Systems.Game.Swap
{
    public sealed class SwapSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Cell, Vector2Int, SwapRequest> _filter = null;
        private readonly EcsFilter<ChangeFieldAnimating> _fieldChangers = null;
        private readonly EcsFilter<AnimateExplosion> _explosionAnimations = null;
        private readonly EcsFilter<ChainEvent> _chains = null;

        public void Run()
        {
            bool swapLocked = _fieldChangers.GetEntitiesCount() > 0 || _chains.GetEntitiesCount() > 0 || _explosionAnimations.GetEntitiesCount() > 0;

            if (swapLocked)
            {
                return;
            }

            foreach (int index in _filter)
            {
                Vector2Int cellPosition = _filter.Get2(index);
                Vector2Int targetPosition = _filter.Get3(index).To;

                EcsEntity swapCell = _filter.GetEntity(index);
                EcsEntity secondCell = Global.Data.InGame.GameField.Cells[targetPosition];

                swapCell.Set<Vector2Int>() = targetPosition;
                swapCell.Set<AnimateSwapRequest>().MainCell = true;

                secondCell.Set<Vector2Int>() = cellPosition;
                secondCell.Set<AnimateSwapRequest>().MainCell = false;

                Global.Data.InGame.GameField.Cells[cellPosition] = secondCell;
                Global.Data.InGame.GameField.Cells[targetPosition] = swapCell;

                List<ChainEvent> chains = GameFieldAnalyst.GetChains(Global.Data.InGame.GameField.Cells);

                if (chains.Count == 0)
                {
                    Global.Data.InGame.GameField.Cells[cellPosition] = swapCell;
                    Global.Data.InGame.GameField.Cells[targetPosition] = secondCell;

                    swapCell.Set<Vector2Int>() = cellPosition;

                    AnimateSwapBackRequest request = new AnimateSwapBackRequest();
                    request.TargetPosition = targetPosition;
                    swapCell.Set<AnimateSwapBackRequest>() = request;

                    secondCell.Set<Vector2Int>() = targetPosition;

                    AnimateSwapBackRequest secondRequest = new AnimateSwapBackRequest();
                    secondRequest.TargetPosition = cellPosition;
                    secondCell.Set<AnimateSwapBackRequest>() = secondRequest;
                }
                else
                {
                    if (Global.Data.InGame.PlayerState.Active)
                    {
                        Global.Data.InGame.PlayerState.StepsCount += 1;
                    }

                     Global.Data.InGame.World.NewEntity().Set<NextPlayerRequest>();
                }
            }
        }
    }
}