using System;
using GGJ.GameFlow;
using UnityEngine;

namespace GGJ.Bubbles
{
    [RequireComponent(typeof(Rigidbody))]
    public class ElevatorBubble : BubbleBase, IBubble
    {
        public Bubbel bubbleHandler;
        private Player player;
        
        public void Pop()
        {
            bubbleHandler.PopBubble(this);    
        }
        
        private void Update()
        {
            if (player)
            {
                player.MoveWithBubble(transform.position);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            player = other.GetComponent<Player>();
            //player.AttachToBubble(this);
        }
    }
}
