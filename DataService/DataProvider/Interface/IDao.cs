﻿using Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    public interface IDao
    {
        string ConnStr { get; }

        int Add(IDomainModel entity);

        bool Contains(IDomainModel entity);

        void AddList<T>(List<T> list) where T : class;

        IEnumerable<TEntity> All<TEntity>() where TEntity : IDomainModel;
    }
}
