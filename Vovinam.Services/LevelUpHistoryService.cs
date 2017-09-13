using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vovinam.Data;
using Vovinam.Common;
using System.Data.Entity;

namespace Vovinam.Services
{
    public class LevelUpHistoryService
    {
        public static List<LevelUpHistory> Gets(int LevelUpId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.LevelUpHistories.Where(x => x.LevelUpId == LevelUpId)    
                    .OrderBy(x => x.Id)
                    .ToList();
            }
        }

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
