using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Components.Game.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3.Assets.Scripts.Systems.Game.UI
{
    public sealed class RoundResultPopupSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly PlayerData _playerData = null;
        private readonly PlayerState _playerState = null;
        private readonly InGameViews _sceneData = null;
        private readonly EcsFilter<EndRoundRequest> _filter = null;

        public void Init()
        {
            _sceneData.RoundResultPopupView.Play.onClick.AddListener(PlayClickHandler);
            _sceneData.RoundResultPopupView.BackToLobby.onClick.AddListener(BackToLobbyClickHandler);
        }

        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0)
            {
                return;
            }

            _sceneData.RoundResultPopupView.gameObject.SetActive(true);
            _sceneData.RoundResultPopupView.SetStepsCount(_playerState.StepsCount);

            bool userWin = _playerState.CurrentLife > 0;
            int deltaRating = userWin ? _playerState.DeltaRatingReward : -1 * _playerState.DeltaRatingUnreward;
            int deltaCoins = userWin ? _playerState.CurrentBet : -1 * _playerState.CurrentBet;
            _sceneData.RoundResultPopupView.SetRating(deltaRating, Global.Data.Player.Rating);
            _sceneData.RoundResultPopupView.SetCoins(deltaCoins, Global.Data.Player.Coins);
            _sceneData.RoundResultPopupView.SetSumDemage(_playerState.SumOpponentDemage);
            _sceneData.RoundResultPopupView.SetSumHealthRestore(_playerState.SumHealseRestored);
            _sceneData.RoundResultPopupView.UpdateHeader(userWin);
        }

        private void PlayClickHandler()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void BackToLobbyClickHandler()
        {
            SceneManager.LoadScene("Lobby");
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
