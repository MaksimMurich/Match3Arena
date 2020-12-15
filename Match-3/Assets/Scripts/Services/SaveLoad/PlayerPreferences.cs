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
        }

        public PlayerPreferences(string nick)
        {
            Nick = nick;
        }
    }
}
