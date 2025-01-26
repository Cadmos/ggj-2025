using UnityEngine;
using System;
using System.Collections.Generic;

namespace GGJ
{
    public abstract class AbilityBase : ScriptableObject
    {
        protected GameObject owner;
        protected Transform cameraTransform;
        protected Transform orientationTransform;
        protected CharacterController controller;
        protected Animator animator;
        
        // If we need reference back to manager
        protected AbilityManager manager;

        public virtual bool IsActive { get; protected set; } = true;
        public virtual List<Type> DisableAbilitiesWhileActive => new List<Type>();
        
        /// <summary>
        /// Which animator state to represent when this ability is active.
        /// </summary>
        public virtual int AnimationState => 0;  // e.g. default idle

        /// <summary>
        /// Higher means this ability takes precedence if multiple are active.
        /// </summary>
        public virtual int AnimationPriority => 0;

        // UPDATED SIGNATURE:
        public virtual void Initialize(GameObject go,
            Transform camera,
            Transform orientation,
            CharacterController character,
            Animator characterAnimator,
            AbilityManager abilityManager = null)
        {
            owner = go;
            cameraTransform = camera;
            orientationTransform = orientation;
            controller = character;
            manager = abilityManager;
            animator = characterAnimator;
        }

        public virtual void UpdateAbility(float deltaTime) { }
        public virtual Vector3 GetDesiredVelocity(float deltaTime) => Vector3.zero;

        public virtual void OnMoveInput(Vector2 moveInput) { }
        public virtual void OnJumpInput() { }
        public virtual void OnDashInput() { }
        public virtual void OnAttackInput() { }
        public virtual void OnInteractInput() { }
        
        public virtual void OnAimingModeInput() {}
    }
}