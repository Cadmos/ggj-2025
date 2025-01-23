using System;
using GGJ.GameEventSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace GGJ
{
    [RequireComponent(typeof(GroundCheckRaycast))]
    public class MovePlayer : MonoBehaviour
    {
        private Vector2 _movementChange;
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool shouldJump;
        [SerializeField] private float jump;

        [FormerlySerializedAs("_canJump")] [SerializeField] private bool canJump = false;
        private GroundCheckRaycast _groundCheck; 
        
        private void Start()
        {
            _groundCheck = GetComponent<GroundCheckRaycast>();
            _groundCheck.RegisterEvent(this.GetComponent<GameEventListener>());
            
        }

        public void OnMove(InputValue input)
        {
            _movementChange = input.Get<Vector2>();
        }

        public void OnJump(InputValue input)
        {
            canJump = false;
            if (input.isPressed)
            {
                shouldJump = true;
                return;
            }
            
            shouldJump = input.isPressed && _groundCheck.isGrounded;
        }

        public void OnPlayerGrounded()
        {
            Debug.Log("Grounded");
            if(_groundCheck.isGrounded) canJump = true;
            
        }

        private void Update()
        {
            var jumpChange = jump * moveSpeed;
            var trans = new Vector3(_movementChange.x, 0, _movementChange.y);
            
            if (canJump && shouldJump)
            {
                trans.y = jumpChange;
            }
            else
            {
                trans.y = 0;
            }
            
            transform.Translate(trans * (moveSpeed * Time.deltaTime));
        }
    }
}
