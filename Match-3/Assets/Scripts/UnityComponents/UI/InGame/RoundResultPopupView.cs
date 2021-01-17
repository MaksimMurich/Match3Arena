using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI.InGame {
    public class RoundResultPopupView : MonoBehaviour {
        [SerializeField] public Button Play = null;
        [SerializeField] public Button BackToLobby = null;

        [SerializeField] private GameObject _youWinText = null;
        [SerializeField] private GameObject _youLoseText = null;
        [SerializeField] private Text _ratingCount = null;
        [SerializeField] private Text _ratingCountShadow = null;
        [SerializeField] private Text _coinsCount = null;
        [SerializeField] private Text _coinsCountShadow = null;
        [SerializeField] private Text _stepsCount = null;
        [SerializeField] private Text _stepsCountShadow = null;
        [SerializeField] private Text _sumDemageRewardCount = null;
        [SerializeField] private Text _sumDemageRewardCountShadow = null;
        [SerializeField] private Text _sumHealthRewardCount = null;
        [SerializeField] private Text _sSumHealthRewardCountShadow = null;

        public void SetStepsCount(int stepsCount) {
            _stepsCount.text = $"Steps : {stepsCount}";
            _stepsCountShadow.text = $"Steps : {stepsCount}";
        }

        public void SetRating(int delta, int rating) {
            string sign = delta > 0 ? "+" : string.Empty;
            _ratingCount.text = $"Rating : {sign} {delta} ({rating})";
            _ratingCountShadow.text = $"Rating : {sign} {delta} ({rating})";
        }

        public void SetCoins(int delta, long coins) {
            string sign = delta > 0 ? "+" : string.Empty;
            _coinsCount.text = $"Coins : {sign} {delta} ({coins})";
            _coinsCountShadow.text = $"Coins : {sign} {delta} ({coins})";
        }

        public void SetSumDemage(int value) {
            _sumDemageRewardCount.text = $"Opponent demage : {value}";
            _sumDemageRewardCountShadow.text = $"Opponent demage : {value}";
        }

        public void SetSumHealthRestore(int value) {
            _sumHealthRewardCount.text = $"Health restored  : {value}";
            _sSumHealthRewardCountShadow.text = $"Health restored  : {value}";
        }

        public void UpdateHeader(bool userWin) {
            _youWinText.SetActive(userWin);
            _youLoseText.SetActive(!userWin);
        }
    }
}
