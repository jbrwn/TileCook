using TileProj;

namespace TileCook
{
    public static class TileStore
    {
        public static void Copy(this ITileStore source, IWritableTileStore target)
        {
            SphericalMercator sm = new SphericalMercator();

            //scanline
            for (int i = target.MinZoom; i <= target.MaxZoom; i++)
            {
                IEnvelope env = sm.LongLatToXY(target.Bounds);
                ICoord LL = sm.PointToCoord(env.LL, i);
                ICoord UR = sm.PointToCoord(env.UR, i);

                for (int j = LL.X; j <= UR.X; j++)
                {
                    for (int k = LL.Y; k >= UR.Y; k--)
                    {
                        ICoord coord = new Coord(i, j, k);
                        VectorTile tile = source.GetTile(coord);
                        if (tile != null)
                        {
                            target.PutTile(coord, tile);
                        }
                    }
                }

            }
        }
    }
}
