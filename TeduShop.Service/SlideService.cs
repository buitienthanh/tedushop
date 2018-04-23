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
    public interface ISlideService
    {
        Slide Add(Slide Slide);

        void Update(Slide Slide);

        Slide Delete(int id);

        IEnumerable<Slide> GetAll();

        IEnumerable<Slide> GetAll(string keyword);

        Slide GetById(int id);

        void Save();
    }
    public class SlideService : ISlideService
    {
        private ISlideRepository _SlideRepository;
        private IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository SlideRepository, IUnitOfWork unitOfWork)
        {
            this._SlideRepository = SlideRepository;
            this._unitOfWork = unitOfWork;
        }
        public Slide Add(Slide Slide)
        {
            return _SlideRepository.Add(Slide);
        }

        public Slide Delete(int id)
        {
            return _SlideRepository.Delete(id);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _SlideRepository.GetAll();
        }

        public IEnumerable<Slide> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _SlideRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _SlideRepository.GetAll();

        }


        public Slide GetById(int id)
        {
            return _SlideRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide Slide)
        {
            _SlideRepository.Update(Slide);
        }
    }
}
