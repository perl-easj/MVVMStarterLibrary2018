namespace Data.Transformed.Interfaces
{
    /// <summary>
    /// Interface for objects having meaningfful default values.
    /// This will typically be ViewData classes, where default 
    /// values are shown in a corresponding view.
    /// </summary>
    public interface IDefaultValues
    {
        /// <summary>
        /// A subclass can assign default values to
        /// the properties of the transformed data.
        /// </summary>
        void SetDefaultValues();
    }
}