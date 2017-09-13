using System;
using System.Web;
using System.IO;
using Vovinam.Services;

namespace Vovinam.WebBackend.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
        public string GroupID { set; get; }

        //public static UserSession Session()
        //{
        //    var obj = HttpContext.Current.Session[Constant.SessionUser];
        //    if (obj == null)
        //    {
        //        var user = UserService.GetUserByUsername(HttpContext.Current.User.Identity.GetUserName());
        //        if (user == null)
        //        {
        //            FormsAuthentication.SignOut();
        //            return new UserSession();
        //        }
        //        var session = new UserSession();
        //        session.Id = user.Id;
        //        session.Permission = user.Groups.SelectMany(x => x.Group_Permission.Select(y => y.PermissionId)).Distinct().ToList();
        //        session.Permission.AddRange(user.User_Permission.Select(x => x.PermissionId));

        //        session.PathImage = HttpUtility.UrlEncode(user.Image);
        //        HttpContext.Current.Session[Constant.SessionUser] = session;
        //        return session;
        //    }
        //    else
        //    {
        //        return obj as UserSession;
        //    }
        //}

        public static string Avatar()
        {
            var image = UserService.GetUserInfo() != null ? UserService.GetUserInfo().Image : "";
            if (string.IsNullOrEmpty(image) || !File.Exists(HttpContext.Current.Server.MapPath(string.Format("~/Content/UserImage/{0}", HttpUtility.UrlEncode(image)))))
            {
               return "/Content/Upload/noimage.png";
            }
            //return $"/Content/Upload/{HttpUtility.UrlEncode(image)}";
            return string.Format("/Content/UserImage/{0}", HttpUtility.UrlEncode(image));
        }
        public static string DowloadFile()
        {
            var image = UserService.GetUserInfo() != null ? UserService.GetUserInfo().Image : "";
            if (string.IsNullOrEmpty(image) || !File.Exists(HttpContext.Current.Server.MapPath(string.Format("~/Content/Upload/{0}", HttpUtility.UrlEncode(image)))))
            {
                return "/Content/Upload/noimage.png";
            }
            return string.Format("/Content/Upload/{0}", HttpUtility.UrlEncode(image));
        }

    }

}