using System.Collections.Generic;
using UnityEngine;

namespace Stat {

    [System.Serializable]
    public class Stat {

        [SerializeField]
        private int baseValue;

        private List<int> modifiers = new List<int>();

        /// <summary>
        /// Get these stat value
        /// </summary>
        /// <returns></returns>
        public int GetValue() {
            int finalValue = baseValue;
            modifiers.ForEach(x => finalValue += x);

            return finalValue;
        }

        /// <summary>
        /// Add weapon / equiment stats to the charachter
        /// </summary>
        /// <param name="value"></param>
        public void AddModifier(int value) {
            if (value != 0)
                modifiers.Add(value);
        }

        /// <summary>
        /// Remove weapon / equitment stats from the charachter
        /// </summary>
        /// <param name="value"></param>
        public void RemoveModifier(int value) {
            if (value != 0)
                modifiers.Remove(value);
        }

        /// <summary>
        /// Set current stat base value
        /// </summary>
        /// <param name="value"></param>
        public void SetBaseValue(float value) {
            baseValue = (int)value;
        }

    }

}
