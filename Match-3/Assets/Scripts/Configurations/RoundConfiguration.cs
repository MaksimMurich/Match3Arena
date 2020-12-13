using UnityEngine;

namespace Match3.Configurations
{
    [CreateAssetMenu]
    public class RoundConfiguration : ScriptableObject
    {
        public int LevelWidth = 8;
        public int LevelHeight = 8;

        [SerializeField] private float _topMenuPadding = 0.2f;
        [SerializeField] private float _minFieldPadding = 0.05f;
        [SerializeField] private int _minRewardableChain = 3;
        [SerializeField] private float _swapMinMouseOffset = .5f;
        [SerializeField] private float _combo4RewardMultiplayer = 2f;
        [SerializeField] private float _combo5RewardMultiplayer = 4f;
        [SerializeField] private UserStateConfiguration _userStateConfiguration = null;
        [SerializeField] private AnimationsConfiguration _animationsConfiguration = null;
        [SerializeField] private CellConfiguration[] _cellConfigurations = null;

        public float TopMenuPadding { get => _topMenuPadding; }
        public float MinFieldPadding { get => _minFieldPadding; }
        public int MinRewardableChain => _minRewardableChain;
        public float SwapMinMouseOffset => _swapMinMouseOffset;
        public float Combo4Configuration => _combo4RewardMultiplayer;
        public float Combo5Configuration => _combo5RewardMultiplayer;
        public UserStateConfiguration UserStateConfiguration { get => _userStateConfiguration; }
        public AnimationsConfiguration Animation => _animationsConfiguration;
        public CellConfiguration[] CellConfigurations => _cellConfigurations;
    }
}
