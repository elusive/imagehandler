using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Imaging.Filters
{
    /// <summary>
    /// Class to apply gamma filter to an image.
    /// </summary>
    public class GammaFilter : IFilter
    {
        private Bitmap _image;
        private double _gamma;
        private byte[] _table = new byte[256];

        #region IFilter Members

        public unsafe bool Apply(Bitmap srcBitmap, double value)
        {
        	// get the gamma value
            double g;
            g = Math.Max(0.1, Math.Min(5.0, value));
            g = 1 / g;

            _image = (Bitmap)srcBitmap.Clone();
		
			for ( int i = 0; i < 256; i++ )
            {
                _table[i] = (byte) Math.Min( 255, 
                	(int) ( Math.Pow( i / 255.0, 
                	g ) * 255 + 0.5 ) );
            }

			// GDI+ still lies to us - the return format is BGR, NOT RG_image.
			BitmapData bmData = _image.LockBits(new Rectangle(0, 0, _image.Width, _image.Height), 
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - _image.Width*3;

				for(int y=0;y<_image.Height;++y)
				{
					for(int x=0; x < _image.Width; ++x )
					{						
						*p = _table[*p];						
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
