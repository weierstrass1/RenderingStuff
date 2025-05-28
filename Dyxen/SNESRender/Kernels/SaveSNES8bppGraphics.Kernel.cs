using ILGPU;

namespace SNESRender
{
    public partial class SaveSNES8bppGraphics
    {
        private Action<Index1D, ArrayView<byte>, ArrayView<byte>, int, int> kernel;
        private static void saveSNES8bppGraphics(Index1D index, ArrayView<byte> src, ArrayView<byte> dest, int srcOffset, int destOffset)
        {
            int destIndex = (index << 6) + destOffset;
            if (destIndex + 63 >= src.Extent)
                return;

            int srcX = index & 0x0F;
            int srcY = index >> 4;
            int srcIndex = ((srcY << 7) + srcX) << 3;
            srcIndex += srcOffset;
            if (srcIndex + 903 >= src.Extent)
                return;

            byte b0, b1, b2, b3, b4, b5, b6, b7;
            byte pix;
            for (int j = 0, k = 0; k < 16; k += 2, j += 128)
            {
                dest[destIndex + k] = 0;
                dest[destIndex + k + 1] = 0;
                dest[destIndex + k + 16] = 0;
                dest[destIndex + k + 17] = 0;
                dest[destIndex + k + 32] = 0;
                dest[destIndex + k + 33] = 0;
                dest[destIndex + k + 48] = 0;
                dest[destIndex + k + 49] = 0;
                for (int i = 0, m = 128; i < 8; i++, m /= 2)
                {
                    pix = src[srcIndex + i + j];
                    b0 = (byte)(pix & 0x01) ;
                    b1 = (byte)((pix & 0x02) >> 1);
                    b2 = (byte)((pix & 0x04) >> 2);
                    b3 = (byte)((pix & 0x08) >> 3);
                    b4 = (byte)((pix & 0x10) >> 4);
                    b5 = (byte)((pix & 0x20) >> 5);
                    b6 = (byte)((pix & 0x40) >> 6);
                    b7 = (byte)((pix & 0x80) >> 7);
                    dest[destIndex + k] += (byte)(b0 * m);
                    dest[destIndex + k + 1] += (byte)(b1 * m);
                    dest[destIndex + k + 16] += (byte)(b2 * m);
                    dest[destIndex + k + 17] += (byte)(b3 * m);
                    dest[destIndex + k + 32] += (byte)(b4 * m);
                    dest[destIndex + k + 33] += (byte)(b5 * m);
                    dest[destIndex + k + 48] += (byte)(b6 * m);
                    dest[destIndex + k + 49] += (byte)(b7 * m);
                }
            }
        }
    }
}
