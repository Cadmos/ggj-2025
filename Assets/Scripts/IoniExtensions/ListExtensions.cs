using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.IoniExtensions
{
    /// <summary>
    /// Extension methods for Lists
    /// </summary>
    public static class ListExtensions 
    {
        ///<summary>
        ///Selects and returns a random item from the list.
        ///</summary>
        ///<param name="list">The list from which to pick a random item.</param>
        ///<typeparam name="T">The type of items in the list.</typeparam>
        ///<returns>A randomly selected item from the list.</returns>
        ///<remarks>
        ///This method uses Unity's Random.Range method to generate a random index within the bounds of the list,
        ///and then retrieves and returns the item at that index.
        ///</remarks>
        public static T RandomItem<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
        
        ///<summary>
        ///Removes and returns a random item from the list.
        ///</summary>
        ///<param name="list">The list from which to remove a random item.</param>
        ///<typeparam name="T">The type of items in the list.</typeparam>
        ///<returns>The item that was randomly selected and removed from the list.</returns>
        ///<exception cref="System.IndexOutOfRangeException">Thrown when attempting to remove an item from an empty list.</exception>
        ///<remarks>
        ///This method uses Unity's Random.Range method to generate a random index within the bounds of the list. 
        ///It then retrieves the item at that index, removes it from the list, and return the item.
        ///If the list is empty, an IndexOutOfRangeException is thrown.
        ///</remarks>
        public static T RemoveRandom<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }
        
        ///<summary>
        ///Shuffles the list in-place using the Fisher-Yates algorithm.
        ///</summary>
        ///<param name="list">The original list.</param>
        ///<remarks>
        ///This method uses the Fisher-Yates shuffle, also known as the Knuth shuffle, to randomize the order of elements in the list.
        ///The list is modified in-place which means it doesn't return a new list but rather modifies the original list.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the Shuffle() method:
        ///<code>
        /// IList<int> numbers = new List<int> {1, 2, 3, 4, 5};
        /// numbers.Shuffle();
        /// </code>
        ///After this call, the 'numbers' list will have its elements in a randomized order.
        ///</example>
        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new System.Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }   
}
