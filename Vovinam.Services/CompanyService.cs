using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Vovinam.Common;
using Vovinam.Data;

namespace Vovinam.Services
{
    public class CompanyService
    {
        public static List<Company> GetAll()
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Companies.AsNoTracking().ToList();
            }
        }

        public static Company Get(int id)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Companies.First(x => x.Id == id);
            }
        }

        public static void CreateCompany(Company data)
        {
            using (var context = new vovinamEntities())
            {
                data.CreateDate = DateTime.Now;
                context.Companies.Add(data);
                context.SaveChanges();
            }
        }

        public static void UpdateCompany(Company data)
        {
            using (var context = new vovinamEntities())
            {
                context.Companies.Attach(data);
                context.Entry(data).State = EntityState.Modified;
                context.Entry(data).Property(x => x.CreateDate).IsModified = false;
                context.SaveChanges();
            }
        }

        public static PagedSearchList<Company> Search(string companyName,
            int pageSize, int pageIndex)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                IQueryable<Company> query = context.Companies.AsNoTracking();

                if (companyName.NotEmpty())
                {
                    query = query.Where(x => x.Name.Contains(companyName));
                }

                query = query.OrderByDescending(x => x.Id);
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<Company>(query, pageIndex, pageSize);
            }
        }
    }
}
