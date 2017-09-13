using System.Configuration;
using System.IO;
using System.Web;
using Vovinam.Common.Enums;


namespace Vovinam.WebBackend.Web
{

    public class UploadHelper
    {
        public static UploadFileStatus CheckImageUpload(HttpPostedFileBase httpPostedFileBase)
        {
            const string extension = "jpge,jpg,png,gif,bmp";
            const int limitedLength = 4 * 1024;

            if (httpPostedFileBase == null) return UploadFileStatus.NotFile;
            if (httpPostedFileBase.ContentLength == 0) return UploadFileStatus.NotFile;

            var ext = Path.GetExtension(httpPostedFileBase.FileName).ToLower();
            ext = ext.Replace(".", "");
            if (extension.IndexOf(ext) < 0)
            {
                return UploadFileStatus.NotSupportExtension;
            }
            var overLimited = httpPostedFileBase.ContentLength > limitedLength * 1024;
            if (overLimited)
            {
                return UploadFileStatus.OverLimited;
            }

            return UploadFileStatus.Success;
        }

        public static UploadFileStatus CheckUploadSubmissionFile(HttpPostedFileBase httpPostedFileBase)
        {
            const string extension = "jpge,jpg,png,gif,bmp,doc,docx,xls,xlsx,pdf";
            const int limitedLength = 10 * 1024;

            if (httpPostedFileBase == null) return UploadFileStatus.NotFile;
            if (httpPostedFileBase.ContentLength == 0) return UploadFileStatus.NotFile;

            var ext = Path.GetExtension(httpPostedFileBase.FileName).ToLower();
            ext = ext.Replace(".", "");
            if (extension.IndexOf(ext) < 0)
            {
                return UploadFileStatus.NotSupportExtension;
            }
            var overLimited = httpPostedFileBase.ContentLength > limitedLength * 1024;
            if (overLimited)
            {
                return UploadFileStatus.OverLimited;
            }

            return UploadFileStatus.Success;
        }
        
    }
}