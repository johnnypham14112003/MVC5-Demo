using Repository.Models;
using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        Account Get(Guid id);
        Account GetByMail(string mail);
        IEnumerable<Account> Search(string keyword, string role);
        bool Add(Account item);
        bool Update(Account item);
        bool Delete(Guid id);
    }
}
