using Leopotam.Ecs;
using Match3.Components.Game.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3.Assets.Scripts.Systems.Game.UI
{
    public sealed class RoundResultPopupSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<EndRoundRequest> _filter = null;

        public void Init()
        {
            Global.Views.InGame.RoundResultPopupView.Play.onClick.AddListener(PlayClickHandler);
            Global.Views.InGame.RoundResultPopupView.BackToLobby.onClick.AddListener(BackToLobbyClickHandler);
        }

        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0)
            {
                return;
            }

            Global.Views.InGame.RoundResultPopupView.gameObject.SetActive(true);
            Global.Views.InGame.RoundResultPopupView.SetStepsCount(Global.Data.InGame.PlayerState.StepsCount);

            bool userWin = Global.Data.InGame.PlayerState.CurrentLife > 0;
            int deltaRating = userWin ? Global.Data.InGame.PlayerState.DeltaRatingReward : -1 * Global.Data.InGame.PlayerState.DeltaRatingUnreward;
            int deltaCoins = userWin ? Global.Data.InGame.PlayerState.CurrentBet : -1 * Global.Data.InGame.PlayerState.CurrentBet;
            Global.Views.InGame.RoundResultPopupView.SetRating(deltaRating, Global.Data.Player.Rating);
            Global.Views.InGame.RoundResultPopupView.SetCoins(deltaCoins, Global.Data.Player.Coins);
            Global.Views.InGame.RoundResultPopupView.SetSumDemage(Global.Data.InGame.PlayerState.SumOpponentDemage);
            Global.Views.InGame.RoundResultPopupView.SetSumHealthRestore(Global.Data.InGame.PlayerState.SumHealseRestored);
            Global.Views.InGame.RoundResultPopupView.UpdateHeader(userWin);
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
