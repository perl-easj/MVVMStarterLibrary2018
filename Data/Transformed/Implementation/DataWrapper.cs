using Data.Transformed.Interfaces;

namespace Data.Transformed.Implementation
{
    /// <summary>
    /// Base implementation of IDataWrapper
    /// </summary>
    public class DataWrapper<TData> : IDataWrapper<TData>
    {
        /// <inheritdoc />
        public TData DataObject { get; }

        /// <summary>
        /// Constructor. Sets the wrapped data object
        /// </summary>
        protected DataWrapper(TData obj)
        {
            DataObject = obj;
        }
    }
}