using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(fileName = "InteractAbility", menuName = "Scriptable Objects/Character/Abilities/Interact")]
    public class InteractAbility : AbilityBase
    {
        [Header("Interact Settings")]
        public float interactRange = 2f;
        public LayerMask interactableLayers;
        // Optional: if you have multiple layers or a single 'Interactable' layer

        public override void OnInteractInput()
        {
            base.OnInteractInput();

            // We attempt to interact when the 'Interact' input is received (e.g., "E" key, or button)
            AttemptInteract();
        }

        private void AttemptInteract()
        {
            if (owner == null) return;

            // For simplicity, do a raycast or sphere overlap in front of the character
            Transform ownerTransform = owner.transform;
            Vector3 startPosition = ownerTransform.position;
            Vector3 forward = ownerTransform.forward;

            // For example, a small sphere overlap or ray
            RaycastHit hit;
            if (Physics.Raycast(startPosition, forward, out hit, interactRange, interactableLayers))
            {
                // If we hit something, check if it has IInteract
                IInteract interactable = hit.collider.GetComponent<IInteract>();
                if (interactable != null)
                {
                    interactable.OnInteract(owner);
                }
            }
            else
            {
                // Optionally, you can do a Physics.OverlapSphere if you'd rather do an area check
                // or check an object in front in a small radius.
            }
        }
    }
}
