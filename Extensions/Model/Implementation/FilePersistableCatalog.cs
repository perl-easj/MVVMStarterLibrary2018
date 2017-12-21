﻿using System.Collections.Generic;
using Data.InMemory.Implementation;
using Data.InMemory.Interfaces;
using Data.Persistent.Interfaces;
using DataSources.FileJSON.Implementation;
using Model.Implementation;

namespace Extensions.Model.Implementation
{
    /// <summary>
    /// This class injects specific dependencies into 
    /// the FilePersistableCatalog class:
    /// 1) Data is read from a file-based source.
    /// 2) Data is stored as a string in the file source.
    /// 3) The string is on JSON format.
    /// 4) The InMemoryCollection implementation is used.
    /// </summary>
    public abstract class FilePersistableCatalog<TDomainData, TViewData, TPersistentData> : PersistableCatalog<TDomainData, TViewData, TPersistentData>
        where TDomainData : class, IStorable
        where TViewData : IStorable
    {
        protected FilePersistableCatalog(bool loadOnCreation = false, bool saveOnChanges = false)
            : base(new InMemoryCollection<TDomainData>(),
                new ConfiguredFileSource<TPersistentData>(new FileStringPersistence(), new JSONConverter<TPersistentData>()),
                new List<PersistencyOperations> { PersistencyOperations.Load, PersistencyOperations.Save })
        {
            if (loadOnCreation)
            {
                Load();
            }

            if (saveOnChanges)
            {
                CatalogChanged += (i => Save());
            }
        }
    }
}