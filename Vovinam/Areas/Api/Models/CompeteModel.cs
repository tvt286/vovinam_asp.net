using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vovinam.Areas.Api.Models
{
    public class CompeteModel
    {
        public virtual LevelUpModel levelup_1 { get; set; }
        public virtual LevelUpModel levelup_2 { get; set; }
    }
}