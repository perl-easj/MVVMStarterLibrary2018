namespace Data.Transformed.Interfaces
{
    /// <summary>
    /// Interface for converting between domain data representation
    /// and persistent data representation.
    /// </summary>
    public interface IPersistentDataTransform<TDomainData, TPersistentData>
    {
        TDomainData CreateDomainObjectFromPersistentDataObject(TPersistentData vmObj);
        TPersistentData CreatePersistentDataObject(TDomainData obj);
    }
}