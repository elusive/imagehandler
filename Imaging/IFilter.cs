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
    public interface IFilter
    {
        /// <summary>
        /// Apply filter to image.
        /// </summary>
        /// <param name="image">Source image to apply filter to.</param>
        /// <param name="value">Value used in applying filter.</param>
        /// <returns>Success indicator, true for success, else false.</returns>
        bool Apply(Bitmap image, sbyte value);

        /// <summary>
        /// Method to return the destination bitmap that contains
        /// the results of the filter operation.
        /// </summary>
        /// <returns>Bitmap that has been filtered.</returns>
        Bitmap GetResult();
    }
}
