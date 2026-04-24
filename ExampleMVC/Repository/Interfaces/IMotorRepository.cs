using Repository.Models;
using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IMotorRepository
    {
        IEnumerable<Motor> GetAll();
        Motor Get(Guid id);
        IEnumerable<Motor> GetByName(string keyword);
        bool Add(Motor item);
        Motor UpdateImage(Guid id, string url);
        bool Update(Motor item);
        bool Delete(Guid id);
    }
}
