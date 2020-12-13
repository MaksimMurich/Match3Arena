using UnityEngine;

namespace Match3.Configurations
{
    [CreateAssetMenu]
    public class LobbyConfiguration : ScriptableObject
    {
        [SerializeField] private UserStateConfiguration _userStateConfiguration = null;
        [SerializeField] private ArenaConfig[] arenaConfig = null;

        public UserStateConfiguration UserStateConfiguration { get => _userStateConfiguration; }
        public ArenaConfig[] ArenaConfig { get => arenaConfig; }
    }
}
