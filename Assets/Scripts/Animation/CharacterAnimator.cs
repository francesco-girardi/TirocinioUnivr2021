using Enemy;
using Stat;
using UnityEngine;

namespace Animation {

    public class CharacterAnimator : MonoBehaviour {

        protected Animator animator;

        protected EnemyController enemyController;

        protected CharacterCombat characterCombat;

        protected virtual void Start() {
            animator = GetComponentInChildren<Animator>();

            enemyController = GetComponent<EnemyController>();
            enemyController.onEnemyMove += OnEnemyMove;

            characterCombat = GetComponent<CharacterCombat>();
            characterCombat.OnAttack += OnAttack;
        }

        protected virtual void OnEnemyMove() {
            animator.SetTrigger("isMoving");
        }

        protected virtual void OnAttack() {
            animator.SetTrigger("isAttacking");
        }

    }

}
