using DG.Tweening;
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
        private int _timeRemain = (int)Global.Config.InGame.MaxTurnTime;
        private readonly Color _botViewColorDefault = Global.Views.InGame.BotDataView.TurnTimer.color;
        private readonly Color _playerViewColorDefault = Global.Views.InGame.PlayerDataView.TurnTimer.color;

        private Text _botView = Global.Views.InGame.BotDataView.TurnTimer;
        private Text _playerView = Global.Views.InGame.PlayerDataView.TurnTimer;
        private Transform _playerViewTransform = Global.Views.InGame.PlayerDataView.TurnTimer.transform;
        private Transform _botViewTransform = Global.Views.InGame.BotDataView.TurnTimer.transform;

        private Sequence _viewsAnimation = DOTween.Sequence();

        public void Init()
		{
			_botView.text = _defaultTimerView;
			_playerView.text = _defaultTimerView;

            SetScaleViewsDefaultIfNeeded();
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

            UpdateTurnTimerViewRequest updateTurnTimerViewRequest = _updateTurnTimerRequestsFilter.Get1(0);
            _timeRemain = (int)updateTurnTimerViewRequest.TimeRamain;
            int timeViewValue = _timeRemain;
            timeViewValue = Mathf.Max(timeViewValue, 0);

            _botView.text = timeViewValue.ToString();
            _playerView.text = timeViewValue.ToString();

            bool isPlayerTime = Global.Data.InGame.PlayerState.Active;
            _botView.gameObject.SetActive(!isPlayerTime);
            _playerView.gameObject.SetActive(isPlayerTime);

			if (_timeRemain <= _timeToSignal)
			{
				ScaleViews();
                SetViewsColor(Color.red);
            }
			else
			{
                SetScaleViewsDefaultIfNeeded();
                SetViewsColor();
            }
		}

        private void DeactivateIfNeed()
        {
            if(_nextPlayerRequestsfilter.GetEntitiesCount() > 0)
            {
                SetScaleViewsDefaultIfNeeded();
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
            SetScaleViewsDefaultIfNeeded();

            Tween upscalingBot = _botView.transform.DOScale(new Vector3(_scaleCoefficient, _scaleCoefficient), 0.4f);
            Tween upscalingPlayer = _playerView.transform.DOScale(new Vector3(_scaleCoefficient, _scaleCoefficient), 0.4f);
            Tween downscalingBot = _botView.transform.DOScale(new Vector3(_defaultTimerScale, _defaultTimerScale), 0.4f);
            Tween downscalingPlayer = _playerView.transform.DOScale(new Vector3(_defaultTimerScale, _defaultTimerScale), 0.4f);

            if(_timeRemain > 0)
			{
                _viewsAnimation.Join(upscalingBot);
                _viewsAnimation.Join(upscalingPlayer);
                _viewsAnimation.Append(downscalingBot);
                _viewsAnimation.Join(downscalingPlayer);
            }
			else
			{
                _viewsAnimation.Join(upscalingBot);
                _viewsAnimation.Join(upscalingPlayer);
            }
        }

        private void SetScaleViewsDefaultIfNeeded()
		{
            if(_timeRemain > 0)
			{
                _viewsAnimation.Kill();
                _viewsAnimation = DOTween.Sequence();
                _botView.transform.localScale = new Vector3(_defaultTimerScale, _defaultTimerScale);
                _playerView.transform.localScale = new Vector3(_defaultTimerScale, _defaultTimerScale);
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