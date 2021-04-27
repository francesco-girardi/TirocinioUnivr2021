using UnityEngine;
using UnityEngine.Audio;

namespace Audio {

    public class SoundManager : MonoBehaviour {

        /// <summary>
        /// <br>Instance of SoundManager give you access to all public methods.</br>
        /// ( <b>There can only be one</b> )
        /// </summary>
        public static SoundManager Instance { get; private set; }

        [Tooltip("Background audio that has to be analyzed")]
        public AudioSource backgroundMusic;

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

        /// <summary>
        /// Find and stop a sound in game 
        /// </summary>
        /// <param name="soundName"></param>
        public void StopSound(string soundName) {
            Sound s = System.Array.Find(sounds, sound => sound.soundName == soundName);

            if (s == null) {
                Debug.LogError("File " + s.soundName + " not found!");
                return;
            }

            s.audioSource.Stop();
        }

        /// <summary>
        /// Find and play as background music a sound in game
        /// </summary>
        /// <param name="soundName"></param>
        public void ChangeBackground(string soundName) {

            backgroundMusic.Stop();

            Sound s = System.Array.Find(sounds, sound => sound.soundName == soundName);

            if (s == null) {
                Debug.LogError("File " + s.soundName + " not found!");
                return;
            }

            backgroundMusic = s.audioSource;

            if(SongController.Instance.bgThread != null)
                if(SongController.Instance.bgThread.IsAlive)
                    SongController.Instance.bgThread.Abort();

            SongController.Instance.analyze = true;
            SongController.Instance.StartPreprocess();
            backgroundMusic.Play();
        }

        private void Awake() {
            #region Singleton
            if (Instance == null)
                Instance = this;
            else {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            #endregion
            
            AudioMixerGroup[] audioMixerGroup = audioMixer.FindMatchingGroups("Master");

            foreach (Sound sound in sounds) {
                sound.audioSource = gameObject.AddComponent<AudioSource>();
                sound.audioSource.outputAudioMixerGroup = audioMixerGroup[GetMixerIndex(sound.mixerName)];
                sound.audioSource.clip = sound.soundClip;
                sound.audioSource.volume = sound.soundVolume;
                sound.audioSource.pitch = sound.soundPitch;
                sound.audioSource.loop = sound.soundLoop;
            }

            AudioSource audioSource = GetComponentInChildren<AudioSource>();
            audioSource.Play();
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

        private void Update() {
            if(Input.GetKey(KeyCode.L))
                ChangeBackground("BestSongEvah");
            if(Input.GetKey(KeyCode.K))
                ChangeBackground("background");
        }

    }

}
