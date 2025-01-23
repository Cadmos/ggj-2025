using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using GGJ.IoniDebug;
using Object = UnityEngine.Object;

namespace GGJ.IoniExtensions
{
    /// <summary>
    /// Common string extensions
    /// </summary>
    public static class StringExtensions
    {
        ///<summary>
        ///Encloses a string with bold HTML tags.
        ///</summary>
        ///<param name="text">The string to format as bold.</param>
        ///<returns>A string enclosed in HTML tags for bold ("b") if the input is not null or empty; otherwise, an empty string.</returns>
        ///<remarks>
        ///This method will wrap the provided string in the HTML "b" tag, which will format the text as bold in HTML contexts. 
        ///If the input string is null or empty, it returns an empty string.
        ///</remarks>
        public static string B(this string text)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<b>{text}</b>";
        }
        
        ///<summary>
        ///Encloses a string with bold HTML tags.
        ///</summary>
        ///<param name="text">The string to format as bold.</param>
        ///<returns>A string enclosed in HTML tags for bold ("b") if the input is not null or empty; otherwise, an empty string.</returns>
        ///<remarks>
        ///This method makes a call to the 'B' extension method, hence, it will wrap the provided string in the HTML "b" tag, which will format the text as bold in HTML contexts. 
        ///If the input string is null or empty, it returns an empty string.
        ///</remarks>
        public static string Bold(this string text)
        {
            return B(text);
        }
        
        ///<summary>
        ///Encloses a string with italic HTML tags.
        ///</summary>
        ///<param name="text">The string to format as italic.</param>
        ///<returns>A string enclosed in HTML tags for italic ("i") if the input is not null or empty; otherwise, an empty string.</returns>
        ///<remarks>
        ///This method will wrap the provided string in the HTML "i" tag, which will format the text as italic in HTML contexts. 
        ///If the input string is null or empty, it returns an empty string.
        ///</remarks>
        public static string I(this string text)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<i>{text}</i>";
        }
        
        ///<summary>
        ///Encloses a string with italic HTML tags.
        ///</summary>
        ///<param name="text">The string to format as italic.</param>
        ///<returns>A string enclosed in HTML tags for italic ("i") if the input is not null or empty; otherwise, an empty string.</returns>
        ///<remarks>
        ///This method makes a call to the 'I' extension method, hence, it will wrap the provided string in the HTML "i" tag, which will format the text as italic in HTML contexts. 
        ///If the input string is null or empty, it returns an empty string.
        ///</remarks>
        public static string Italic(this string text)
        {
            return I(text);
        }

        ///<summary>
        ///Encloses a string with color HTML tags.
        ///</summary>
        ///<param name="text">The string to color.</param>
        ///<param name="color">The color value to apply to the string text.</param>
        ///<returns>A string enclosed in HTML color tags with the specified color value if the input text is not null or empty; otherwise, an empty string.</returns>
        ///<remarks>
        ///This method will wrap the provided string in the HTML "color" tag, which will format the text with the specified color in HTML contexts. 
        ///If the input string or color is null or empty, it returns an empty string.
        ///</remarks>
        public static string Color(this string text, string color)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<color={color}>{text}</color>";
        }
        
        ///<summary>
        ///Encloses a string with color HTML tags, using a `StringColor` instance.
        ///</summary>
        ///<param name="text">The string to color.</param>
        ///<param name="color">The `StringColor` instance expressing a color value to apply to the string text.</param>
        ///<returns>A string enclosed in HTML color tags with the specified color value if the input text is not null or empty; otherwise, an empty string.</returns>
        ///<remarks>
        ///This method will wrap the provided string in the HTML "color" tag, which will format the text with the specified color in HTML contexts. 
        ///The color is defined by the `StringColor` parameter. If the input string is null or empty, it returns an empty string.
        ///`StringColor` is expected to have a method named `HexValue` that returns a string representing a color in hexadecimal.
        ///</remarks>
        public static string Color(this string text, StringColor color)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<color={color.HexValue()}>{text}</color>";
        }

        ///<summary>
        ///Encloses a string with size HTML tags to reflect the provided font size.
        ///</summary>
        ///<param name="text">The string whose font size is to be modified.</param>
        ///<param name="size">The font size to apply to the string text.</param>
        ///<returns>A string enclosed in HTML size tags with the specified font size if the input text is not null or empty; otherwise, an empty string.</returns>
        ///<remarks>
        ///This method will wrap the provided string in the HTML "size" tag, which will affect the font size of the text in HTML contexts. 
        ///If the input string is null or empty, it returns an empty string.
        ///</remarks>
        public static string Size(this string text, int size)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<size={size}>{text}</size>";
        }
        
        ///<summary>
        ///Gets the hexadecimal color value associated with a StringColor enumeration value.
        ///</summary>
        ///<param name="environment">The StringColor value to retrieve the hexadecimal color for.</param>
        ///<returns>A string representing the hexadecimal color associated with the StringColor enumeration value.</returns>
        ///<remarks>
        ///This method retrieves the Description Attribute of the specified StringColor enumeration value. 
        ///If the Description Attribute is present, its value (which should represent a hexadecimal color) is returned.
        ///If there is no Description Attribute, or if it is not of type DescriptionAttribute, the method returns the name of the enumeration value.
        ///</remarks>
        public static string HexValue(this StringColor environment) 
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
        
        ///<summary>
        ///Logs a string using the Debug Information method `D.Info`.
        ///</summary>
        ///<param name="text">The string to be logged.</param>
        ///<remarks>
        ///This method outputs the provided string to the debug console using `D.Info` method. 
        ///It serves as a quick logging mechanism within the application context.
        ///</remarks>
        public static void Log(this string text)
        {
            D.Info(text);
        }

        ///<summary>
        ///Logs a string with an associated context using the Debug Information method `D.Info`.
        ///</summary>
        ///<param name="text">The string to be logged.</param>
        ///<param name="context">The context object associated with the logging information.</param>
        ///<remarks>
        ///This method outputs the provided string along with its context to the debug console using `D.Info` method. 
        ///It serves as a quick logging mechanism within the application context, providing additional context to the logged message.
        ///</remarks>
        public static void Log(this string text, Object context)
        {
            D.Info(context, text);
        }
        
        ///<summary>
        ///Logs a string as information using the Debug Information method `D.Info`.
        ///</summary>
        ///<param name="text">The string to be logged as information.</param>
        ///<remarks>
        ///This method outputs the provided string to the debug console as informational data using `D.Info` method. 
        ///It serves as a quick informational logging mechanism within the application context.
        ///</remarks>
        public static void Info(this string text)
        {
            D.Info(text);
        }

        ///<summary>
        ///Logs a string as information with an associated context using the Debug Information method `D.Info`.
        ///</summary>
        ///<param name="text">The string to be logged as information.</param>
        ///<param name="context">The context object associated with the informational log.</param>
        ///<remarks>
        ///This method outputs the provided string along with its context to the debug console as informational data using `D.Info` method. 
        ///It serves as a quick informational logging mechanism within the application context, providing additional context to the logged information.
        ///</remarks>
        public static void Info(this string text, Object context)
        {
            D.Info(context, text);
        }
        
        ///<summary>
        ///Logs a string as an error using the Debug Error method `D.Err`.
        ///</summary>
        ///<param name="text">The string to be logged as an error.</param>
        ///<remarks>
        ///This method outputs the provided string to the debug console as an error using the `D.Err` method. 
        ///It serves as a quick error logging mechanism within the application context.
        ///</remarks>
        public static void Err(this string text)
        {
            D.Err(text);
        }
        
        ///<summary>
        ///Logs a string as an error with an associated context using the Debug Error method `D.Err`.
        ///</summary>
        ///<param name="text">The string to be logged as an error.</param>
        ///<param name="context">The context object associated with the error log.</param>
        ///<remarks>
        ///This method outputs the provided string along with its context to the debug console as an error using `D.Err` method. 
        ///It serves as a quick error logging mechanism within the application context, providing additional context to the logged error.
        ///</remarks>
        public static void Err(this string text, Object context)
        {
            D.Err(context, text);
        }
        
        ///<summary>
        ///Logs a string as a warning using the Debug Warning method `D.Warn`.
        ///</summary>
        ///<param name="text">The string to be logged as a warning.</param>
        ///<remarks>
        ///This method outputs the provided string to the debug console as a warning using the `D.Warn` method.
        ///It serves as a quick warning logging mechanism within the application context.
        ///</remarks>
        public static void Warn(this string text)
        {
            D.Warn(text);
        }
        
        ///<summary>
        ///Logs a string as a warning with an associated context using the Debug Warning method `D.Warn`.
        ///</summary>
        ///<param name="text">The string to be logged as a warning.</param>
        ///<param name="context">The context object associated with the warning log.</param>
        ///<remarks>
        ///This method outputs the provided string along with its context to the debug console as a warning using `D.Warn` method. 
        ///It serves as a quick warning logging mechanism within the application context, providing additional context to the logged warning.
        ///</remarks>
        public static void Warn(this string text, Object context)
        {
            D.Warn(context, text);
        }
        
        ///<summary>
        ///Prepends a given prefix to the string.
        ///</summary>
        ///<param name="text">The string to which the prefix will be added.</param>
        ///<param name="prefix">The prefix to be added to the string. Defaults to an empty string if not provided.</param>
        ///<returns>A new string consisting of the prefix and the original string.</returns>
        ///<remarks>
        ///This method returns a new string that starts with the provided prefix followed by the original string. 
        ///If no prefix is provided, it returns the original string.
        ///</remarks>
        public static string WithPrefix(this string text, string prefix = "")
        {
            return $"{prefix}{text}";
        }
        
        ///<summary>
        ///Appends a given suffix to the string.
        ///</summary>
        ///<param name="text">The string to which the suffix will be added.</param>
        ///<param name="suffix">The suffix to be added to the string. Defaults to an empty string if not provided.</param>
        ///<returns>A new string consisting of the original string followed by the suffix.</returns>
        ///<remarks>
        ///This method returns a new string that consists of the original string followed by the provided suffix. 
        ///If no suffix is provided, it returns the original string.
        ///</remarks>
        public static string WithSuffix(this string text, string suffix = "")
        {
            return $"{text}{suffix}";
        }
        
        ///<summary>
        ///Truncates the string to the specified maximum length.
        ///</summary>
        ///<param name="value">The string instance.</param>
        ///<param name="maxLength">The maximum length of the result string. If this is greater than or equal to the length of the string, the original string is returned.</param>
        ///<returns>A string that is equivalent to the substring of length 'maxLength' starting from the beginning of the instance; or the instance itself, if maxLength is greater than or equal to the string length or the string is null or empty.</returns>
        ///<remarks>
        ///Use this method when you need to limit the length of a string, such as when presenting a preview of a text or limiting user input.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the Truncate() method:
        ///<code>
        /// string original = "Hello, World!";
        /// string truncated = original.Truncate(5);
        /// </code>
        ///In this example, 'truncated' will be a string with value "Hello".
        ///</example>
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        
        ///<summary>
        ///Converts the value of this instance to a string using the specified format.
        ///</summary>
        ///<param name="anObject">The object instance.</param>
        ///<param name="aFormat">A format string containing formatting specifications.</param>
        ///<returns>A string representation of the current object, formatted as specified by 'aFormat'.</returns>
        ///<remarks>
        ///This method uses reflection to try properties and then fields. If a format is not found for the property or field, it is reinserted into the final string.
        ///If there is no format provider given, it defaults to null.
        ///</remarks>
        ///<exception cref="System.Reflection.TargetInvocationException">Thrown when the property or field does not have a "ToString" method.</exception>
        ///<example>
        ///This is a usage example. Please replace 'YourObjectName' and 'YourFormat' with the actual parameters:
        ///<code>
        /// object yourObject = new YourObjectName();
        /// string formatted = yourObject.ToString("YourFormat");
        /// </code>
        ///And other example more suit in many cases:
        /// <code>
        /// string blaStr = aPerson.ToString("My name is {FirstName} {LastName}.")
        /// </code>
        ///</example>
        public static string ToString(this object anObject, string aFormat)
        {
            return ToString(anObject, aFormat, null);
        }
        
        ///<summary>
        ///Converts the value of this instance to a string using the specified format.
        ///</summary>
        ///<param name="anObject">The object instance.</param>
        ///<param name="aFormat">A format string containing formatting specifications.</param>
        ///<param name="formatProvider">An IFormatProvider that provides culture-specific formatting.</param>
        ///<returns>A string representation of the current object, formatted as specified by 'aFormat' and 'formatProvider'.</returns>
        ///<remarks>
        ///This method uses reflection to try properties and then fields. If a format is not found for the property or field, it is reinserted into the final string.
        ///</remarks>
        ///<exception cref="System.Reflection.TargetInvocationException">Thrown when the property or field does not have a "ToString" method.</exception>
        ///<example>
        ///This is a usage example. Please replace 'YourObjectName' and 'YourFormat' with the actual parameters:
        ///<code>
        /// object yourObject = new YourObjectName();
        /// string formatted = yourObject.ToString("YourFormat", CultureInfo.InvariantCulture);
        /// </code>
        ///</example>
        public static string ToString(this object anObject, string aFormat, IFormatProvider formatProvider)
        {
            var sb = new StringBuilder();
            var type = anObject.GetType();
            var reg = new Regex(@"({)([^}]+)(})", RegexOptions.IgnoreCase);
            var mc = reg.Matches(aFormat);
            var startIndex = 0;
            foreach (Match m in mc)
            {
                var g = m.Groups[2]; //it's second in the match between { and }
                var length = g.Index - startIndex - 1;
                sb.Append(aFormat.Substring(startIndex, length));

                var toGet = string.Empty;
                var toFormat = string.Empty;
                var formatIndex = g.Value.IndexOf(":"); //formatting would be to the right of a :
                if (formatIndex == -1) //no formatting, no worries
                {
                    toGet = g.Value;
                }
                else //pickup the formatting
                {
                    toGet = g.Value.Substring(0, formatIndex);
                    toFormat = g.Value.Substring(formatIndex + 1);
                }

                //first try properties
                var retrievedProperty = type.GetProperty(toGet);
                Type retrievedType = null;
                object retrievedObject = null;
                if (retrievedProperty != null)
                {
                    retrievedType = retrievedProperty.PropertyType;
                    retrievedObject = retrievedProperty.GetValue(anObject, null);
                }
                else //try fields
                {
                    var retrievedField = type.GetField(toGet);
                    if (retrievedField != null)
                    {
                        retrievedType = retrievedField.FieldType;
                        retrievedObject = retrievedField.GetValue(anObject);
                    }
                }

                if (retrievedType != null) //Cool, we found something
                {
                    var result = string.Empty;
                    if (toFormat == string.Empty) //no format info
                    {
                        result = retrievedType.InvokeMember("ToString",
                            BindingFlags.Public | BindingFlags.NonPublic |
                            BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                            , null, retrievedObject, null) as string;
                    }
                    else //format info
                    {
                        result = retrievedType.InvokeMember("ToString",
                            BindingFlags.Public | BindingFlags.NonPublic |
                            BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                            , null, retrievedObject, new object[] { toFormat, formatProvider }) as string;
                    }
                    sb.Append(result);
                }
                else //didn't find a property with that name, so be gracious and put it back
                {
                    sb.Append("{");
                    sb.Append(g.Value);
                    sb.Append("}");
                }
                startIndex = g.Index + g.Length + 1;
            }
            if (startIndex < aFormat.Length) //include the rest (end) of the string
            {
                sb.Append(aFormat.Substring(startIndex));
            }
            return sb.ToString();
        }
        
        /// <summary>
        /// Converts the object to a string representation in a log format.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <returns>A string representation of the object in a log format.</returns>
        /// <remarks>
        /// This method converts the object to a string representation by iterating through its fields and formatting them
        /// as field name-value pairs enclosed in square brackets.
        /// </remarks>
        /// <example>
        /// <code>
        /// class OwnTest
        /// {
        ///     string name = "Ioni";
        ///     int age = 25;
        /// }
        /// 
        /// class Program
        /// {
        ///     static void Main(string[] args)
        ///     {
        ///         OwnTest obj = new OwnTest();
        ///         Console.WriteLine(obj.ToLogFormat());
        ///     }
        /// }
        /// </code>
        /// The output will be: <c>[name: Ioni, age: 25]</c>
        /// </example>
        public static string ToLogFormat<T>(this T obj)
        {
            Type type = obj.GetType();
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            bool isFirstField = true;

            foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!isFirstField)
                {
                    sb.Append(", ");
                }

                sb.Append(field.Name);
                sb.Append(": ");
                sb.Append(field.GetValue(obj));

                isFirstField = false;
            }

            sb.Append("]");

            return sb.ToString();
        }
    }
}