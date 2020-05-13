using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OurMoneyWpf.DataProvider.Entities;

namespace OurMoneyWpf.DataProvider.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly OurMoneyDbContext context;
        private readonly DbSet<T> dbset;

        public Repository(OurMoneyDbContext context)
        {
            this.context = context;
            this.dbset = context.Set<T>();
        }

        public virtual List<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
            Save();
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
            Save();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
            Save();
        }

        public virtual T GetById(int? id)
        {
            if (id == null) return null;
            return dbset.Find(id);
        }

        public virtual IList<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IList<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault();
        }

        public int Count(Expression<Func<T, bool>> where = null)
        {
            return dbset.Count(where);
        }

        public bool IsExist(Expression<Func<T, bool>> where = null)
        {
            return dbset.FirstOrDefault(where) != null ? true : false;
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
#if DEBUG
            catch (DbUpdateException e)
            {
                foreach (var eve in e.Entries)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entity.GetType().Name, eve.State);
                }
                throw;
            }
#endif
            catch (Exception exception)
            {
                throw exception;
            }

        }
    }
}
