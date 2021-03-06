﻿using System.Threading.Tasks;

namespace DataSources.FileJSON.Interfaces
{
    /// <summary>
    /// Interface for loading/saving a 
    /// string to/from a persistent source.
    /// It is assumed that the source itself 
    /// can be identified by a string.
    /// </summary>
    public interface IStringPersistence
    {
        /// <summary>
        /// Saves the provided string to the specified source.
        /// </summary>
        /// <param name="source">
        /// Source to which data is saved.
        /// </param>
        /// <param name="data">
        /// Data in string format.
        /// </param>
        Task SaveAsync(string source, string data);

        /// <summary>
        /// Loads a string from the specified source.
        /// </summary>
        /// <param name="source">
        /// Source from which data is loaded.
        /// </param>
        /// <returns>
        /// Data in string format.
        /// </returns>
        Task<string> LoadAsync(string source);
    }
}