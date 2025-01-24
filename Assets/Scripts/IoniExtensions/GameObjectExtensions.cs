using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.IoniExtensions
{
    /// <summary>
    /// Extension methods for GameObjects
    /// </summary>
    public static class GameObjectExtensions
    {
        ///<summary>
        ///Gets the specified Component if it's attached or adds one to the GameObject if it's not.
        ///</summary>
        ///<param name="gameObject">The GameObject instance from which to retrieve or add the Component.</param>
        ///<typeparam name="T">The type of Component to retrieve or add.</typeparam>
        ///<returns>The Component of type T attached to the GameObject. If it was not initially present, it is added and then returned.</returns>
        ///<remarks>
        ///This method simplifies the process of ensuring a GameObject has a specific Component. 
        ///It retrieves the Component if it is already attached using the GetComponent method. 
        ///If the Component is not already present, this method adds it to the GameObject using the AddComponent method before returning it.
        ///</remarks>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            var component = gameObject.GetComponent<T>();
            if (component == null) component = gameObject.AddComponent<T>();
            return component;
        }
        
        ///<summary>
        ///Determines whether the GameObject has a specified Component attached.
        ///</summary>
        ///<param name="gameObject">The GameObject instance to check for the Component.</param>
        ///<typeparam name="T">The type of Component to check for.</typeparam>
        ///<returns>True if the GameObject has the specified Component attached; otherwise, false.</returns>
        ///<remarks>
        ///This method simplifies the process of checking if a GameObject has a specific Component. 
        ///It retrieves the Component using the GetComponent method and then checks if the result is not null.
        ///If the GetComponent method returns a non-null result, the GameObject has the Component, and the method returns true. 
        ///Otherwise, it returns false.
        ///</remarks>
        public static bool HasComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            return gameObject.GetComponent<T>() != null;
        }
        
        ///<summary>
        ///Sets the layer of this GameObject and all its children recursively.
        ///</summary>
        ///<param name="gameObject">The initial GameObject.</param>
        ///<param name="layer">The layer to set.</param>
        ///<remarks>
        ///This method sets the layer of the GameObject and also all of its children recursively. 
        ///It can be used when you want to change the layer of a GameObject and also all the GameObjects nested inside it.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the SetLayerRecursively() method:
        ///<code>
        /// GameObject myObject = GameObject.Find("MyObject");
        /// myObject.SetLayerRecursively(5);
        /// </code>
        ///In this example, 'myObject' and all its child GameObjects will have their layer changed to 5.
        ///</example>
        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetLayerRecursively(layer);
            }
        }
    }
}
