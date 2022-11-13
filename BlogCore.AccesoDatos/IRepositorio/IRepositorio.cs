using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll(
            Expression<Func<T,bool>> filtro = null,
            Func<IQueryable<T>,IOrderedQueryable<T>> ordeBy = null,
            string incluirPropiedad = null
        );

        T GetFirtsOrDefault(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedad = null
        );

        void Add(T item );
        void Remove(T item );
        void Remover(int id);
    }
}
