using ILGPU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNESRender.Main
{
    public interface ISaveSnesGraphics
    {
        static virtual int BPPs { get => 4; }
        void Run(ArrayView<byte> src, ArrayView<byte> dest, int srcOffset, int destOffset);
    }
}
