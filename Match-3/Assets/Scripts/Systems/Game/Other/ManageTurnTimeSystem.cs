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
				if (needUpdateView)
				{
                    UpdateView();
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
            _timeViewRemain = Mathf.Max((int)(_timeRemain - _expirationDelay), 0);
            EcsEntity timerUpdateRequest = Global.Data.InGame.World.NewEntity();
            timerUpdateRequest.Set<UpdateTurnTimerViewRequest>().TimeRamain = _timeViewRemain;
        }
	}
}