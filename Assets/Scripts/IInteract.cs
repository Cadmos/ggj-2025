using UnityEngine;

namespace GGJ
{
    public interface IInteract
    {
        // The 'interactor' is the GameObject that initiated the interaction (the player, typically).
        void OnInteract(GameObject interactor);
    }
}
