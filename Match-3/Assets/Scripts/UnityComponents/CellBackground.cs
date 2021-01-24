using Match3.Assets.Scripts.Services.Pool;
using UnityEngine;

namespace Match3.UnityComponents {
    public class CellBackground : MonoBehaviour, IClone<CellBackground> {
        private CellBackground Original;

        CellBackground IClone<CellBackground>.GetOriginal() {
            return Original;
        }

        public void SetOriginal(CellBackground value) {
            Original = value;
        }

        public void Reset() {
        }
    }
}