using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using GGJ.IoniDebug;
using UnityEngine;
    
namespace GGJ.IoniExtensions
{
    /// <summary>
    /// Common MonoBehaviour extensions
    /// </summary>
    public static class MonoBehaviourExtensions
    {
        ///<summary>
        ///Tries to get a component of type T from the MonoBehaviour, and logs an error if no such component is found.
        ///</summary>
        ///<param name="mono">The MonoBehaviour from which the component will be retrieved.</param>
        ///<returns>The component of type T if it exists; otherwise, null.</returns>
        ///<typeparam name="T">The type of the Component to retrieve.</typeparam>
        ///<remarks>This method attempts to retrieve a component of the specified type from the MonoBehaviour. If such a component does not exist, it will log an error message.</remarks>
        ///<example>
        ///This is an example of how to use the SafeGetComponent&lt;T&gt;() method:
        ///<code>
        /// Rigidbody rb = someMonoBehaviour.SafeGetComponent&lt;Rigidbody&gt;();
        /// </code>
        ///In this example, the method tries to retrieve a Rigidbody component from 'someMonoBehaviour'. 
        ///If the Rigidbody does not exist, it will log an error message. 
        ///In either case, 'rb' will be assigned the returned Rigidbody (which may be null).
        ///</example>
        public static T SafeGetComponent<T>(this MonoBehaviour mono) where T : Component
        {
            T comp = mono.GetComponent<T>();
            if (comp == null)
            {
                D.Err("No " + typeof(T).Name + " found on " + mono.name);
            }
            return comp;
        }
        
        ///<summary>
        ///Tries to get a component of type T from the children of the given MonoBehaviour, and logs an error if no such component is found.
        ///</summary>
        ///<param name="mono">The MonoBehaviour from which the component will be retrieved.</param>
        ///<returns>The component of type T if it exists; otherwise, null.</returns>
        ///<typeparam name="T">The type of the Component to retrieve.</typeparam>
        ///<remarks>
        ///This method attempts to retrieve a component of the specified type from the children of the given MonoBehaviour. If such a component does not exist, it logs an error message.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the SafeGetComponentInChildren&lt;T&gt;() method:
        ///<code>
        /// Rigidbody rb = someMonoBehaviour.SafeGetComponentInChildren&lt;Rigidbody&gt;();
        /// </code>
        ///In this example, the method will try to retrieve a Rigidbody component from the children of 'someMonoBehaviour'. If it does not exist, it will log an error, and 'rb' will be assigned null.
        ///</example>
        public static T SafeGetComponentInChildren<T>(this MonoBehaviour mono) where T : Component
        {
            T comp = mono.GetComponentInChildren<T>();
            if (comp == null)
            {
                D.Err("No " + typeof(T).Name + " found on " + mono.name);
            }
            return comp;
        }
    }
}