using UnityEngine;

namespace GGJ.IoniExtensions
{
    /// <summary>
    /// Common Extensions
    /// </summary>
    public static class CommonExtensions
    {
        ///<summary>
        ///Retrieves the value of the specified key from PlayerPrefs and deserializes it from JSON into an object of type T.
        ///</summary>
        ///<param name="prefs">The PlayerPrefs instance from which to retrieve the value.</param>
        ///<param name="key">The key of the preference to retrieve.</param>
        ///<typeparam name="T">The type of object to which the JSON string should be deserialized.</typeparam>
        ///<returns>Returns the deserialized object of type T, or default(T) if the key does not exist.</returns>
        ///<remarks>
        ///This is an extension method for the PlayerPrefs class. It allows direct invocation from an instance of PlayerPrefs.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the `GetJson` extension method:
        ///<code>
        ///YourType instance = prefs.GetJson<YourType>("YourKey");
        ///</code>
        ///</example>
        public static T GetJson < T > (this PlayerPrefs prefs, string key) {
            return JsonUtility.FromJson < T > (PlayerPrefs.GetString(key));
        }
    }
}
