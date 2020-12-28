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
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    sealed class LobbyEcsStartup : MonoBehaviour
    {
        [SerializeField] private LobbyConfiguration _configuration = null;
        [SerializeField] private LobbySceneData _sceneData = null;

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

            //_playerState = new PlayerState(_configuration.PlayersMaxLife, 100);

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _objectPool = new ObjectPool();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            _systems

                // log system just for test
                .Add(new LobbyLoggingSystem())

                // common systems
                .Add(new PlayerPreferencesSystem())

                // initialization
                .Add(new AudioSystem())
                .OneFrame<PlaySoundRequest>()


                

                // inject service instances here (order doesn't important), for example:
                .Inject(_configuration)
                .Inject(_sceneData)
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