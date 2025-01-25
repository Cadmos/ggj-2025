using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(fileName = "MoveAbility", menuName = "Scriptable Objects/Character/Abilities/Move")]
    public class MoveAbility : AbilityBase
    {
        [Header("Movement Settings")]
        public float moveSpeed = 5f;

        private Vector2 _moveInput;

        // We'll keep IsActive = true so we can always move (unless locked).
        public override bool IsActive => true;

        public override void UpdateAbility(float deltaTime)
        {
            // You might do rotation and other logic here as before...
            if (!orientationTransform || !cameraTransform) 
                return;

            Vector3 worldUp = orientationTransform.up;
            Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, worldUp).normalized;
            Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, worldUp).normalized;

            Vector3 moveDirection = camForward * _moveInput.y + camRight * _moveInput.x;

            // Optional: rotate the character to face movement direction:
            if (moveDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, worldUp);
                owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, targetRotation, 0.1f);
            }
        }

        // Return the horizontal velocity
        public override Vector3 GetDesiredVelocity(float deltaTime)
        {
            // We'll build a horizontal vector only (x,0,z)
            Vector3 worldUp = orientationTransform ? orientationTransform.up : Vector3.up;
            Vector3 camForward = cameraTransform ? 
                Vector3.ProjectOnPlane(cameraTransform.forward, worldUp).normalized : Vector3.forward;
            Vector3 camRight = cameraTransform ? 
                Vector3.ProjectOnPlane(cameraTransform.right, worldUp).normalized : Vector3.right;

            Vector3 moveDirection = camForward * _moveInput.y + camRight * _moveInput.x;
            return moveDirection * (moveSpeed * deltaTime);
        }

        public override void OnMoveInput(Vector2 value)
        {
            _moveInput = value;
        }
    }
}
