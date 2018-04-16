using System;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IFooterRepository : IRepository<Footer>
    {
        Footer GetByIdString(string Id);

        Footer DeleteStringId(string Id);
    }

    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Footer DeleteStringId(string Id)
        {
            var entity = this.DbContext.Footers.Find(Id);
            return this.DbContext.Footers.Remove(entity);
        }

        public Footer GetByIdString(string Id)
        {
            return this.DbContext.Footers.Find(Id);
        }


    }
}