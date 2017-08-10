using System;
using System.Web;

namespace AsyncUploaderDemo.Models
{
    public interface IFileStore
    {
        Guid SaveUploadedFile(HttpPostedFileBase fileBase);
    }
}