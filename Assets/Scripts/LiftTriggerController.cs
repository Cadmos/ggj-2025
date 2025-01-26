using System;
using UnityEngine;

namespace GGJ
{
    public class LiftTriggerController : MonoBehaviour
    {
        [Tooltip("Speed at which the character is lifted (meters per second).")]
        public float liftSpeed = 2f;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("lift");
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("Lift triggered");
            // Check if the object that entered the trigger has a CharacterController
            AbilityManager cc = other.GetComponent<AbilityManager>();
            CharacterController cc2 = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                // Move the character upwards every frame they stay in the trigger
                Vector3 lift = Vector3.up * (liftSpeed * Time.deltaTime);
                cc.AddToFinalVelocity(lift);
                cc2.Move(lift);
            }
        }
    }
}