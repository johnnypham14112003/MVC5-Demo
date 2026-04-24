using Mapster;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using Service.ViewModels;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class MotorService : IMotorService
    {
        private readonly IMotorRepository _motorRepository;

        public MotorService(IMotorRepository motorRepository)
        {
            _motorRepository = motorRepository;
        }

        public IEnumerable<MotorViewModel> GetAll()
        {
            return _motorRepository.GetAll().Adapt<IEnumerable<MotorViewModel>>();
        }

        public IEnumerable<MotorViewModel> Search(string keyword)
        {
            return string.IsNullOrWhiteSpace(keyword) == true ?
                GetAll() : _motorRepository.GetByName(keyword).Adapt<IEnumerable<MotorViewModel>>();
        }

        public bool Add(MotorViewModel item)
        {
            if (item == null)
            {
                return false;
            }

            return _motorRepository.Add(item.Adapt<Motor>());
        }

        public bool Delete(Guid id)
        {
            return _motorRepository.Delete(id);
        }

        public MotorViewModel GetDetail(Guid id)
        {
            var exist = _motorRepository.Get(id);
            return exist.Adapt<MotorViewModel>();
        }

        public MotorViewModel UpdateImage(Guid id, string url)
        {
            return _motorRepository.UpdateImage(id, url).Adapt<MotorViewModel>();
        }

        public bool Update(MotorViewModel item)
        {
            return _motorRepository.Update(item.Adapt<Motor>());
        }
    }
}
