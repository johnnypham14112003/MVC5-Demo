using Service.ViewModels;
using System;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IMotorService
    {
        IEnumerable<MotorViewModel> GetAll();
        IEnumerable<MotorViewModel> Search(string keyword);
        bool Add(MotorViewModel item);
        bool Delete(Guid id);
        MotorViewModel GetDetail(Guid id);
        bool Update(MotorViewModel item);
        MotorViewModel UpdateImage(Guid id, string url);
    }
}
