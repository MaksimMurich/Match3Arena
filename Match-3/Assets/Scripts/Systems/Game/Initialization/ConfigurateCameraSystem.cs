using Leopotam.Ecs;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Initialization {
    public sealed class ConfigurateCameraSystem : IEcsInitSystem {
        public void Init() {
            InGameConfiguration config = Global.Config.InGame;
            InGameViews views = Global.Views.InGame;

            Application.targetFrameRate = config.TargetFrameRate;

            float fillScreenPart = 1 / (1 + 2 * config.MinFieldPadding);
            float cameraSize = config.LevelHeight * (1 + config.TopMenuPadding + config.BottomPadding) / fillScreenPart / 2f;
            float cameraOffsetY = (config.TopMenuPadding - config.BottomPadding) * config.LevelHeight / 2;
            float cameraViewWidth = cameraSize * Screen.width / (float)Screen.height;

            if (cameraViewWidth < config.LevelWidth / 2f) {
                cameraSize *= (1 + 2 * config.MinFieldPadding) * config.LevelWidth / 2f / cameraViewWidth;
            }

            float cameraScale = cameraSize / views.Camera.orthographicSize;
            views.Background.localScale *= cameraScale;

            views.Camera.orthographic = true;
            views.Camera.orthographicSize = cameraSize;

            float cameraXPosition = (config.LevelWidth - 1) / 2f;
            float cameraYPosition = cameraOffsetY + (config.LevelHeight - 1f) / 2f;
            views.Camera.transform.position = new Vector3(cameraXPosition, cameraYPosition, -10);

            views.Background.position = new Vector3(cameraXPosition, cameraYPosition, 100);
        }
    }
}
