using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vovinam.Data;

namespace Vovinam.Areas.Api.Services
{
    public class LevelUpHistoryService
    {
        public static void Create(LevelUpHistory data)
        {
            using (var context = new vovinamEntities())
            {
                data.TimeCreate = DateTime.Now;
                context.LevelUpHistories.Add(data);
                context.SaveChanges();
            }
        }
    }
}