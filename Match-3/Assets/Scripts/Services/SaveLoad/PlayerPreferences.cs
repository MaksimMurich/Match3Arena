using System;

namespace Match3.Assets.Scripts.Services.SaveLoad
{
    [Serializable]
    public class PlayerPreferences
    {
        public string Nick;
        public bool Vibration = true;
        public bool AudeoEffects = true;
        public float Volume = .5f;

        public PlayerPreferences()
        {
            int guestNumber = UnityEngine.Random.Range(0, 100000);
            Nick = "Guest" + guestNumber;
            LocalSaveLoad<PlayerPreferences>.Save(this);
        }
    }
}
