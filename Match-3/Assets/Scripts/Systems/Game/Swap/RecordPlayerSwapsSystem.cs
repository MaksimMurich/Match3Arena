using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match3.Systems.Game.Swap
{
    public sealed class RecordPlayerSwapsSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly GameField _gameField = null;
        private readonly PlayerData _playerData = null;
        private readonly PlayerState _playerState = null;
        private readonly InGameConfiguration _configuration = null;
        private readonly EcsFilter<Cell, Vector2Int, SwapRequest> _filter = null;

        public void Init()
        {
            if(_playerData.UserSwaps == null)
            {
                _playerData.UserSwaps = new List<SwapRecord>();
            }
        }

        public void Run()
        {
            //@TODO check is correct swap and field didn't lock and only then record swap

            if (!_playerState.Active || _filter.GetEntitiesCount() == 0)
            {
                return;
            }

            SwapRequest swap = _filter.Get3(0);
            bool swapHasResult = GameFieldAnalyst.CheckIsCorrectSwap(swap.From, swap.To - swap.From, _gameField.Cells, _configuration);

            if (!swapHasResult)
            {
                return;
            }

            SwapRecord record = GenerateSwapRecord(_filter.Get3(0));
            _playerData.UserSwaps.Add(record);

            if (_playerData.UserSwaps.Count > _configuration.SaveUserSwapsCount)
            {
                _playerData.UserSwaps.RemoveRange(0, _playerData.UserSwaps.Count - _configuration.SaveUserSwapsCount);
            }
        }

        private SwapRecord GenerateSwapRecord(SwapRequest swap)
        {
            SwapRecord result = new SwapRecord();
            int maxHealthReward = (int)(_playerState.MaxLife - _playerState.CurrentLife);
            List<SwapPossibility> possibilities = GameFieldAnalyst.GetAllSwapPossibilities(maxHealthReward, _gameField, _configuration);
            possibilities = possibilities.OrderBy(s => s.SwapRewards.CalculateTotal()).ToList();

            result.SelectedSwap = possibilities.Where(p => CompareSwaps(swap, p)).First();
            int selectedSwapID = possibilities.IndexOf(result.SelectedSwap);
            float maxID = possibilities.Count - 1;
            result.SwapRating = selectedSwapID / maxID;

            return result;
        }

        private bool CompareSwaps(SwapRequest request, SwapPossibility possibility)
        {
            bool result = true;

            result &= request.From.x == possibility.FromX;
            result &= request.From.y == possibility.FromY;
            result &= request.To.x == possibility.ToX;
            result &= request.To.y == possibility.ToY;
           
            bool invertedResult = true;

            invertedResult &= request.From.x == possibility.ToX;
            invertedResult &= request.From.y == possibility.ToY;
            invertedResult &= request.To.x == possibility.FromX;
            invertedResult &= request.To.y == possibility.FromY;

            return result || invertedResult;
        }
    }
}