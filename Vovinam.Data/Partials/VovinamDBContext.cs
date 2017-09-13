using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vovinam.Data.Extensions;

namespace Vovinam.Data
{
    public partial class vovinamEntities
    {
        public vovinamEntities(IsolationLevel isolationLevel)
            : base()
        {
            //   this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.LazyLoadingEnabled = true;
            this.SetIsolationLevel(isolationLevel);
        }
    }
}
