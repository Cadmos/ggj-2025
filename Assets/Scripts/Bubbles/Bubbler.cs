using System.Collections.Generic;
using GGJ.IoniExtensions;
using UnityEngine;

namespace GGJ.Bubbles
{
    public class Bubbler : MonoBehaviour
    {
        public List<GameObject> bubblePrefabs = new ();
        private GameObject _bubblePrefab;
        [SerializeField] private GameObject bubbleContainer;
        

        private void Start()
        {
            _bubblePrefab = bubblePrefabs.RandomItem();
        }

        public GameObject MakeBubble()
        {
            var newBubble = Instantiate(_bubblePrefab, transform.position, Quaternion.identity, bubbleContainer.transform);
            return newBubble;
        }
    }
}
