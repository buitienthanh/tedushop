using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Data.Infrastructure
{
    public class DbFactory:Disposable ,IDbFactory
    {
        TeduShopDbContext dbContect;

        public TeduShopDbContext Init()
        {
            return dbContect ?? (dbContect = new Data.TeduShopDbContext() );
        }
        protected override void DisposeCore()
        {
            if (dbContect != null)
                dbContect.Dispose();
        }
    }
}
