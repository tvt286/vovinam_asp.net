using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vovinam.Areas.Api.Models
{
    public class UserPermissionModel
    {
        public int user_id { get; set; }
        public int permission_id { get; set; }
    }
}