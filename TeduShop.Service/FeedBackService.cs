using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IFeedBackService
    {
        FeedBack Create(FeedBack FeedBack);

        void Save();
    }

    public class FeedBackService : IFeedBackService
    {
        private IFeedBackRepository _FeedBackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedBackService(IFeedBackRepository FeedBackRepository, IUnitOfWork unitOfWork)
        {
            _FeedBackRepository = FeedBackRepository;
            _unitOfWork = unitOfWork;
        }

        public FeedBack Create(FeedBack FeedBack)
        {
            return _FeedBackRepository.Add(FeedBack);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}