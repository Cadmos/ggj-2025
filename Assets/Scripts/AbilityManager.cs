using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ
{
    public class AbilityManager : MonoBehaviour
    {
        [Tooltip("List of abilities (ScriptableObjects) attached to this character.")]
        public List<AbilityBase> abilities = new List<AbilityBase>();
        
        public Transform cameraTransform;
        public Transform orientationTransform;

        public CharacterController controller;

        private void Awake()
        {
            // Initialize each ability
            foreach (var ability in abilities)
            {
                if (ability == null) continue;
                ability.Initialize(gameObject, cameraTransform, orientationTransform, controller);
            }
        }

        private void Update()
        {
            // Call Tick on every ability each frame
            float dt = Time.deltaTime;
            foreach (var ability in abilities)
            {
                ability.UpdateAbility(dt);
            }
        }

        // ==== Input Callbacks (assuming PlayerInput uses SendMessages) ====
        // Move Input
        public void OnMove(InputValue value)
        {
            Vector2 moveInput = value.Get<Vector2>();
            foreach (var ability in abilities)
            {
                ability.OnMoveInput(moveInput);
            }
        }

        // Interact / Action Input
        public void OnInteract(InputValue value)
        {
            if (value.isPressed) // or read a bool/float if you want
            {
                foreach (var ability in abilities)
                {
                    ability.OnInteractInput();
                }
            }
        }

        // You can add more, like OnJump, OnDash, OnAttack, etc.
    }
}