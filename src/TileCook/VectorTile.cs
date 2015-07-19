
namespace TileCook
{
    public class VectorTile : ITile
    {
        public VectorTile(byte[] buffer)
        {
            Buffer = buffer;
        }

        public byte[] Buffer { get; private set; }
    }
}
