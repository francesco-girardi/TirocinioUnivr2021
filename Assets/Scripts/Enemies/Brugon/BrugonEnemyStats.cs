using Audio;
using Events;
using Stat;
using UnityEngine;

namespace Enemy.Brugon {

    public class BrugonEnemyStats : CharacterStats {

        [Tooltip("Max damage that brucon can do")]
        public int maxDamage;

        [Header("Audio Info")]
        [Tooltip("First index of frequency to use")]
        [Range(0, 63)]
        public int frequencyIndexA;

        [Tooltip("Second index of frequency to use")]
        [Range(0, 63)]
        public int frequencyIndexB;

        private int startDamage;

        public override void Die() {
            base.Die();

            EnemyDeathInfo enemyDeathInfo = new EnemyDeathInfo();
            enemyDeathInfo.EventDescription = "Brugon enemy death event";
            enemyDeathInfo.enemy = gameObject;
            enemyDeathInfo.killer = PlayerManager.Instance.playerObject;

            EventSystem.Current.FireEvent(enemyDeathInfo);

            Destroy(gameObject);
        }

        protected override void Start() {
            startDamage = damage.GetValue();
        }

        protected override void Update() {
            float newDamage;

            if (damage.GetValue() != 0) {
                newDamage = (SoundAnalyzer.AudioBand[frequencyIndexA] + SoundAnalyzer.AudioBand[frequencyIndexB])
                    * damage.GetValue();

                if (newDamage > maxDamage)
                    newDamage = maxDamage;
            } else
                newDamage = startDamage;

            damage.SetBaseValue((int)Mathf.Round(newDamage));
        }

    }

}
