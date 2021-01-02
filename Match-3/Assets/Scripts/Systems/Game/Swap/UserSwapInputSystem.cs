using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Systems.Game.Swap
{
    public sealed class UserSwapInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Cell, Vector2Int, Selected> _filter = null;

        public void Run()
        {
            if (!Global.Data.InGame.PlayerState.Active)
            {
                return;
            }

            Vector2 mousePosition = Global.Views.InGame.Camera.ScreenToWorldPoint(Input.mousePosition);

            foreach (int index in _filter)
            {
                Cell cell = _filter.Get1(index);
                Vector2 cellPosition = cell.View.transform.position;
                Vector2 mouseOffset = mousePosition - cellPosition;
                EcsEntity cellEntity = _filter.GetEntity(index);

                Vector2Int offset = Vector2Int.zero;

                if (Mathf.Abs(mouseOffset.x) >  Global.Config.InGame.SwapMinMouseOffset)
                {
                    int offsetX = mouseOffset.x > 0 ? 1 : -1;
                    offset = new Vector2Int(offsetX, 0);
                }
                else if (Mathf.Abs(mouseOffset.y) >  Global.Config.InGame.SwapMinMouseOffset)
                {
                    int offsetY = mouseOffset.y > 0 ? 1 : -1;
                    offset = new Vector2Int(0, offsetY);
                }

                Vector2Int fieldPosition = _filter.Get2(index);
                Vector2Int targetPosition = fieldPosition + offset;

                if (offset.Equals(Vector2Int.zero) || !Global.Data.InGame.GameField.Cells.ContainsKey(targetPosition))
                {
                    continue;
                }

                SwapRequest swap = new SwapRequest()
                {
                    From = fieldPosition,
                    To = targetPosition
                };

                cellEntity.Set<SwapRequest>() = swap;
                cellEntity.Unset<Selected>();
                cellEntity.Set<DeselectCellAnimationRequest>();
            }
        }
    }
}