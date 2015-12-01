using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TileCook;

namespace TileCook.Test
{
	public class TileInfoTests
    {
        [Fact]
        public void Defaults()
        {
			TileInfo info = new TileInfo();
			Assert.Equal("", info.Name);
			Assert.Equal("", info.Description);
			Assert.Equal(0, info.MinZoom);
			Assert.Equal(14, info.MaxZoom);
			Assert.Equal(new double[] {-180,-90,180,90}, info.Bounds);
			Assert.Null(info.Center);
			Assert.Null(info.VectorLayers);
        }
		
				[Fact]
        public void Valid_Properties()
        {
			TileInfo info = new TileInfo()
			{
				Name = "test",
				Description = "test info",
				MinZoom = 0,
				MaxZoom = 22,
				Bounds = new double[] {-10,-10,10,10},
				Center = new double[] {0,0,10}
			};
			info.Validate();
			Assert.Equal("test", info.Name);
			Assert.Equal("test info", info.Description);
			Assert.Equal(0, info.MinZoom);
			Assert.Equal(22, info.MaxZoom);
			Assert.Equal(new double[] {-10,-10,10,10}, info.Bounds);
			Assert.Equal(new double[] {0,0,10}, info.Center);
			Assert.Null(info.VectorLayers);
		}
		
		[Fact]
        public void Invalid_Properties()
        {
			TileInfo info = new TileInfo();
			Assert.Throws<ArgumentNullException>(() => info.Name = null);
			Assert.Throws<ArgumentException>(() => info.Name = new string('x', 256));
			Assert.Throws<ArgumentNullException>(() => info.Description= null);
			Assert.Throws<ArgumentException>(() => info.Description = new string('x', 2001));
			Assert.Throws<ArgumentException>(() => info.MinZoom = -1);
			Assert.Throws<ArgumentException>(() => info.MinZoom = 25);
			Assert.Throws<ArgumentException>(() => info.MaxZoom = -1);
			Assert.Throws<ArgumentException>(() => info.MaxZoom = 25);
			Assert.Throws<ArgumentException>(() => info.Bounds = null);
			Assert.Throws<ArgumentException>(() => info.Bounds = new double[] {0});
		    Assert.Throws<ArgumentException>(() => info.Bounds = new double[] {400,400,400,400});
			Assert.Throws<ArgumentException>(() => info.Bounds = new double[] {10,10,0,0});
			Assert.Throws<ArgumentException>(() => info.Center = new double [] {400,400,0});
			Assert.Throws<ArgumentException>(() => info.Center = new double[] {0});
        }
		
		[Fact]
        public void Invalid_State()
        {
			TileInfo info = new TileInfo();
			info.MinZoom = 10;
			info.MaxZoom = 0;
			Assert.Throws<InvalidOperationException>(() => info.Validate());
			info.MinZoom = 0;
			info.MaxZoom = 20;
			info.Center = new double[] {0,0,21};
			Assert.Throws<InvalidOperationException>(() => info.Validate());
			info.MinZoom = 0;
			info.MaxZoom = 10;
			info.Bounds = new double[] {-10,-10,10,10};
			info.Center = new double[] {20,20,5};
			Assert.Throws<InvalidOperationException>(() => info.Validate());
			info.Center = new double[] {0,0,5};
			info.Validate();
			
		}
    }
	
}