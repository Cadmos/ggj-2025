using System;
using GGJ.GameEventSystem;
using UnityEngine;

namespace GGJ
{
    public class GroundCheckRaycast : MonoBehaviour
    {
        public bool isGrounded = false;
        public float groundCheckDistance;
        public float checkDistance = 0.1f;
        private CapsuleCollider _capsuleCollider;
        [SerializeField] private GameEvent onGroundedEvent;
        
        private void Start()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
        }

        public void RegisterEvent(GameEventListener gel)
        {
            onGroundedEvent.Register(gel);
        }

        private void SetIsGrounded(bool grounded)
        {
            var wasGrounded = isGrounded;
            isGrounded = grounded;

            if (wasGrounded != grounded)
            {
                onGroundedEvent.Invoke();
            }
        }
        
        private void Update()
        {
            groundCheckDistance = (_capsuleCollider.height / 2) + checkDistance;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance))
            {
                SetIsGrounded(true);
            }
            else
            {
                SetIsGrounded(false);
            }
        }
    }
}
