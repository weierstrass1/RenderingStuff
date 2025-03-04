using ILGPU;
using ILGPU.Runtime;
using RenderLibrary;
using RenderLibrary.Drawing;
using SNESRender.ColorManagement;
using SNESRender.Main;

namespace SNESRender.Canvas
{
    public class SNESGraphicsCanvas<T> : DrawingCanvasDecorator where T : ILoadSNESGraphics, IKernel
    {
        public SNESPalette? Palette

        {
            get => _palette;
            set
            {
                if (_palette != null)
                    _palette.Changed -= previewChangedTrigger;
                _palette = value;
                if (_palette != null)
                    _palette.Changed += previewChangedTrigger;
                updatePreview();
            }
        }
        public int SNESPaletteOffset
        {
            get => _snesPaletteOffset;
            set
            {
                _snesPaletteOffset = value;
                updatePreview();
            }
        }
        public event Action<SNESGraphicsCanvas<T>>? PreviewChanged;
        public MemoryBuffer1D<byte, Stride1D.Dense> GFXBuffer { get; private set; }
        private SNESPalette? _palette;
        private int _snesPaletteOffset;
        public SNESGraphicsCanvas(int height, SNESPalette palette, SNESPaletteOffset palOffset) : base(new DrawingCanvas(128, height))
        {
            GFXBuffer = KernelManager.Core.Allocate1D<byte>(128 * height);
            _snesPaletteOffset = (int)palOffset;
            Palette = palette;
        }
        public SNESGraphicsCanvas(int height, SNESPalette palette, int palOffset = 0) : base(new DrawingCanvas(128, height))
        {
            GFXBuffer = KernelManager.Core.Allocate1D<byte>(128 * height);
            _snesPaletteOffset = palOffset;
            Palette = palette;
        }
        public SNESGraphicsCanvas(int height, byte[] data, SNESPalette palette, SNESPaletteOffset palOffset) : base(new DrawingCanvas(128, height))
        {
            GFXBuffer = KernelManager.Core.Allocate1D<byte>(128 * height);
            _snesPaletteOffset = (int)palOffset;
            Palette = palette;
        }
        public SNESGraphicsCanvas(int height, byte[] data, SNESPalette palette, int palOffset = 0) : base(new DrawingCanvas(128, height))
        {
            GFXBuffer = KernelManager.Core.Allocate1D<byte>(128 * height);
            _snesPaletteOffset = palOffset;
            Palette = palette;
        }
        public SNESGraphicsCanvas(byte[] data, SNESPalette palette, SNESPaletteOffset palOffset) : base(new DrawingCanvas(128, getHeight(data.Length) / 128))
        {
            GFXBuffer = KernelManager.Core.Allocate1D<byte>(getNumberOfColors(data.Length));
            _snesPaletteOffset = (int)palOffset;
            Palette = palette;
            LoadGraphics(data);
        }
        public SNESGraphicsCanvas(byte[] data, SNESPalette palette, int palOffset = 0) : base(new DrawingCanvas(128, getHeight(data.Length) / 128))
        {
            GFXBuffer = KernelManager.Core.Allocate1D<byte>(getNumberOfColors(data.Length));
            _snesPaletteOffset = palOffset;
            Palette = palette;
            LoadGraphics(data);
        }
        public void LoadGraphics(byte[] data, int srcOffset = 0, int destOffset = 0)
        {
            T kernel = KernelManager.GetKernel<T>()!;
            MemoryBuffer1D<byte, Stride1D.Dense> dataBuffer = KernelManager.Core.Allocate1D<byte>(data.Length);
            dataBuffer.CopyFromCPU(data);
            kernel.Run(dataBuffer.View, GFXBuffer.View, srcOffset, destOffset);
            KernelManager.Core.DisposeBuffer(dataBuffer);
            updatePreview();
            TriggerChangedEvent();
        }
        private void previewChangedTrigger(SNESPalette obj)
        {
            updatePreview();
            PreviewChanged?.Invoke(this);
        }
        private void updatePreview()
        {
            if (Palette == null)
                return;
            Clear();
            SNESGraphicsToARGB kernel = KernelManager.GetKernel<SNESGraphicsToARGB>()!;
            kernel.Run(GFXBuffer.View, Palette.Colors.View, Buffer.View, 0, SNESPaletteOffset, 0, GFXBuffer.IntExtent);
            PreviewChanged?.Invoke(this);
        }
        private static int getNumberOfColors(int dataLength)
        {
            int bpps = T.BPPs;
            return (dataLength << 3) / bpps;
        }
        private static int getHeight(int dataLength)
        {
            int colors = getNumberOfColors(dataLength);
            int adder = (colors & 0x7F) > 0 ? 1 : 0;
            return (colors >> 7) + adder;
        }
    }
}
