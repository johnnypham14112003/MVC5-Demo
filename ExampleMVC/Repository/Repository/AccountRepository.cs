using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ExampleDbContext _context;
        public AccountRepository(ExampleDbContext context)
        {
            _context = context;
        }

        public bool Add(Account item)
        {
            if (item == null)
            {
                return false;
            }

            _context.Accounts.Add(item);
            return _context.SaveChanges() > 0;
        }

        public Account Get(Guid id)
        {
            var exist = _context.Accounts.FirstOrDefault(a => a.Id == id);
            return exist;
        }

        public Account GetByMail(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail)) return null;
            return _context.Accounts.FirstOrDefault(a => a.Email.Equals(mail, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Account> Search(string keyword, string role)
        {
            var query = _context.Accounts
                .AsNoTracking()
                .AsQueryable();

            // Apply search
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(acc => acc.Email.ToLower().Contains(keyword.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(role))
            {
                query = query.Where(acc => acc.Role.ToLower().Equals(role.ToLower()));
            }

            return query.ToList();
        }

        public IEnumerable<Account> GetAll()
        {
            var list = _context.Accounts.AsNoTracking().ToList();
            return list;
        }

        public bool Update(Account item)
        {
            var exist = Get(item.Id);
            if (exist == null)
            {
                return false;
            }

            exist.Email = item.Email;
            exist.Password = item.Password;
            exist.Role = item.Role;
            return _context.SaveChanges() > 0;
        }

        public bool Delete(Guid id)
        {
            var exist = Get(id);
            if (exist == null)
            {
                return false;
            }

            _context.Accounts.Remove(exist);
            return _context.SaveChanges() > 0;
        }
    }
}
