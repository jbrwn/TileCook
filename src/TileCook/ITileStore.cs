using TileProj;
using System.Collections.Generic;

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
        IEnumerable<VectorLayer> VectorLayers { get; }
    }
}
