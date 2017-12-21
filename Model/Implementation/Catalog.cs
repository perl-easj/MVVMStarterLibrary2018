﻿using System;
using System.Collections.Generic;
using Data.InMemory.Interfaces;
using Data.Persistent.Interfaces;
using Data.Transformed.Interfaces;
using Model.Interfaces;

namespace Model.Implementation
{
    /// <summary>
    /// Main implementation of the ICatalog interface,
    /// which contains CRUD operations.
    /// </summary>
    /// <typeparam name="TDomainData">Domain class</typeparam>
    /// <typeparam name="TViewData">View data type</typeparam>
    /// <typeparam name="TPersistentData">Persistent data type</typeparam>
    public abstract class Catalog<TDomainData, TViewData, TPersistentData> :
        ICatalog<TViewData>,
        IViewDataTransform<TDomainData, TViewData>,
        IPersistentDataTransform<TDomainData, TPersistentData>
        where TDomainData : IStorable
        where TViewData : IStorable
    {
        protected IInMemoryCollection<TDomainData> _collection;
        protected IDataSourceCRUD<TPersistentData> _source;
        protected List<PersistencyOperations> _supportedOperations;

        public event Action<int> CatalogChanged;

        protected Catalog(
            IInMemoryCollection<TDomainData> collection,
            IDataSourceCRUD<TPersistentData> source,
            List<PersistencyOperations> supportedOperations)
        {
            _collection = collection;
            _source = source;
            _supportedOperations = supportedOperations;
        }

        /// <inheritdoc />
        public List<TViewData> All
        {
            get
            {
                List<TViewData> transformedAll = new List<TViewData>();
                foreach (TDomainData obj in _collection.All)
                {
                    transformedAll.Add(CreateViewDataObject(obj));
                }
                return transformedAll;
            }
        }

        /// <inheritdoc />
        public void Create(TViewData vmObj, KeyManagementStrategyType keyManagement = KeyManagementStrategyType.CollectionDecides)
        {
            // Create the new domain object (this is where it happens :-)).
            TDomainData obj = CreateDomainObjectFromViewDataObject(vmObj);

            // Strategy for key selection (DataSource decides)
            // 1) Throw exception if Create operation is not supported,
            //    since choosing DataSourceDecides is then meaningless.
            // 2) Call Create on data source, and use returned key value 
            //    as the new key for the object.
            // 3) Call Insert on the in-memory collection.
            if (keyManagement == KeyManagementStrategyType.DataSourceDecides)
            {
                if (!_supportedOperations.Contains(PersistencyOperations.Create))
                {
                    throw new NotSupportedException("The referenced data source does not support Create.");
                }

                obj.Key = _source.Create(CreatePersistentDataObject(obj)).Result;
                _collection.Insert(obj, keyManagement);
            }

            // Strategy for key selection (Caller decides)
            // 1) If the data source supports the Create operation,
            //    call Create on data source. It is assumed that it
            //    is NOT an error to call this method, even if the
            //    data source does not support it.
            // 2) Call Insert on the in-memory collection.
            if (keyManagement == KeyManagementStrategyType.CallerDecides)
            {
                if (_supportedOperations.Contains(PersistencyOperations.Create))
                {
                    _source.Create(CreatePersistentDataObject(obj));
                }
                _collection.Insert(obj, keyManagement);
            }

            // Strategy for key selection (Collection decides)
            // 1) Throw exception if Create operation is not supported,
            //    since choosing DataSourceDecides is then meaningless.
            // 2) If the data source supports the Create operation,
            //    call Create on data source. It is assumed that it
            //    is NOT an error to call this method, even if the
            //    data source does not support it.
            if (keyManagement == KeyManagementStrategyType.CollectionDecides)
            {
                obj.Key = _collection.Insert(obj, keyManagement);
                if (_supportedOperations.Contains(PersistencyOperations.Create))
                {
                    _source.Create(CreatePersistentDataObject(obj));
                }
            }

            CatalogChanged?.Invoke(obj.Key);
        }

        /// <inheritdoc />
        public TViewData Read(int key)
        {
            return CreateViewDataObject(_collection[key]);
        }

        /// <inheritdoc />
        public void Update(TViewData obj, int key)
        {
            Delete(key);
            obj.Key = key;
            Create(obj, KeyManagementStrategyType.CallerDecides);
        }

        /// <inheritdoc />
        public void Delete(int key)
        {
            _collection.Remove(key);
            if (_supportedOperations.Contains(PersistencyOperations.Delete))
            {
                _source.Delete(key);
            }

            CatalogChanged?.Invoke(key);
        }

        /// <summary>
        /// Override these methods in domain-specific catalog class, 
        /// to define transformation from/to domain object.
        /// </summary>
        public abstract TDomainData CreateDomainObjectFromViewDataObject(TViewData vmObj);
        public abstract TPersistentData CreatePersistentDataObject(TDomainData obj);
        public abstract TDomainData CreateDomainObjectFromPersistentDataObject(TPersistentData vmObj);
        public abstract TViewData CreateViewDataObject(TDomainData obj);
    }
}