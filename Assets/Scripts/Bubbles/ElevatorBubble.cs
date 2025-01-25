using System;
using GGJ.GameFlow;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ.Bubbles
{
    public class ElevatorBubble : BubbleBase, IBubble
    {
        public Bubbel bubbleHandler;
        [SerializeField] private Player player;
        [SerializeField] private Vector2 floatSpeedRange;
        
        public void Pop()
        {
            bubbleHandler.PopBubble(this);    
        }
        
        private void Update()
        {
            FloatUp();
            if (!player) return;
            var speedModifier = Random.Range(floatSpeedRange.x, floatSpeedRange.y);
            player.MoveWithBubble(transform.position, speedModifier);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            player = other.GetComponent<Player>();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            player = null;
        }
    }
}
