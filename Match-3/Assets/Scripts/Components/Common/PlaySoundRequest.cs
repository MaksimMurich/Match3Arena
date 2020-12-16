using UnityEngine;

namespace Match3.Assets.Scripts.Components.Common
{
    public struct PlaySoundRequest
    {
        public AudioClip AudioClip;

        private static int _ID;

        public PlaySoundRequest(AudioClip audioClip)
        {
            AudioClip = audioClip;
        }
    }
}
