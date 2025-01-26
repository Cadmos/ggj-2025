using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine; // If you're using Cinemachine

namespace GGJ
{
    public class AbilityManager : MonoBehaviour
    {
        [Tooltip("List of abilities (ScriptableObjects) attached to this character.")]
        public List<AbilityBase> abilities = new List<AbilityBase>();

        [Tooltip("Transform for the camera (e.g., main camera).")]
        public Transform cameraTransform;

        [Tooltip("Orientation helper transform (used by some abilities).")]
        public Transform orientationTransform;

        [Tooltip("Reference to the CharacterController on this object.")]
        public CharacterController controller;

        [Header("Animator")]
        [Tooltip("Animator that all abilities can use.")]
        public Animator animator;

        [Header("Cinemachine Setup")]
        [Tooltip("Optional: Cinemachine virtual camera used for aiming.")]
        public CinemachineCamera aimCamera;

        public Vector3 _finalVelocity;
        private int _lastState = -1;

        private void Awake()
        {
            // Initialize each ability, passing a reference to THIS manager
            foreach (var ability in abilities)
            {
                if (ability == null) continue;
                ability.Initialize(
                    gameObject, 
                    cameraTransform, 
                    orientationTransform, 
                    controller,
                    animator,
                    this  // pass the manager reference
                );
            }
        }

private void Update()
{
    float dt = Time.deltaTime;

    // 1. Update abilities
    foreach (var ability in abilities)
    {
        if (!ability) continue;
        if (ability.IsActive)
        {
            ability.UpdateAbility(dt);
        }
    }

    // 2. Determine which abilities are disabled by others
    var disabledAbilityTypes = new HashSet<System.Type>();
    foreach (var ability in abilities)
    {
        if (!ability) continue;
        if (ability.IsActive)
        {
            foreach (var type in ability.DisableAbilitiesWhileActive)
            {
                disabledAbilityTypes.Add(type);
            }
        }
    }

    // 3. Collect final velocity
    _finalVelocity = Vector3.zero;
    foreach (var ability in abilities)
    {
        if (!ability) continue;
        if (ability.IsActive && !disabledAbilityTypes.Contains(ability.GetType()))
        {
            _finalVelocity += ability.GetDesiredVelocity(dt);
        }
    }

    // 4. Move once
    controller.Move(_finalVelocity);
    _finalVelocity = Vector3.zero;

    // === NEW ANIMATION LOGIC ===

    // Find the highest-priority active ability
    AbilityBase highestPriorityAbility = null;
    int highestPriority = -1;

    foreach (var ability in abilities)
    {
        if (!ability) continue;
        if (ability.IsActive)
        {
            // If it's not disabled
            if (!disabledAbilityTypes.Contains(ability.GetType()))
            {
                if (ability.AnimationPriority > highestPriority)
                {
                    highestPriority = ability.AnimationPriority;
                    highestPriorityAbility = ability;
                }
            }
        }
    }

    int stateToPlay = 0;
    if (highestPriorityAbility != null)
    {
        stateToPlay = highestPriorityAbility.AnimationState;
    }

    // Only update the animator if the state changed
    if (animator && stateToPlay != _lastState)
    {
        animator.SetInteger("AbilityState", stateToPlay);
        _lastState = stateToPlay;
    }
}
        public void AddToFinalVelocity(Vector3 value)
        {
            _finalVelocity += value;
        }
        // === Input Callbacks ===
        public void OnMove(InputValue value)
        {
            Vector2 moveInput = value.Get<Vector2>();
            foreach (var ability in abilities)
            {
                if (ability == null) continue;
                ability.OnMoveInput(moveInput);
            }
        }

        public void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                foreach (var ability in abilities)
                {
                    if (ability == null) continue;
                    ability.OnJumpInput();
                }
            }
        }

        public void OnDash(InputValue value)
        {
            if (value.isPressed)
            {
                foreach (var ability in abilities)
                {
                    if (ability == null) continue;
                    ability.OnDashInput();
                }
            }
        }

        public void OnInteract(InputValue value)
        {
            if (value.isPressed)
            {
                foreach (var ability in abilities)
                {
                    if (ability == null) continue;
                    ability.OnInteractInput();
                }
            }
        }

        public void OnAimingMode(InputValue value)
        {
            // If it's performed, value.isPressed will be true
            if (value.isPressed)
            {
                // Pressed => start aiming in the AimingModeAbility
                foreach (var ability in abilities)
                {
                    if (ability is AimingModeAbility aimAbility)
                    {
                        aimAbility.StartAiming();
                    }
                }
            }
            else
            {
                // Released => stop aiming
                foreach (var ability in abilities)
                {
                    if (ability is AimingModeAbility aimAbility)
                    {
                        aimAbility.StopAiming();
                    }
                }
            }
        }
    }
}
