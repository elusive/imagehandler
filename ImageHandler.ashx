<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

public class ImageHandler : IHttpHandler {
    
	internal Image _bitmap = null;
    internal string _ctype = string.Empty;

    public void ProcessRequest (HttpContext context) {
    	
        // determine appropriate content type [jpeg only for now]
        context.Response.ContentType = "image/jpeg";
        
        // need to get the image
        string path = context.Request.PhysicalPath;
        _bitmap = Bitmap.FromFile(path);
        
        
        
        
        
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}