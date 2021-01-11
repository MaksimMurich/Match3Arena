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
        private readonly float _maxTimerScale = Global.Config.InGame.TurnTimerMaxScale;
        private readonly float _defaultTimerScale = 1f;
        private readonly Color _botViewColorDefault = Global.Views.InGame.BotDataView.TurnTimer.color;
        private readonly Color _playerViewColorDefault = Global.Views.InGame.PlayerDataView.TurnTimer.color;

        private Text _botView = Global.Views.InGame.BotDataView.TurnTimer;
        private Text _playerView = Global.Views.InGame.PlayerDataView.TurnTimer;
        private Transform _playerViewTransform = Global.Views.InGame.PlayerDataView.TurnTimer.transform;
        private Transform _botViewTransform = Global.Views.InGame.BotDataView.TurnTimer.transform;

        public void Init()
		{
            // set timer`s init view
			_botView.text = _defaultTimerView;
			_playerView.text = _defaultTimerView;

            ScaleViews();
            SetViewsColor();
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

            Debug.Log("Timer updated...");

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
				ScaleViews(_scaleCoefficient);
                SetViewsColor(Color.red);

            }
			else
			{
                ScaleViews();
                SetViewsColor();
            }
		}

        private void DeactivateIfNeed()
        {
            if(_nextPlayerRequestsfilter.GetEntitiesCount() > 0)
            {
                ScaleViews();
                SetViewsColor();
                Hide();
            }
        }

        private void Hide()
        {
            _botView.gameObject.SetActive(false);
            _playerView.gameObject.SetActive(false);
        }

        private void ScaleViews()
		{
            _botViewTransform.localScale = new Vector3(_defaultTimerScale, _defaultTimerScale);
            _playerViewTransform.localScale = new Vector3(_defaultTimerScale, _defaultTimerScale);
        }

        private void ScaleViews(float scaleCoefficient)
        {
            Vector3 botNewScale = _botViewTransform.localScale * scaleCoefficient;
            Vector3 playerNewScale = _playerViewTransform.localScale * scaleCoefficient;

            _botViewTransform.localScale = new Vector3(botNewScale.x, botNewScale.y);
            _playerViewTransform.localScale = new Vector3(playerNewScale.x, playerNewScale.y);

            if(_botViewTransform.localScale.x > _maxTimerScale)
			{
                _botViewTransform.localScale = new Vector3(_maxTimerScale, _maxTimerScale);
                _playerViewTransform.localScale = new Vector3(_maxTimerScale, _maxTimerScale);
            }
        }

        private void SetViewsColor()
		{
            _playerView.color = _playerViewColorDefault;
            _botView.color = _botViewColorDefault;
		}

        private void SetViewsColor(Color color)
		{
            _playerView.color = color;
            _botView.color = color;
		}
    }
}