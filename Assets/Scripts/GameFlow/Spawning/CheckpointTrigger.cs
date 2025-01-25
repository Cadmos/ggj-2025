using UnityEngine;

namespace GGJ.GameFlow.Spawning
{
    public class CheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private Checkpoint checkpoint;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            var player = other.GetComponent<Player>();
            checkpoint.PlayerTriggered(player);
        }
    }
}
