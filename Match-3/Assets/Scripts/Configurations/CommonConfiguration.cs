using UnityEngine;

namespace Match3.Configurations {
    [CreateAssetMenu]
    public class CommonConfiguration : ScriptableObject {
        [SerializeField] private UserStateConfiguration _userStateConfiguration = null;

        public UserStateConfiguration UserStateConfiguration { get => _userStateConfiguration; }
    }
}
