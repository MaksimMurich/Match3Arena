﻿using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;

namespace Match3.Assets.Scripts.Systems.Game
{
    public sealed class ChangeActivePlayerSystem : IEcsRunSystem
    {
        private bool _needChangeActivePlayer;

        private readonly EcsWorld _world = null;
        private readonly PlayerState _playerState = null;
        private readonly EcsFilter<NextPlayerRequest> _filter = null;
        private readonly EcsFilter<ChangeFieldAnimating> _fieldChangers = null;
        private readonly EcsFilter<AnimateExplosion> _explosionAnimations = null;
        private readonly EcsFilter<ChainEvent> _chains = null;
        private readonly EcsFilter<Selected> _selected = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() > 0)
            {
                _needChangeActivePlayer = true;
                return; // skip one frame before change active player for locker animations activation
            }

            bool actionLocked = _fieldChangers.GetEntitiesCount() > 0 || _chains.GetEntitiesCount() > 0 || _explosionAnimations.GetEntitiesCount() > 0;

            if (actionLocked || !_needChangeActivePlayer)
            {
                return;
            }

            _needChangeActivePlayer = false;
            _playerState.Active = !_playerState.Active;

            if (!_playerState.Active)
            {
                EcsEntity makeSwapRequest = _world.NewEntity();
                makeSwapRequest.Set<PlayerChangedEvent>();
            }

            foreach (int index in _selected)
            {
                _selected.GetEntity(index).Unset<Selected>();
                _selected.GetEntity(index).Set<DeselectCellAnimationRequest>();
            }
        }
    }
}
