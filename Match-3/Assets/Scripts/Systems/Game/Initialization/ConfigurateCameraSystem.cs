using Leopotam.Ecs;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Initialization
{
    public sealed class ConfigurateCameraSystem : IEcsInitSystem
    {
        private readonly InGameSceneData _sceneData = null;
        private readonly InGameConfiguration _configuration = null;

        public void Init()
        {
            Application.targetFrameRate = _configuration.TargetFrameRate;

            float fillScreenPart = 1 / (1 + 2 * _configuration.MinFieldPadding);
            float cameraSize = _configuration.LevelHeight * (1 + _configuration.TopMenuPadding + _configuration.BottomPadding) / fillScreenPart / 2f;
            float cameraOffsetY = (_configuration.TopMenuPadding - _configuration.BottomPadding) * _configuration.LevelHeight / 2;
            float cameraViewWidth = cameraSize * Screen.width / (float)Screen.height;

            if (cameraViewWidth < _configuration.LevelWidth / 2f)
            {
                cameraSize *= (1 + 2 * _configuration.MinFieldPadding) * _configuration.LevelWidth / 2f / cameraViewWidth;
            }

            float cameraScale = cameraSize / _sceneData.Camera.orthographicSize;
            _sceneData.Background.localScale *= cameraScale;

            _sceneData.Camera.orthographic = true;
            _sceneData.Camera.orthographicSize = cameraSize;

            float cameraXPosition = (_configuration.LevelWidth - 1) / 2f;
            float cameraYPosition = cameraOffsetY + (_configuration.LevelHeight - 1f) / 2f;
            _sceneData.Camera.transform.position = new Vector3(cameraXPosition, cameraYPosition, -10);

            _sceneData.Background.position = new Vector3(cameraXPosition, cameraYPosition, 100);
        }
    }
}
