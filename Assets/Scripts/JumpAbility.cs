using UnityEngine;
using System;
using System.Collections.Generic;

namespace GGJ
{
    [CreateAssetMenu(fileName = "JumpAbility", menuName = "Scriptable Objects/Character/Abilities/Jump")]
    public class JumpAbility : AbilityBase
    {
        [Header("Jump Arc Settings")]
        [Tooltip("Duration of one jump arc, in seconds.")]
        public float jumpDuration = 0.5f;

        [Tooltip("Animation curve defining the jump arc. " +
                 "X-axis: time (0 to 1). Y-axis: relative height (0 to 1).")]
        public AnimationCurve jumpCurve;

        [Tooltip("Maximum jump height that the curve's Y=1 corresponds to.")]
        public float jumpHeight = 2f;

        [Header("Charges (for multi-jump)")]
        [Tooltip("Number of jumps allowed before landing (e.g. 2 = double jump).")]
        public int maxJumpCharges = 2;

        [Header("Disable Gravity While Jumping?")]
        [Tooltip("If true, we disable the GravityAbility while this jump arc is active.")]
        public bool disableGravityDuringJump = false;

        // Internals
        private bool  _isJumping;           // True while in the jump arc
        private float _jumpTimer;           // Time since jump start
        private float _previousCurveValue;  // The curve value from last frame
        private int   _remainingJumps;      // How many jumps are left
        
        private float _jumpStartYPos;       // Baseline Y (if needed)

        public override int AnimationState => 3; 
        public override int AnimationPriority => _isJumping ? 3 : 0; 

        public override List<Type> DisableAbilitiesWhileActive
        {
            get
            {
                // Disable Gravity if requested, but only while actually jumping
                if (disableGravityDuringJump && _isJumping)
                {
                    return new List<Type> { typeof(GravityAbility) };
                }
                return new List<Type>();
            }
        }

        /// <summary>
        /// Only "active" while currently in the jump arc, so the AbilityManager knows to play jump anim.
        /// </summary>
        public override bool IsActive => _isJumping;

        public override void Initialize(
            GameObject go, 
            Transform camera, 
            Transform orientation, 
            CharacterController character,
            Animator characterAnimator,
            AbilityManager abilityManager = null)
        {
            base.Initialize(go, camera, orientation, character, characterAnimator, abilityManager);

            _isJumping          = false;
            _jumpTimer          = 0f;
            _previousCurveValue = 0f;
            _remainingJumps     = maxJumpCharges;
        }

        public override void UpdateAbility(float deltaTime)
        {
            if (!controller) return;

            // Check if character is on the ground
            if (controller.isGrounded)
            {
                // If we were jumping, forcibly end the jump
                if (_isJumping)
                {
                    EndJump();
                }
                // Reset jump charges now that we're grounded
                _remainingJumps = maxJumpCharges;
            }
            else
            {
                // If we're in a jump arc, continue it
                if (_isJumping)
                {
                    _jumpTimer += deltaTime;
                    float normalizedTime = _jumpTimer / jumpDuration;

                    // If we exceed the jump duration, end the jump
                    if (normalizedTime >= 1f)
                    {
                        EndJump();
                    }
                }
            }
        }

        public override Vector3 GetDesiredVelocity(float deltaTime)
        {
            // If we're not jumping, no vertical displacement
            if (!_isJumping)
                return Vector3.zero;

            float normalizedTime = Mathf.Clamp01(_jumpTimer / jumpDuration);
            float currentCurveValue = jumpCurve.Evaluate(normalizedTime) * jumpHeight;

            float deltaY = currentCurveValue - _previousCurveValue;
            _previousCurveValue = currentCurveValue;

            // Return the displacement (the manager doesn't multiply by deltaTime again)
            return new Vector3(0f, deltaY, 0f);
        }

        public override void OnJumpInput()
        {
            // Only jump if we still have charges
            if (_remainingJumps > 0)
            {
                StartJump();
                _remainingJumps--;
            }
        }

        private void StartJump()
        {
            _isJumping          = true;
            _jumpTimer          = 0f;
            _previousCurveValue = 0f;
            _jumpStartYPos      = owner.transform.position.y;
        }

        private void EndJump()
        {
            _isJumping          = false;
            _jumpTimer          = 0f;
            _previousCurveValue = 0f;
        }
    }
}
