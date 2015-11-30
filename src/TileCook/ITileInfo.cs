using System.Collections.Generic;

namespace TileCook
{
    public interface ITileInfo
    {
		string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int MinZoom { get; set; }
        int MaxZoom { get; set; }
        double[] Bounds { get; set; }
        IEnumerable<VectorLayer> VectorLayers { get; set; }
	}
}