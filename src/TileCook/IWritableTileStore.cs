using TileProj;

namespace TileCook
{
    public interface IWritableTileStore : ITileStore
    {
        void PutTile(ICoord coord, VectorTile tile);
    }
}
