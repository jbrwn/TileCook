using TileProj;

namespace TileCook
{
    public interface ITileStore
    {
        VectorTile GetTile(ICoord coord);

        int MinZoom { get; }
        int MaxZoom { get; }
        IEnvelope Bounds { get; }
    }
}
