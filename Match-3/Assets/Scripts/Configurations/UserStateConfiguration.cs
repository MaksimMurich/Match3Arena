using UnityEngine;

namespace Match3.Configurations {
    [CreateAssetMenu]
    public class UserStateConfiguration : ScriptableObject {
        public string UserName;
        public long CoinsCount;
        public int Rating;
        public int LastArenaID;
        public bool Sound;
        public int WinCount;
        public int GameSessionsCount;
    }
}
