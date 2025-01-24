using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ.IoniExtensions
{
    /// <summary>
    /// Extension methods for Scenes
    /// </summary>
    public static class SceneExtensions
    {
        ///<summary>
        ///Finds a root GameObject in the provided Scene by its name.
        ///</summary>
        ///<param name="scene">The Scene to search in.</param>
        ///<param name="name">The name of the GameObject to find.</param>
        ///<returns>The root GameObject with the provided name, or `null` if no such GameObject is found.</returns>
        ///<remarks>
        ///This method searches only among the root GameObjects in the Scene. It does not find child GameObjects.
        ///If multiple root GameObjects have the same name, it will return the first one encountered in the order they are stored in the Scene.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the RootObjectByName() method:
        ///<code>
        /// Scene currentScene = SceneManager.GetActiveScene();
        /// GameObject soughtObject = currentScene.RootObjectByName("DesiredObjectName");
        /// </code>
        ///In this example, 'soughtObject' will hold the root GameObject with the name "DesiredObjectName", if such a GameObject exists in 'currentScene'.
        ///</example>
        public static GameObject RootObjectByName(this Scene scene, string name)
        {
            return scene.GetRootGameObjects().FirstOrDefault(go => go.name == name);
        }
    }

}