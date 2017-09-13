using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vovinam.Data;

namespace Vovinam.Areas.Api.Models
{
    public class StudentModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string hometown { get; set; }
        public Nullable<System.DateTime> date_in { get; set; }
        public string image { get; set; }
        public string gender { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public bool tuition1 { get; set; }
        public bool tuition2 { get; set; }
        public bool tuition3 { get; set; }
        public bool tuition4 { get; set; }
        public bool tuition5 { get; set; }
        public bool tuition6 { get; set; }
        public bool tuition7 { get; set; }
        public bool tuition8 { get; set; }
        public bool tuition9 { get; set; }
        public bool tuition10 { get; set; }
        public bool tuition11 { get; set; }
        public bool tuition12 { get; set; }

        public virtual StudentClubModel club { get; set; }
        public virtual StudentLevelModel level { get; set; }
        public virtual StudentSchoolModel school { get; set; }
    }

    public class StudentClubModel
    {
        public int id { get; set; }
        public string Name;
    }

    public class StudentSchoolModel
    {
        public int id { get; set; }
        public string Name;
    }

    public class StudentLevelModel
    {
        public int id { get; set; }
        public string Name;
    }
}