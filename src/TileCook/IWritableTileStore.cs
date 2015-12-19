namespace TileCook
{
    public interface IWritableTileStore : ITileStore
    {
        void PutTile(int z, int x, int y, ITile tile);
        void SetTileInfo(TileInfo tileinfo);
    }
}
