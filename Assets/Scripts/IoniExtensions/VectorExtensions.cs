using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.IoniExtensions
{
    /// <summary>
    /// Extension methods for Vectors
    /// </summary>
    public static class VectorExtensions
    {
        ///<summary>
        ///Returns a new Vector3 with the 'x' value replaced with the given value.
        ///</summary>
        ///<param name="vector">The original Vector3.</param>
        ///<param name="x">The new value for the 'x' component.</param>
        ///<returns>A new Vector3 with the updated 'x' value and the same 'y' and 'z' values as the original Vector3.</returns>
        ///<remarks>
        ///This method creates a new Vector3 with the same 'y' and 'z' components as the original Vector3, but replaces the 'x' value with the provided one.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the WithX() method:
        ///<code>
        /// Vector3 originalVector = new Vector3(1, 1, 1);
        /// Vector3 newVector = originalVector.WithX(2);
        /// </code>
        ///In this example, 'newVector' will be a Vector3 with its 'x', 'y', and 'z' components as 2, 1, and 1 respectively.
        ///</example>
        public static Vector3 WithX(this Vector3 vector, float x) {
            return new Vector3(x, vector.y, vector.z);
        }

        ///<summary>
        ///Returns a new Vector3 with the 'y' value replaced with the given value.
        ///</summary>
        ///<param name="vector">The original Vector3.</param>
        ///<param name="y">The new value for the 'y' component.</param>
        ///<returns>A new Vector3 with the updated 'y' value and the same 'x' and 'z' values as the original Vector3.</returns>
        ///<remarks>
        ///This method creates a new Vector3 with the same 'x' and 'z' components as the original Vector3, but replaces the 'y' value with the provided one.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the WithY() method:
        ///<code>
        /// Vector3 originalVector = new Vector3(1, 1, 1);
        /// Vector3 newVector = originalVector.WithY(2);
        /// </code>
        ///In this example, 'newVector' will be a Vector3 with its 'x', 'y', and 'z' components as 1, 2, and 1 respectively.
        ///</example>
        public static Vector3 WithY(this Vector3 vector, float y) {
            return new Vector3(vector.x, y, vector.z);
        }
        
        ///<summary>
        ///Returns a new Vector3 with the 'z' value replaced with the given value.
        ///</summary>
        ///<param name="vector">The original Vector3.</param>
        ///<param name="z">The new value for the 'z' component.</param>
        ///<returns>A new Vector3 with the updated 'z' value and the same 'x' and 'y' values as the original Vector3.</returns>
        ///<remarks>
        ///This method creates a new Vector3 with the same 'x' and 'y' components as the original Vector3, but replaces the 'z' value with the provided one.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the WithZ() method:
        ///<code>
        /// Vector3 originalVector = new Vector3(1, 1, 1);
        /// Vector3 newVector = originalVector.WithZ(2);
        /// </code>
        ///In this example, 'newVector' will be a Vector3 with its 'x', 'y', and 'z' components as 1, 1, and 2 respectively.
        ///</example>
        public static Vector3 WithZ(this Vector3 vector, float z) {
            return new Vector3(vector.x, vector.y, z);
        }
        
        ///<summary>
        ///Creates a new Vector3 from a Vector2 with the 'z' value set to the given value.
        ///</summary>
        ///<param name="vector">The original Vector2.</param>
        ///<param name="z">The new value for the 'z' component.</param>
        ///<returns>A new Vector3 with 'x' and 'y' values from Vector2 and the 'z' value set to the provided one.</returns>
        ///<remarks>
        ///This method creates a new Vector3, taking the 'x' and 'y' components from the original Vector2 and setting the 'z' value to the provided one.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the WithZ() method:
        ///<code>
        /// Vector2 originalVector = new Vector2(1, 1);
        /// Vector3 newVector = originalVector.WithZ(2);
        /// </code>
        ///In this example, 'newVector' will be a Vector3 with its 'x', 'y', and 'z' components as 1, 1, and 2 respectively.
        ///</example>
        public static Vector3 WithZ(this Vector2 vector, float z) {
            return new Vector3(vector.x, vector.y, z);
        }
        
        ///<summary>
        ///Returns a new Vector2 with the 'x' value replaced with the given value.
        ///</summary>
        ///<param name="vector">The original Vector2.</param>
        ///<param name="x">The new value for the 'x' component.</param>
        ///<returns>A new Vector2 with the updated 'x' value and the same 'y' value as the original Vector2.</returns>
        ///<remarks>
        ///This method creates a new Vector2 with the same 'y' component as the original Vector2, but replaces the 'x' value with the provided one.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the WithX() method:
        ///<code>
        /// Vector2 originalVector = new Vector2(1, 1);
        /// Vector2 newVector = originalVector.WithX(2);
        /// </code>
        ///In this example, 'newVector' will be a Vector2 with its 'x' and 'y' components as 2 and 1 respectively.
        ///</example>
        public static Vector2 WithX(this Vector2 vector, float x)
        {
            return new Vector2(x, vector.y);
        }
        
        ///<summary>
        ///Returns a new Vector2 with the 'x' value augmented by the given value.
        ///</summary>
        ///<param name="vector">The original Vector2.</param>
        ///<param name="x">The value to add to the 'x' component.</param>
        ///<returns>A new Vector2 with the 'x' value increased by the given amount and the same 'y' value as the original Vector2.</returns>
        ///<remarks>
        ///This method creates a new Vector2, adding the provided 'x' value to the 'x' component of the original Vector2 while preserving the 'y' component.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the AddX() method:
        ///<code>
        /// Vector2 originalVector = new Vector2(1, 1);
        /// Vector2 newVector = originalVector.AddX(2);
        /// </code>
        ///In this example, 'newVector' will be a Vector2 with its 'x' and 'y' components as 3 and 1 respectively.
        ///</example>
        public static Vector2 AddX(this Vector2 vector, float x)
        {
            return new Vector2(vector.x + x, vector.y);
        }
        
        ///<summary>
        ///Returns a new Vector2 with the 'y' value replaced with the given value.
        ///</summary>
        ///<param name="vector">The original Vector2.</param>
        ///<param name="y">The new value for the 'y' component.</param>
        ///<returns>A new Vector2 with the updated 'y' value and the same 'x' value as the original Vector2.</returns>
        ///<remarks>
        ///This method creates a new Vector2 with the same 'x' component as the original Vector2, but replaces the 'y' value with the provided one.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the WithY() method:
        ///<code>
        /// Vector2 originalVector = new Vector2(1, 1);
        /// Vector2 newVector = originalVector.WithY(2);
        /// </code>
        ///In this example, 'newVector' will be a Vector2 with its 'x' and 'y' components as 1 and 2 respectively.
        ///</example>
        public static Vector2 WithY(this Vector2 vector, float y)
        {
            return new Vector2(vector.x, y);
        }
        
        ///<summary>
        ///Returns a new Vector2 with the 'y' value augmented by the given value.
        ///</summary>
        ///<param name="vector">The original Vector2.</param>
        ///<param name="y">The value to add to the 'y' component.</param>
        ///<returns>A new Vector2 with the 'y' value increased by the given amount and the same 'x' value as the original Vector2.</returns>
        ///<remarks>
        ///This method creates a new Vector2, adding the provided 'y' value to the 'y' component of the original Vector2 while preserving the 'x' component.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the AddY() method:
        ///<code>
        /// Vector2 originalVector = new Vector2(1, 1);
        /// Vector2 newVector = originalVector.AddY(2);
        /// </code>
        ///In this example, 'newVector' will be a Vector2 with its 'x' and 'y' components as 1 and 3 respectively.
        ///</example>
        public static Vector2 AddY(this Vector2 vector, float y)
        {
            return new Vector2(vector.x, vector.y + y);
        }
        
        ///<summary>
        ///Creates a new vector with the X-coordinate flattened (set to zero).
        ///</summary>
        ///<param name="vector">The Vector3 instance.</param>
        ///<returns>A new Vector3 where the X-coordinate is zero, but the Y and Z coordinates remain the same.</returns>
        ///<remarks>
        ///Use this method when you want to ignore the X-coordinate of a Vector3, such as when operating in a 2D plane or when you want to ignore horizontal movement.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the FlattenX() method:
        ///<code>
        /// Vector3 original = new Vector3(1,2,3);
        /// Vector3 flattened = original.FlattenX();
        /// </code>
        ///In this example, 'flattened' will be a new Vector3 with values (0,2,3).
        ///</example>
        public static Vector3 FlattenX(this Vector3 vector)
        {
            return new Vector3(0f, vector.y, vector.z);
        }
        
        ///<summary>
        ///Creates a new vector with the Y-coordinate flattened (set to zero).
        ///</summary>
        ///<param name="vector">The Vector3 instance.</param>
        ///<returns>A new Vector3 where the Y-coordinate is zero, but the X and Z coordinates remain the same.</returns>
        ///<remarks>
        ///Use this method when you want to ignore the Y-coordinate of a Vector3, such as when operating in a 2D plane or when you want to ignore vertical movement.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the FlattenY() method:
        ///<code>
        /// Vector3 original = new Vector3(1,2,3);
        /// Vector3 flattened = original.FlattenY();
        /// </code>
        ///In this example, 'flattened' will be a new Vector3 with values (1,0,3).
        ///</example>
        public static Vector3 FlattenY(this Vector3 vector)
        {
            return new Vector3(vector.x, 0f, vector.z);
        }
        
        ///<summary>
        ///Creates a new vector with the Z-coordinate flattened (set to zero).
        ///</summary>
        ///<param name="vector">The Vector3 instance.</param>
        ///<returns>A new Vector3 where the Z-coordinate is zero, but the X and Y coordinates remain the same.</returns>
        ///<remarks>
        ///Use this method when you want to ignore the Z-coordinate of a Vector3, such as when operating in a 2D plane or when you want to ignore depth movement.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the FlattenZ() method:
        ///<code>
        /// Vector3 original = new Vector3(1,2,3);
        /// Vector3 flattened = original.FlattenZ();
        ///</code>
        ///In this example, 'flattened' will be a new Vector3 with values (1,2,0).
        ///</example>
        public static Vector3 FlattenZ(this Vector3 vector)
        {
            return new Vector3(vector.x, vector.y, 0f);
        }
        
        ///<summary>
        ///Creates a new Vector2 excluding the X-coordinate from the original Vector3.
        ///</summary>
        ///<param name="vector">The Vector3 instance.</param>
        ///<returns>A new Vector2 formed from the Y and Z coordinates of the given Vector3.</returns>
        ///<remarks>
        ///Use this method when you want to ignore the X-coordinate of a Vector3 and create a Vector2 such as when operating in a 2D plane.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the WithoutX() method:
        ///<code>
        /// Vector3 original = new Vector3(1,2,3);
        /// Vector2 result = original.WithoutX();
        /// </code>
        ///In this example, 'result' will be a new Vector2 with values (2,3).
        ///</example>
        public static Vector2 WithoutX(this Vector3 vector)
        {
            return new Vector2(vector.y, vector.z);
        }
        
        ///<summary>
        ///Creates a new Vector2 excluding the Y-coordinate from the original Vector3.
        ///</summary>
        ///<param name="vector">The Vector3 instance.</param>
        ///<returns>A new Vector2 composed from the X and Z coordinates of the given Vector3.</returns>
        ///<remarks>
        ///Use this method when you want to ignore the Y-coordinate of a Vector3 and create a Vector2 such as when operating in a 2D plane.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the WithoutY() method:
        ///<code>
        /// Vector3 original = new Vector3(1, 2, 3);
        /// Vector2 result = original.WithoutY();
        /// </code>
        ///In this example, 'result' will be a new Vector2 with values (1, 3).
        ///</example>
        public static Vector2 WithoutY(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }
        
        ///<summary>
        ///Creates a new Vector2 excluding the Z-coordinate from the original Vector3.
        ///</summary>
        ///<param name="vector">The Vector3 instance.</param>
        ///<returns>A new Vector2 composed from the X and Y coordinates of the given Vector3.</returns>
        ///<remarks>
        ///Use this method when you want to ignore the Z-coordinate of a Vector3 and create a Vector2 such as when processing 2D data.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the WithoutZ() method:
        ///<code>
        /// Vector3 original = new Vector3(1,2,3);
        /// Vector2 result = original.WithoutZ();
        /// </code>
        ///In this example, 'result' will be a new Vector2 with values (1,2).
        ///</example>
        public static Vector2 WithoutZ(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
    }
}

