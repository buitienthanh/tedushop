using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ICommonService
    {
        IEnumerable<Slide> GetSlides();
        Footer GetFooter();
        IEnumerable<Footer> GetAll();
        Footer Add(Footer Footer);

        Footer GetById(string id);

        Footer Delete(string id);

        void Update(Footer Footer);
        void Save();
    }
    public class CommonService : ICommonService
    {
        IFooterRepository _footerRepository;
        ISlideRepository _slideRepository;
        IUnitOfWork _unitOfWork;
        public CommonService(IFooterRepository footerRepository, IUnitOfWork unitOfWork, ISlideRepository slideRepository)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
            _slideRepository = slideRepository;
        }

        public Footer Add(Footer Footer)
        {
            return _footerRepository.Add(Footer);
        }

        public Footer Delete(string id)
        {
            return _footerRepository.DeleteStringId(id);
        }

        public IEnumerable<Footer> GetAll()
        {
            return _footerRepository.GetAll();
        }

        public Footer GetById(string id)
        {
            return _footerRepository.GetByIdString(id);
        }

        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x=>x.ID == CommonConstants.DefaultFooterId);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return _slideRepository.GetMulti(x=>x.Status==true);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Footer Footer)
        {
            _footerRepository.Update(Footer);
        }
    }
}