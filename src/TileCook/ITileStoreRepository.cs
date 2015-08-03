namespace TileCook
{
    public interface ITileStoreRepository
    {
        void AddOrUpdate(ITileStore tilestore);
        void Delete(ITileStore tilestore);
        ITileStore GetById(string Id);
    }
}
