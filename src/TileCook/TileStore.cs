using TileProj;

namespace TileCook
{
    public static class TileStore
    {
        public static void Copy(this ITileStore source, IWritableTileStore target)
        {
            // set target metadata
            var info = source.GetTileInfo();
            target.SetTileInfo(info);
            
            SphericalMercator sm = new SphericalMercator();
            
            //scanline
            for (int i = info.MinZoom; i <= info.MaxZoom; i++)
            {
                IEnvelope bounds = new Envelope(info.Bounds[0], info.Bounds[1], info.Bounds[2], info.Bounds[3]);
                IEnvelope env = sm.LongLatToXY(bounds);
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
                    }
                }

            }
        }
    }
}
