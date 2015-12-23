using System;
using TileProj;

namespace TileCook
{
    public static class TileStore
    {
        public static void Copy(this ITileStore source, IWritableTileStore target)
        {
            // set target metadata
            var info = source.GetTileInfo();
            source.Copy(target, info.Bounds, info.MinZoom, info.MaxZoom);
        }
        
        public static void Copy(this ITileStore source, IWritableTileStore target, double[] bounds, int minzoom, int maxzoom)
        {
            source.Copy(target, bounds, minzoom, maxzoom, null);
        }
        
        
        public static void Copy(this ITileStore source, IWritableTileStore target, double[] bounds, int minzoom, int maxzoom, IProgress<int> progress)
        {
            // set target metadata
            var info = source.GetTileInfo();
            info.Bounds = bounds;
            info.MinZoom = minzoom;
            info.MaxZoom = maxzoom;
            target.SetTileInfo(info);
            
            //scanline
            int ops = 0;
            SphericalMercator sm = new SphericalMercator();
            for (int i = info.MinZoom; i <= info.MaxZoom; i++)
            {
                IEnvelope b = new Envelope(info.Bounds[0], info.Bounds[1], info.Bounds[2], info.Bounds[3]);
                IEnvelope env = sm.LongLatToXY(b);
                ICoord LL = sm.PointToCoord(env.LL, i);
                ICoord UR = sm.PointToCoord(env.UR, i);

                for (int j = LL.X; j <= UR.X; j++)
                {
                    for (int k = LL.Y; k >= UR.Y; k--)
                    {
                        ITile tile = source.GetTile(i, j, k);
                        if (tile != null)
                        {
                            target.PutTile(i, j, k, tile);
                            ops++;
                            if (progress != null && ops % 100 == 0)
                            {
                                progress.Report(ops);
                            }
                        }
                    }
                }
            } 
        }
    }
}