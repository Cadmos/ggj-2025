using System;
using UnityEngine;

namespace GGJ.Bubbles
{
    [RequireComponent(typeof(Rigidbody))]
    public class BubbleBase : MonoBehaviour
    {
        [SerializeField] private float floatSpeed;
        [SerializeField] private Vector2 floatRange;

        private Rigidbody rb;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.linearVelocity = new Vector3(0, floatSpeed, 0);
        }
    }
}
