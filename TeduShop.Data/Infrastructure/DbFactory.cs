namespace TeduShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private TeduShopDbContext dbContect;

        public TeduShopDbContext Init()
        {
            return dbContect ?? (dbContect = new Data.TeduShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContect != null)
                dbContect.Dispose();
        }
    }
}