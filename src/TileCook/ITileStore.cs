namespace TileCook
{
    public interface ITileStore
    {
        ITile GetTile(int z, int x, int y);
        ITileInfo GetTileInfo();
    }
}
