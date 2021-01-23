using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI.Lobby {
    class ArenaLobbyView : MonoBehaviour {
        public int ID = 0;
        public Button StartButton = null;
        public Text CoinText = null;
        public Action<int> PlayHandler;

        private void Start() {
            StartButton.onClick.AddListener(OnPlayClicked);
        }

        private void OnPlayClicked() {
            PlayHandler?.Invoke(ID);
        }
    }
}
