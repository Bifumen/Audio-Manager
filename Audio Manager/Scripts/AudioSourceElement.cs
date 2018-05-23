using UnityEngine;
using System.Collections;

namespace AudioPoolingSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceElement : MonoBehaviour
    {
        public AudioSource audioSource;
        public Sound sound;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            // Check if the audio clip has stopped playing
            if (!audioSource.isPlaying)
            {
                // Disable this game object
                sound = null;
                this.gameObject.SetActive(false);
            }
        }
    }
}