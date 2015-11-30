
namespace TileCook
{
    public class Tile : ITile
    {
        public Tile(byte[] buffer)
        {
            Buffer = buffer;
        }

        public byte[] Buffer { get; private set; }
    }
}
