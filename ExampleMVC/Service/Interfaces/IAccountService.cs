using Service.ViewModels;
using System;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IAccountService
    {
        AccountViewModel Login(AuthViewModel input);
        IEnumerable<AccountViewModel> GetAll();
        AccountViewModel GetDetail(Guid id);
        IEnumerable<AccountViewModel> Search(string keyword, string role);
        bool Add(AccountViewModel item);
        bool Delete(Guid id);
        bool Update(AccountViewModel item);
    }
}
