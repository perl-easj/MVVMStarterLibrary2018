using System.Collections.Generic;
using System.Threading.Tasks;
using Data.InMemory.Interfaces;
using Data.Persistent.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataSources.EFCore.Implementation
{
    public class EFCoreSource<TPersistentData> : IDataSourceCRUD<TPersistentData>, IDataSourceLoad<TPersistentData>
        where TPersistentData : class, IStorable
    {
        #region Instance fields
        private DbContext _context;
        #endregion

        #region Constructor
        public EFCoreSource(DbContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<List<TPersistentData>> Load()
        {
            return await _context.Set<TPersistentData>().ToListAsync();
        }

        public async Task<int> Create(TPersistentData obj)
        {
            await _context.Set<TPersistentData>().AddAsync(obj);
            _context.SaveChanges();
            return obj.Key; // TODO
        }

        public async Task<TPersistentData> Read(int key)
        {
            return await _context.Set<TPersistentData>().FindAsync(key);
        }

        public async Task Update(int key, TPersistentData obj)
        {
            TPersistentData oldObj = await _context.Set<TPersistentData>().FindAsync(key);
            if (oldObj != null)
            {
                _context.Set<TPersistentData>().Remove(oldObj);
                obj.Key = key;
                await _context.Set<TPersistentData>().AddAsync(obj);
                _context.SaveChanges();
            }
        }

        public async Task Delete(int key)
        {
            TPersistentData obj = await _context.Set<TPersistentData>().FindAsync(key);
            if (obj != null)
            {
                _context.Set<TPersistentData>().Remove(obj);
                _context.SaveChanges();
            }
        }
    }
}