using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vovinam.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeCreate { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public bool Seen { get; set; }
        public int CountNoti { get; set; }
        public bool Show { get; set; }

    }
}