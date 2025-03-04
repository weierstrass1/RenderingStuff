using ILGPU;

namespace SNESRender.Main
{
    public interface ILoadSNESGraphics
    {
        static virtual int BPPs { get => 4; }
        void Run(ArrayView<byte> src, ArrayView<byte> dest, int srcOffset, int destOffset);
    }
}
