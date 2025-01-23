using UnityEngine.UI;

namespace GGJ.IoniExtensions
{
    /// <summary>
    /// Image Extensions
    /// </summary>
    public static class ImageExtensions
    {
        ///<summary>
        ///Sets the alpha value of the provided Image's color.
        ///</summary>
        ///<param name="img">The Image whose color's alpha value is to be set.</param>
        ///<param name="value">The value to which the alpha should be set. Value should range from 0 (completely transparent) to 1 (completely opaque).</param>
        ///<remarks>
        ///This is an extension method for the Image class. Hence, can be invoked directly from an instance of Image.
        ///</remarks>
        ///<example>
        ///Here is an example of how to use the `SetAlpha` extension method:
        ///<code>
        ///Image imageInstance;
        ///...
        ///imageInstance.SetAlpha(0.5f); // sets the alpha of the imageInstance color to 0.5
        ///</code>
        ///</example>
        public static void SetAlpha(this Image img , float value)
        {
            var col = img.color;
            col.a = value;
            img.color = col;
        }
    }
}
