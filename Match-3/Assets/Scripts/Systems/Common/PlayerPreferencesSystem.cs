using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Common;
using Match3.Assets.Scripts.Services.SaveLoad;

namespace Match3.Assets.Scripts.Systems.Common {
    public sealed class PlayerPreferencesSystem : IEcsRunSystem {
        private readonly EcsFilter<SetAudioEffectsActiveRequest> _enableSoundFilter = null;
        private readonly EcsFilter<SetAudioEffectsVolumeRequest> _audioVolumeFilter = null;

        public void Run() {
            foreach (int index in _enableSoundFilter) {
                SetAudioEffectsActiveRequest value = _enableSoundFilter.Get1(index);
                Global.Preferences.AudeoEffects = value.Value;
                LocalSaveLoad<PlayerPreferences>.Save(Global.Preferences);
            }

            foreach (int index in _audioVolumeFilter) {
                SetAudioEffectsVolumeRequest value = _audioVolumeFilter.Get1(index);
                Global.Preferences.Volume = value.Value;
                LocalSaveLoad<PlayerPreferences>.Save(Global.Preferences);
            }
        }
    }
}
