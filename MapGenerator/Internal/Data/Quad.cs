namespace Dungeoneer.MapGenerator.Data
{
    internal class Quad : IQuad
    {
        public IPosition Position { get; }
        public uint Width { get; }
        public uint Height { get; }
        public uint Area => Width * Height;

        public Quad(IPosition position, uint width, uint height)
        {
            Position = position;
            Width = width;
            Height = height;
        }
    }
}