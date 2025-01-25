using System;
using GGJ.GameFlow.Spawning;
using UnityEngine;

namespace GGJ.GameFlow
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        public PlayerSpawn activePlayerSpawn;
        private CharacterController _controller;
        
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            //Debug.Log($"Player is touching ground: {_controller.isGrounded}. From side: {_controller.collisionFlags}");
        }

        public void MoveTo(Vector3 spawnPointPosition)
        {
            transform.position = spawnPointPosition;
        }

        public void GoToCheckpoint()
        {
            MoveTo(activePlayerSpawn.transform.position);
        }
    }
}
