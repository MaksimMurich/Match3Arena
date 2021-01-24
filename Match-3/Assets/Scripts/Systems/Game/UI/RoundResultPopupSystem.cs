using Leopotam.Ecs;
using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using Match3.Components.Game.Events;
using UnityEngine.SceneManagement;

namespace Match3.Assets.Scripts.Systems.Game.UI {
    public sealed class RoundResultPopupSystem : IEcsInitSystem, IEcsRunSystem {
        private readonly EcsFilter<EndRoundRequest> _filter = null;

        private RoundResultPopupView _view;

        public void Init() {
            _view = Global.Views.InGame.RoundResultPopupView;
            _view.Play.onClick.AddListener(PlayClickHandler);
            _view.BackToLobby.onClick.AddListener(BackToLobbyClickHandler);
        }

        public void Run() {
            if (_filter.GetEntitiesCount() == 0) {
                return;
            }

            PlayerState state = Global.Data.Common.PlayerState;

            _view.gameObject.SetActive(true);
            _view.SetStepsCount(state.StepsCount);

            bool userWin = state.CurrentLife > 0;
            int deltaRating = userWin ? state.DeltaRatingReward : -1 * state.DeltaRatingUnreward;
            int deltaCoins = userWin ? state.CurrentBet : -1 * state.CurrentBet;
            _view.SetRating(deltaRating, Global.Data.Player.Rating);
            _view.SetCoins(deltaCoins, Global.Data.Player.Coins);
            _view.SetSumDemage(state.SumOpponentDemage);
            _view.SetSumHealthRestore(state.SumHealseRestored);
            _view.UpdateHeader(userWin);
        }

        private void PlayClickHandler() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void BackToLobbyClickHandler() {
            SceneManager.LoadScene("Lobby");
        }

        //TODO use this function when need close app
        //        private void CloseAppClickEventHandler()
        //        {
        //#if UNITY_EDITOR
        //            UnityEditor.EditorApplication.isPlaying = false;
        //#endif

        //            if (Application.platform != RuntimePlatform.WindowsEditor)
        //            {
        //                Application.Quit();
        //            }
        //        }
    }
}
