﻿using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game
{
    public sealed class DisableActivePlayerIndicatorSystem : IEcsRunSystem
    {
        private readonly InGameSceneData _inGameSceneData = null;
        private readonly EcsFilter<Cell, Vector2Int, AnimateSwapRequest>.Exclude<AnimateSwapBackRequest> _filter = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() > 0)
            {
                _inGameSceneData.PlayerDataView.ActivateOutline(false);
                _inGameSceneData.BotDataView.ActivateOutline(false);
            }
        }
    }
}
