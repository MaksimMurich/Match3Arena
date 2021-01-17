using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI.InGame {
    public class BetView : MonoBehaviour {
        [SerializeField] private Text _bet = null;

        public Transform Bet { get => _bet.transform; }

        public void Set(long value) {
            _bet.text = value.ToString();
        }
    }
}
