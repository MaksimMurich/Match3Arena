﻿using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.Configurations;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Systems.Game
{
    public sealed class CreateChainsSystem : IEcsRunSystem
    {
        private bool _fieldChanged;

        private readonly EcsWorld _ecsWorld = null;
        private readonly GameField _gameField = null;
        private readonly InGameConfiguration _configuration = null;
        private readonly EcsFilter<ChangeFieldAnimating> _changeField = null;

        public void Run()
        {
            if (_changeField.GetEntitiesCount() > 0)
            {
                _fieldChanged = true;
            }
            else if (_fieldChanged)
            {
                _fieldChanged = false;
                List<ChainEvent> chains = GameFieldAnalyst.GetChains(_gameField.Cells, _configuration);

                for (int i = 0; i < chains.Count; i++)
                {
                    AddChain(chains[i].Position, chains[i].Direction, chains[i].Size);
                }
            }
        }

        private void AddChain(Vector2Int position, Vector2Int direction, int size)
        {
            var chainEntity = _ecsWorld.NewEntity();

            ChainEvent chain = new ChainEvent()
            {
                Size = size,
                Direction = direction,
                Position = position
            };

            chainEntity.Set<ChainEvent>() = chain;
        }
    }
}