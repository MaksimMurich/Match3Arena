using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game.Events;
using UnityEngine;
using UnityEngine.UI;


namespace Match3.Systems.Game {
    sealed class ManageTurnTimeViewSystem : IEcsRunSystem, IEcsInitSystem {

        private readonly EcsFilter<UpdateTurnTimerViewRequest> _updateTurnTimerRequestsFilter = null;
        private readonly EcsFilter<NextPlayerRequest> _nextPlayerRequestsfilter = null;

        private readonly string _defaultTimerView = "0";
        private readonly float _timeToSignal = Global.Config.InGame.TurnTimerSignalTime;
        private readonly float _scaleCoefficient = Global.Config.InGame.TurnTimerScaleCoefficient;
        private readonly float _defaultScale = 1f;
        private readonly float _animationDuration = 0.8f; // do not set more than 1
        private int _timeRemain = (int)Global.Config.InGame.MaxTurnTime;
        private bool _lastTickMade = false;

        private readonly Color _botViewColorDefault = Global.Views.InGame.BotDataView.TurnTimer.color;
        private readonly Color _playerViewColorDefault = Global.Views.InGame.PlayerDataView.TurnTimer.color;
        private readonly Color _singnalColor = Color.red;

        private Text _botView = Global.Views.InGame.BotDataView.TurnTimer;
        private Text _playerView = Global.Views.InGame.PlayerDataView.TurnTimer;

        private Sequence _viewsAnimation = DOTween.Sequence();

        public void Init()
		{
			_botView.text = _defaultTimerView;
			_playerView.text = _defaultTimerView;

            SetScaleViewsDefault();
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

            Debug.Log("Timer has been updated...");

            UpdateTurnTimerViewRequest updateTurnTimerViewRequest = _updateTurnTimerRequestsFilter.Get1(0);
            _timeRemain = updateTurnTimerViewRequest.TimeRamain;
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
                SetViewsColor(_singnalColor);
            }
			else
			{
                SetScaleViewsDefault();
                SetViewsColor();
            }
		}

        private void DeactivateIfNeed()
        {
            if(_nextPlayerRequestsfilter.GetEntitiesCount() > 0)
            {
                SetScaleViewsDefault();
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
            SetScaleViewsDefault();

            if(_timeRemain == 1)
			{
                _lastTickMade = false;
			}
            else if(_timeRemain <= 0 && _lastTickMade == true)
			{
                return;
			}
            else if(_timeRemain <= 0)
			{
                _lastTickMade = true;
            }

            Tween upscalingBot = _botView.transform.DOScale(new Vector3(_scaleCoefficient, _scaleCoefficient), _animationDuration/2);
            Tween downscalingBot = _botView.transform.DOScale(new Vector3(_defaultScale, _defaultScale), _animationDuration/2);
            Tween upscalingPlayer = _playerView.transform.DOScale(new Vector3(_scaleCoefficient, _scaleCoefficient), _animationDuration/2);
            Tween downscalingPlayer = _playerView.transform.DOScale(new Vector3(_defaultScale, _defaultScale), _animationDuration/2);
            
            _viewsAnimation.Join(upscalingBot);
            _viewsAnimation.Join(upscalingPlayer);
            _viewsAnimation.Append(downscalingBot);
            _viewsAnimation.Join(downscalingPlayer);
        }

        private void SetScaleViewsDefault()
		{
            _viewsAnimation.Kill();
            _viewsAnimation = DOTween.Sequence();
            _botView.transform.localScale = new Vector3(_defaultScale, _defaultScale);
            _playerView.transform.localScale = new Vector3(_defaultScale, _defaultScale);
		}

        private void SetViewsColor()
		{
            _botView.color = _botViewColorDefault;
            _playerView.color = _playerViewColorDefault;
		}

        private void SetViewsColor(Color color)
		{
            _playerView.color = color;
            _botView.color = color;
		}
    }
}