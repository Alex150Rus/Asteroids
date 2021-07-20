using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Sound: MonoBehaviour
    {
        [SerializeField] private List<DataSound> _dataSounds;
        private AudioSource _audioSource;
        
        [Serializable]
        private class DataSound
        {
            public string name;
            public AudioClip audioClip;
        }
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play(string nameClip)
        {
            var audioClip = GetAudioClip(nameClip);
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public void PlayByIndex(int index)
        {
            var audioClip = GetAudioClip(index);
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        private AudioClip GetAudioClip(string nameClip)
        {
            AudioClip clip = null;
        
            foreach (var sound in _dataSounds)
            {
                if (sound.name == nameClip)
                    clip = sound.audioClip;
            }

            return clip;
        }
        
        private AudioClip GetAudioClip(int index)
        {
            return _dataSounds[index].audioClip;
        }
    }
}