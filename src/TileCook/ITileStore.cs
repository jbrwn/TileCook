using TileProj;

namespace TileCook
{
    public interface ITileStore
    {
        VectorTile GetTile(ICoord coord);

        double MinZoom { get; }
        double MaxZoom { get; }
        string Scheme { get; }
        IPoint Center { get; }
        IEnvelope Bounds { get; }
    }
}
