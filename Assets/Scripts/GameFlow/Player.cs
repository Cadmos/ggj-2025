using System;
using GGJ.Bubbles;
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

        public void MoveWithBubble(Vector3 bubblePosition, float speedModifier = 1)
        {
            Vector3 offset = bubblePosition;
            offset.x -= transform.position.x;
            _controller.Move(offset * (speedModifier * Time.deltaTime));
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.CompareTag("Bubble"))
            {
                var elevatorBubble = hit.gameObject.GetComponent<ElevatorBubble>();
                
                if (elevatorBubble)
                {
                    elevatorBubble.player = this;
                }
            }
        }
    }
}
