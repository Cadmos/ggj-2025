using System;
using GGJ.GameFlow;
using UnityEngine;

namespace GGJ.Bubbles
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class BubbleTrigger : MonoBehaviour
    {
        private CapsuleCollider _collider;
        [SerializeField] private Player player;
        
        private void Awake()
        {
            _collider = GetComponent<CapsuleCollider>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("BubbleTrigger: OnTriggerEnter");
            if (!other.CompareTag("Bubble")) return;
            var bubble = other.GetComponent<ElevatorBubble>();
            bubble.player = player;
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("BubbleTrigger: OnTriggerExit");
            if (!other.CompareTag("Bubble")) return;
            var bubble = other.GetComponent<ElevatorBubble>();
            bubble.player = null;
        }
    }
}
