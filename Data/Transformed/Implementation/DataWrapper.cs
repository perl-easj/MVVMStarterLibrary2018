using Data.Transformed.Interfaces;

namespace Data.Transformed.Implementation
{
    /// <summary>
    /// Base implementation of IDataWrapper
    /// </summary>
    public class DataWrapper<TTransformedData> : IDataWrapper<TTransformedData>
    {
        /// <inheritdoc />
        public TTransformedData DataObject { get; }

        /// <summary>
        /// Constructor. Sets the wrapped transformed data object
        /// </summary>
        protected DataWrapper(TTransformedData obj)
        {
            DataObject = obj;
        }
    }
}