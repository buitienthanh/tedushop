using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;


namespace TeduShop.Service
{
    public interface IPageService
    {
        Page GetByAlias(string alias);
        void Add(Page post);

        void Update(Page post);

        Page Delete(int id);

        IEnumerable<Page> GetAll(string keyword);

        IEnumerable<Page> GetAll();

        IEnumerable<Page> GetAllPaging(int page, int pageSize, out int totalRow);

        Page GetById(int id);

        void Save();
    }
    class PageService : IPageService
    {
        IPageRepository _pageRepository;
        IUnitOfWork _unitOfWork;

        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }
        public void Add(Page page)
        {
            _pageRepository.Add(page);
        }

        public Page Delete(int id)
        {
          return  _pageRepository.Delete(id);
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageRepository.GetAll();
        }

        public IEnumerable<Page> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _pageRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _pageRepository.GetAll();
        }

        public IEnumerable<Page> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _pageRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }

        public Page GetById(int id)
        {
            return _pageRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Page page)
        {
            _pageRepository.Update(page);
        }
    }
}
