using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vovinam.Areas.Api.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string user_name { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string image { get; set; }
        public bool is_admin_root { get; set; }
        public bool is_admin_company { get; set; }

        public  List<UserPermissionModel> user_permission { get; set; }
        public  List<GroupModel> group { get; set; }

    }
}