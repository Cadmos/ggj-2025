using UnityEngine;

namespace GGJ.GameFlow
{
    [RequireComponent(typeof(Collider))]
    public class DeepestDeep : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("Player has reached the deepest deep");
                
            var player = other.GetComponent<Player>();
            player.GoToCheckpoint();
        }
    }
}
