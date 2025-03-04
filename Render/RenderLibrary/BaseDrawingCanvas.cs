using ILGPU;
using ILGPU.Runtime;
using ILGPUUtils;
using RenderLibrary.Drawing;
using System.Drawing;

namespace RenderLibrary;

public abstract class BaseDrawingCanvas : IDisposable
{
    public int Width
    {
        get => Size.X;
    }
    public int Height
    {
        get => Size.Y;
    }
    public abstract Index2D Size { get; }
    public ArrayView<byte> View { get => Buffer.View; }
    public abstract MemoryBuffer1D<byte, Stride1D.Dense> Buffer { get; }
    public abstract RenderCore Core { get; }
    public abstract bool Disposed { get; }
    public event Action<BaseDrawingCanvas>? Changed;
    public void CopyTo(BaseDrawingCanvas dest, Index2D offset)
    {
        CopyTo(dest, offset, KernelManager.GetKernel<CopyTo>()!);
    }
    public void CopyTo(BaseDrawingCanvas dest, Index2D offset, CopyTo copyto)
    {
        CopyTo(dest.Buffer, offset, dest.Size, copyto);
    }
    public void CopyTo(MemoryBuffer1D<byte, Stride1D.Dense> dest, Index2D offset, Index2D dimDest, CopyTo copyto)
    {
        copyto.Run(Buffer, dest, offset, Size, dimDest);
        TriggerChangedEvent();
    }
    public void CopyFrom(BaseDrawingCanvas src, Index2D offset)
    {
        CopyFrom(src, offset, KernelManager.GetKernel<CopyTo>()!);
    }
    public void CopyFrom(BaseDrawingCanvas src, Index2D offset, CopyTo copyto)
    {
        src.CopyTo(this, offset, copyto);
        TriggerChangedEvent();
    }
    public void CopyFrom(MemoryBuffer1D<byte, Stride1D.Dense> src, Index2D offset, Index2D dimSrc)
    {
        CopyFrom(src, offset, dimSrc, KernelManager.GetKernel<CopyTo>()!);
    }
    public void CopyFrom(MemoryBuffer1D<byte, Stride1D.Dense> src, Index2D offset, Index2D dimSrc, CopyTo copyto)
    {
        copyto.Run(src, Buffer, offset, dimSrc, Size);
        TriggerChangedEvent();
    }
    public void Draw(BaseDrawingCanvas src, Index2D offset)
    {
        Draw(src, offset, KernelManager.GetKernel<CopyTo>()!);
    }
    public void Draw(BaseDrawingCanvas src, Index2D offset, CopyTo copyto)
    {
        Draw(src.Buffer, offset, src.Size, copyto);
    }
    public void Draw(MemoryBuffer1D<byte, Stride1D.Dense> src, Index2D offset, Index2D dimSrc)
    {
        Draw(src, offset, dimSrc, KernelManager.GetKernel<CopyTo>()!);
    }
    public void Draw(MemoryBuffer1D<byte, Stride1D.Dense> src, Index2D offset, Index2D dimSrc, CopyTo copyto)
    {
        copyto.Run(src, Buffer, offset, dimSrc, Size, CopyToSkipInvisible.Yes);
        TriggerChangedEvent();
    }
    public void Clear()
    {
        DrawFillSquare((0, 0), Size, default);
    }
    public void Clear(DrawFillSquare drawFillSquare)
    {
        DrawFillSquare((0, 0), Size, default, drawFillSquare);
    }
    public void DrawFillSquare(Index2D offset, Index2D size, Color value)
    {
        DrawFillSquare(offset, size, value, KernelManager.GetKernel<DrawFillSquare>()!);
    }
    public void DrawFillSquare(Index2D offset, Index2D size, Color value, DrawFillSquare drawFillSquare)
    {
        drawFillSquare.Run(Buffer, value, offset, size, Size);
        TriggerChangedEvent();
    }
    public void DrawSquare(Index2D offset, Index2D size, Color value)
    {
        DrawSquare(offset, size, value, KernelManager.GetKernel<DrawSquare>()!);
    }
    public void DrawSquare(Index2D offset, Index2D size, Color value, DrawSquare drawSquare)
    {
        drawSquare.Run(Buffer, value, offset, size, Size);
        TriggerChangedEvent();
    }
    public void TriggerChangedEvent()
    {
        Changed?.Invoke(this);
    }
    public abstract void Dispose();
}
