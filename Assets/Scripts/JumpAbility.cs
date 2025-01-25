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
        private bool  _isJumping;          // Are we currently in a jump arc?
        private float _jumpTimer;          // Tracks time since jump start.
        private float _previousCurveValue; // The jump curve value from the previous frame.
        private int   _remainingJumps;     // How many jumps we have left.
        
        // We'll store the ground (start) Y position for the jump,
        // so we can measure relative height from that baseline.
        private float _jumpStartYPos;

        /// <summary>
        /// If we want to disable other abilities while jumping, specify them here.
        /// For example, disable GravityAbility so it doesn't interfere.
        /// If you also want to disable MoveAbility or anything else, add them here.
        /// </summary>
        public override List<Type> DisableAbilitiesWhileActive
        {
            get
            {
                if (disableGravityDuringJump && _isJumping)
                {
                    // Example: disable just GravityAbility
                    return new List<Type> { typeof(GravityAbility) };
                }
                // Otherwise, disable nothing
                return new List<Type>();
            }
        }

        /// <summary>
        /// We keep this ability "active" at all times so we can detect jump input.
        /// If you only wanted it active while there's a jump available, 
        /// you could do something else, but typically "true" is simplest.
        /// </summary>
        public override bool IsActive => true;

        public override void Initialize(
            GameObject go, 
            Transform camera, 
            Transform orientation, 
            CharacterController character,
            AbilityManager abilityManager = null
        )
        {
            base.Initialize(go, camera, orientation, character, abilityManager);

            _isJumping = false;
            _jumpTimer = 0f;
            _previousCurveValue = 0f;
            _remainingJumps = maxJumpCharges;
        }

        public override void UpdateAbility(float deltaTime)
        {
            if (controller == null) return;

            // Check if we're grounded; if so, reset charges and ensure no leftover arc
            if (controller.isGrounded && !_isJumping)
            {
                _remainingJumps = maxJumpCharges;
            }

            // If we are currently in a jump arc, update the arc timer
            if (_isJumping)
            {
                _jumpTimer += deltaTime;
                float normalizedTime = _jumpTimer / jumpDuration;

                if (normalizedTime >= 1f)
                {
                    // Jump arc completed
                    EndJump();
                }
            }
        }

        public override Vector3 GetDesiredVelocity(float deltaTime)
        {
            // If we're not jumping, return no vertical displacement
            if (!_isJumping)
                return Vector3.zero;

            // Calculate how far along we are (0..1)
            float normalizedTime = Mathf.Clamp01(_jumpTimer / jumpDuration);

            // Evaluate the curve at the current time
            float currentCurveValue = jumpCurve.Evaluate(normalizedTime) * jumpHeight;

            // The difference from last frame's curve value is the actual displacement in Y
            float deltaY = currentCurveValue - _previousCurveValue;

            // Update for next frame
            _previousCurveValue = currentCurveValue;

            // Because the manager expects "velocity * deltaTime" for movement, 
            // we can either:
            // 1) Return 'deltaY' directly as displacement (which the manager won't multiply again),
            // 2) Or treat 'deltaY / deltaTime' as velocity.

            // Usually, each ability's GetDesiredVelocity() is returning velocity * dt 
            // or "displacement" for the manager's final Move(). If your manager 
            // sums "velocities" and multiplies by deltaTime once, we want velocity here.

            // Let's assume the manager does: finalVelocity += ability.GetDesiredVelocity(dt);
            // and then calls controller.Move(finalVelocity). If the manager doesn't multiply 
            // by dt again, we should return displacement. 
            // -> We'll call it "desiredDisplacement" to clarify:

            Vector3 desiredDisplacement = new Vector3(0f, deltaY, 0f);

            return desiredDisplacement;
        }

        public override void OnJumpInput()
        {
            // We only trigger a jump if:
            // 1) We have remaining jumps
            // 2) We are NOT already in a jump, OR we decide to allow mid-arc re-jump 
            //    (in which case you'd forcibly start a new arc, but that might feel jarring).
            // For a standard multi-jump, you might allow re-jump mid-air, so let's do that:
            if (_remainingJumps > 0)
            {
                StartJump();
                // Decrement a jump charge
                _remainingJumps--;
            }
            // If no jumps remain, do nothing.
        }

        private void StartJump()
        {
            // Begin a fresh jump arc from the current position
            _isJumping = true;
            _jumpTimer = 0f;

            // Record the baseline for our curve
            _jumpStartYPos = owner.transform.position.y;
            _previousCurveValue = 0f;
        }

        private void EndJump()
        {
            // Jump arc has ended
            _isJumping = false;
            _jumpTimer = 0f;
            _previousCurveValue = 0f;
        }
    }
}
