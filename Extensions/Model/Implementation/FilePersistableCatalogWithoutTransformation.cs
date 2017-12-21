using Data.InMemory.Interfaces;

namespace Extensions.Model.Implementation
{
    /// <summary>
    /// Simplified version of FilePersistableCatalog, where the domain data classes will  
    /// also act as both view data and persistent data classes, i.e. no data transformations.
    /// All transformation methods are therefore trivial.
    /// </summary>
    public class FilePersistableCatalogWithoutTransformation<TDomainData> : FilePersistableCatalog<TDomainData, TDomainData, TDomainData>
        where TDomainData : class, IStorable
    {
        public FilePersistableCatalogWithoutTransformation(bool loadOnCreation = false, bool saveOnChanges = false)
            : base(loadOnCreation, saveOnChanges)
        {
        }

        public override TDomainData CreateDomainObjectFromViewDataObject(TDomainData obj)
        {
            return obj;
        }

        public override TDomainData CreateDomainObjectFromPersistentDataObject(TDomainData obj)
        {
            return obj;
        }

        public override TDomainData CreateViewDataObject(TDomainData obj)
        {
            return obj;
        }

        public override TDomainData CreatePersistentDataObject(TDomainData obj)
        {
            return obj;
        }
    }
}