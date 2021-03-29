using Interactions;
using Stat;
using UnityEngine;

namespace Enemy.Slime.FireSlime {

    [RequireComponent(typeof(CharacterStats))]
    public class FireSlimeEnemy : Interactable {

        private CharacterStats myStats;

        private PlayerManager playerManager;

        public override void Interact() {
            base.Interact();

            AttackPlayer();
        }

        private void AttackPlayer() {
            CharacterCombat playerCombat = playerManager.playerObject.GetComponent<CharacterCombat>();
            if (playerCombat != null)
                playerCombat.Attack(myStats);
        }

        private void Start() {
            myStats = GetComponent<CharacterStats>();
            playerManager = PlayerManager.Instance;
        }

    }

}
