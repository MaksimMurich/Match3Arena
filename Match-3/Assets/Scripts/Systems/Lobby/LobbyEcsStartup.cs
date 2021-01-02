using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Common;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Assets.Scripts.Systems.Common;
using Match3.Configurations;
using UnityEngine;

namespace Match3
{
    sealed class LobbyEcsStartup : MonoBehaviour
    {
        [SerializeField] private LobbyConfiguration _configuration = null;
        [SerializeField] private LobbyViews _sceneData = null;

        private EcsSystems _systems;

        void Start()
        {
            Global.Config.Lobby = _configuration;
            Global.Views.Lobby = _sceneData;

            Global.Data.Player = LocalSaveLoad<PlayerData>.Load();
            Global.Data.Player = Global.Data.Player != null ? Global.Data.Player : new PlayerData(Global.Config.InGame.UserStateConfiguration.Rating, Global.Config.InGame.UserStateConfiguration.CoinsCount);
            Global.Data.Lobby.World = new EcsWorld();
            _systems = new EcsSystems(Global.Data.Lobby.World);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(Global.Data.Lobby.World);
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
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            LocalSaveLoad<PlayerData>.Save(Global.Data.Player);

            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                Global.Data.Lobby.World.Destroy();
                Global.Data.Lobby.World = null;
            }
        }
    }
}