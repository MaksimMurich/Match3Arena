using Leopotam.Ecs;
using Match3.Components.Game.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Systems.Game {
    sealed class ManageTurnTimeViewSystem : IEcsRunSystem, IEcsInitSystem {

        private readonly EcsFilter<UpdateTurnTimerViewRequest> _updateTurnTimerRequestsFilter = null;
        private readonly EcsFilter<NextPlayerRequest> _nextPlayerRequestsfilter = null;

        private const string _defaultTimerView = "0";
        private readonly float _timeToSignal = Global.Config.InGame.TurnTimerSignalTime;
        private readonly float _scaleCoefficient = Global.Config.InGame.TurnTimerScaleCoefficient;
        private Text _botView = Global.Views.InGame.BotDataView.TurnTimer;
        private Text _playerView = Global.Views.InGame.PlayerDataView.TurnTimer;
        private int _playerViewFontSizeOriginal;
        private int _botViewFontSizeOriginal;
        private float _playerViewFontSize;
        private float _botViewFontSize;

		public void Init()
		{
            // set timer`s init view
			_botView.text = _defaultTimerView;
			_playerView.text = _defaultTimerView;

            // get views` size font
            _playerViewFontSizeOriginal = _playerView.fontSize;
            _botViewFontSizeOriginal = _botView.fontSize;
            _playerViewFontSize = _playerViewFontSizeOriginal;
            _botViewFontSize = _botViewFontSizeOriginal;

            Hide();
        }

        public void Run()
        {
            // for debug:
            _playerView.fontSize = 2;
            _botView.fontSize = 2;
            
            DeactivateIfNeed();

            bool timeChanged = _updateTurnTimerRequestsFilter.GetEntitiesCount() > 0;
			if (!timeChanged)
            {
                return;
            }

            UpdateTurnTimerViewRequest updateTurnTimerViewRequest = _updateTurnTimerRequestsFilter.Get1(0);
            int timeRemain = (int)updateTurnTimerViewRequest.TimeRamain;
            int timeViewValue = timeRemain;
            timeViewValue = Mathf.Max(timeViewValue, 0);

            _botView.text = timeViewValue.ToString();
            _playerView.text = timeViewValue.ToString();

            bool isPlayerTime = Global.Data.InGame.PlayerState.Active;
            _botView.gameObject.SetActive(!isPlayerTime);
            _playerView.gameObject.SetActive(isPlayerTime);

            if (timeRemain <= _timeToSignal)
            {
                _playerViewFontSize *= _scaleCoefficient;
                _botViewFontSize *= _scaleCoefficient;
                _playerView.fontSize = (int)_playerViewFontSize;                
                _botView.fontSize = (int)_botViewFontSize;                
            }
        }

        private void DeactivateIfNeed()
        {
            if(_nextPlayerRequestsfilter.GetEntitiesCount() > 0)
            {
                Hide();
                ResetViewsFontSizes();
            }
        }

        private void Hide()
        {
            _botView.gameObject.SetActive(false);
            _playerView.gameObject.SetActive(false);
        }

        private void ResetViewsFontSizes()
		{
            /*
            _botView.fontSize = _botViewFontSizeOriginal;
            _playerView.fontSize = _playerViewFontSizeOriginal;
            _playerViewFontSize = _playerViewFontSizeOriginal;
            _botViewFontSize = _botViewFontSizeOriginal;
            */
        }
    }
}