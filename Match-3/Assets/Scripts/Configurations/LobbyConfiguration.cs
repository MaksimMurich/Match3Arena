using UnityEngine;

namespace Match3.Configurations {
    [CreateAssetMenu]
    public class LobbyConfiguration : ScriptableObject {
        [SerializeField] private UserStateConfiguration _userStateConfiguration = null;
        [SerializeField] private ArenaConfig defaultArenaConfig = new ArenaConfig() { ID = 10000000, Name = "default", Bet = 100 };
        [SerializeField] private ArenaConfig[] arenaConfigs = null;


        public UserStateConfiguration UserStateConfiguration { get => _userStateConfiguration; }
        public ArenaConfig DefaultArenaConfig { get => defaultArenaConfig; }
        public ArenaConfig[] ArenaConfigs { get => arenaConfigs; }
    }
}
