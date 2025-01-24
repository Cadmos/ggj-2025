using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ
{
    [CreateAssetMenu(fileName = "MoveAbility", menuName = "Scriptable Objects/Character/Abilities/Move")]
    public class MoveAbility : AbilityBase
    {
        [Header("Movement Settings")] public float moveSpeed = 5f;
        public float rotationSpeed = 720f;

        // We'll store the current move input in a Vector2, then convert to 3D
        private Vector2 _moveInput;

        public override void Initialize(GameObject go, Transform camera, Transform orientation, CharacterController character)
        {
            base.Initialize(go, camera, orientation, character);
            // Grab the CharacterController (or any other needed components)
            controller = owner.GetComponent<CharacterController>();
            if (controller == null)
            {
                Debug.LogWarning($"{nameof(MoveAbility)} requires a CharacterController on the owner.");
            }
        }

        public override void UpdateAbility(float deltaTime)
        {
            Debug.Log("update on " + name);
            // 1. Define our 'worldUp' using the orientation transform
            Vector3 worldUp = orientationTransform != null 
                ? orientationTransform.up 
                : Vector3.up; // fallback to default if none assigned

            // 2. Project camera vectors onto plane defined by 'worldUp'
            Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, worldUp).normalized;
            Vector3 camRight   = Vector3.ProjectOnPlane(cameraTransform.right,   worldUp).normalized;

            // 3. Build the desired movement direction (X => horizontal, Y => vertical input)
            Vector3 moveDirection = (camForward * _moveInput.y + camRight * _moveInput.x);

            // 4. Move the character
            //    - Using SimpleMove if you want built-in gravity
            //    - Or controller.Move(...) if you handle gravity yourself
            controller.SimpleMove(moveDirection * moveSpeed);

            // 5. Optionally rotate the character to face movement direction
            if (moveDirection.sqrMagnitude > 0.001f)
            {
                // Create a target rotation that faces moveDirection, using worldUp as "up"
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, worldUp);

                // Slerp or rotate towards it smoothly
                owner.transform.rotation = Quaternion.RotateTowards(
                    owner.transform.rotation,
                    targetRotation,
                    rotationSpeed * deltaTime
                );
            }
        }

        public override void OnMoveInput(Vector2 value)
        {
            _moveInput = value;
        }
    }
}
