using UnityEngine;

public class LobbySceneData : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;

    public Camera Camera { get => _camera; }
}
