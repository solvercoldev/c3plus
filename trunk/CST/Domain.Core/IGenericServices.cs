using System;
using System.Collections.Generic;

namespace Domain.Core
{
    public interface IGenericServices<T> : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T NewEntity();

        /// <summary>
        /// Adiciona un  nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Modifica un registro existente
        /// </summary>
        /// <param name="entity"></param>
        void Modify(T entity);

        /// <summary>
        /// Elimina un registro existente
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);

        /// <summary>
        /// Busca un registro por Id
        /// </summary>
        T FindById(int id);

        
        /// <summary>
        /// Lista los registros paginados
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <returns>una colección de registros</returns>
        List<T> FindPaged(int pageIndex, int pageCount);


        /// <summary>
        /// Listado de registros del tipo T
        /// </summary>
        /// <returns>una colección de registros</returns>
        List<T> FindBySpec(bool activo);

    }
}