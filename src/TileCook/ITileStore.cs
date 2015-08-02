using TileProj;

namespace TileCook
{
    public interface ITileStore
    {
        VectorTile GetTile(ICoord coord);

        string Id { get; }
        string Name { get; }
        string Description { get; }
        int MinZoom { get; }
        int MaxZoom { get; }
        IEnvelope Bounds { get; }
        VectorTileInfo VectorLayers { get; }
    }
}
