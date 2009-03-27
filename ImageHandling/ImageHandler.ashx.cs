using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Services;
using Imaging;

namespace ImageHandling
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ImageHandler : IHttpHandler
    {
        internal Bitmap _bitmap = null;

        public void ProcessRequest(HttpContext context)
        {
            // reset bitmap
            _bitmap = null;

            // determine appropriate content type [jpeg only for now]
            context.Response.ContentType = "image/jpeg";

            if (context.Request.QueryString.Count > 0)
            {
                // need to get the image
                string path = context.Server.MapPath(context.Request.Url.AbsolutePath);
                _bitmap = new Bitmap(path);

                // need to get the filter name and value
                foreach (var key in context.Request.QueryString.Keys)
                {
                    int val;
                    if (Int32.TryParse(context.Request[key.ToString()], out val))
                    {
                        string filterName = key.ToString();
                        this.ProcessFilter(filterName, val);
                    }
                }

                _bitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                //context.Response.Write(_bitmap);
            }
            else
            {
                context.Response.WriteFile(context.Request.Url.AbsolutePath);
            }
        }

        private void ProcessFilter(string filterName, int filterVal)
        {
            IFilter f;

            switch (filterName.ToLower())
            {
                case "contrast":
                    f = new Imaging.Filters.ContrastFilter();
                    if (f.Apply(_bitmap, (sbyte)filterVal))
                        _bitmap = f.GetResult();
                    break;
                default:
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
