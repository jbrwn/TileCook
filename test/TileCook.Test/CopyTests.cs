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
        public Tile GetTile(int z, int x, int y)
        {
            return new Tile(new byte[0]);
        }
        public TileInfo GetTileInfo()
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
        public Tile GetTile(int z, int x, int y)
        {
            return new Tile(new byte[0]);
        }
        public void PutTile(int z, int x, int y, ITile tile)
        {
            Count++;
            return;
        }
        public TileInfo GetTileInfo()
        {
            return new TileInfo();
        }
        public void SetTileInfo(TileInfo tileinfo)
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

        [Fact]
        public void CopyWithParams()
        {
            MockStore source = new MockStore();
            MockWritableStore target = new MockWritableStore();
            source.Copy(target, new double[] {-180, -90, 180, 90}, 1, 2);
            Assert.Equal(20, target.Count);
        }
    }
}
