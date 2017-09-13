using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vovinam.Data;
using Vovinam.Services;

namespace Vovinam.Models
{
    public class ListLevelUpModel:SearchModel
    {

        public string LevelName { get; set; }
        public PagedSearchList<LevelUp> arrLevelUp { get; set; }
    }
}