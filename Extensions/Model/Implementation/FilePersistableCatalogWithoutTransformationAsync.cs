using Data.InMemory.Interfaces;

namespace Extensions.Model.Implementation
{
    /// <summary>
    /// Simplified version of FilePersistableCatalogAsync, where the domain data classes will  
    /// also act as both view data and persistent data classes, i.e. no data transformations.
    /// All transformation methods are therefore trivial.
    /// </summary>
    public class FilePersistableCatalogWithoutTransformationAsync<TDomainData> : FilePersistableCatalogAsync<TDomainData, TDomainData, TDomainData>
        where TDomainData : class, IStorable
    {
        public FilePersistableCatalogWithoutTransformationAsync(bool saveOnChanges = false)
            : base(saveOnChanges)
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