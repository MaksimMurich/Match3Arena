﻿using Leopotam.Ecs;
using UnityEngine.SceneManagement;

namespace Match3.Systems.Game.UserInputs.UI
{
    public sealed class RestartSystem : IEcsInitSystem
    {
        private readonly InGameSceneData _sceneData = null;

        public void Init()
        {
            _sceneData.SettingsView.StartGameClickEvent += StartGameClickEventHandler;
        }

        private void StartGameClickEventHandler()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
