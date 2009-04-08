using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Imaging.Filters
{
    public class BrightnessFilter : IFilter
    {
        private Bitmap _image;

        #region IFilter Members

        public bool Apply(System.Drawing.Bitmap srcBitmap, double value)
        {
            if (value < -255 || value > 255) return false;

            _image = (Bitmap)srcBitmap.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RG_image.
            BitmapData bmData = _image.LockBits(new Rectangle(0, 0, _image.Width, _image.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            int nVal = 0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - _image.Width * 3;
                int nWidth = _image.Width * 3;

                for (int y = 0; y < _image.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nVal = (int)(p[0] + value);

                        if (nVal < 0) nVal = 0;
                        if (nVal > 255) nVal = 255;

                        p[0] = (byte)nVal;

                        ++p;
                    }
                    p += nOffset;
                }
            }

            _image.UnlockBits(bmData);

            return true;
        }

        public System.Drawing.Bitmap GetResult()
        {
            return _image;
        }

        #endregion
    }
}
