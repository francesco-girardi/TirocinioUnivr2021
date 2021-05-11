using UnityEngine;

namespace Audio {

    public class SoundAnalyzer : MonoBehaviour {

        public enum Bands { Band8, Band64 };

        [Header("Info")]
        [Tooltip("Number of bands in wich we divide the signal")]
        public Bands bands = new Bands();

        public enum channel { Stereo, Left, Right };

        [Tooltip("Describe the channel you want to analyze")]
        public channel Channel = new channel();

        private static float[] _audioBand;

        /// <summary>
        /// The amplitude of our sound diveded into bands;<br></br>
        /// <br>Band 0: (86) Hz</br><br>Band 1: (87 - 258) Hz</br>
        /// <br>Band 2: (259 - 602) Hz</br><br>Band 3: (603 - 1290) Hz</br>
        /// <br>Band 4: (1291 - 2666) Hz</br><br>Band 5: (2667 - 5418) Hz</br>
        /// <br>Band 6: (5419 - 10922) Hz</br><br>Band 7: (10923 - 21930) Hz</br>
        /// </summary>
        public static float[] AudioBand {
            get {
                return _audioBand;
            }
            set {
                _audioBand = value;
            }
        }

        private static float[] _audioBandBuffer;

        public static float[] AudioBandBuffer {
            get {
                return _audioBandBuffer;
            }
            set {
                _audioBandBuffer = value;
            }
        }

        private static float _amplitude;

        /// <summary>
        /// The total amplitude of our sound
        /// </summary>
        public static float Amplitude {
            get {
                return _amplitude;
            }
            set {
                _amplitude = value;
            }
        }

        private static float _amplitudeBuffer;

        public static float AmplitudeBuffer {
            get {
                return _amplitudeBuffer;
            }
            set {
                _amplitudeBuffer = value;
            }
        }

        public float AudioProfile;

        private int bandsNum;

        private float amplitudeHighest;

        private float[] samplesLeft = new float[512];
        private float[] samplesRight = new float[512];
        private float[] frequencyBand;
        private float[] bandBuffer;
        private float[] bufferDecrease;
        private float[] frequencyBandHighest;

        private AudioSource audioSource;

        private void Awake() {
            switch (bands) {
                case Bands.Band64:
                    bandsNum = 64;
                    break;
                default:
                    bandsNum = 8;
                    break;
            }

            _audioBand = new float[bandsNum];
            _audioBandBuffer = new float[bandsNum];

            frequencyBand = new float[bandsNum];
            bandBuffer = new float[bandsNum];
            bufferDecrease = new float[bandsNum];
            frequencyBandHighest = new float[bandsNum];
        }

        private void Start() {
            audioSource = GetComponent<AudioSource>();
            AudioProfileCalculation(AudioProfile);
        }

        private void Update() {
            GetSpectrumData();

            if (bandsNum == 8) {
                MakeFrequencyBands();
                BandBufferCalculation();
                CreateAudioBands();
                GetAmplitude();
            } else {
                MakeFrequencyBands64();
                BandBufferCalculation();
                CreateAudioBands();
                GetAmplitude();
            }
        }

        private void GetSpectrumData() {
            audioSource.GetSpectrumData(samplesLeft, 0, FFTWindow.Hamming);
            audioSource.GetSpectrumData(samplesRight, 1, FFTWindow.Hamming);
        }

        private void MakeFrequencyBands() {

            int count = 0;

            for (int i = 0; i < frequencyBand.Length; i++) {
                float average = 0;
                int sampleCount = (int)Mathf.Pow(2, i) * 2;

                if (i == 7)
                    sampleCount += 2;

                for (int j = 0; j < sampleCount; j++) {
                    switch (Channel) {
                        case channel.Left:
                            average += samplesLeft[count] * (count + 1);
                            break;
                        case channel.Right:
                            average += samplesRight[count] * (count + 1);
                            break;
                        default:
                            average += (samplesLeft[count] + samplesRight[count]) * (count + 1);
                            break;
                    }

                    count++;
                }

                average /= count;

                frequencyBand[i] = average * 10;
            }
        }

        private void MakeFrequencyBands64() {

            int count = 0;
            int sampleCount = 1;
            int power = 0;

            for (int i = 0; i < frequencyBand.Length; i++) {
                float average = 0;

                if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56) {
                    power++;
                    sampleCount = (int)Mathf.Pow(2, power);

                    if (power == 3)
                        power -= 2;
                }

                for (int j = 0; j < sampleCount; j++) {
                    switch (Channel) {
                        case channel.Left:
                            average += samplesLeft[count] * (count + 1);
                            break;
                        case channel.Right:
                            average += samplesRight[count] * (count + 1);
                            break;
                        default:
                            average += (samplesLeft[count] + samplesRight[count]) * (count + 1);
                            break;
                    }

                    count++;
                }

                average /= count;

                frequencyBand[i] = average * 80;
            }
        }

        private void BandBufferCalculation() {
            for (int i = 0; i < frequencyBand.Length; i++) {
                if (frequencyBand[i] > bandBuffer[i]) {
                    bandBuffer[i] = frequencyBand[i];
                    bufferDecrease[i] = 0.005f;
                }

                if (frequencyBand[i] < bandBuffer[i]) {
                    bandBuffer[i] -= bufferDecrease[i];
                    bufferDecrease[i] *= 1.2f;
                }
            }
        }

        private void CreateAudioBands() {
            for (int i = 0; i < frequencyBand.Length; i++) {
                if (frequencyBand[i] > frequencyBandHighest[i])
                    frequencyBandHighest[i] = frequencyBand[i];

                _audioBand[i] = (frequencyBand[i] / frequencyBandHighest[i]);
                _audioBandBuffer[i] = (bandBuffer[i] / frequencyBandHighest[i]);
            }
        }

        private void GetAmplitude() {
            float currentAmplitude = 0;
            float currentAmplitudeBuffer = 0;

            for (int i = 0; i < _audioBand.Length; i++) {
                currentAmplitude += _audioBand[i];
                currentAmplitudeBuffer += _audioBandBuffer[i];
            }

            if (currentAmplitude > amplitudeHighest)
                amplitudeHighest = currentAmplitude;

            _amplitude = currentAmplitude / amplitudeHighest;
            _amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
        }

        private void AudioProfileCalculation(float audioProfile) {
            for (int i = 0; i < frequencyBandHighest.Length; i++) {
                frequencyBandHighest[i] = audioProfile;
            }
        }

    }

}