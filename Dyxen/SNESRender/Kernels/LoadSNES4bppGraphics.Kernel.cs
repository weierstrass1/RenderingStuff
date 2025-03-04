using ILGPU;

namespace SNESRender
{
    public partial class LoadSNES4bppGraphics
    {
        private Action<Index1D, ArrayView<byte>, ArrayView<byte>, int, int> kernel;
        private static void loadSNES4bppGraphics(Index1D index, ArrayView<byte> src, ArrayView<byte> dest, int srcOffset, int destOffset)
        {
            int srcIndex = (index << 5) + srcOffset;
            if (srcIndex + 31 >= src.Extent)
                return;

            int destX = index & 0x0F;
            int destY = index >> 4;
            int destIndex = ((destY << 7) + destX) << 3;
            destIndex += destOffset;
            if (destIndex + 903 >= dest.Extent)
                return;

            byte b0;
            byte b1;
            byte b2;
            byte b3;
            int res;
            int mul;
            int yAdder = 0;
            for (int i = 0; i < 16; i += 2) 
            {
                mul = 1;
                b0 = src[srcIndex + i];
                b1 = src[srcIndex + i + 1];
                b2 = src[srcIndex + i + 16];
                b3 = src[srcIndex + i + 17];
                for (int j = 0; j < 8; j++)
                {
                    res = ((b0 & mul) | ((b1 & mul) << 1) | ((b2 & mul) << 2) | ((b3 & mul) << 3)) >> j;

                    dest[destIndex + (7 - j) + yAdder] = (byte)res;
                    mul <<= 1;
                }
                yAdder += 128;
            }
        }
    }
}
