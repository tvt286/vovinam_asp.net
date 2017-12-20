using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vovinam.Data;


namespace Vovinam.WebBackend.Web
{
    public static class UrlHelperExtension
    {
        public static string ImageStudent(this UrlHelper url, Student student)
        {
            if (string.IsNullOrEmpty(student.Image))
            {
                return url.Content("~/Content/Upload/default.png");
            }
            return url.Content(student.Image);
        }

        public static string ImageUser(this UrlHelper url, User user)
        {
            if (string.IsNullOrEmpty(user.Image))
            {
                return url.Content("~/Content/Upload/default.png");
            }
            return url.Content(user.Image);
        }

        public static string ImageUserString(this UrlHelper url, string image)
        {
            if (string.IsNullOrEmpty(image))
            {
                return url.Content("~/Content/Upload/default.png");
            }
            return url.Content(image);
        }

        public static string ImageLevel(this UrlHelper url, Level level)
        {
            if (string.IsNullOrEmpty(level.Image))
            {
                return url.Content("~/Content/Upload/Company1/Level/tu_ve.png");
            }
            return url.Content(level.Image);
        }
    }
}