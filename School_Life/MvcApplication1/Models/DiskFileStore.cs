using System;
using System.Web;
using AsyncUploaderDemo.Models;
using System.IO;
using System.Web.Hosting;

namespace AsyncUploaderDemo.Models
{
    internal class DiskFileStore : IFileStore
    {
        private string _uploadsFolder = HostingEnvironment.MapPath("~/Scripts/ckfinder/userfiles/_thumbs/Images/");

        public Guid SaveUploadedFile(HttpPostedFileBase fileBase)
        {
            var identifier = Guid.NewGuid();
            fileBase.SaveAs(_uploadsFolder+identifier+suffix(fileBase.FileName));
            return identifier;
        }

        private string GetDiskLocation(Guid identifier)
        {
            return Path.Combine(_uploadsFolder, identifier.ToString());
        }

        private string suffix(string filename)
        {
            string[] names = filename.Split('.');

            return "."+names[1].ToString();
        }
    }
}