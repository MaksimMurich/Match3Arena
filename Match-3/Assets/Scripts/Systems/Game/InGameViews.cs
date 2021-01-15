using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using UnityEngine;

public class InGameViews : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private Transform _background = null;
    [SerializeField] private NavigationView _navigationView = null;
    [SerializeField] private BetView _betView = null;
    [SerializeField] private FirstPlayerSelectionView _firstPlayerSelectionView = null;
    [SerializeField] private PlayerInGameDataView _playerDataView = null;
    [SerializeField] private PlayerInGameDataView _botDataView = null;
    [SerializeField] private RoundResultPopupView _roundResultPopupView = null;
    [SerializeField] private Canvas _rewardsContainer = null;
    [SerializeField] private CellRewardViewTable _cellRewardViewTable = null;
    //[SerializeField] private Text _turnTimer = null;

    public Camera Camera { get => _camera; }
    public Transform Background { get => _background; }
    public NavigationView NavigationView { get => _navigationView; }
    public BetView BetView { get => _betView; }
    public FirstPlayerSelectionView FirstPlayerSelectionView { get => _firstPlayerSelectionView; }
    public PlayerInGameDataView PlayerDataView { get => _playerDataView; }
    public PlayerInGameDataView BotDataView { get => _botDataView; }
    public RoundResultPopupView RoundResultPopupView { get => _roundResultPopupView; }
    public Canvas RewardsContainer { get => _rewardsContainer; }
    public CellRewardViewTable CellRewardViewTable { get => _cellRewardViewTable; }
	//public TextAsset TurnTimer { get => _turnTimer; }
}
