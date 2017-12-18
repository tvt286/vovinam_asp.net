using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vovinam.Services;

namespace Vovinam.Models
{
    public class ExaminationSearchModel:SearchModel
    {
        public int? ExaminationId { get; set; }
    }
}