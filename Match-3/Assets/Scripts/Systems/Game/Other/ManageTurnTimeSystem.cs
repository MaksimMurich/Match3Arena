using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;
using Match3.Components.Game.Events;

namespace Match3.Systems.Game {
    sealed class ManageTurnTimeSystem : IEcsRunSystem, IEcsInitSystem {
		private readonly EcsFilter<SwapRequest> _swapFilter = null;
		private readonly EcsFilter<PlayerChangedEvent> _playerChanged = null;

        private int _timeViewRemain = 0;
        private bool _isTimerActive = false;
        private static readonly int _expirationDelay = Global.Config.InGame.ExpirationDelay;
        private float _timeRemain = Global.Config.InGame.MaxTurnTime + _expirationDelay;
        private float _lastTimeUpdate = 0;

		public void Init()
		{
            Sequence sequence = DOTween.Sequence();
            sequence.SetDelay(Global.Config.InGame.Animation.SelectFirstPlayerDuration);
            sequence.OnComplete(() =>
            {
                _isTimerActive = true;
                ResetTimeRemain();
            });
        }

        void IEcsRunSystem.Run()
        {
            _isTimerActive = _isTimerActive && _swapFilter.GetEntitiesCount() == 0;
            _isTimerActive = _isTimerActive || _playerChanged.GetEntitiesCount() > 0;

            bool needResetTimer = _playerChanged.GetEntitiesCount() > 0;
            if (needResetTimer)
            {
                ResetTimeRemain();
            }

            if (_isTimerActive)
            {
                _timeRemain -= Time.deltaTime;

				bool needUpdateView = _timeViewRemain != (int)(_timeRemain - (float)_expirationDelay);
                needUpdateView = needUpdateView || ((Time.time-_lastTimeUpdate) > 1.07f); // force update if needed
				if (needUpdateView)
				{
                    UpdateView();
                    _lastTimeUpdate = Time.time;
				}
            }

            if (_timeRemain <= 0)
            {
                Global.Data.InGame.World.NewEntity().Set<TurnTimeIsUpEvent>();
                ResetTimeRemain();
            }
        }

        private void ResetTimeRemain()
        {
            _timeRemain = Global.Config.InGame.MaxTurnTime + _expirationDelay + (2*Time.deltaTime); // do not remove additional deltaTime. It will cause visual bug.
            UpdateView();
        }

        private void UpdateView()
		{
            _timeViewRemain = (int)(_timeRemain - _expirationDelay);
            EcsEntity timerUpdateRequest = Global.Data.InGame.World.NewEntity();
            timerUpdateRequest.Set<UpdateTurnTimerViewRequest>().TimeRamain = _timeViewRemain;
        }
	}
}