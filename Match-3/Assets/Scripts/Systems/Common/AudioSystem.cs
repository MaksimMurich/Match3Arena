using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Common;
using Match3.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Common
{
    public sealed class AudioSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameObject _audioContainer = null;
        private AudioSource _audioSource = null;

        private readonly EcsFilter<PlaySoundRequest> _filter = null;

        public void Init()
        {
            _audioContainer = new GameObject
            {
                name = nameof(AudioSystem)
            };

            _audioContainer.AddComponent<AudioListener>();
            _audioSource = _audioContainer.AddComponent<AudioSource>();
        }

        public void Run()
        {
            foreach (int index in _filter)
            {
                PlaySoundRequest value = _filter.Get1(index);
                AudioSource.PlayClipAtPoint(value.AudioClip, Vector3.zero);
            }
        }
    }
}
