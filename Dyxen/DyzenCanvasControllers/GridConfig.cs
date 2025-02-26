using ILGPU;

namespace DyxenCanvasControllers
{
    public struct GridConfig
    {
        public static GridConfig Dot = new((1, 1), (3, 3));
        public static GridConfig Line = new((1, 1), (0, 0));
        public static GridConfig Dashed = new((4, 4), (4, 4));
        public Index2D LineLength;
        public Index2D Spacing;
        private GridConfig(Index2D lineLength, Index2D spacing)
        {
            LineLength = lineLength;
            Spacing = spacing;
        }
    }
}
