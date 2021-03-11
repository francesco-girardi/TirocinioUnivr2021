using System.Collections.Generic;
using UnityEngine;

namespace Movement {

    public class SimpleCharacterController : MonoBehaviour {

        public Animator anim;       // Reference all'Animator altrimenti non posso prendere i valori per cambiare animazione

        public float moveSpeed = 20f;

        public float sprintSpeedMultiplier = 2f;

        public float crouchSpeedMultiplier = 0.5f;

        public float dashDistance = 20f;

        public float dashTime = 0.2f;

        public float dashCD = 0.5f;

        public float jumpSpeed = 30f;

        public float rotationSpeed = 720f;

        public float gravity = -60f;

        public CharacterMover mover;

        public CharacterCapsule capsule;

        public GroundDetector groundDetector;

        public MeshRenderer groundedIndicator;

        private float baseMoveSpeed;

        private float sprintSpeed;

        private float crouchSpeed;

        private float dashFinishTime;

        private bool isDashing = false;

        private float nextDashTime = 0f;

        private const float minVerticalSpeed = -12f;

        // Allowed time before the character is set to ungrounded from the last time he was safely grounded.
        private const float timeBeforeUngrounded = 0.1f;

        // Speed along the character local up direction.
        private float verticalSpeed = 0f;

        // Time after which the character should be considered ungrounded.
        private float nextUngroundedTime = -1f;

        private Transform cameraTransform;

        private List<MoveContact> moveContacts = new List<MoveContact>(10);

        private float GroundClampSpeed => -Mathf.Tan(Mathf.Deg2Rad * mover.maxFloorAngle) * moveSpeed;


        private void Start() {
            this.baseMoveSpeed = moveSpeed;
            this.sprintSpeed = baseMoveSpeed * sprintSpeedMultiplier;
            this.crouchSpeed = baseMoveSpeed * crouchSpeedMultiplier;
            cameraTransform = Camera.main.transform;
        }

        private void Update() {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if((horizontalInput != 0 || verticalInput != 0) && moveSpeed <= 20.0f)                          //
                {                                                                                           //
                    anim.SetBool("isWalking", true);                                                        //
                    anim.SetBool("isDashingAnim", false);                                                   //  Se sta camminando setta il valore di "isWalking" a true in modo da attivare la transizione dell'animazione da "Idle" a "Walk" dio madonna
                }else if ((horizontalInput != 0 || verticalInput != 0) && moveSpeed > 20.0f){               //   Ho provato a usare il tuo "isDashing", ma non gli piace se viene dichiarato sullo script -- Permette di disattivare l'azione di corsa --
                    anim.SetBool("isDashingAnim", true);                                                    //  Permette di attivare l'azione di corsa
                }else{                                                                                      //
                    anim.SetBool("isDashingAnim", false);                                                   //
                    anim.SetBool("isWalking", false);                                                       //
                }                                                                                           //

            Vector3 moveDirection = CameraRelativeVectorFromInput(horizontalInput, verticalInput);

            UpdateMovement(moveDirection, Time.deltaTime);
        }

        private void UpdateMovement(Vector3 moveDirection, float deltaTime) {
            Vector3 velocity = moveSpeed * moveDirection;
            PlatformDisplacement? platformDisplacement = null;

            bool groundDetected = groundDetector.DetectGround(out GroundInfo groundInfo);

            if (IsSafelyGrounded(groundDetected, groundInfo.isOnFloor))
                nextUngroundedTime = Time.time + timeBeforeUngrounded;

            bool isGrounded = Time.time < nextUngroundedTime;

            SetGroundedIndicatorColor(isGrounded);

            if (isGrounded && Input.GetButtonDown("Jump") && !isDashing) {
                verticalSpeed = jumpSpeed;
                nextUngroundedTime = -1f;
                isGrounded = false;
            }

            if (isGrounded) {
                mover.preventMovingUpSteepSlope = true;
                mover.canClimbSteps = true;

                verticalSpeed = 0f;
                velocity += GroundClampSpeed * transform.up;

                if (groundDetected && IsOnMovingPlatform(groundInfo.collider, out MovingPlatform movingPlatform))
                    platformDisplacement = GetPlatformDisplacementAtPoint(movingPlatform, groundInfo.point);
                
                // STOP DASH
                if(Time.time >= dashFinishTime){
                    isDashing = false; 
                    moveSpeed = baseMoveSpeed;
                }
                
                // CROUCH - SPRINT
                if (capsule.IsCrouched){
                    moveSpeed = crouchSpeed;
                }else if (Input.GetButton("Sprint") && !isDashing)
                    moveSpeed = sprintSpeed;
                else if(!isDashing)
                    moveSpeed = baseMoveSpeed;


                // DASH
                if(Input.GetButtonDown("Dash") && Time.time >= nextDashTime){
                    dashFinishTime = Time.time + dashTime;
                    nextDashTime = dashFinishTime + dashCD;
                    moveSpeed = dashDistance/dashTime;
                    isDashing = true;
                }

            } else {
                mover.preventMovingUpSteepSlope = false;
                mover.canClimbSteps = false;

                BounceDownIfTouchedCeiling();

                verticalSpeed += gravity * Time.deltaTime;

                // if (verticalSpeed < minVerticalSpeed)
                //     verticalSpeed = minVerticalSpeed;

                velocity += verticalSpeed * transform.up;

            }

            RotateTowards(velocity);
            mover.Move(velocity * deltaTime, moveContacts);

            if (platformDisplacement.HasValue)
                ApplyPlatformDisplacement(platformDisplacement.Value);
        }

        // Gets world space vector in respect of camera orientation from two axes input.
        private Vector3 CameraRelativeVectorFromInput(float x, float y) {
            Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, transform.up).normalized;
            Vector3 right = Vector3.Cross(transform.up, forward);

            return x * right + y * forward;
        }

        private bool IsSafelyGrounded(bool groundDetected, bool isOnFloor) {
            return groundDetected && isOnFloor && verticalSpeed < 0.1f;
        }

        private void SetGroundedIndicatorColor(bool isGrounded) {
            if (groundedIndicator != null)
                groundedIndicator.material.color = isGrounded ? Color.green : Color.blue;
        }

        private bool IsOnMovingPlatform(Collider groundCollider, out MovingPlatform platform) {
            return groundCollider.TryGetComponent(out platform);
        }

        private void RotateTowards(Vector3 direction) {
            Vector3 direzioneOrizz = Vector3.ProjectOnPlane(direction, transform.up);

            if (direzioneOrizz.sqrMagnitude < 1E-06f)
                return;

            Quaternion rotazioneObbiettivo = Quaternion.LookRotation(direzioneOrizz, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotazioneObbiettivo, rotationSpeed * Time.deltaTime);
        }

        private PlatformDisplacement GetPlatformDisplacementAtPoint(MovingPlatform platform, Vector3 point) {
            platform.GetDisplacement(out Vector3 platformDeltaPosition, out Quaternion platformDeltaRotation);
            Vector3 localPosition = point - platform.transform.position;
            Vector3 deltaPosition = platformDeltaPosition + platformDeltaRotation * localPosition - localPosition;

            platformDeltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
            angle *= Mathf.Sign(Vector3.Dot(axis, transform.up));

            return new PlatformDisplacement() {
                deltaPosition = deltaPosition,
                deltaUpRotation = angle
            };
        }

        private void BounceDownIfTouchedCeiling() {
            for (int i = 0; i < moveContacts.Count; i++) {
                if (Vector3.Dot(moveContacts[i].normal, transform.up) < -0.7f) {
                    verticalSpeed = -0.25f * verticalSpeed;
                    break;
                }
            }
        }

        private void ApplyPlatformDisplacement(PlatformDisplacement platformDisplacement) {
            transform.Translate(platformDisplacement.deltaPosition, Space.World);
            transform.Rotate(0f, platformDisplacement.deltaUpRotation, 0f, Space.Self);
        }

        private struct PlatformDisplacement {
            public Vector3 deltaPosition;
            public float deltaUpRotation;
        }

    }

}