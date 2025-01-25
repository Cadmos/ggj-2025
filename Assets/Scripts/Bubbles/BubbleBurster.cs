using System;
using UnityEngine;

namespace GGJ.Bubbles
{
    public class BubbleBurster : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Bubble")) return;
            var bubble = other.GetComponent<ElevatorBubble>();
            bubble.Pop();
        }
    }
}
