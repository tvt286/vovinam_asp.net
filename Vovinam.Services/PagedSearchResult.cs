using PagedList;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Vovinam.Services
{
    public class PagedSearchList<T> : PagedList<T>
    {
        public SearchModel SearchModel { get; set; }

        public string queryString { get; set; }

        public PagedSearchList(IQueryable<T> superset, int pageNumber, int pageSize) : base(superset, pageNumber, pageSize)
        {
        }

        public PagedSearchList(IEnumerable<T> superset, int pageNumber, int pageSize) : base(superset, pageNumber, pageSize)
        {
        }
    }

    public class SearchModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public SearchModel()
        {
            PageIndex = 1;
            PageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
        }
    }
}
