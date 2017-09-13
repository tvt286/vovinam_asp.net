using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vovinam.Data;
using Vovinam.Services;

namespace Vovinam.Models
{
    public class LevelUpViewModel
    {
        public int Stt { get; set; }
        public string Name { get; set; }
        public int BirthDay { get; set; }
        public Gender Gender { get; set; }
        public string ClubName { get; set; }
        public string LevelName { get; set; }
        public int Weight { get; set; }
        public string ExaminationName { get; set; }

    }
}