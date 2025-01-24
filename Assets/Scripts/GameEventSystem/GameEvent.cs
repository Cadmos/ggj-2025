using System.Collections.Generic;
using UnityEngine;

namespace GGJ.GameEventSystem
{
    /// <summary>
    /// GameEvent
    /// 
    /// A scriptable object for global game events.
    /// </summary>
    [CreateAssetMenu(menuName = "Game Event", fileName = "New Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly HashSet<GameEventListener> _listeners = new ();
        
        /// <summary>
        /// Invoke
        ///
        /// Raises event for all the listeners registered to this GameEvent
        /// </summary>
        public void Invoke()
        {
            foreach (var globalEventListener in _listeners)
            {
                globalEventListener.RaiseEvent();
            }
        }

        
        /// <summary>
        /// ListenerCount - Returns the amount of listeners attached to this GameEvent
        /// </summary>
        public int ListenerCount => _listeners.Count;

        
        /// <summary>
        /// Register
        ///
        /// Apply a game event listener on this game event
        /// </summary>
        /// <param name="geListener">Game event listener to add</param>
        public void Register(GameEventListener geListener) => _listeners.Add(geListener);
        
        /// <summary>
        /// Deregister
        ///
        /// Remove a game event listener from this game event
        /// </summary>
        /// <param name="geListener">Game event listener to remove</param>
        public void Deregister(GameEventListener geListener) => _listeners.Remove(geListener);
    }
}
