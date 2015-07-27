using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TileProj;
using TileCook;

namespace TileCook.Test
{
    public class MockStore : ITileStore
    {
        public VectorTile GetTile(ICoord coord)
        {
            return new VectorTile(new byte[0]);
        }
        public int MinZoom { get; set; }
        public int MaxZoom { get; set; }
        public IEnvelope Bounds { get; set; }
    }

    public class MockWritableStore : IWritableTileStore
    {

        public VectorTile GetTile(ICoord coord)
        {
            return new VectorTile(new byte[0]);
        }
        public void PutTile(ICoord coord, VectorTile tile)
        {
            Count++;
            return;
        }
        public int MinZoom { get; set; }
        public int MaxZoom { get; set; }
        public IEnvelope Bounds { get; set; }
        public int Count { get; private set; }
        
    }

    public class CopyTests
    {
        [Fact]
        public void Copy()
        {
            MockStore source = new MockStore();
            MockWritableStore target = new MockWritableStore()
            {
                Bounds = new Envelope(-180, -90, 180, 90),
                MinZoom = 0,
                MaxZoom = 5
            };
            source.Copy(target);
            Assert.Equal(1365, target.Count);
        }
    }
}
