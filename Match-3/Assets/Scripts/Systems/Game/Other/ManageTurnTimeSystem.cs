using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;
using Match3.Components.Game.Events;

namespace Match3.Systems.Game {
    sealed class ManageTurnTimeSystem : IEcsRunSystem, IEcsInitSystem {
		private readonly EcsFilter<ResetTurnTimerRequest> _resetTurnTimerRequestsfilter = null;
		private readonly EcsFilter<SwapRequest> _swapFilter = null;
		private readonly EcsFilter<PlayerChangedEvent> _playerChanged = null;

        private float _timeRamain = Global.Config.InGame.MaxTurnTime; // value>0 needed to avoid starting timer before starting turn
        private bool _isTimerActive = false;

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
                _timeRamain -= Time.deltaTime;
                EcsEntity timerUpdateRequest = Global.Data.InGame.World.NewEntity();
                timerUpdateRequest.Set<UpdateTurnTimerViewRequest>().RemainTime = _timeRamain;
            }

            if (_timeRamain <= 0)
            {
                Global.Data.InGame.World.NewEntity().Set<NextPlayerRequest>();
                ResetTimeRemain();
            }
        }

        private void ResetTimeRemain()
        {
            _timeRamain = Global.Config.InGame.MaxTurnTime; // additional time over the time, setted in config to make such time in-game
            
            EcsEntity timerUpdateRequest = Global.Data.InGame.World.NewEntity();
            timerUpdateRequest.Set<UpdateTurnTimerViewRequest>().RemainTime = _timeRamain;
        }
	}
}