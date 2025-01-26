using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(fileName = "MoveAbility", menuName = "Scriptable Objects/Character/Abilities/MoveOrIdle")]
    public class MoveAbility : AbilityBase
    {
        private static readonly int Blend  = Animator.StringToHash("Blend");

        [Header("Movement Settings")]
        [Tooltip("Base move speed for the character.")]
        public float moveSpeed = 5f;

        [Header("Animator Blend Settings")]
        [Tooltip("The maximum speed considered '1' for blending animations.")]
        public float maxBlendSpeed = 5f;
        
        [Tooltip("Damp time for smoothing the blend animation parameter changes.")]
        public float blendDampTime = 0.1f;

        private Vector2 _moveInput;
        private float   _currentSpeed;

        /// <summary>
        /// We'll decide which state to return based on input magnitude.
        ///  - 0: Idle
        ///  - 2: Move
        /// 
        /// You can assign your own integer IDs for Idle/Move in your Animator.
        /// </summary>
        public override int AnimationState
        {
            get
            {
                // If we're actually moving, use "2" for Move
                if (_currentSpeed > 0.01f)
                    return 2;
                
                // Otherwise, 0 for Idle
                return 0;
            }
        }

        // If you're comfortable giving it a mid-level priority so Jump or Dash can override, 
        // use something like 2. Just ensure Jump has > 2, Dash has > 2 if you want them overriding.
        public override int AnimationPriority => 2; 

        // Always "active" so it can handle user input
        public override bool IsActive => true;

        public override void UpdateAbility(float deltaTime)
        {
            if (!orientationTransform || !cameraTransform) 
                return;

            // 1. Compute move direction based on camera orientation
            Vector3 worldUp      = orientationTransform.up;
            Vector3 camForward   = Vector3.ProjectOnPlane(cameraTransform.forward, worldUp).normalized;
            Vector3 camRight     = Vector3.ProjectOnPlane(cameraTransform.right,   worldUp).normalized;
            Vector3 moveDir      = camForward * _moveInput.y + camRight * _moveInput.x;

            // 2. Rotate character to face the move direction if any
            if (moveDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir, worldUp);
                owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, targetRotation, 0.1f);
            }

            // 3. Calculate current speed
            _currentSpeed = moveDir.magnitude * moveSpeed;

            // 4. Optionally update an animator blend for the Move animation speed
            //    This won't force a re-entry if the Animator transitions are set properly.
            if (animator)
            {
                float normalizedSpeed = Mathf.Clamp01(_currentSpeed / maxBlendSpeed);
                animator.SetFloat(Blend, normalizedSpeed, blendDampTime, deltaTime);
            }
        }

        public override Vector3 GetDesiredVelocity(float deltaTime)
        {
            if (!orientationTransform || !cameraTransform)
                return Vector3.zero;

            Vector3 worldUp     = orientationTransform.up;
            Vector3 camForward  = Vector3.ProjectOnPlane(cameraTransform.forward, worldUp).normalized;
            Vector3 camRight    = Vector3.ProjectOnPlane(cameraTransform.right,   worldUp).normalized;
            Vector3 moveDir     = camForward * _moveInput.y + camRight * _moveInput.x;

            return moveDir * (moveSpeed * deltaTime);
        }

        public override void OnMoveInput(Vector2 value)
        {
            _moveInput = value;
        }
    }
}
