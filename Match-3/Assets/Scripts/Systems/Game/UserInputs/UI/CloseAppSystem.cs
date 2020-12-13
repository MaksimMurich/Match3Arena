using Leopotam.Ecs;
using UnityEngine;

namespace Match3.Systems.Game.UserInputs.UI
{
    public sealed class CloseAppSystem : IEcsInitSystem
    {
        private readonly InGameSceneData _sceneData = null;

        public void Init()
        {
            _sceneData.SettingsView.CloseAppClickEvent += CloseAppClickEventHandler;
        }

        private void CloseAppClickEventHandler()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

            if (Application.platform != RuntimePlatform.WindowsEditor)
            {
                Application.Quit();
            }
        }
    }
}
