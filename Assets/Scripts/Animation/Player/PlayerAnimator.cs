using Movement;
using UnityEngine;

namespace Animation.Player {
    public class PlayerAnimator : CharacterAnimator {

        [Header("Audio Info")]
        [SerializeField]
        [Tooltip("Step sounds on Tile Terrain")]
        private AudioClip[] tileClips = new AudioClip[3];

        [SerializeField]
        [Tooltip("Step sounds on Grass Terrain")]
        private AudioClip[] grassClips = new AudioClip[3];

        private AudioSource audioSource;

        private Animator anim;

        private SimpleCharacterController controller;

        private float baseMoveSpeed;
        private float sprintSpeed;
        private float crouchSpeed;
        private bool isWalking;
        private bool isRunning;
        private bool isCrouched;
        private bool isDashing;
        private bool jumped;
        private bool isGrounded;
        private bool chain = false;
        private bool attacked = false;
        public bool Attacked {
            get => this.attacked;
        }
        private bool startedAttack = false;
        public bool StartedAttack {
            get => this.startedAttack;
        }

        [SerializeField]
        private GameObject SwordBack;

        [SerializeField]
        private GameObject SwordHand;



        private void Awake() {
            audioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        protected override void Start() {
            anim = GetComponent<Animator>();
            controller = GetComponentInParent<SimpleCharacterController>();
            // animator = GetComponentInChildren<Animator>();
            // base.Start();                                        //momentaneo finchè non mettiamo le animazioni di attacco o finchè non capisco meglio
        }

        // Update is called once per frame
        protected void Update() {
            this.attacked = false;

            baseMoveSpeed = controller.BaseMoveSpeed;
            sprintSpeed = controller.RunSpeed;
            crouchSpeed = controller.CrouchSpeed;

            anim.SetFloat("walkSpeed", baseMoveSpeed / 1.8f);
            anim.SetFloat("runSpeed", sprintSpeed / 18f);
            anim.SetFloat("crouchSpeed", crouchSpeed / 2.5f);    // DA IMPOSTARE IL NUMERO POI

            isWalking = controller.IsWalking;
            isRunning = controller.IsRunning;
            isCrouched = controller.IsCrouched;
            isDashing = controller.IsDashing;
            jumped = controller.Jumped;
            isGrounded = controller.IsGrounded;

            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) {
                isWalking = false;
                isRunning = false;
            }

            anim.SetBool("isWalking", isWalking);
            anim.SetBool("isRunning", isRunning);
            anim.SetBool("isCrouched", isCrouched);
            anim.SetBool("isDashing", isDashing);
            anim.SetBool("jumped", jumped);
            anim.SetBool("isGrounded", isGrounded);



            if (Input.GetButtonDown("CombatStance") && !isWalking && !isRunning && !isCrouched && !jumped && isGrounded) {

                anim.SetBool("combatStance", true);

            }

            if (Input.GetButtonDown("EndCombatStance")) {

                anim.SetBool("combatStance", false);
            }

            if (Input.GetButtonDown("Fire1") && anim.GetBool("combatStance") && !anim.GetBool("c1") && !anim.GetBool("repeatController")) {

                this.attacked = true;
                this.startedAttack = true;
                anim.SetBool("c1", true);
                chain = false;
            }

            if (Input.GetButtonDown("Fire1") && anim.GetBool("combatStance") && (anim.GetBool("c1") || anim.GetBool("c2")) && anim.GetBool("repeatController")) {
                
                chain = true;
            }



        }

        private void Step() {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 100)) {
                GameObject terrain = hit.collider.gameObject;
                if (terrain != null) {
                    if (terrain.tag == "Tile")
                        audioSource.PlayOneShot(tileClips[0]);
                    else if (terrain.tag == "Grass")
                        audioSource.PlayOneShot(grassClips[0]);
                    else
                        return;
                }
            }
        }

        private void RunStep() {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 100)) {
                GameObject terrain = hit.collider.gameObject;
                if (terrain != null) {
                    if (terrain.tag == "Tile")
                        audioSource.PlayOneShot(tileClips[1]);
                    else if (terrain.tag == "Grass")
                        audioSource.PlayOneShot(grassClips[1]);
                    else
                        return;
                }
            }
        }

        private void JumpStep() {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 100)) {
                GameObject terrain = hit.collider.gameObject;
                if (terrain != null) {
                    if (terrain.tag == "Tile")
                        audioSource.PlayOneShot(tileClips[2]);
                    else if (terrain.tag == "Grass")
                        audioSource.PlayOneShot(grassClips[2]);
                    else
                        return;
                }
            }
        }

        private void SwordIn() {

            SwordBack.SetActive(false);
            SwordHand.SetActive(true);

        }

        private void SwordOut() {

            SwordBack.SetActive(true);
            SwordHand.SetActive(false);

        }

        private void ComboTwo() {

            anim.SetBool("c1", false);
            this.startedAttack = false;

            if (chain) {
                this.attacked = true;
                this.startedAttack = true;
                anim.SetBool("c2", true);
                chain = false;
            }
            else { anim.SetBool("exitAttack", true); }
        }

        private void ComboThree() {

            anim.SetBool("c2", false);
            this.startedAttack = false;

            if (chain) {
                this.attacked = true;
                this.startedAttack = true;
                anim.SetBool("c3", true);
                chain = false;
            }
            else { anim.SetBool("exitAttack", true); }
        }

        private void ComboEnd() {

            anim.SetBool("c3", false);

        }

        private void ResetController() {

            anim.SetBool("repeatController", false);
        }

        private void AResetController() {
            anim.SetBool("repeatController", true);
            anim.SetBool("exitAttack", false);

        }

        private void RotateSword() {


            SwordHand.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);

        }

        private void FinishAttack(){
            this.startedAttack = false;
        }
    }

}
