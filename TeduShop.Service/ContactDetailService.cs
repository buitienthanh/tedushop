﻿using System;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IContactDetailService
    {

        ContactDetail GetDefaultContact();
        ContactDetail Add(ContactDetail ContactDetail);

        void Update(ContactDetail ContactDetail);

        ContactDetail Delete(int id);

        IEnumerable<ContactDetail> GetAll();

        IEnumerable<ContactDetail> GetAll(string keyword);

        ContactDetail GetById(int id);

        void Save();
    }

    public class ContactDetailService : IContactDetailService
    {
        private IContactDetailRepository _contactDetailRepository;

        private IUnitOfWork _unitOfWork;

        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._contactDetailRepository = contactDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public ContactDetail Add(ContactDetail ContactDetail)
        {
            return _contactDetailRepository.Add(ContactDetail);
        }

        public ContactDetail Delete(int id)
        {
            return _contactDetailRepository.Delete(id);
        }

        public IEnumerable<ContactDetail> GetAll()
        {
            return _contactDetailRepository.GetAll();
        }

        public IEnumerable<ContactDetail> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _contactDetailRepository.GetMulti(x => x.Name.Contains(keyword) || x.Email.Contains(keyword));
            else
                return _contactDetailRepository.GetAll();
        }

        public ContactDetail GetById(int id)
        {
            return _contactDetailRepository.GetSingleById(id);
        }

        public ContactDetail GetDefaultContact()
        {
            return _contactDetailRepository.GetSingleByCondition(x => x.Status);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ContactDetail ContactDetail)
        {
            _contactDetailRepository.Update(ContactDetail);
        }
    }
}