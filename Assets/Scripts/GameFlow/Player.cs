using System;
using GGJ.GameFlow.Spawning;
using UnityEngine;
using UnityEngine.Serialization;

namespace GGJ.GameFlow
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        [FormerlySerializedAs("activePlayerSpawn")] public PlayerSpawn startSpawn;
        public Checkpoint currentCheckpoint;
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
            Vector3 offset = spawnPointPosition - transform.position;
            _controller.Move(offset);
        }

        public void GoToCheckpoint()
        {
            if (currentCheckpoint == null)
            {
                Debug.LogWarning("No checkpoint assigned to the player");
                MoveTo(startSpawn.transform.position);
            }
            else
            {
                MoveTo(currentCheckpoint.SpawnPoint);
            }
        }
    }
}
