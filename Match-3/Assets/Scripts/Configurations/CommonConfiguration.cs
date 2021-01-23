using UnityEngine;

namespace Match3.Configurations {
    [CreateAssetMenu]
    public class CommonConfiguration : ScriptableObject {
        [SerializeField] private int _targetFrameRate = 50;
        [SerializeField] private int _minDeltaRating = 10; // min delta player rating for win or lose round
        [SerializeField] private float _deltaRatingRewardMultiplayer = 0.1f;
        [SerializeField] private float _playersMaxLife = 1000;
        [SerializeField] private UserStateConfiguration _userStateConfiguration = null;
        public int TargetFrameRate { get => _targetFrameRate; }
        public int MinDeltaRating { get => _minDeltaRating; }
        internal float DeltaRatingRewardMultiplayer { get => _deltaRatingRewardMultiplayer; }
        internal float PlayersMaxLife { get => _playersMaxLife; }
        public UserStateConfiguration UserStateConfiguration { get => _userStateConfiguration; }
    }
}
