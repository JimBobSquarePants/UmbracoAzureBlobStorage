using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace idseefeld.de.UmbracoAzure
{
    public class ABFSstaticFileModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication app)
        {
            app.EndRequest += new EventHandler(context_EndRequest);
            app.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            var url = app.Request.CurrentExecutionFilePath;// app.Request.Url.AbsoluteUri.ToLower();
            if (url.StartsWith("/media/"))
            {
                var filePath = app.Server.MapPath(url);
                if (!File.Exists(filePath))
                {
                    //ToDo: check querystring and process with ImageProcessor.Web.HttpModules.ImageProcessingModule
                    GetFileFromBlobStorage(app);
                    app.CompleteRequest();
                }
            }
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            if (app.Response.StatusCode == 404)
            {
                GetFileFromBlobStorage(app);
            }
        }

        private static void GetFileFromBlobStorage(HttpApplication app)
        {
            string path = app.Request.Path;
            string ext = app.Request.CurrentExecutionFilePathExtension;
            try
            {
                string mimeType = MimeMapping.GetMimeMapping(ext);
                AzureBlobFileSystem azureBlobFs = new AzureBlobFileSystem();
                using (var blobStream = azureBlobFs.OpenFile(path))
                {
                    if (blobStream.CanSeek)
                    {
                        blobStream.Seek(0, SeekOrigin.Begin);
                    }
                    blobStream.CopyTo(app.Response.OutputStream);
                    app.Response.ContentType = mimeType;
                    app.Response.StatusCode = 200;
                }
            }
            catch
            {
                app.Response.StatusCode = 404;
                app.Response.StatusDescription = "File not found";
            }
        }

    }
}
