using System.Collections;
using System.Collections.Generic;
using GGJ.IoniExtensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace GGJ.Bubbles
{
    public class Bubbel: MonoBehaviour
    {
        [SerializeField] private int maxBubbles;
        [SerializeField] private List<GameObject> bubbles = new();
        [SerializeField] private List<Bubbler> bubblers = new();
        [SerializeField] private float spawnInterval = 2.0f; // Interval in seconds

        private void Start()
        {
            StartCoroutine(SpawnBubbles());
        }

        private IEnumerator SpawnBubbles()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);
                MakeBubble();
            }
        }

        public void MakeBubble()
        {
            if (bubbles.Count >= maxBubbles)
            {
                Debug.Log("Bubble count is at max");
                return;
            }
            
            Debug.Log($"Making a new bubble <3");
            var newBubble = bubblers.RandomItem().MakeBubble();
            newBubble.GetComponent<ElevatorBubble>().bubbleHandler = this;
            bubbles.Add(newBubble); 
            Debug.Log($"Bubble count is now: {maxBubbles}");
        }

        public void PopBubble(ElevatorBubble bubble)
        {
            if (bubbles.Count <= 0)
            {
                Debug.Log("No bubbles to pop");
                return;
            }
            
            if(bubble == null)
            {
                Debug.Log("Bubble could not be popped. It was null");
                return;
            }
            
            Debug.Log($"Popping a bubble :(");
            
            bubbles.Remove(bubble.gameObject);
            Destroy(bubble.gameObject);
            Debug.Log($"Bubble count is now: {maxBubbles}");
        }
    }
}
