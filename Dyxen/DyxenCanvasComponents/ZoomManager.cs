
using DyxenCanvasControllers.Interfaces;
using RenderLibrary.Decorators;
using WindowsFormsRender;

namespace DyxenCanvasComponents
{
    public static class ZoomManager
    {
        public static void Link(IZoom zoomeableCanvas, Control control)
        {
            ZoomeableDrawingCanvas canvas = zoomeableCanvas.ZoomedCanvas;
            canvas.SizeChanged += size => control.MaximumSize = new(size.X, size.Y);
            control.Resize += (obj, ev) => canvas.ChangeSize(control.Size.Width, control.Height);
            canvas.Changed += cvs => control.BackgroundImage = canvas.ToBitmap();
        }
    }
}
