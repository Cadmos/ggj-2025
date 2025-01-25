using UnityEngine;
using System;
using System.Collections.Generic;

namespace GGJ
{
    [CreateAssetMenu(fileName = "AimingModeAbility", menuName = "Scriptable Objects/Character/Abilities/AimingMode")]
    public class AimingModeAbility : AbilityBase
    {
        [Header("Aiming Settings")]
        [Tooltip("How much to slow the entire game while aiming (0.1 = 10% speed).")]
        public float aimTimeScale = 0.1f;

        [Tooltip("Cooldown (seconds) after you stop aiming before you can aim again.")]
        public float aimCooldown = 1f;

        // Are we currently in aiming mode?
        private bool _isAiming;
        // Tracks remaining cooldown after we exit aiming
        private float _cooldownRemaining;

        // We want the ability active so it can respond to input
        public override bool IsActive => true;

        /// <summary>
        /// Disable Jump and Dash while aiming.
        /// </summary>
        public override List<Type> DisableAbilitiesWhileActive
        {
            get
            {
                if (_isAiming)
                {
                    return new List<Type> { typeof(JumpAbility), typeof(DashAbility) };
                }
                return new List<Type>();
            }
        }

        public override void Initialize(
            GameObject go, 
            Transform camera, 
            Transform orientation, 
            CharacterController character,
            AbilityManager abilityManager = null
        )
        {
            base.Initialize(go, camera, orientation, character, abilityManager);

            _isAiming = false;
            _cooldownRemaining = 0f;

            // Ensure normal time scale on start
            UpdateGlobalTimeScale(1f);

            // If there's an aim camera in the manager, disable it now
            if (manager?.aimCamera != null)
            {
                manager.aimCamera.gameObject.SetActive(false);
            }
        }

        public override void UpdateAbility(float deltaTime)
        {
            // Decrement cooldown
            if (_cooldownRemaining > 0f)
            {
                _cooldownRemaining -= deltaTime;
                if (_cooldownRemaining < 0f)
                {
                    _cooldownRemaining = 0f;
                }
            }
        }

        /// <summary>
        /// No velocity added by aiming itself.
        /// </summary>
        public override Vector3 GetDesiredVelocity(float deltaTime)
        {
            return Vector3.zero;
        }

        /// <summary>
        /// Call this when the aim button is pressed.
        /// </summary>
        public void StartAiming()
        {
            // Check cooldown
            if (_cooldownRemaining > 0f)
            {
                return; // can't aim yet
            }

            _isAiming = true;
            UpdateGlobalTimeScale(aimTimeScale);

            if (manager?.aimCamera != null)
            {
                manager.aimCamera.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Call this when the aim button is released.
        /// </summary>
        public void StopAiming()
        {
            if (_isAiming)
            {
                _isAiming = false;
                UpdateGlobalTimeScale(1f);

                if (manager?.aimCamera != null)
                {
                    manager.aimCamera.gameObject.SetActive(false);
                }

                // Start cooldown
                _cooldownRemaining = aimCooldown;
            }
        }

        ~AimingModeAbility()
        {
            // If destroyed, restore normal timescale
            UpdateGlobalTimeScale(1f);
        }

        private void UpdateGlobalTimeScale(float scale)
        {
            Time.timeScale = scale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
}
