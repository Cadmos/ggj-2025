using UnityEngine;
using System;
using System.Collections.Generic;

namespace GGJ
{
    [CreateAssetMenu(fileName = "DashAbility", menuName = "Scriptable Objects/Character/Abilities/Dash")]
    public class DashAbility : AbilityBase
    {
        [Header("Dash Settings")]
        [Tooltip("How long the dash lasts (seconds).")]
        public float dashDuration = 0.3f;

        [Tooltip("Speed (m/s) during dash.")]
        public float dashSpeed = 15f;

        [Tooltip("If true, you can dash while in the air.")]
        public bool canDashInAir = false;

        private float _dashTimer;
        private Vector3 _dashDirection;

        // If you want to disable other abilities while active,
        // override DisableAbilitiesWhileActive:
        public override List<Type> DisableAbilitiesWhileActive 
            => IsActive ? new List<Type> { typeof(MoveAbility), typeof(JumpAbility) } 
                        : new List<Type>();

        // "Active" means we're dashing right now
        public override bool IsActive => _dashTimer > 0f;

        public override void Initialize(
            GameObject go, 
            Transform camera, 
            Transform orientation, 
            CharacterController character,
            AbilityManager abilityManager = null
        )
        {
            base.Initialize(go, camera, orientation, character, abilityManager);

            _dashTimer = 0f;
            _dashDirection = Vector3.zero;
        }

        public override void UpdateAbility(float deltaTime)
        {
            if (controller == null) return;

            if (_dashTimer > 0f)
            {
                _dashTimer -= deltaTime;
                if (_dashTimer <= 0f)
                {
                    _dashTimer = 0f;
                    _dashDirection = Vector3.zero;
                }
            }
        }

        public override Vector3 GetDesiredVelocity(float deltaTime)
        {
            if (_dashTimer > 0f)
            {
                // Return dash velocity
                return _dashDirection * (dashSpeed * deltaTime);
            }
            return Vector3.zero;
        }

        public override void OnDashInput()
        {
            // If not already dashing, start if conditions allow
            if (_dashTimer <= 0f)
            {
                if (!controller.isGrounded && !canDashInAir)
                    return;

                _dashTimer = dashDuration;

                // Dash direction is typically forward 
                // (or could be last move input direction, etc.)
                Vector3 forward = owner.transform.forward;
                forward.y = 0f;
                forward.Normalize();
                _dashDirection = forward;
            }
        }
    }
}
