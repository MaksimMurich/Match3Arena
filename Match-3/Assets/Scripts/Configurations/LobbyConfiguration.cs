using UnityEngine;

namespace Match3.Configurations
{
    [CreateAssetMenu]
    public class LobbyConfiguration : CommonConfiguration
    {
        [SerializeField] private ArenaConfig[] arenaConfig = null;

        public ArenaConfig[] ArenaConfig { get => arenaConfig; }
    }
}
