﻿namespace Model.Interfaces
{
    public interface IPersistableCatalog
    {
        /// <summary>
        /// Invoke a Load operation on the catalog,
        /// meaning that all existing items in the
        /// catalog are replaced with the loaded items.
        /// </summary>
        void Load(bool suppressException = true);

        /// <summary>
        /// Invoke a Save operation on the catalog,
        /// meaning that all existing items in the
        /// catalog are saved to persistent storage,
        /// thereby replacing the items present in
        /// persistent storage.
        /// </summary>
        void Save(bool suppressException = true);
    }
}