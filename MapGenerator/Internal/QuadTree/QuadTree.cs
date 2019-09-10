using Dungeoneer.MapGenerator.Data;
using Dungeoneer.MapGenerator.Helpers;

namespace Dungeoneer.MapGenerator.QuadTree
{
    internal class QuadTree : IQuadTree
    {
        public IQuadTree TopLeftQuadTree { get; private set; }
        public IQuadTree TopRightQuadTree { get; private set; }
        public IQuadTree BottomLeftQuadTree { get; private set; }
        public IQuadTree BottomRightQuadTree { get; private set; }

        public Quad Quad { get; }
        public uint QuadArea => Quad.Height * Quad.Width;

        public bool HasChildren => TopLeftQuadTree != null && TopRightQuadTree != null && BottomLeftQuadTree != null && BottomRightQuadTree != null;

        public QuadTree(IRngHelper rngHelper, Quad quad, uint quadTreeSplitDepth, uint quadTreeSplitMaximumOffset)
        {
            Quad = quad;
            
            GenerateQuadTree(rngHelper, quadTreeSplitDepth, quadTreeSplitMaximumOffset);
        }

        private void GenerateQuadTree(IRngHelper rngHelper, uint quadTreeSplitDepth, uint quadTreeSplitMaximumOffset)
        {
            if (quadTreeSplitDepth == 0)
            {
                return;
            }
            
            var xAxisMidpoint = Quad.Width / 2 + Quad.Position.X;
            var yAxisMidpoint = Quad.Height / 2 + Quad.Position.Y;

            var xAxisOffset = (float)xAxisMidpoint / 100 * quadTreeSplitMaximumOffset;
            var yAxisOffset = (float)yAxisMidpoint / 100 * quadTreeSplitMaximumOffset;

            var xAxisSplitPoint = (uint)rngHelper.Next(xAxisMidpoint - xAxisOffset, xAxisMidpoint + xAxisOffset);
            var yAxisSplitPoint = (uint)rngHelper.Next(yAxisMidpoint - yAxisOffset, yAxisMidpoint + yAxisOffset);

            quadTreeSplitDepth--;
            
            TopLeftQuadTree = new QuadTree(
                rngHelper, 
                new Quad(new Position(Quad.Position.X, Quad.Position.Y), xAxisSplitPoint - Quad.Position.X, yAxisSplitPoint - Quad.Position.Y), 
                quadTreeSplitDepth, 
                quadTreeSplitMaximumOffset);
            
            TopRightQuadTree = new QuadTree(
                rngHelper,
                new Quad(new Position(xAxisSplitPoint, Quad.Position.Y), Quad.Width - xAxisSplitPoint + Quad.Position.X, yAxisSplitPoint - Quad.Position.Y), 
                quadTreeSplitDepth, 
                quadTreeSplitMaximumOffset);
            
            BottomLeftQuadTree = new QuadTree(
                rngHelper,
                new Quad(new Position(Quad.Position.X, yAxisSplitPoint), xAxisSplitPoint - Quad.Position.X, Quad.Height - yAxisSplitPoint + Quad.Position.Y),
                quadTreeSplitDepth,
                quadTreeSplitMaximumOffset);
            
            BottomRightQuadTree = new QuadTree(
                rngHelper,
                new Quad(new Position(xAxisSplitPoint, yAxisSplitPoint), Quad.Width - xAxisSplitPoint + Quad.Position.X, Quad.Height - yAxisSplitPoint + Quad.Position.Y),
                quadTreeSplitDepth,
                quadTreeSplitMaximumOffset);
        }
    }
}