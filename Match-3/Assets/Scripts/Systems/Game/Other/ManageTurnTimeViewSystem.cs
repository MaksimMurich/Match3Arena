using Leopotam.Ecs;
using Match3.Components.Game.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Systems.Game {
    sealed class ManageTurnTimeViewSystem : IEcsRunSystem, IEcsInitSystem {

        private readonly EcsFilter<UpdateTurnTimerViewRequest> _updateTurnTimerRequestsFilter = null;
        private readonly EcsFilter<NextPlayerRequest> _nextPlayerRequestsfilter = null;

        private bool _isTimerUpdateNeeded;
        private const string _defaultTimerView = "0";
        private float _timeToSignal = Global.Config.InGame.TurnTimerSignalTime;
        private float _scaleCoefficient = Global.Config.InGame.TurnTimerScaleCoefficient;
        private Text _botView = Global.Views.InGame.BotDataView.TurnTimer;
        private Text _playerView = Global.Views.InGame.PlayerDataView.TurnTimer;
        private string _debugTemp;

		public void Init()
		{
			_botView.text = _defaultTimerView;
			_playerView.text = _defaultTimerView;

            Hide();
        }

        public void Run()
        {
            DeactivateIfNeed();

			bool timeChanged = _updateTurnTimerRequestsFilter.GetEntitiesCount() > 0;
			if (!timeChanged)
            {
                return;
            }

            UpdateTurnTimerViewRequest updateTurnTimerViewRequest = _updateTurnTimerRequestsFilter.Get1(0);
            int timeRemain = (int)updateTurnTimerViewRequest.RemainTime;
            int timeViewValue = timeRemain - Global.Config.InGame.UserStepDelay;
            timeViewValue = Mathf.Max(timeViewValue, 0);

            _botView.text = timeViewValue.ToString();
            _playerView.text = timeViewValue.ToString();

            bool isPlayerTime = Global.Data.InGame.PlayerState.Active;
            _botView.gameObject.SetActive(!isPlayerTime);
            _playerView.gameObject.SetActive(isPlayerTime);

            if (timeRemain <= _timeToSignal)
            {
                //_currentTimerView. *= _scaleCoefficient;
            }
        }

        private void DeactivateIfNeed()
        {
            if(_nextPlayerRequestsfilter.GetEntitiesCount() > 0)
            {
                Hide();
            }
        }

        private void Hide()
        {
            _botView.gameObject.SetActive(false);
            _playerView.gameObject.SetActive(false);
        }
    }
}