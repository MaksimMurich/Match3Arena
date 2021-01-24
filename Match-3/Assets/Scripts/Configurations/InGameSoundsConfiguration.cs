using UnityEngine;

namespace Match3.Configurations {
    [CreateAssetMenu]
    public class InGameSoundsConfiguration : ScriptableObject {
        [SerializeField] private AudioClip _swap = null;
        [SerializeField] private AudioClip _swapBack = null;
        [SerializeField] private AudioClip _cellsExplosion = null;
        [SerializeField] private AudioClip _dropDownCells = null;
        [SerializeField] private AudioClip _playerTurn = null;

        public AudioClip Swap { get => _swap; }
        public AudioClip SwapBack { get => _swapBack; }
        public AudioClip CellsExplosion { get => _cellsExplosion; }
        public AudioClip DropDownCells { get => _dropDownCells; }
        public AudioClip PlayerTurn { get => _playerTurn; }
    }
}
