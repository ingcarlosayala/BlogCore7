using BlogCore.AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.IRepositorio.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext db;
        internal DbSet<T> dbset;

        public Repositorio(ApplicationDbContext db)
        {
            this.db = db;
            dbset = db.Set<T>();
        }
        public void Add(T item)
        {
            dbset.Add(item);
        }

        public T Get(int id)
        {
            return dbset.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> ordeBy = null, string incluirPropiedad = null)
        {
            IQueryable<T> quety = dbset;
            if (filtro != null)
            {
                quety = quety.Where(filtro);
            }
            if (incluirPropiedad != null)
            {
                foreach (var item in incluirPropiedad.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    quety = quety.Include(item);
                }
            }
            if (ordeBy != null)
            {
                return ordeBy(quety).ToList();
            }
            return quety.ToList();
        }

        public T GetFirtsOrDefault(Expression<Func<T, bool>> filtro = null, string incluirPropiedad = null)
        {
            IQueryable<T> quety = dbset;
            if (filtro != null)
            {
                quety = quety.Where(filtro);
            }
            if (incluirPropiedad != null)
            {
                foreach (var item in incluirPropiedad.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quety = quety.Include(item);
                }
            }
            return quety.FirstOrDefault();
        }

        public void Remove(T item)
        {
            dbset.Remove(item);
        }

        public void Remover(int id)
        {
            T item = dbset.Find(id);
            Remove(item);
        }
    }
}
