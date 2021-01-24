using Match3;
using Match3.Assets.Scripts.UnityComponents.UI.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class LobbyViews : MonoBehaviour {
    [SerializeField] private Camera _camera = null;
    [SerializeField] private List<ArenaLobbyView> _arenas = null;
    [SerializeField] private LobbyTopPannelView _topPannel = null;

    public Camera Camera { get => _camera; }
    public LobbyTopPannelView TopPannel { get => _topPannel; }
    internal List<ArenaLobbyView> Arenas { get => _arenas; }
}
