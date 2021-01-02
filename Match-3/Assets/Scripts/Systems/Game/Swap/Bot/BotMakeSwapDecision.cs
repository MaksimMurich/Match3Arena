using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Bot
{
    public sealed class BotMakeSwapDecision : IEcsRunSystem
    {
        private readonly EcsFilter<BotMakeSwapDecisionRequest> _filter = null;


        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0)
            {
                return;
            }

            List<SwapPossibility> swaps = GameFieldAnalyst.GetAllSwapPossibilities((int)(OpponentState.MaxLife - OpponentState.CurrentLife), Global.Data.InGame.GameField);
            swaps = swaps.OrderBy(s => s.SwapRewards.CalculateTotal()).ToList();

            float swapRangesPoint = UnityEngine.Random.Range(0f, 1f);
            float swapRating = OpponentState.SwapPowerRanges.Where(r => r.RangeMin <= swapRangesPoint && r.RangeMax >= swapRangesPoint).First().Power;
            int swapDecisionID = Math.Min(swaps.Count - 1, Mathf.RoundToInt(swapRating * swaps.Count));

            SwapPossibility decision = swaps[swapDecisionID];
            Vector2Int cellId = new Vector2Int(decision.FromX, decision.FromY);
            Vector2Int direction = new Vector2Int(decision.ToX - decision.FromX, decision.ToY - decision.FromY);

            if (UnityEngine.Random.Range((int)0, 2) > 0) // change swap direction for real player imitation
            {
                cellId += direction;
                direction *= -1;
            }

            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() => SelectSomeCell(cellId));
            sequence.AppendInterval(Global.Config.InGame.BotBehaviour.FromSelectToSwapDelay);
            sequence.AppendCallback(() => SwapCells(cellId, direction));
        }

        private void SelectSomeCell(Vector2Int cellId)
        {
            Global.Data.InGame.GameField.Cells[cellId].Set<Selected>();
            Global.Data.InGame.GameField.Cells[cellId].Set<SelectCellAnimationRequest>();
            Debug.Log("select cell vector2Int(2, 2)");
        }

        private void SwapCells(Vector2Int cellId, Vector2Int direction)
        {
            EcsEntity cellEntity = Global.Data.InGame.GameField.Cells[cellId];
            cellEntity.Unset<Selected>();
            cellEntity.Set<DeselectCellAnimationRequest>();
            cellEntity.Set<SwapRequest>() = new SwapRequest()
            {
                From = cellId,
                To = cellId + direction
            };

            Debug.Log("SwapCells");
        }
    }
}
