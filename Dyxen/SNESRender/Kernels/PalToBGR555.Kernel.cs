using ILGPU;
using SNESRender.ColorManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNESRender
{
    public partial class PalToBGR555
    {
        private Action<Index1D, ArrayView<byte>, ArrayView<BGR555Color>, int, int> kernel;
        private static void palToBGR555(Index1D index, ArrayView<byte> src, ArrayView<BGR555Color> dest, int srcOffset, int destOffset)
        {
            int srcIndex = index * 3;
            if (srcIndex + 2 >= src.Extent || index >= dest.Extent)
                return;
            dest[index] = new(src[srcIndex], src[srcIndex + 1], src[srcIndex + 2]);
        }
    }
}
