using Match3.Assets.Scripts.Services.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI.InGame
{
    public class CellRewardViewTableItem : MonoBehaviour, IClone<CellRewardViewTableItem>
    {
        [SerializeField] private Image _cell = null;
        [SerializeField] private Text _demage = null;
        [SerializeField] private Text _demageShadow = null;
        [SerializeField] private Text _heath = null;
        [SerializeField] private Text _heathShadow = null;

        private CellRewardViewTableItem _original;

        public CellRewardViewTableItem GetOriginal()
        {
            return _original;
        }

        public void Reset()
        {
            gameObject.SetActive(true);
        }

        public void SetOriginal(CellRewardViewTableItem value)
        {
            _original = value;
        }

        internal void SetValues(int health, int demage, Sprite sprite)
        {
            _cell.sprite = sprite;

            SetTextValue(health, _heath, _heathShadow);
            SetTextValue(demage, _demage, _demageShadow);
        }

        private void SetTextValue(int value, Text text, Text shadow)
        {
            text.gameObject.SetActive(value > 0);
            shadow.gameObject.SetActive(value > 0);
            text.text = value.ToString();
            shadow.text = value.ToString();
        }
    }
}
