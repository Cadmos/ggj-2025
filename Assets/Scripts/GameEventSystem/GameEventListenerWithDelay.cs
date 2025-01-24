using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ.GameEventSystem
{
    /// <summary>
    /// GameEventListenerWithDelay
    /// Extended game event listener that can be configured with delay
    /// Delay is handled by a coroutine
    /// </summary>
    public class GameEventListenerWithDelay : GameEventListener
    {
        [SerializeField] private float delay = 1f; 
        [SerializeField] private UnityEvent delayedUnityEvent;

        private void Awake() => listenEvent.Register(this);
        private void OnDestroy() => listenEvent.Deregister(this);
        
        /// <summary>
        /// Invoke ActEvent after waiting the delay 
        /// </summary>
        public override void RaiseEvent()
        {
            actEvent.Invoke();
            if(debuggingEnabled) Debug.Log("Unity event was triggered, invoking on delay: " + delay);
            StartCoroutine(RunDelayedEvent());
        }

        private IEnumerator RunDelayedEvent()
        {
            yield return new WaitForSeconds(delay);
            delayedUnityEvent.Invoke();
        }
    }
}
