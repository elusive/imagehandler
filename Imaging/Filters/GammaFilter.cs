using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Imaging.Filters
{
    /// <summary>
    /// Class to apply gamma filter to an image.
    /// <remarks>Rather than take in 3 separate values for RGB a 
    /// single value is used for all three.  The acceptable range 
    /// for the values is 0.2 thru 5.0</remarks>
    /// </summary>
    public class GammaFilter : IFilter
    {
        private Bitmap _image;

        #region IFilter Members

        public unsafe bool Apply(Bitmap srcBitmap, sbyte value)
        {
        	// use same gamma value for r, g, & b
        	double red = (double)value;
            double green = red;
            double blue = green;
        	
            if (red < .2 || red > 5) return false;
			if (green < .2 || green > 5) return false;
			if (blue < .2 || blue > 5) return false;

			byte [] redGamma = new byte [256];
			byte [] greenGamma = new byte [256];
			byte [] blueGamma = new byte [256];

			for (int i = 0; i< 256; ++i)
			{
				redGamma[i] = (byte)Math.Min(255, (int)(( 255.0 * Math.Pow(i/255.0, 1.0/red)) + 0.5));
				greenGamma[i] = (byte)Math.Min(255, (int)(( 255.0 * Math.Pow(i/255.0, 1.0/green)) + 0.5));
				blueGamma[i] = (byte)Math.Min(255, (int)(( 255.0 * Math.Pow(i/255.0, 1.0/blue)) + 0.5));
			}

			// GDI+ still lies to us - the return format is BGR, NOT RG_image.
			BitmapData bmData = _image.LockBits(new Rectangle(0, 0, _image.Width, _image.Height), 
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;

				int nOffset = stride - _image.Width*3;

				for(int y=0;y<_image.Height;++y)
				{
					for(int x=0; x < _image.Width; ++x )
					{
						p[2] = redGamma[ p[2] ];
						p[1] = greenGamma[ p[1] ];
						p[0] = blueGamma[ p[0] ];

						p += 3;
					}
					p += nOffset;
				}
			}

			_image.UnlockBits(bmData);

			return true;
        }


        /// <summary>
        /// Returns results of filter operation.
        /// </summary>
        /// <returns>Filtered bitmap.</returns>
        public Bitmap GetResult()
        {
            return _image;
        }

        #endregion
    }
}
