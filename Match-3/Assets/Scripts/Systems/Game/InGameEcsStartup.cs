using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Common;
using Match3.Assets.Scripts.Components.Game.Events.Rewards;
using Match3.Assets.Scripts.Services.Pool;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Assets.Scripts.Systems.Common;
using Match3.Assets.Scripts.Systems.Game;
using Match3.Assets.Scripts.Systems.Game.Animations;
using Match3.Assets.Scripts.Systems.Game.CellsExplosion;
using Match3.Assets.Scripts.Systems.Game.Initialization;
using Match3.Assets.Scripts.Systems.Game.Initialization.FirstPlaer;
using Match3.Assets.Scripts.Systems.Game.Swap.Bot;
using Match3.Assets.Scripts.Systems.Game.Swap.Rewards;
using Match3.Assets.Scripts.Systems.Game.UI;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using Match3.Systems.Game;
using Match3.Systems.Game.Initialization;
using Match3.Systems.Game.Initialization.Bet;
using Match3.Systems.Game.Swap;
using Match3.Systems.Game.UserInputs;
using UnityEngine;

namespace Match3
{
    sealed class InGameEcsStartup : MonoBehaviour
    {
        [SerializeField] private InGameConfiguration _configuration = null;
        [SerializeField] private InGameSceneData _sceneData = null;

        private EcsWorld _world;
        private EcsSystems _systems;
        private ObjectPool _objectPool;

        private readonly GameField _gameField = new GameField();
        private PlayerState _playerState;
        private PlayerData _playerData;

        void Start()
        {
            _playerData = LocalSaveLoad<PlayerData>.Load();
            _playerData = _playerData != null ? _playerData : new PlayerData(_configuration.UserStateConfiguration.Rating, _configuration.UserStateConfiguration.CoinsCount);

            _playerState = new PlayerState(_configuration.PlayersMaxLife, 100);

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _objectPool = new ObjectPool();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            _systems

                // common systems
                .Add(new PlayerPreferencesSystem())

                // initialization
                .Add(new AudioSystem())
                .OneFrame<PlaySoundRequest>()

                .Add(new SetCellConfigSpawnRangesSystem())
                .Add(new InitializeCellRewardViewTable())
                .Add(new InitializeFieldSystem())
                .Add(new InitializeFieldViewSystem())
                .Add(new ConfigurateCameraSystem())
                .Add(new GenerateBotDifficultSystem())
                .Add(new GenerateBotSwapPowerRangesSystem())
                .Add(new ShowPlayersDataSystem())
                .Add(new UnrewardPlayerOnStartRoundSystem())

                .Add(new AnimateInitializedCellsMovingSystem())
                .Add(new BetAccumulationAnimationSystem())
                .Add(new BetPulseScalesOnStartAnimationSystem())

                .Add(new SelectFirstPlayerSystem())
                .Add(new SelectFirstPlayerAnimationSystem())
                .Add(new HighlightFirstStepPlayerOutlineSystem())

                // ----------- user input event handlers --------------
                //select cell user inputs
                .Add(new UserSelectCellSystem())
                .Add(new ScaleSelectedCellAnimationSystem())
                .OneFrame<SelectCellAnimationRequest>()

                .OneFrame<NextPlayerRequest>() // on user or bot swap cells

                // user swap
                .Add(new UserSwapInputSystem())

                // bot 
                .Add(new BotWaitSwapSystem())
                .Add(new BotMakeSwapDecision())
                .OneFrame<BotMakeSwapDecisionRequest>()
                //.Add(new BotSwapSystem())
                // ----------- end user input event handlers --------------

                .Add(new RecordPlayerSwapsSystem())

                .OneFrame<AnimateSwapRequest>()
                .OneFrame<AnimateSwapBackRequest>()
                .Add(new SwapSystem())
                .OneFrame<SwapRequest>()

                // animate swap
                .Add(new AnimateSwapSystem())
                .Add(new AnimateSwapBackSystem())

                .Add(new DisableActivePlayerIndicatorSystem())

                // deselect cell
                .Add(new DeselectCellSystem())
                .Add(new UnscaleDeselectedCellSystem())
                .OneFrame<DeselectCellAnimationRequest>()

                // create chains
                .OneFrame<ChainEvent>()
                .Add(new CreateChainsSystem()) // on SwapRequest or field was unlocked

                // explode cells
                .OneFrame<AnimateExplosionRequest>()
                .Add(new ChargeCellsToExplosionSystem())
                .Add(new AnimateCellsExplosionSystem()) // mark as AnimateExplosion while animating
                .Add(new DestroyExplodedCellsViewSystem())
                .OneFrame<RewardRequest>()
                .Add(new DestroyExplodedCellsSystem())

                // rewards
                .OneFrame<HealthRewardRequest>()
                .OneFrame<DemageRewardRequest>()
                .Add(new ActivateSwapRewardsSystem())

                .Add(new HealthRewardSystem())
                .Add(new HealthRewardAnimationSystem())
                .Add(new IncreaseLifeLineAnimationSystem())
                .OneFrame<EndRoundRequest>()
                .Add(new DemageRewardSystem())
                .Add(new DemageRewardAnimationSystem())
                .Add(new DecreaseLifeLineAnimationSystem())

                // fill field
                .OneFrame<AnimateFallDownRequest>()
                .Add(new FallCellsToEmptySpacesSystem())
                .Add(new AnimateFallDownSystem())

                .OneFrame<CreateCellViewRequest>()
                .Add(new CreateRandomCellsToEmptySpacesSystem())
                .OneFrame<AnimateCreatedViewRequest>()
                .Add(new CreateCellsViewSystem())
                .Add(new AnimateCreatedViewSystem())

                .OneFrame<PlayerChangedEvent>()
                .Add(new ChangeActivePlayerSystem()) // processing NextPlayerRequest
                .Add(new HighlightCurrentPlayerOutlineSystem())

                // on round ended
                .Add(new EndRoundRewardPlayerSystem())
                .Add(new RoundResultPopupSystem())

                // inject service instances here (order doesn't important), for example:
                .Inject(_gameField)
                .Inject(_configuration)
                .Inject(_sceneData)
                .Inject(_playerState)
                .Inject(_playerData)
                .Inject(_objectPool)
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            LocalSaveLoad<PlayerData>.Save(_playerData);

            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}