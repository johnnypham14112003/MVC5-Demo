using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Repository.Repository
{
    public class MotorRepository : IMotorRepository
    {
        private readonly ExampleDbContext _context;
        public MotorRepository(ExampleDbContext context)
        {
            _context = context;
        }

        public bool Add(Motor item)
        {
            if (item == null)
            {
                return false;
            }

            _context.Motors.Add(item);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(Guid id)
        {
            var exist = Get(id);
            if (exist == null)
            {
                return false;
            }

            _context.Motors.Remove(exist);
            return _context.SaveChanges() > 0;
        }

        public Motor Get(Guid id)
        {
            var exist = _context.Motors.FirstOrDefault(a => a.Id == id);
            return exist;
        }

        public IEnumerable<Motor> GetAll()
        {
            var list = _context.Motors.AsNoTracking().ToList();
            return list;
        }

        public IEnumerable<Motor> GetByName(string keyword)
        {
            var list = _context.Motors.AsNoTracking().Where(m => m.Name.ToLower().Contains(keyword.ToLower())).ToList();
            return list;
        }

        public Motor UpdateImage(Guid id, string url)
        {
            var exist = Get(id);
            if (exist != null)
            {
                exist.ImageUrl = url;
                _context.SaveChanges();
            }
            return exist;
        }

        public bool Update(Motor motor)
        {
            var exist = Get(motor.Id);
            if (exist == null)
            {
                return false;
            }

            exist.Name = motor.Name;
            exist.Description = motor.Description;
            exist.Price = motor.Price;
            return _context.SaveChanges() > 0;
        }
    }
}
