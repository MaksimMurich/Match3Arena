using System;
using UnityEngine;

namespace Match3.Configurations {
    [Serializable]
    public class AnimationsConfiguration {
        [SerializeField] private float _explodedRewardUpAnimatingDuration = 1;
        [SerializeField] private int _randomAvatarsCount = 30;
        [SerializeField] private int _randomAvatarsDuration = 1;
        [SerializeField] private float _setectedCellScaleSeconds = .3f;
        [SerializeField] private float _setectedCellScale = 1.2f;
        [SerializeField] private float _swapDuration = .4f;
        [SerializeField] private float _explosionScale = 0.05f;
        [SerializeField] private float _explosionSeconds = .5f;
        [SerializeField] private float _updateCellPositionSeconds = .5f;
        [SerializeField] private float _startGameBetAccumulationDurationSec = 3;
        [SerializeField] private float _updateLifeTime = 1;
        [SerializeField] private Vector3 _upCellOnAnimate = new Vector3(0, 0, -.1f);
        [SerializeField] private float _turnTimerScaleCoefficient = 1.5f; // coefficient to scale turns`s timer each second

        public float ExplodedRewardUpAnimatingDuration { get => _explodedRewardUpAnimatingDuration; }
        public int SelectFirstPlayerAvatarsCount => _randomAvatarsCount; // count of avatar images in select first user animation 
        public int SelectFirstPlayerDuration => _randomAvatarsDuration;
        public float SetectedCellScaleSeconds => _setectedCellScaleSeconds;
        public float SetectedCellScale => _setectedCellScale;
        public float SwapDuration => _swapDuration;
        public float ExplosionScale => _explosionScale;
        public float ExplosionSeconds => _explosionSeconds;
        public float CellMovingSeconds => _updateCellPositionSeconds;
        public float StartGameBetAccumulationDurationSec { get => _startGameBetAccumulationDurationSec; }
        public float UpdateLifeTime { get => _updateLifeTime; }
        public Vector3 UpCellOnAnimate => _upCellOnAnimate;
        public float TurnTimerScaleCoefficient { get => _turnTimerScaleCoefficient; }
    }
}