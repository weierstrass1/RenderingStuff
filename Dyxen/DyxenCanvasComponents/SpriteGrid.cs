using DyxenCanvasControllers;
using RenderLibrary;
using WindowsFormsRender;
using ILGPU;

namespace DyxenCanvasComponents
{
    public partial class SpriteGrid : UserControl
    {
        private SpriteGridController controller;
        private Bitmap? image;
        public SpriteGrid()
        {
            controller = new(256, 256, (16, 16));
            controller.CanvasChanged += canvasChanged;
            controller.MaxSizeChanged += canvasMaxSizeChanged;
            Resize += resize;
            InitializeComponent();
            canvasMaxSizeChanged(controller.MaxSize);
        }
        private void canvasMaxSizeChanged(Index2D obj)
        {
            MaximumSize = new(obj.X, obj.Y);
        }
        private void resize(object? sender, EventArgs e)
        {
            controller.Canvas.ChangeSize(Width, Height);
        }
        private void canvasChanged(BaseDrawingCanvas obj)
        {
            image = obj.ToBitmap();
            Refresh();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (image != null)
            {
                pe.Graphics.DrawImageUnscaled(image, 0, 0);
                image.Save("image.png");
            }
            base.OnPaint(pe);
        }
    }
}
