using ILGPU;
using ILGPUUtils.ColorType;

namespace ILGPUUtils;

public class IndexUtils
{
    public static int Index2DToInt(Index2D index, Index2D dim)
    {
        return (index.X + (dim.X * index.Y)) * 4;
    }
    public static void SetValue(long index, ArrayView<byte> view, ARGBColor color)
    {
        if (index < 0 || index + 3 >= view.Length)
            return;
        view[index] = color.B;
        view[index + 1] = color.G;
        view[index + 2] = color.R;
        view[index + 3] = color.A;
    }
    public static void SetValue(long index, ArrayView<byte> view, byte B, byte G, byte R, byte A)
    {
        if (index < 0 || index + 3 >= view.Length)
            return;
        view[index] = B;
        view[index + 1] = G;
        view[index + 2] = R;
        view[index + 3] = A;
    }
}
