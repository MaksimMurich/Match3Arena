using Leopotam.Ecs;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Initialization
{
    public sealed class ConfigurateCameraSystem : IEcsInitSystem
    {
        private readonly InGameViews _sceneData = null;
        private readonly InGameConfiguration _configuration = null;

        public void Init()
        {
            Application.targetFrameRate =  Global.Config.InGame.TargetFrameRate;

            float fillScreenPart = 1 / (1 + 2 *  Global.Config.InGame.MinFieldPadding);
            float cameraSize =  Global.Config.InGame.LevelHeight * (1 +  Global.Config.InGame.TopMenuPadding +  Global.Config.InGame.BottomPadding) / fillScreenPart / 2f;
            float cameraOffsetY = ( Global.Config.InGame.TopMenuPadding -  Global.Config.InGame.BottomPadding) *  Global.Config.InGame.LevelHeight / 2;
            float cameraViewWidth = cameraSize * Screen.width / (float)Screen.height;

            if (cameraViewWidth <  Global.Config.InGame.LevelWidth / 2f)
            {
                cameraSize *= (1 + 2 *  Global.Config.InGame.MinFieldPadding) *  Global.Config.InGame.LevelWidth / 2f / cameraViewWidth;
            }

            float cameraScale = cameraSize / _sceneData.Camera.orthographicSize;
            _sceneData.Background.localScale *= cameraScale;

            _sceneData.Camera.orthographic = true;
            _sceneData.Camera.orthographicSize = cameraSize;

            float cameraXPosition = ( Global.Config.InGame.LevelWidth - 1) / 2f;
            float cameraYPosition = cameraOffsetY + ( Global.Config.InGame.LevelHeight - 1f) / 2f;
            _sceneData.Camera.transform.position = new Vector3(cameraXPosition, cameraYPosition, -10);

            _sceneData.Background.position = new Vector3(cameraXPosition, cameraYPosition, 100);
        }
    }
}
