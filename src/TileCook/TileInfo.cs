using System.Collections.Generic;

namespace TileCook
{
    public class TileInfo : ITileInfo
    {
        public TileInfo() { }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinZoom { get; set; }
        public int MaxZoom { get; set; }
        public double[] Bounds { get; set; }
        public IEnumerable<VectorLayer> VectorLayers { get; set; }
    }
}
