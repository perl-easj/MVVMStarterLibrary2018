namespace Data.Transformed.Interfaces
{
    /// <summary>
    /// Interface for converting between domain data representation
    /// and view data representation.
    /// </summary>
    public interface IViewDataTransform<TDomainData, TViewData>
    {
        TDomainData CreateDomainObjectFromViewDataObject(TViewData vmObj);
        TViewData CreateViewDataObject(TDomainData obj);
    }
}