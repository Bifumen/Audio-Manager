using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioPoolingSystem
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        [Space]
        public AudioClip clip;

        [Range(0, 1)]
        public float volume;

        [Range(0, 2)]
        public float pitch;

        public bool loop;

        [Space]
        public AudioMixerGroup mixer;
    }
}
