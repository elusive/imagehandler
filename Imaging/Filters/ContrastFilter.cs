using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Imaging.Filters
{
    public class ContrastFilter : IFilter
    {
        private Bitmap _image;

        #region IFilter Members

        public unsafe bool Apply(Bitmap srcBitmap, sbyte value)
        {
            if (value < -100) return false;
            if (value > 100) return false;

            _image = (Bitmap)srcBitmap.Clone();

            double pixel = 0, contrast = (100.0 + value) / 100.0;

            contrast *= contrast;

            int red, green, blue;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), 
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - image.Width * 3;

                for (int y = 0; y < image.Height; ++y)
                {
                    for (int x = 0; x < image.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        pixel = red / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[2] = (byte)pixel;

                        pixel = green / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[1] = (byte)pixel;

                        pixel = blue / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[0] = (byte)pixel;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            image.UnlockBits(bmData);

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
