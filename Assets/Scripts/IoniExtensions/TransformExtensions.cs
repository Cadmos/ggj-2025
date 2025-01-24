using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.IoniExtensions
{
    /// <summary>
    /// Extension methods for Transforms
    /// </summary>
    public static class TransformExtensions 
    {
        ///<summary>
        ///Destroys all children of a Transform.
        ///</summary>
        ///<param name="transform">The Transform whose children will be destroyed.</param>
        ///<remarks>
        ///This method iterates through each child of the given Transform and destroys them. 
        ///It can be used to clean up a Transform by removing all of its child objects.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the DestroyChildren() method:
        ///<code>
        ///transform.DestroyChildren();
        ///</code>
        ///The above code will destroy all child objects of the "transform" instance.
        ///</example>
        public static void DestroyChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }
        
        ///<summary>
        ///Resets the position, rotation, and scale of a Transform.
        ///</summary>
        ///<param name="transform">The Transform to be reset.</param>
        ///<remarks>
        ///This method sets the position of the Transform to the origin (0,0,0), 
        ///its local rotation to the identity quaternion (no rotation), 
        ///and its scale to one, effectively resetting any transformations applied to it.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the ResetTransformation() method:
        ///<code>
        /// transform.ResetTransformation();
        ///</code>
        ///The above code will reset the position, rotation, and scale of the "transform" instance.
        ///</example>
        public static void ResetTransformation(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        ///<summary>
        ///Returns the number of children the Transform has.
        ///</summary>
        ///<param name="transform">The original Transform.</param>
        ///<returns>The child count of the Transform.</returns>
        ///<remarks>
        ///This method returns the number of children the Transform has. It can be useful when you need to iterate over the children of a Transform or check if a Transform has any children.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the ChildCount() method:
        ///<code>
        /// Transform parentTransform = gameObject.transform;
        /// int childCount = parentTransform.ChildCount();
        /// </code>
        ///In this example, 'childCount' will hold the number of children the 'parentTransform' has.
        ///</example>
        public static int ChildCount(this Transform transform)
        {
            return transform.childCount;
        }
        
        ///<summary>
        ///Generates a random point inside the given collider's bounding box.
        ///</summary>
        ///<param name="collider">The Collider within which a random point will be generated.</param>
        ///<returns>A Vector3 representing a random point inside the bounds of the given collider.</returns>
        ///<remarks>
        ///This method creates a random point inside the Collider's minimum and maximum extents to generate a random position. 
        ///Note that this method does not guarantee that the point will be inside the Collider if it has a complex shape. 
        ///It will be inside the bounding box of the Collider.
        ///</remarks>
        ///<example>
        ///This is an example showing how to use the 'RandomPointInside()' method:
        ///<code>
        /// Collider collider;
        /// // assume 'collider' has been initialized
        /// Vector3 randomPoint = collider.RandomPointInside();
        ///</code>
        ///In this example, 'randomPoint' will be a random point inside the bounding box of 'collider'.
        ///</example>
        public static Vector3 RandomPointInside(this Collider collider)
        {
            var bounds = collider.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
        
        ///<summary>
        ///Generates a random point inside the given Bounds.
        ///</summary>
        ///<param name="bounds">The Bounds within which a random point will be generated.</param>
        ///<returns>A Vector3 representing a random point inside the given bounds.</returns>
        ///<remarks>
        ///This method creates a random point inside the Bounds by generating a random position on each axis between the minimum and maximum extents of the Bounds.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the RandomPointInside() method:
        ///<code>
        /// Bounds bounds;
        /// // assume 'bounds' has been initialized
        /// Vector3 randomPoint = bounds.RandomPointInside();
        ///</code>
        ///In this example, 'randomPoint' will be a Vector3 representing a random point inside 'bounds'.
        ///</example>
        public static Vector3 RandomPointInside(this Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}
