using Stat;
using UnityEngine;

namespace Animation {

    public class CharacterAnimator : MonoBehaviour {

        protected Animator animator;

        protected CharacterCombat characterCombat;

        protected virtual void Start() {
            animator = GetComponentInChildren<Animator>();

            characterCombat = GetComponent<CharacterCombat>();
            characterCombat.OnAttack += OnAttack;
        }

        protected virtual void OnAttack() {
            animator.SetTrigger("isAttacking");
        }

    }

}
