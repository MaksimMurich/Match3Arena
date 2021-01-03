using Leopotam.Ecs;
using UnityEngine;

namespace Match3
{
    internal class LobbyLoggingSystem : IEcsInitSystem
    {
        public void Init()
        {
            Debug.Log("Lobby has been started successfuly.");
        }
    }
}