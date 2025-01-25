using UnityEngine;
using UnityEngine.Serialization;

namespace GGJ.GameFlow.Spawning
{
    public class Checkpoint : MonoBehaviour
    {
        [FormerlySerializedAs("spawnPoint")] [SerializeField] private CheckpointSpawn spawn;
        public void PlayerTriggered(Player player)
        {
            player.currentCheckpoint = this;
        }
        
        public Vector3 SpawnPoint => spawn.SpawnPoint;
    }
}
