using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI.InGame {
    public class FirstPlayerSelectionView : MonoBehaviour {
        [SerializeField] private Transform _playerAvatarItemExample = null;
        [SerializeField] private Transform _botAvatarItemExample = null;
        [SerializeField] private RectTransform _itemsContainer = null;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup = null;

        public Transform PlayerAvatarItemExample { get => _playerAvatarItemExample; }
        public Transform BotAvatarItemExample { get => _botAvatarItemExample; }
        public RectTransform ItemsContainer { get => _itemsContainer; }
        public VerticalLayoutGroup VerticalLayoutGroup { get => _verticalLayoutGroup; }

        private void Start() {
            _playerAvatarItemExample.gameObject.SetActive(false);
            _botAvatarItemExample.gameObject.SetActive(false);
        }
    }
}
