using System;
using System.ComponentModel;
using GGJ.IoniExtensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GGJ.IoniDebug
{
    /// <summary>
    /// D - Logging
    ///
    /// Custom wrapper for default unity debug console. 
    /// </summary>
    public static class D
    {
        ///<summary>
        ///Formats a debug message with the provided prefix, GameObject, DebugLevel, and other message objects.
        ///</summary>
        ///<param name="prefix">The prefix to prepend to the message.</param>
        ///<param name="obj">The GameObject instance associated with the message.</param>
        ///<param name="dLevel">The desired DebugLevel for the message.</param>
        ///<param name="message">Additional message objects to include in the formatted message.</param>
        ///<returns>A string representing the formatted debug message.</returns>
        ///<remarks>
        ///The 'Message' method formats the debug message by firstly checking if a GameObject was provided and includes its name in the message.
        ///It then checks whether a prefix was provided and if it should be included in the message.
        ///The separator is determined based on whether a prefix or a GameObject name was included in the message.
        ///Finally, all the provided messages are joined together, separated by " | ", to constitute the complete message.
        ///</remarks>
        private static string Message(string prefix, Object obj, DebugLevel dLevel, params object[] message)
        {
            var objName = obj != null 
                ? $"<b>{obj.name.Color(dLevel.Color())}</b>" 
                : String.Empty;
            var prefixes = !String.IsNullOrEmpty(prefix) && obj != null
                ? $"{prefix.Color(dLevel.Color())}"
                : String.Empty;
            var separator = String.IsNullOrEmpty(objName) && String.IsNullOrEmpty(prefixes) ? String.Empty : ": ";
            var messages = string.Join(" | ", message);
            return $"{prefixes}{objName}{separator}{messages}";
        }
        
        ///<summary>
        ///Logs an informational debug message using Unity's Debug.Log(). The method's behavior changes depending on whether UNITY_EDITOR is defined.
        ///</summary>
        ///<param name="message">The message objects to include in the debug log.</param>
        ///<remarks>
        ///If UNITY_EDITOR is defined, this method calls UnityEngine.Debug.Log() with the informational debug message. 
        ///If UNITY_EDITOR is not defined, this method will not perform any action. The debug message is formatted using the 'Message' method, which joins all provided message objects into a single string.
        ///A prefix of "Info - " is added to the message, and the message is marked with DebugLevel.Info.
        ///This method is typically used for logging non-critical informational messages during editor runs.
        ///</remarks>
        public static void Info(params object[] message)
        {
#if UNITY_EDITOR
            Debug.Log(Message("Info - ", null, DebugLevel.Info, message)); 
#endif
        }

        ///<summary>
        ///Logs an informational debug message, with a specified context, using Unity's Debug.Log(). The method's behavior changes depending on whether UNITY_EDITOR is defined.
        ///</summary>
        ///<param name="context">The Object to which the informational message applies.</param>
        ///<param name="message">The message objects to include in the debug log.</param>
        ///<remarks>
        ///If UNITY_EDITOR is defined, this method calls UnityEngine.Debug.Log() with the informational debug message. 
        ///If UNITY_EDITOR is not defined, this method will not perform any action. The debug message is formatted using the 'Message' method, 
        ///which joins all provided message objects into a single string with the supplied context and a prefix of "Info - " is added to the message.
        ///The message is marked with DebugLevel.Info.
        ///This method is typically used for logging non-critical informational messages during editor runs to help with understanding the state of specific objects.
        ///</remarks>
        public static void Info(Object context, params object[] message)
        {
#if UNITY_EDITOR
            Debug.Log(Message("Info - ", context, DebugLevel.Info, message)); 
#endif
        }
        
        ///<summary>
        ///Logs an error debug message using Unity's Debug.LogError(). The method's behavior changes depending on whether UNITY_EDITOR is defined.
        ///</summary>
        ///<param name="message">The message objects to include in the debug log.</param>
        ///<remarks>
        ///If UNITY_EDITOR is defined, this method calls UnityEngine.Debug.LogError() with the error debug message. 
        ///If UNITY_EDITOR is not defined, this method will not perform any action. The debug message is formatted using the 'Message' method, 
        ///which joins all provided message objects into a single string.
        ///A prefix of "Error - " is added to the message, and the message is marked with DebugLevel.Err.
        ///This method is typically used for logging error messages during editor runs.
        ///</remarks>
        public static void Err(params object[] message)
        {
#if UNITY_EDITOR
            Debug.LogError(Message("Error - ", null, DebugLevel.Err, message)); 
#endif
        }
        
        ///<summary>
        ///Logs an error debug message, with a specified context, using Unity's Debug.LogError(). The method's behavior changes depending on whether UNITY_EDITOR is defined.
        ///</summary>
        ///<param name="context">The Object to which the error message applies.</param>
        ///<param name="message">The message objects to include in the debug log.</param>
        ///<remarks>
        ///If UNITY_EDITOR is defined, this method calls UnityEngine.Debug.LogError() with the error debug message. 
        ///If UNITY_EDITOR is not defined, this method will not perform any action. The debug message is formatted using the 'Message' method, 
        ///which joins all provided message objects into a single string with the supplied context.
        ///A prefix of "Error - " is added to the message, and the message is marked with DebugLevel.Err.
        ///This method is typically used for logging error messages during editor runs to help with identifying and understanding the state of specific objects.
        ///</remarks>
        public static void Err(Object context, params object[] message)
        {
#if UNITY_EDITOR
            Debug.LogError(Message("Error - ", context, DebugLevel.Err, message)); 
#endif
        }
        
        ///<summary>
        ///Logs a warning debug message using Unity's Debug.LogWarning(). The method's behavior changes depending on whether UNITY_EDITOR is defined.
        ///</summary>
        ///<param name="message">The message objects to include in the debug log.</param>
        ///<remarks>
        ///If UNITY_EDITOR is defined, this method calls UnityEngine.Debug.LogWarning() with the warning debug message. 
        ///If UNITY_EDITOR is not defined, this method will not perform any action. The debug message is formatted using the 'Message' method, 
        ///which joins all provided message objects into a single string.
        ///A prefix of "Warn - " is added to the message, and the message is marked with DebugLevel.Warn.
        ///This method is typically used for logging warning messages during editor runs.
        ///</remarks>
        public static void Warn(params object[] message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(Message("Warn - ", null, DebugLevel.Warn, message)); 
#endif
        }
        
        ///<summary>
        ///Logs a warning debug message, with a specified context, using Unity's Debug.LogWarning(). The method's behavior changes depending on whether UNITY_EDITOR is defined.
        ///</summary>
        ///<param name="context">The Object to which the warning message applies.</param>
        ///<param name="message">The message objects to include in the debug log.</param>
        ///<remarks>
        ///If UNITY_EDITOR is defined, this method calls UnityEngine.Debug.LogWarning() with the warning debug message. 
        ///If UNITY_EDITOR is not defined, this method will not perform any action. The debug message is formatted using the 'Message' method, 
        ///which joins all provided message objects into a single string with the supplied context.
        ///A prefix of "Warn - " is added to the message, and the message is marked with DebugLevel.Warn.
        ///This method is typically used for logging warning messages during editor runs to help with identifying possible issues or concerns with specific objects.
        ///</remarks>
        public static void Warn(Object context, params object[] message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(Message("Warn - ", context, DebugLevel.Warn, message)); 
#endif
        }
        
        ///<summary>
        ///Logs an Exception using Unity's Debug.LogException().
        ///</summary>
        ///<param name="exception">The exception to include in the debug log.</param>
        ///<remarks>
        ///This method calls UnityEngine.Debug.LogException() and logs the provided Exception. 
        ///This method is typically used for logging exceptions during runtime, enabling the tracking and debugging of errors in Unity-based applications.
        ///</remarks>
        public static void Exception(Exception exception) => Debug.LogException(exception);

        ///<summary>
        ///Logs an Exception, with a specified context, using Unity's Debug.LogException().
        ///</summary>
        ///<param name="exception">The exception to include in the debug log.</param>
        ///<param name="context">The Object to which the exception applies.</param>
        ///<remarks>
        ///This method calls UnityEngine.Debug.LogException() and logs the provided Exception along with the context. 
        ///The context can be useful to pinpoint where exactly the exception occurred, especially in complex scenarios with multiple objects.
        ///This method is typically used for logging exceptions during runtime, maintaining context to aid in the tracking and debugging of errors in Unity-based applications.
        ///</remarks>
        public static void Exception(Exception exception, Object context) => Debug.LogException(exception, context);
        
        
        ///<summary>
        ///Colors a text using Unity's rich text tags. If the supplied text is empty or null, returns an empty string.
        ///</summary>
        ///<param name="text">The string that needs to be colored.</param>
        ///<param name="color">The color value to be used for the text color.</param>
        ///<returns>A colored string if text is not null or empty; otherwise, returns an empty string.</returns>
        ///<remarks>
        ///This method uses Unity's rich text color tag to color a string. The color value should be passed as a string in any 
        ///format that Unity's color tag supports (named colors, hexadecimal, or RGBA).
        ///</remarks>
        private static string Color(this string text, string color)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<color={color}>{text}</color>";
        }
    }
    
    /// <summary>
    /// Debug Levels for D Library
    /// </summary>
    public enum DebugLevel
    {
        Info,
        Err,
        Warn,
        Exception
    }
    public static class DebugLevelExtensions
    {
        ///<summary>
        ///Maps DebugLevel values to specific color values defined as a hexadecimal StringColor, using the HexValue extension method.
        ///</summary>
        ///<param name="debugLevel">The DebugLevel enumeration value for which the color string is required.</param>
        ///<returns>A color in hexadecimal format associated with the provided DebugLevel value.</returns>
        ///<remarks>
        ///This method matches each DebugLevel enumeration value to a corresponding StringColor enumeration value,
        ///then retrieves the color's hexadecimal value using the HexValue extension method.
        ///Default color is Lightblue if the debug level does not match a predefined color.
        ///</remarks>
        public static string Color(this DebugLevel debugLevel)
        {
            return debugLevel switch
            {
                DebugLevel.Info => StringColor.Lightblue.HexValue(),
                DebugLevel.Err => StringColor.Red.HexValue(),
                DebugLevel.Warn => StringColor.Yellow.HexValue(),
                DebugLevel.Exception => StringColor.Red.HexValue(),
                _ => StringColor.Lightblue.HexValue(),
            };
        }
        
        ///<summary>
        ///Gets the hex value of a StringColor enum value, specified through a DescriptionAttribute. If no DescriptionAttribute is found, it returns the enumeration's name.
        ///</summary>
        ///<param name="environment">The StringColor instance for which the hex value is required.</param>
        ///<returns>The hexadecimal color value if the DescriptionAttribute is present on the enum field, otherwise, the name of the enumeration.</returns>
        ///<remarks>
        ///This method uses reflection to access the DescriptionAttribute of the provided StringColor enumeration value. 
        ///The description field of this attribute should be used to specify the hexadecimal value of the color represented by the enumeration.
        ///If no DescriptionAttribute is present, the method falls back to returning the enum value's name.
        ///</remarks>
        private static string HexValue(this StringColor environment) 
        {
            // get the field 
            var field = environment.GetType().GetField(environment.ToString());
            var customAttributes = field.GetCustomAttributes(typeof (DescriptionAttribute), false);

            if(customAttributes.Length > 0 
               && customAttributes[0] is DescriptionAttribute colorDescription)
            {
                return colorDescription.Description;
            }
            else
            {
                return environment.ToString(); 
            }
        }
    }
}