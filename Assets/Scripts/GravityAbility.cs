using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(fileName = "GravityAbility", menuName = "Scriptable Objects/Character/Abilities/Gravity")]
    public class GravityAbility : AbilityBase
    {
        [Header("Gravity Settings")]
        public float gravity = -9.81f;

        [Tooltip("Small downward velocity so the character 'sticks' to slopes.")]
        public float groundedOffset = -2f;

        private float _verticalVelocity;

        public override bool IsActive => true;

        // Initialize with the manager reference
        public override void Initialize(
            GameObject go, 
            Transform camera, 
            Transform orientation, 
            CharacterController character,
            Animator characterAnimator,
            AbilityManager abilityManager = null
        )
        {
            base.Initialize(go, camera, orientation, character, characterAnimator, abilityManager);
            _verticalVelocity = 0f;
        }

        public override void UpdateAbility(float deltaTime)
        {
            if (controller == null) return;

            // If grounded and moving downward, reset velocity
            if (controller.isGrounded && _verticalVelocity <= 0f)
            {
                _verticalVelocity = groundedOffset;
            }
            else
            {
                _verticalVelocity += gravity * deltaTime;
            }
        }

        public override Vector3 GetDesiredVelocity(float deltaTime)
        {
            return new Vector3(0f, _verticalVelocity, 0f) * deltaTime;
        }
    }
}