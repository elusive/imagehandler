/*
 *  DATE:  24 MARCH 2009
 *  AUTHOR:  John C. Gilliand
 *  FILENAME: IFilter.cs
 *  
 *  Interface for image processing filters.
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Imaging
{
    /// <summary>
    /// Interface for image processing filters. 
    /// </summary>
    public class IFilter
    {
        /// <summary>
        /// Apply filter to image data.
        /// </summary>
        /// <param name="imageData">Source image data to apply filter to.</param>
        /// <returns>Resulting bitmap image with filter applied.</returns>
        public Bitmap Apply(BitmapData imageData);

        /// <summary>
        /// Apply filter to image.
        /// </summary>
        /// <param name="image">Source image to apply filter to.</param>
        /// <returns>Resulting bitmap image with filter applied.</returns>
        public Bitmap Apply(Bitmap image);
    }
}
