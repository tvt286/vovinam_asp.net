using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vovinam.Data;
using Vovinam.Services;

namespace Vovinam.Models
{
    public class LevelUpSearchModel:SearchModel
    {
        public string Name { get; set; }
        public int? ExaminationId { get; set; }
        public int? LevelId { get; set; }

        public KetQua? KetQua { get; set; }

    
 
    }
}