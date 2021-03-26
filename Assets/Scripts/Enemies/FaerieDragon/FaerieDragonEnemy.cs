using Interactions;
using Stat;

namespace Enemy.FaerieDragon {
    public class FaerieDragonEnemy : Interactable {

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

