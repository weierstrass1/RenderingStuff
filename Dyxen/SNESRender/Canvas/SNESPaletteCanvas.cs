using RenderLibrary;
using RenderLibrary.Drawing;
using SNESRender.ColorManagement;

namespace SNESRender.Canvas
{
    public class SNESPaletteCanvas : DrawingCanvasDecorator
    {
        public SNESPalette? Palette

        {
            get => _palette;
            set
            {
                if (_palette != null)
                    _palette.Changed -= changedTrigger;
                _palette = value;
                if (_palette != null)
                    _palette.Changed += changedTrigger;
                Refresh();
            }
        }
        public int SNESPaletteOffset
        {
            get => _snesPaletteOffset;
            set
            {
                _snesPaletteOffset = value;
                Refresh();
            }
        }
        private SNESPalette? _palette;
        private int _snesPaletteOffset;
        public SNESPaletteCanvas(int width, int height, SNESPalette palette, int snesPaletteOffset = 0) : base(new DrawingCanvas(width, height))
        {
            _snesPaletteOffset = snesPaletteOffset;
            Palette = palette;
        }
        public SNESPaletteCanvas(int width, int height, SNESPalette palette, SNESPaletteOffset snesPaletteOffset) : base(new DrawingCanvas(width, height))
        {
            _snesPaletteOffset = (int)snesPaletteOffset;
            Palette = palette;
        }
        public void Refresh()
        {
            if (Palette == null)
                return;
            BGR555ToARGB kernel = KernelManager.GetKernel<BGR555ToARGB>()!;
            kernel.Run(Palette.Colors, Buffer, _snesPaletteOffset, 0, Palette.Colors.IntExtent);
            TriggerChangedEvent();
        }
        private void changedTrigger(SNESPalette palette)
        {
            Refresh();
        }
    }
}
