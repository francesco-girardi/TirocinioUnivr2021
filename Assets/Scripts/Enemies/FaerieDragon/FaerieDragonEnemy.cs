using Interactions;
using Stat;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.FaerieDragon {

    [RequireComponent(typeof(CharacterStats))]
    public class FaerieDragonEnemy : Interactable {

        private NavMeshAgent agent;

        private CharacterStats myStats;

        private PlayerManager playerManager;

        public override void Interact() {
            base.Interact();

            AttackPlayer();
        }

        private void AttackPlayer() {
            agent.isStopped = true;

            CharacterCombat playerCombat = playerManager.playerObject.GetComponent<CharacterCombat>();
            if (playerCombat != null)
                playerCombat.Attack(myStats);

            agent.isStopped = false;
        }

        private void Start() {
            agent = GetComponent<NavMeshAgent>();
            myStats = GetComponent<CharacterStats>();
            playerManager = PlayerManager.Instance;
        }
    }

}

