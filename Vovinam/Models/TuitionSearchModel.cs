using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vovinam.Common.Enums;
using Vovinam.Services;

namespace Vovinam.Models
{
    public class TuitionSearchModel:SearchModel
    {
        public string Name { get; set; }
        public int ClubId { get; set; }
        public Tuition? HasCollected { get; set; }
        public Tuition? UnCollected { get; set; }

    }
}