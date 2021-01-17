using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI.InGame {
    public class CellRewardViewTable : MonoBehaviour {
        [SerializeField] private HorizontalLayoutGroup _horizontalLayout = null;
        [SerializeField] private CellRewardViewTableItem _cellRewardViewTableItemExample = null;

        public CellRewardViewTableItem CellRewardViewTableItemExample { get => _cellRewardViewTableItemExample; }

        private void Start() {
            CellRewardViewTableItemExample.gameObject.SetActive(false);
        }

        public void AddItem(CellRewardViewTableItem item, int health, int demage, Sprite sprite) {
            item.transform.SetParent(_horizontalLayout.transform);
            item.transform.localScale = Vector3.one;
            item.SetValues(health, demage, sprite);
        }
    }
}
