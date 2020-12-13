﻿using Leopotam.Ecs;

namespace Match3.Systems.Game.UserInputs.UI
{
    public sealed class CancelSettingsSystem : IEcsInitSystem
    {
        private readonly InGameSceneData _sceneData = null;

        public void Init()
        {
            _sceneData.SettingsView.CancelSettingsClickEvent += CancelSettingsClickEventHandler;
        }

        private void CancelSettingsClickEventHandler()
        {
            _sceneData.SettingsView.gameObject.SetActive(false);
        }
    }
}
