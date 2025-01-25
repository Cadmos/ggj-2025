using UnityEngine;

namespace GGJ.GameFlow.Spawning
{
    public class PlayerSpawn : MonoBehaviour
    {
        private Player _player;
        private Transform _spawnPoint;

        private void Start()
        {
            var spawnPoint = transform;
            _spawnPoint = spawnPoint;
            FindAndAssignPlayer();
            MovePlayerToSpawn();
        }
        
        private void FindAndAssignPlayer()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        
        private void MovePlayerToSpawn()
        {
            _player.MoveTo(_spawnPoint.position);
            //Instantiate(playerPrefab, _spawnPoint.position, Quaternion.identity);
        }
    }
}
