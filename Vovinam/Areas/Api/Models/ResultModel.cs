using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vovinam.Areas.Api.Models
{
    public class ResultModel
    {
        public string gender { get; set; }
        public List<LevelUpModel> students { get; set; }
    }
}