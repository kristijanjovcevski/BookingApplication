using BookingApplication.Domain.Domain;
using BookingApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> entities;

        public Repository (ApplicationDbContext context)
        {
            _context = context;
            this.entities = context.Set<T>();
        }

        public T Delete(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);

            _context.SaveChanges();

            return entity;
        }

        public T Get(Guid? id)
        {
            if (typeof(T).Equals(typeof(Reservation))){
                return entities.Include("Apartment")
                    .First(e => e.Id == id);
            }
            return entities.First(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T).Equals(typeof(Reservation))) {
                return entities.Include("Apartment")
                    .AsEnumerable();
            }
            return entities.AsEnumerable();
        }

        public T Insert(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);

            _context.SaveChanges();
            return entity;
        }

        public List<T> InsertMany(List<T> entities)
        {
            entities.AddRange(entities);
            _context.SaveChanges();

            return entities;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
