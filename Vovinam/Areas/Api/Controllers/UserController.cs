using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vovinam.Areas.Api.Common;
using Vovinam.Areas.Api.Services;

namespace Vovinam.Areas.Api.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public object Login(string user_name, string password)
        {
            return Json(UserService.Login(user_name, password));
        }

        [HttpPost]
        public object LockAccount(int user_id)
        {
            return Json(new JsonResult(ResultCode.Success, UserService.LockAccount(user_id)));
        }

        [HttpPost]
        public object ChangePassword(string user_name, string password_old, string password_new)
        {
            return Json(UserService.ChangePassword(user_name,password_old,password_new));
        }

	}
}