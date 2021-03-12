using Interactions;
using Stat;
using UnityEngine;

namespace Enemy.Fly {

    [RequireComponent(typeof(CharacterStats))]
    public class FlyEnemy : Interactable {

        private CharacterStats myStats;

        private PlayerManager playerManager;

        public override void Interact() {
            base.Interact();

            AttackPlayer();
        }

        private void AttackPlayer() {
            CharacterCombat playerCombat = playerManager.GetComponent<CharacterCombat>();
            if (playerManager != null)
                playerCombat.Attack(myStats);
        }

        private void Start() {
            myStats = GetComponent<CharacterStats>();
            playerManager = PlayerManager.Instance;
        }
    }

}
