using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class Options : MonoBehaviour
    {
        public AudioMixer mixer;
        private Resolution[] resolutions;
        public TMP_Dropdown resolutionDD;

        int resIndex;

        int quality;

        bool fullScreen;

        private void Start()
        {
            resolutions = Screen.resolutions;
            resolutionDD.ClearOptions();

            List<string> resOptions = new List<string>();

            int currentRes = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string opt = resolutions[i].width + " x " + resolutions[i].height;
                resOptions.Add(opt);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                    currentRes = i;
            }

            resolutionDD.AddOptions(resOptions);
            resolutionDD.value = currentRes;
            resolutionDD.RefreshShownValue();
        }

        public void SetVolumeMaster(float masterSlider)
        {
            mixer.SetFloat("MasterVol", Mathf.Log10(masterSlider) * 80 + 20);
        }

        public void SetVolumeEffects(float effectsSlider)
        {
            mixer.SetFloat("EffectsVol", Mathf.Log10(effectsSlider) * 80 + 20);
        }

        public void SetVolumeMusic(float musicSlider)
        {
            mixer.SetFloat("MusicVol", Mathf.Log10(musicSlider) * 80 + 20);
        }

        public void SetQuality(int quality)
        {
            this.quality = quality;
        }

        public void SetResolution(int resIndex)
        {
            this.resIndex = resIndex;
        }

        public void FullScreen(bool toggle)
        {
            this.fullScreen = toggle;
        }

        public void Apply()
        {
            Screen.fullScreen = this.fullScreen;

            QualitySettings.SetQualityLevel(this.quality);

            Resolution resolution = resolutions[this.resIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

    }
}
