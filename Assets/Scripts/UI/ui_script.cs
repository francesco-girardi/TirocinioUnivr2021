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

        GameObject player;

        PlayerLogic playerLogic;

        // Start is called before the first frame update
        void Start() {
            StartCoroutine(getTarget());
            // healthSlider.GetComponentInChildren<Slider>();
            // healthText.GetComponentInChildren<TMP_Text>();

            fill.color = gradient.Evaluate(1f);
            border.color = gradient.Evaluate(1f);
        }

        // Update is called once per frame
        void Update() {
            int health = 0;

            if (playerLogic != null)
                health = playerLogic.currentHealth;

            healthSlider.value = health;
            healthText.text = health.ToString();
            fill.color = gradient.Evaluate(healthSlider.normalizedValue);
            border.color = gradient.Evaluate(healthSlider.normalizedValue);
        }

        private IEnumerator getTarget() {
            yield return new WaitForSeconds(0.5f);

            player = PlayerManager.Instance.playerObject;
            playerLogic = player.GetComponent<PlayerLogic>();
        }

    }
}
