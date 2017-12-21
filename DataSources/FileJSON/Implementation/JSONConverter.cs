using System.Collections.Generic;
using DataSources.FileJSON.Interfaces;
using Newtonsoft.Json;

namespace DataSources.FileJSON.Implementation
{
    /// <summary>
    /// JSON-specific implementation of the 
    /// IDataConverter interface. Uses the 
    /// 3rd-party NewtonSoft JSON package.
    /// </summary>
    /// <typeparam name="TPersistentData">Type of objects to convert</typeparam>
    public class JSONConverter<TPersistentData> : IStringConverter<TPersistentData>
    {
        /// <summary>
        /// Convert a List of objects into a JSON string.
        /// </summary>
        /// <param name="objects">
        /// Objects to convert.
        /// </param>
        /// <returns>
        /// Data on JSON string format.
        /// </returns>
        public string ConvertToString(List<TPersistentData> objects)
        {
            return JsonConvert.SerializeObject(objects);
        }

        /// <summary>
        /// Converts from a JSON string into a List of objects.
        /// </summary>
        /// <param name="data">
        /// Data on JSON string format.
        /// </param>
        /// <returns>
        /// List of objects.
        /// </returns>
        public List<TPersistentData> ConvertFromString(string data)
        {
            return (data == null ? new List<TPersistentData>() : (List<TPersistentData>)JsonConvert.DeserializeObject(data, typeof(List<TPersistentData>)));
        }
    }
}