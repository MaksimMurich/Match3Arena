﻿using Leopotam.Ecs;
using Match3.Components.Game.Events;

namespace Match3.Assets.Scripts.Systems.Game.Animations
{
    public sealed class AnimateRewardSystem : IEcsRunSystem
    {
        private readonly InGameSceneData _sceneData = null;
        private readonly PlayerState _playerState = null;
        private readonly EcsFilter<RewardRequest> _filter = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() > 0)
            {
                _sceneData.ScoreView.SetScore(_playerState.Score);
            }
        }
    }
}
