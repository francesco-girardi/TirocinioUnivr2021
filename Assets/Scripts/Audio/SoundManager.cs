using UnityEngine;
using UnityEngine.Audio;

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

        [Tooltip("Game audio mixer")]
        public AudioMixer audioMixer;
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

            AudioMixerGroup[] audioMixerGroup = audioMixer.FindMatchingGroups("Master");

            foreach (Sound sound in sounds) {
                sound.audioSource = gameObject.AddComponent<AudioSource>();
                sound.audioSource.outputAudioMixerGroup = audioMixerGroup[GetMixerIndex(sound.mixerName)];
                sound.audioSource.clip = sound.soundClip;
                sound.audioSource.volume = sound.soundVolume;
                sound.audioSource.pitch = sound.soundPith;
                sound.audioSource.loop = sound.soundLoop;
            }
        }

        private int GetMixerIndex(Sound.MixerName mixerName) {
            int index = 0;

            switch (mixerName) {
                case Sound.MixerName.Music:
                    index = 1;
                    break;
                case Sound.MixerName.Effects:
                    index = 2;
                    break;
                default:
                    break;
            }

            return index;
        }

    }

}
