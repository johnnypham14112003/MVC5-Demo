using Mapster;
using Repository.Interfaces;
using Repository.Models;
using Repository.Repository;
using Service.Interfaces;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountViewModel Login(AuthViewModel input)
        {
            var exist = _accountRepository.GetByMail(input.Email);

            if (exist != null && exist.Password.Equals(input.Password))
                return exist.Adapt<AccountViewModel>();

            return null;
        }

        public IEnumerable<AccountViewModel> GetAll()
        {
            return _accountRepository.GetAll().Adapt<IEnumerable<AccountViewModel>>();
        }
        public AccountViewModel GetDetail(Guid id)
        {
            var exist = _accountRepository.Get(id);
            return exist.Adapt<AccountViewModel>();
        }

        public IEnumerable<AccountViewModel> Search(string keyword, string role)
        {
            if (string.IsNullOrWhiteSpace(keyword) != true || string.IsNullOrWhiteSpace(role) != true)
            {
                return _accountRepository.Search(keyword, role).Adapt<IEnumerable<AccountViewModel>>();
            }
            return GetAll();
        }

        public bool Add(AccountViewModel item)
        {
            if (item == null)
            {
                return false;
            }

            return _accountRepository.Add(item.Adapt<Account>());
        }

        public bool Delete(Guid id)
        {
            return _accountRepository.Delete(id);
        }

        public bool Update(AccountViewModel item)
        {
            return _accountRepository.Update(item.Adapt<Account>());
        }
    }
}
