﻿using Leopotam.Ecs;
using Match3.Configurations;

namespace Match3.Systems.Game.UserInputs.UI
{
    public sealed class ChangeSettingsSystem : IEcsInitSystem
    {
        private readonly InGameSceneData _sceneData = null;
        private readonly RoundConfiguration _configuration = null;

        public void Init()
        {
            _sceneData.SettingsView.SetSettings(_configuration.LevelWidth, _configuration.LevelHeight);

            _sceneData.SettingsView.ChangeFieldHeightEvent += ChangeFieldHeightEventHandler;
            _sceneData.SettingsView.ChangeFieldWidthEvent += ChangeFieldHeightEventWidth;
        }

        private void ChangeFieldHeightEventWidth(int value)
        {
            _configuration.LevelWidth = value;
        }

        private void ChangeFieldHeightEventHandler(int value)
        {
            _configuration.LevelHeight = value;
        }
    }
}