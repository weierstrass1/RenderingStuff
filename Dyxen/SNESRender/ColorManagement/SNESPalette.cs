using ILGPU;
using ILGPU.Runtime;
using RenderLibrary.Drawing;

namespace SNESRender.ColorManagement;

public enum SNESPaletteOffset : int
{
    Pal8bpp0 = 0,
    Pal4bpp0 = 0,
    Pal4bpp1 = 16,
    Pal4bpp2 = 32,
    Pal4bpp3 = 48,
    Pal4bpp4 = 64,
    Pal4bpp5 = 80,
    Pal4bpp6 = 96,
    Pal4bpp7 = 112,
    Pal4bpp8 = 128,
    Pal4bpp9 = 144,
    Pal4bppA = 160,
    Pal4bppB = 176,
    Pal4bppC = 192,
    Pal4bppD = 208,
    Pal4bppE = 224,
    Pal4bppF = 240,
    Pal2bpp0 = 0,
    Pal2bpp1 = 4,
    Pal2bpp2 = 8,
    Pal2bpp3 = 12,
    Pal2bpp4 = 16,
    Pal2bpp5 = 20,
    Pal2bpp6 = 24,
    Pal2bpp7 = 28,
    Pal2bpp0L1Mode0 = 0,
    Pal2bpp1L1Mode0 = 4,
    Pal2bpp2L1Mode0 = 8,
    Pal2bpp3L1Mode0 = 12,
    Pal2bpp4L1Mode0 = 16,
    Pal2bpp5L1Mode0 = 20,
    Pal2bpp6L1Mode0 = 24,
    Pal2bpp7L1Mode0 = 28,
    Pal2bpp0L2Mode0 = 32,
    Pal2bpp1L2Mode0 = 36,
    Pal2bpp2L2Mode0 = 40,
    Pal2bpp3L2Mode0 = 44,
    Pal2bpp4L2Mode0 = 48,
    Pal2bpp5L2Mode0 = 52,
    Pal2bpp6L2Mode0 = 56,
    Pal2bpp7L2Mode0 = 60,
    Pal2bpp0L3Mode0 = 64,
    Pal2bpp1L3Mode0 = 68,
    Pal2bpp2L3Mode0 = 72,
    Pal2bpp3L3Mode0 = 76,
    Pal2bpp4L3Mode0 = 80,
    Pal2bpp5L3Mode0 = 84,
    Pal2bpp6L3Mode0 = 88,
    Pal2bpp7L3Mode0 = 92,
    Pal2bpp0L4Mode0 = 96,
    Pal2bpp1L4Mode0 = 100,
    Pal2bpp2L4Mode0 = 104,
    Pal2bpp3L4Mode0 = 108,
    Pal2bpp4L4Mode0 = 112,
    Pal2bpp5L4Mode0 = 116,
    Pal2bpp6L4Mode0 = 120,
    Pal2bpp7L4Mode0 = 124,
    Pal4bppBG0 = 0,
    Pal4bppBG1 = 16,
    Pal4bppBG2 = 32,
    Pal4bppBG3 = 48,
    Pal4bppBG4 = 64,
    Pal4bppBG5 = 80,
    Pal4bppBG6 = 96,
    Pal4bppBG7 = 112,
    Pal4bppSP0 = 128,
    Pal4bppSP1 = 144,
    Pal4bppSP2 = 160,
    Pal4bppSP3 = 176,
    Pal4bppSP4 = 192,
    Pal4bppSP5 = 208,
    Pal4bppSP6 = 224,
    Pal4bppSP7 = 240,
}
public enum SnesPaletteFormat : int
{
    Bin = 2,
    Pal = 3
}
public enum SnesPaletteType : int
{
    TwoBpp = 4,
    ThreeBpp = 8,
    FourBpp = 16,
    EightBpp = 256
}
public class SNESPalette : IDisposable
{
    public event Action<SNESPalette>? Changed;
    public MemoryBuffer1D<BGR555Color, Stride1D.Dense> Colors { get; private set; }
    public bool Disposed { get; private set; } = false;
    public SNESPalette(SnesPaletteType type)
    {
        Colors = KernelManager.Core.Allocate1D<BGR555Color>((int)type);
    }
    public SNESPalette(int length)
    {
        Colors = KernelManager.Core.Allocate1D<BGR555Color>(length);
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
        MemoryBuffer1D<byte, Stride1D.Dense> buffer = KernelManager.Core.Allocate1D<byte>(data.Length);
        buffer.CopyFromCPU(data);
        int formatSize = (int)format;
        if (format == SnesPaletteFormat.Bin)
            KernelManager.GetKernel<BytesToBGR555>()!.Run(buffer, Colors, srcOffset, destOffset, numberOfColors);
        else
            KernelManager.GetKernel<PalToBGR555>()!.Run(buffer, Colors, srcOffset, destOffset, numberOfColors);
        Changed?.Invoke(this);
        KernelManager.Core.DisposeBuffer(buffer);
    }
    public void Dispose()
    {
        if (Disposed)
            return;
        Disposed = true;
        KernelManager.Core.DisposeBuffer(Colors);
        GC.SuppressFinalize(this);
    }
}
