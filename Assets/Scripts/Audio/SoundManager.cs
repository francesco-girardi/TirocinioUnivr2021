using UnityEngine;

namespace Audio {

    public class SoundManager : MonoBehaviour {

        /// <summary>
        /// <br>Instance of SoundManager give you access to all public methods.</br>
        /// ( <b>There can only be one</b> )
        /// </summary>
        public static SoundManager Instance { get; private set; }

        /// <summary>
        /// All game sounds
        /// </summary>
        [Header("Audio Info")]
        [Tooltip("Array of game sounds")]
        public Sound[] sounds;

        /// <summary>
        /// Find and play a sound in game 
        /// </summary>
        /// <param name="soundName"></param>
        public void PlaySound(string soundName) {
            Sound s = System.Array.Find(sounds, sound => sound.soundName == soundName);

            if (s == null) {
                Debug.LogError("File " + s.soundName + " not found!");
                return;
            }

            s.audioSource.Play();
        }

        private void Awake() {
            if (Instance == null)
                Instance = this;
            else {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            foreach (Sound sound in sounds) {
                sound.audioSource = gameObject.AddComponent<AudioSource>();
                sound.audioSource.clip = sound.soundClip;
                sound.audioSource.volume = sound.soundVolume;
                sound.audioSource.pitch = sound.soundPith;
                sound.audioSource.loop = sound.soundLoop;
            }
        }

        private void Start() {
            PlaySound("background");
        }

    }

}
