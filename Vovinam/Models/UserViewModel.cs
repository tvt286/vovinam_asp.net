using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vovinam.Data;
using Vovinam.Services;

namespace Vovinam.Models
{
    public class UserViewModel : SearchModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int[] GroupId { get; set; }
        public string Phone { get; set; }
        public UserStatus? Status { get; set; }
    }
}