using ILGPU;
using ILGPU.Runtime;
using ILGPUUtils;
using RenderLibrary.Drawing;

namespace SNESRender.ColorManagement;

public enum SnesPaletteFormat : int
{
    Bin = 2,
    Pal = 3
}
public enum SnesPaletteType : int
{
    TwoBpp = 4,
    FourBpp = 16,
    EightBpp = 256
}
public class SNESPalette
{
    public event Action<SNESPalette>? Changed;
    public MemoryBuffer1D<BGR555Color, Stride1D.Dense> Colors { get; private set; }
    public SNESPalette(SnesPaletteType type)
    {
        Colors = KernelManager.Core.Allocate1D<BGR555Color>((int)type);
    }
    public SNESPalette(SnesPaletteType type, RenderCore core)
    {
        Colors = core.Allocate1D<BGR555Color>((int)type);
    }
    public SNESPalette(int length)
    {
        Colors = KernelManager.Core.Allocate1D<BGR555Color>(length);
    }
    public SNESPalette(int length, RenderCore core)
    {
        Colors = core.Allocate1D<BGR555Color>(length);
    }
    public SNESPalette(byte[] data, SnesPaletteFormat format)
    {
        Colors = KernelManager.Core.Allocate1D<BGR555Color>(data.Length / (int)format);
        Load(data, format);
    }
    public SNESPalette(byte[] data, SnesPaletteFormat format, SnesPaletteType type, int offset = 0)
    {
        Colors = KernelManager.Core.Allocate1D<BGR555Color>((int)type);
        Load(data, format, srcOffset: offset);
    }
    public void Load(byte[] data, SnesPaletteFormat format, int srcOffset = 0, int destOffset = 0)
    {
        Load(data, format, Colors.IntExtent, 0, 0);
    }
    public void Load(byte[] data, SnesPaletteFormat format, int numberOfColors, int srcOffset = 0, int destOffset = 0)
    {
        using var buffer = KernelManager.Core.Allocate1D<byte>(data.Length);
        buffer.CopyToCPU(data);
        int formatSize = (int)format;
        if (format == SnesPaletteFormat.Bin)
            KernelManager.GetKernel<BytesToBGR555>()!.Run(buffer, Colors, srcOffset, destOffset, numberOfColors);
        else
            KernelManager.GetKernel<PalToBGR555>()!.Run(buffer, Colors, srcOffset, destOffset, numberOfColors);
        Changed?.Invoke(this);
    }
}
