using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vovinam.Services;

namespace Vovinam.Models
{
    public class StudentModel:SearchModel
    {
        public int? ClubId { get; set; }
        public string Name { get; set; }
        public int? LevelId { get; set; }
    }
}