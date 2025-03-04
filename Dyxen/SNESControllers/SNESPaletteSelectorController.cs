using DyxenCanvasControllers.Interfaces;
using RenderLibrary.Decorators;
using SNESRender.Canvas;
using SNESRender.ColorManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNESControllers
{
    public class SNESPaletteSelectorController : IZoom
    {
        public SNESPalette Palette { get; private set; }
        public ZoomeableDrawingCanvas MainCanvas { get; private set; }
        public SNESPaletteCanvas PaletteCanvas { get; private set; }
        public ZoomeableDrawingCanvas ZoomedCanvas { get => MainCanvas; }
        public SNESPaletteSelectorController(int width, int height, uint zoom, SNESPalette palette)
        {
            if (palette == null)
                palette = new(256);
            Palette = palette;
            PaletteCanvas = new(16, palette.Colors.IntExtent / 16, palette);
            MainCanvas = new(width, height, PaletteCanvas);
            MainCanvas.Zoom = zoom;
        }
        public void Load(byte[] data, SnesPaletteFormat format, int destOffset)
        {
            Palette.Load(data, format, destOffset: destOffset);
        }
    }
}
