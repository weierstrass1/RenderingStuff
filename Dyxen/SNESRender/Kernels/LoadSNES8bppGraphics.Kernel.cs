using ILGPU;

namespace SNESRender
{
    public partial class LoadSNES8bppGraphics
    {
        private Action<Index1D, ArrayView<byte>, ArrayView<byte>, int, int> kernel;
        private static void loadSNES8bppGraphics(Index1D index, ArrayView<byte> src, ArrayView<byte> dest, int srcOffset, int destOffset)
        {
            int srcIndex = (index << 6) + srcOffset;
            if (srcIndex + 63 >= src.Extent)
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
            byte b4;
            byte b5;
            byte b6;
            byte b7;
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
                b4 = src[srcIndex + i + 32];
                b5 = src[srcIndex + i + 33];
                b6 = src[srcIndex + i + 48];
                b7 = src[srcIndex + i + 49];
                for (int j = 0; j < 8; j++)
                {
                    res = ((b0 & mul) | 
                        ((b1 & mul) << 1) | 
                        ((b2 & mul) << 2) | 
                        ((b3 & mul) << 3) |
                        ((b4 & mul) << 4) |
                        ((b5 & mul) << 5) |
                        ((b6 & mul) << 6) |
                        ((b7 & mul) << 7)) >> j;

                    dest[destIndex + (7 - j) + yAdder] = (byte)res;
                    mul <<= 1;
                }
                yAdder += 128;
            }
        }
    }
}
