using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI {
    public class ui_script : MonoBehaviour {
        public Gradient gradient;

        public Image fill;

        public Image border;

        public Slider healthSlider;

        public TMP_Text healthText;

        public TMP_Text fpsText;

        public TMP_Text maxfps;

        public TMP_Text minfps;

        GameObject player;

        PlayerLogic playerLogic;

        // Start is called before the first frame update
        void Start() {
            StartCoroutine(getTarget());
            // healthSlider.GetComponentInChildren<Slider>();
            // healthText.GetComponentInChildren<TMP_Text>();

            StartCoroutine(InitMinMax());

            fill.color = gradient.Evaluate(1f);
            border.color = gradient.Evaluate(1f);

           

            InvokeRepeating("FpsUpdate", 0f, 0.3f);
        }

        // Update is called once per frame
        void Update() {
            HealthUpdate();
            FpsMaxMin();
        }

        void HealthUpdate(){
            int health = 0;

            if (playerLogic != null)
                health = playerLogic.currentHealth;

            healthSlider.value = health;
            healthText.text = health.ToString();
            fill.color = gradient.Evaluate(healthSlider.normalizedValue);
            border.color = gradient.Evaluate(healthSlider.normalizedValue);
        }

        void FpsUpdate(){
            int fps = (int)(1 /Time.unscaledDeltaTime);
            fpsText.SetText(fps.ToString());
        }

        void FpsMaxMin(){
            int fps = (int)(1 /Time.unscaledDeltaTime);
            if(fps<int.Parse(minfps.text))
                minfps.SetText(fps.ToString());
            
            if(fps>int.Parse(maxfps.text))
                maxfps.SetText(fps.ToString());
        }

        private IEnumerator InitMinMax(){
            yield return new WaitForSeconds(5f);

            int fps = (int)(1 /Time.unscaledDeltaTime);
            maxfps.SetText(fps.ToString());
            minfps.SetText(fps.ToString());
        }

        private IEnumerator getTarget() {
            yield return new WaitForSeconds(0.5f);

            player = PlayerManager.Instance.playerObject;
            playerLogic = player.GetComponent<PlayerLogic>();
        }

    }
}
