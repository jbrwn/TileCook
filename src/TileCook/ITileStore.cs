namespace TileCook
{
    public interface ITileStore
    {
        Tile GetTile(int z, int x, int y);
        TileInfo GetTileInfo();
    }
}
