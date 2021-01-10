using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;
using Match3.Components.Game.Events;

namespace Match3.Systems.Game {
    sealed class ManageTurnTimeSystem : IEcsRunSystem, IEcsInitSystem {
		private readonly EcsFilter<ResetTurnTimerRequest> _resetTurnTimerRequestsfilter = null;
		private readonly EcsFilter<SwapRequest> _swapFilter = null;
		private readonly EcsFilter<PlayerChangedEvent> _playerChanged = null;

        private int _timeViewRemain = 0;
        private bool _isTimerActive = false;
        private float _timeRemain = Global.Config.InGame.MaxTurnTime;

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
                
                if(_timeViewRemain != _timeRemain)
				{
                    UpdateView();
                }
            }

            if (_timeRemain <= 0)
            {
                Global.Data.InGame.World.NewEntity().Set<NextPlayerRequest>();
                ResetTimeRemain();
            }
        }

        private void ResetTimeRemain()
        {
            _timeRemain = Global.Config.InGame.MaxTurnTime;
            UpdateView();
        }

        private void UpdateView()
		{
            _timeViewRemain = (int)_timeRemain;
            EcsEntity timerUpdateRequest = Global.Data.InGame.World.NewEntity();
            timerUpdateRequest.Set<UpdateTurnTimerViewRequest>().TimeRamain = _timeViewRemain;
        }
	}
}