using System;
using UnityEngine;

namespace Match3.Assets.Scripts.Configurations {
    [Serializable]
    public class BotBehaviourConfiguration {
        [SerializeField] private AnimationCurve _difficultPossibility = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private float _minThinkingTime = 0.87f;
        [SerializeField] private float _maxThinkingTime = 9;
        [SerializeField] private float _thinkingTimeDeviationProportion = 1.5f;
        [SerializeField] private float _fromSelectToSwapDelay = .5f;

        public float MinThinkingTime { get => _minThinkingTime; }
        public float MaxThinkingTime { get => _maxThinkingTime; }
        public float ThinkingTimeDeviationProportion { get => _thinkingTimeDeviationProportion; }
        public float FromSelectToSwapDelay { get => _fromSelectToSwapDelay; }
        public AnimationCurve DifficultPossibility { get => _difficultPossibility; }
    }
}
