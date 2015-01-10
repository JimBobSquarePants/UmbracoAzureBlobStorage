using System;
using System.IO;
using System.Web;

namespace idseefeld.de.UmbracoAzure
{
    public class IISHandlerUmbracoAzureStaticFiles : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var path = context.Request.Url.AbsolutePath;

            //context.Response.Write("image");

            string ext = null;
            try
            {
                ext = path.Substring(path.LastIndexOf('.')).ToLower();
                var filePath = context.Server.MapPath(path);
                if (!File.Exists(filePath))
                {
                    filePath = context.Server.MapPath("/media/1042/logo.jpg");
                }
                using (var fs = File.OpenRead(filePath))
                {
                    string mimeType = MimeMapping.GetMimeMapping(ext);

                    context.Response.ContentType = mimeType;
                    fs.CopyTo(context.Response.OutputStream);
                }
            }
            catch
            {
                //context.Response.StatusCode = 404;
                //context.Response.StatusDescription = "File not found";
            }
        }

        #endregion
    }
}
