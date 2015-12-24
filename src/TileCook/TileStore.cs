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
        
        
        public static void Copy(this ITileStore source, IWritableTileStore target, double[] bounds, int minzoom, int maxzoom, IProgress<double> progress)
        {
            // set target metadata
            var info = source.GetTileInfo();
            info.Bounds = bounds;
            info.MinZoom = minzoom;
            info.MaxZoom = maxzoom;
            target.SetTileInfo(info);
            
            // get bounds in meters
            var sm = new SphericalMercator();
            IEnvelope b = new Envelope(info.Bounds[0], info.Bounds[1], info.Bounds[2], info.Bounds[3]);
            IEnvelope env = sm.LongLatToXY(b);
            
            // get total 
            double total = 0;
            for (int i = info.MinZoom; i <= info.MaxZoom; i++)
            {
                ICoord LL = sm.PointToCoord(env.LL, i);
                ICoord UR = sm.PointToCoord(env.UR, i);
                total += ((UR.X + 1) - LL.X) * ((LL.Y + 1) - UR.Y);
            }
            
            // stats
            double ops = 0;
            double lastPct = 0;
            double pct = 0;
            
            //scanline
            for (int i = info.MinZoom; i <= info.MaxZoom; i++)
            {
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
                        }
                        ops += 1;
                        pct = (ops/total) * 100;                           
                        if (progress != null && ((pct - lastPct > .1) || pct % 1 == 0))
                        {
                            lastPct = pct;
                            progress.Report(pct);
                        }
                    }
                }
            } 
        }
    }
}