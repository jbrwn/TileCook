using System;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace TileCook
{
    public class MemoryTileStoreRepository : ITileStoreRepository 
    {
        private static ConcurrentDictionary<string, ITileStore> MemoryRepo = new ConcurrentDictionary<string, ITileStore>();

        public MemoryTileStoreRepository() {}

        public void AddOrUpdate(ITileStore tilestore)
        {
            MemoryRepo[tilestore.Id] = tilestore;
        }

        public void Delete(ITileStore tilestore)
        {
            ((IDictionary<string, ITileStore>)MemoryRepo).Remove(tilestore.Id);
        }

        public ITileStore GetById(string Id)
        {
            ITileStore t;
            MemoryRepo.TryGetValue(Id, out t);
            return t;
        }
    }
}
