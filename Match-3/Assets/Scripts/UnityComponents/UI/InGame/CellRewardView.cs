using Match3.Assets.Scripts.Services.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI.InGame
{
    [RequireComponent(typeof(RectTransform))]
    public class CellRewardView : MonoBehaviour, IClone<CellRewardView>
    {
        [SerializeField] private Text _text = null;
        [SerializeField] private Text _shadow = null;

        private CellRewardView _original;

        public CellRewardView GetOriginal()
        {
            return _original;
        }

        public void Reset()
        {
        }

        public void SetOriginal(CellRewardView value)
        {
            _original = value;
        }

        internal void SetValue(int value)
        {
            string text = value.ToString();
            _text.text = text;
            _shadow.text = text;
        }
    }
}
