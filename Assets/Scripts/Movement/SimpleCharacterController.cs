using Audio;
using System.Collections.Generic;
using UnityEngine;

namespace Movement {

    public class SimpleCharacterController : MonoBehaviour {

        public float moveSpeed = 20f;

        public float runSpeedMultiplier = 2f;

        public float crouchSpeedMultiplier = 0.5f;

        public float dashDistance = 20f;

        public float dashTime = 0.2f;

        public float dashCD = 0.5f;

        public float jumpSpeed = 30f;

        public float rotationSpeed = 720f;

        public float gravity = -60f;

        CharacterMover mover;

        CharacterCapsule capsule;

        GroundDetector groundDetector;

        private float baseMoveSpeed;

        private float runSpeed;

        private float crouchSpeed;

        private float dashFinishTime;

        private bool isDashing = false;

        private bool isRunning = false;

        private bool isWalking = false;

        private bool jumped = false;

        private bool isGrounded = false;

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

        public float BaseMoveSpeed {
            get => baseMoveSpeed;
        }

        public float RunSpeed {
            get => runSpeed;
        }

        public float CrouchSpeed {
            get => crouchSpeed;
        }

        public bool IsWalking {
            get => isWalking;
        }

        public bool IsRunning {
            get => isRunning;
        }

        public bool IsDashing {
            get => isDashing;
        }
        
        public bool Jumped {
            get => jumped;
        }

        public bool IsGrounded {
            get => isGrounded;
        }

        public bool IsCrouched {
            get => capsule.IsCrouched;
        }

        private void Start() {
            this.baseMoveSpeed = moveSpeed;
            this.runSpeed = baseMoveSpeed * runSpeedMultiplier;
            this.crouchSpeed = baseMoveSpeed * crouchSpeedMultiplier;
            cameraTransform = Camera.main.transform;

            mover = GetComponent<CharacterMover>();
            capsule = GetComponent<CharacterCapsule>();
            groundDetector = GetComponent<GroundDetector>();

        }

        private void Update() {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = CameraRelativeVectorFromInput(horizontalInput, verticalInput);

            UpdateMovement(moveDirection, Time.deltaTime);
        }

        private void UpdateMovement(Vector3 moveDirection, float deltaTime) {

            Vector3 velocity = moveSpeed * moveDirection;
            PlatformDisplacement? platformDisplacement = null;

            bool groundDetected = groundDetector.DetectGround(out GroundInfo groundInfo);

            if (IsSafelyGrounded(groundDetected, groundInfo.isOnFloor))
                nextUngroundedTime = Time.time + timeBeforeUngrounded;

            isGrounded = Time.time < nextUngroundedTime;
            // if(isGrounded)
            //     jumped = false;

            if (isGrounded && Input.GetButtonDown("Jump") && !isDashing) {
                verticalSpeed = jumpSpeed;
                nextUngroundedTime = -1f;
                isGrounded = false;
                jumped = true;
            }else
                jumped = false;

            if (isGrounded) {

                mover.preventMovingUpSteepSlope = true;
                mover.canClimbSteps = true;

                verticalSpeed = 0f;
                velocity += GroundClampSpeed * transform.up;

                if (groundDetected && IsOnMovingPlatform(groundInfo.collider, out MovingPlatform movingPlatform))
                    platformDisplacement = GetPlatformDisplacementAtPoint(movingPlatform, groundInfo.point);
                
                // STOP DASH
                if(Time.time >= dashFinishTime){
                    Walk();
                }
                
                // CROUCH - RUN
                if (IsCrouched){
                    Crouch();
                }else if (Input.GetButton("Run") && !isDashing){
                    Run();
                }else if(!isDashing){
                    Walk();
                }

                // DASH
                if(Input.GetButtonDown("Dash") && Time.time >= nextDashTime){
                    Dash();
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

        private void Walk(){
            isWalking = true;
            isRunning = false;
            isDashing = false;

            moveSpeed = baseMoveSpeed;
        }
        private void Run(){
            isWalking = false;
            isRunning = true;
            isDashing = false;

            moveSpeed = runSpeed;
            
        }
        private void Dash(){
            isWalking = false;
            isRunning = false;
            isDashing = true;

            dashFinishTime = Time.time + dashTime;
            nextDashTime = dashFinishTime + dashCD;
            moveSpeed = dashDistance/dashTime;
            
        }

        private void Crouch(){
            isWalking = true;
            isRunning = false;
            isDashing = false;

            moveSpeed = crouchSpeed;

        }

        private struct PlatformDisplacement {
            public Vector3 deltaPosition;
            public float deltaUpRotation;
        }

    }

}