using UnityEngine;

namespace Audio {

    [System.Serializable]
    public class Sound {

        [Header("Audio Info")]
        [Tooltip("Sound name")]
        public string soundName;

        [Tooltip("audio clip to play")]
        public AudioClip soundClip;

        [Tooltip("Volume for this audio clip")]
        [Range(0f, 1f)]
        public float soundVolume;

        [Tooltip("Pith for this audio clip")]
        [Range(-3f, 3f)]
        public float soundPith = 1f;

        [Tooltip("Can this sound loop")]
        public bool soundLoop;

        [HideInInspector]
        public AudioSource audioSource;
    }

}