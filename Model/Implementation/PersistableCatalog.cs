using System;
using System.Collections.Generic;
using Data.InMemory.Interfaces;
using Data.Persistent.Interfaces;
using Model.Interfaces;

namespace Model.Implementation
{
    /// <summary>
    /// Implementation of a persistable Catalog.
    /// Note that the implementation inherits from Catalog, 
    /// and thus also contains CRUD methods.
    /// </summary>
    public abstract class PersistableCatalog<TDomainData, TViewData, TPersistentData>
        : Catalog<TDomainData, TViewData, TPersistentData>, IPersistableCatalog
          where TViewData : IStorable
          where TDomainData : IStorable
    {
        private IPersistentSource<TPersistentData> _persistentSource;

        #region Constructor
        protected PersistableCatalog(
            IInMemoryCollection<TDomainData> collection,
            IPersistentSource<TPersistentData> source,
            List<PersistencyOperations> supportedOperations)
            : base(collection, source, supportedOperations)
        {
            _persistentSource = source;
        }
        #endregion

        #region IPersistableCatalog implementation
        /// <inheritdoc />
        /// <summary>
        /// Relays call of Load to data source, if the data 
        /// source supports the Load operation
        /// </summary>
        public async void Load(bool suppressException = true)
        {

            if (_supportedOperations.Contains(PersistencyOperations.Load))
            {
                List<TPersistentData> objects = await _persistentSource.Load();
                _collection.ReplaceAll(CreateDomainObjects(objects), KeyManagementStrategyType.DataSourceDecides);
            }
            else
            {
                if (!suppressException)
                {
                    throw new NotSupportedException();
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Relays call of Save to data source, if the data 
        /// source supports the Load operation
        /// </summary>
        public async void Save(bool suppressException = true)
        {
            if (_supportedOperations.Contains(PersistencyOperations.Save))
            {
                await _persistentSource.Save(CreatePersistentDataObjects(_collection.All));
            }
            else
            {
                if (!suppressException)
                {
                    throw new NotSupportedException();
                }
            }
        }
        #endregion

        #region Private helper methods
        /// <summary>
        /// Transforms a list of domain objects into a 
        /// list of corresponding persistent data objects.
        /// </summary>
        private List<TPersistentData> CreatePersistentDataObjects(IEnumerable<TDomainData> objects)
        {
            List<TPersistentData> pdObjects = new List<TPersistentData>();
            foreach (TDomainData obj in objects)
            {
                pdObjects.Add(CreatePersistentDataObject(obj));
            }
            return pdObjects;
        }

        /// <summary>
        /// Transforms a list of persistent data objects into 
        /// a list of corresponding domain objects.
        /// </summary>
        private List<TDomainData> CreateDomainObjects(IEnumerable<TPersistentData> pdObjects)
        {
            List<TDomainData> objects = new List<TDomainData>();
            foreach (TPersistentData pdObj in pdObjects)
            {
                objects.Add(CreateDomainObjectFromPersistentDataObject(pdObj));
            }
            return objects;
        }
        #endregion
    }
}