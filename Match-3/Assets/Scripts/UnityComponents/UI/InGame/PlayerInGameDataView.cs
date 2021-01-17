using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI.InGame {
    public class PlayerInGameDataView : MonoBehaviour {
        [HideInInspector] public Text Nick;
        [HideInInspector] public Text Rating;

        public RectTransform LifeLine;
        public RectTransform IncreaseLifeLine;
        public RectTransform DecreaseLifeLine;

        [SerializeField] private GameObject _outline = null;

        public void ActivateOutline(bool value) {
            _outline.SetActive(value);
        }
    }
}
