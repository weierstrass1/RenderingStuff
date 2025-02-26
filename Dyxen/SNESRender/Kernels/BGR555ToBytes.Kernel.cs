using ILGPU;
using SNESRender.ColorManagement;

namespace SNESRender
{
    public partial class BGR555ToBytes
    {
		private Action<Index1D, ArrayView<BGR555Color>, ArrayView<byte>, int, int> kernel;
		private static void bgr555ToBytes(Index1D index, ArrayView<BGR555Color> src, ArrayView<byte> dest, int srcOffset, int destOffset)
		{
			int destIndex = index * 2;
			if (index >= src.Extent || destIndex + 1 >= dest.Extent)
				return;
			dest[destIndex] = src[index].LowByte;
			dest[destIndex + 1] = src[index].HighByte;
		}
	}
}
