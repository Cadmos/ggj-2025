using UnityEngine;

namespace GGJ
{
    public class Interactable : MonoBehaviour, IInteract
    {
        public void OnInteract(GameObject interactor)
        {
            Debug.Log($"{interactor.name} interacted with the Door: {name}");
            // Do door logic: open, close, etc.
        }
    }
}
