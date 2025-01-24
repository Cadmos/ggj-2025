using UnityEngine;

namespace GGJ
{
    public abstract class AbilityBase : ScriptableObject
    {
        protected GameObject owner;
        
        protected Transform cameraTransform;
        protected Transform orientationTransform;
        protected CharacterController controller;

        public virtual void Initialize(GameObject go, Transform camera, Transform orientation, CharacterController character)
        {
            owner = go;
            cameraTransform = camera;
            orientationTransform = orientation;
            controller = character;
        }
        
        public virtual void OnMoveInput(Vector2 moveInput) { }
        public virtual void OnJumpInput() { }
        public virtual void OnDashInput() { }
        public virtual void OnAttackInput() { }
        public virtual void OnInteractInput(){ }

        public virtual void UpdateAbility(float deltaTime) { }
    }
}
