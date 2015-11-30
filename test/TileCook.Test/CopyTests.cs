using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TileCook;

namespace TileCook.Test
{
    public class MockStore : ITileStore
    {
        public ITile GetTile(int z, int x, int y)
        {
            return new Tile(new byte[0]);
        }
        public ITileInfo GetTileInfo()
        {
            return new TileInfo()
            {
                Bounds = new double[] {-180, -90, 180, 90},
                MinZoom = 0,
                MaxZoom = 5
            };
        }
    }

    public class MockWritableStore : IWritableTileStore
    {
        public int Count {get; private set;}
        public ITile GetTile(int z, int x, int y)
        {
            return new Tile(new byte[0]);
        }
        public void PutTile(int z, int x, int y, ITile tile)
        {
            Count++;
            return;
        }
        public ITileInfo GetTileInfo()
        {
            return new TileInfo();
        }
        public void SetTileInfo(ITileInfo tileinfo)
        {
            
        }
        
    }

    public class CopyTests
    {
        [Fact]
        public void Copy()
        {
            MockStore source = new MockStore();
            MockWritableStore target = new MockWritableStore();
            source.Copy(target);
            Assert.Equal(1365, target.Count);
        }
    }
}
