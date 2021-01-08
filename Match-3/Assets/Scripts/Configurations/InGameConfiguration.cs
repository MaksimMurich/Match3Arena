using Match3.Assets.Scripts.Configurations;
using Match3.UnityComponents;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Configurations
{
    [CreateAssetMenu]
    public class InGameConfiguration : ScriptableObject
    {
        [SerializeField] private int _targetFrameRate = 50;
        [SerializeField] private int _levelWidth = 8;
        [SerializeField] private int _levelHeight = 8;
        [SerializeField] private float _playersMaxLife = 1000;
        [SerializeField] private float _topMenuPadding = 0.2f;
        [SerializeField] private float _bottomPadding = 0.15f;
        [SerializeField] private float _minFieldPadding = 0.05f;
        [SerializeField] private int _minRewardableChain = 3;
        [SerializeField] private float _swapMinMouseOffset = .5f;
        [SerializeField] private BotBehaviourConfiguration _botBehaviour = null;
        [SerializeField] private int _minDeltaRating = 10; // min delta player rating for win or lose round
        [SerializeField] private float _deltaRatingRewardMultiplayer = 0.1f;
        [SerializeField] private int _saveUserSwapsCount = 100;
        [SerializeField] private float _maxTurnTime = 30; // max time in seconds to make a turn before auto end of the turn

        [SerializeField] private UserStateConfiguration _userStateConfiguration = null;
        [SerializeField] private AnimationsConfiguration _animationsConfiguration = null;
        [SerializeField] private List<CellBackground> _cellViewBackgrounds = null;
        [SerializeField] private TextAsset _botNames = null;
        [SerializeField] private InGameSoundsConfiguration _sounds = null;
        [SerializeField] private CellConfiguration[] _cellConfigurations = null;

        public int TargetFrameRate { get => _targetFrameRate; }
        public int LevelHeight { get => _levelHeight; }
        public int LevelWidth { get => _levelWidth; }
        internal float PlayersMaxLife { get => _playersMaxLife; }
        public float TopMenuPadding { get => _topMenuPadding; }
        public float BottomPadding { get => _bottomPadding; }
        public float MinFieldPadding { get => _minFieldPadding; }
        public int MinRewardableChain => _minRewardableChain;
        public float SwapMinMouseOffset => _swapMinMouseOffset;
        public BotBehaviourConfiguration BotBehaviour { get => _botBehaviour; }
        public int MinDeltaRating { get => _minDeltaRating; }
        internal float DeltaRatingRewardMultiplayer { get => _deltaRatingRewardMultiplayer; }
        public int SaveUserSwapsCount { get => _saveUserSwapsCount; }
        public UserStateConfiguration UserStateConfiguration { get => _userStateConfiguration; }
        public AnimationsConfiguration Animation => _animationsConfiguration;
        public List<CellBackground> CellViewBackgrounds { get => _cellViewBackgrounds; }
        public TextAsset BotNames { get => _botNames; } //TODO add names for random selection
        public CellConfiguration[] CellConfigurations => _cellConfigurations;
        public InGameSoundsConfiguration Sounds { get => _sounds; }
		public float MaxTurnTime { get => _maxTurnTime; }
	}
}
