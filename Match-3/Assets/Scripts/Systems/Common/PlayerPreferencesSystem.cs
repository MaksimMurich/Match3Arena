using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Common;
using Match3.Assets.Scripts.Services.SaveLoad;

namespace Match3.Assets.Scripts.Systems.Common
{
    public sealed class PlayerPreferencesSystem : IEcsInitSystem, IEcsRunSystem
    {
        private PlayerPreferences _playerPreferences;

        private readonly EcsFilter<SetAudioEffectsActiveRequest> _enableSoundFilter = null;
        private readonly EcsFilter<SetAudioEffectsVolumeRequest> _audioVolumeFilter = null;

        public void Init()
        {
            _playerPreferences = LocalSaveLoad<PlayerPreferences>.Load();

            if (_playerPreferences == null)
            {
                int guestNumber = UnityEngine.Random.Range(0, 100000);
                _playerPreferences = new PlayerPreferences("Guest" + guestNumber);
                LocalSaveLoad<PlayerPreferences>.Save(_playerPreferences);
            }
        }

        public void Run()
        {
            foreach (int index in _enableSoundFilter)
            {
                SetAudioEffectsActiveRequest value = _enableSoundFilter.Get1(index);
                _playerPreferences.AudeoEffects = value.Value;
                LocalSaveLoad<PlayerPreferences>.Save(_playerPreferences);
            }

            foreach (int index in _audioVolumeFilter)
            {
                SetAudioEffectsVolumeRequest value = _audioVolumeFilter.Get1(index);
                _playerPreferences.Volume = value.Value;
                LocalSaveLoad<PlayerPreferences>.Save(_playerPreferences);
            }
        }
    }
}
