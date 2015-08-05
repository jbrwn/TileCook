using System;
using System.Collections.Generic;

namespace TileCook
{
    public class VectorLayer
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public IDictionary<string, string> Fields { get; set; }
    }
}
