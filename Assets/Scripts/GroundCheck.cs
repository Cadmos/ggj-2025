using System;
using UnityEngine;

namespace GGJ
{
    [RequireComponent(typeof(Collider))]
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private bool isGrounded = false;

        public bool IsGrounded => isGrounded;
        
        private void OnCollisionEnter(Collision other)
        {
            isGrounded = true;
        }

        private void OnCollisionExit(Collision other)
        {
            isGrounded = false;
        }
    }
}
