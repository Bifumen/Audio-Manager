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
        public bool stopOnDisable;

        public Sound(AudioClip audioClip,bool willLoop)
        {
            clip = audioClip;
            volume = 1;
            pitch = 1;
            loop = willLoop;
            stopOnDisable = false;
        }

        public Sound(AudioClip audioClip, bool willLoop, float audioVolume, float audioPitch, AudioMixerGroup mixerGroup, bool stopOnDis)
        {
            clip = audioClip;
            volume = audioVolume;
            pitch = audioPitch;
            loop = willLoop;
            mixer = mixerGroup;
            stopOnDisable = stopOnDis;
        }
    }
}
