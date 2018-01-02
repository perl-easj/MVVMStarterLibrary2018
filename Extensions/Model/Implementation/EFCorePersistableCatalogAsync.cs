﻿using System.Collections.Generic;
using Data.InMemory.Implementation;
using Data.InMemory.Interfaces;
using Data.Persistent.Interfaces;
using DataSources.EFCore.Implementation;
using Microsoft.EntityFrameworkCore;
using Model.Implementation;

namespace Extensions.Model.Implementation
{
    /// <summary>
    /// This class injects specific dependencies into 
    /// the PersistableCatalog class:
    /// 1) Data is read from a relational database
    ///    through the Entity Framework Core v.2,
    ///    supporting Load + CRUD operations
    /// 2) The InMemoryCollection implementation is used.
    /// </summary>
    public abstract class EFCorePersistableCatalogAsync<TDbContext, TDomainData, TViewData, TPersistableData> : PersistableCatalogAsync<TDomainData, TViewData, TPersistableData>
        where TDbContext : DbContext, new()
        where TDomainData : class, IStorable
        where TPersistableData : class, IStorable
        where TViewData : IStorable
    {
        protected EFCorePersistableCatalogAsync()
            : base(new InMemoryCollection<TDomainData>(), new ConfiguredEFCoreSource<TDbContext, TPersistableData>(), new List<PersistencyOperations>
            {
                PersistencyOperations.Load,
                PersistencyOperations.Create,
                PersistencyOperations.Read,
                PersistencyOperations.Update,
                PersistencyOperations.Delete
            })
        {
        }
    }
}