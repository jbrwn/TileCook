using System;
using System.Collections.Generic;

namespace TileCook
{
    public class VectorTileInfo
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<Tuple<string, string>> Fields { get; set; }
    }
}
