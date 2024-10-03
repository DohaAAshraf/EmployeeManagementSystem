﻿using System.Collections.Generic;

namespace EmployeeSystem.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity); // Keep this generic
        void Save();
    }
}
