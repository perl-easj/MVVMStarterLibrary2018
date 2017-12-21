using Data.InMemory.Interfaces;
using Data.Persistent.Implementation;
using Microsoft.EntityFrameworkCore;

namespace DataSources.EFCore.Implementation
{
    public class ConfiguredEFCoreSource<TPersistentData> : ConfiguredPersistentSource<TPersistentData>
        where TPersistentData : class, IStorable
    {
        public ConfiguredEFCoreSource(DbContext context)
        {
            EFCoreSource<TPersistentData> efCoreSource = new EFCoreSource<TPersistentData>(context);

            DataSourceLoad = efCoreSource;
            DataSourceSave = new DataSourceSaveNotSupported<TPersistentData>();
            DataSourceCRUD = efCoreSource;
        }
    }
}