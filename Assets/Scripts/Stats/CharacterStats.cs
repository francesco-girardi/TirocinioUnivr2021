using UnityEngine;

namespace Stat {

    public class CharacterStats : MonoBehaviour {

        [Header("Character Stats")]
        public int maxHealth = 100;
        public int currentHealth { get; private set; }

        public Stat armor;

        [Header("Character Combat Stats")]
        public float attackSpeed = 1f;
        public float attackCooldown = 0f;
        public float attackDelay = 0.6f;

        public Stat damage;

        /// <summary>
        /// Set character max health
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxHealth(int value) {
            maxHealth = value;
        }

        /// <summary>
        /// Set character current health
        /// </summary>
        /// <param name="value"></param>
        public void SetCurrentHealth(int value) {
            currentHealth = value;
        }

        /// <summary>
        /// Decrease character halth base by taken damage
        /// </summary>
        /// <param name="value"></param>
        public void TakeDamage(int value) {
            value -= armor.GetValue();
            value = Mathf.Clamp(value, 0, int.MaxValue);

            currentHealth -= value;

            if (currentHealth <= 0)
                Die();
        }

        public void RegenHealth(int value){
            
            currentHealth += value;
            if(currentHealth > maxHealth)
                currentHealth = maxHealth;
            
            if (currentHealth <= 0)
                Die();

        }

        /// <summary>
        /// Call on player die when health <= 0
        /// </summary>
        public virtual void Die() {
            Debug.Log(transform.name + " died.");
        }

        private void Awake() {
            currentHealth = maxHealth;
        }

        protected virtual void Start() {

        }

        protected virtual void Update() {

        }
    }

}
