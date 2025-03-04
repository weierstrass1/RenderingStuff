using DyxenCanvasControllers.Interfaces;
using RenderLibrary;
using RenderLibrary.Decorators;
using RenderLibrary.Drawing;
using SNESRender.Canvas;
using SNESRender.ColorManagement;
using SNESRender.Main;
using System.Drawing;

namespace SNESControllers
{
    public class SNESGraphicsSelectorController<T> : IZoom where T : IKernel, ILoadSNESGraphics
    {
        public Color ChessGridColor1
        {
            get => _chessGridColor1;
            set
            {
                _chessGridColor1 = value;
                drawChessGrid();
            }
        }
        public Color ChessGridColor2
        {
            get => _chessGridColor2;
            set
            {
                _chessGridColor2 = value;
                drawChessGrid();
            }
        }
        public int SmallSize { get; private set; }
        public int BigSize { get; private set; }
        public ZoomeableDrawingCanvas MainCanvas { get; private set; }
        public MultilayerCanvas Layers { get; private set; }
        public DrawingCanvas Background { get; private set; }
        public SNESGraphicsCanvas<T> canvas { get; private set; }

        public ZoomeableDrawingCanvas ZoomedCanvas { get => MainCanvas; }

        private Color _chessGridColor1 = Color.Magenta;
        private Color _chessGridColor2 = Color.DarkMagenta;
        public SNESGraphicsSelectorController(int height, int zoom, int size1, int size2, SNESPalette palette)
        {
            SmallSize = Math.Max(Math.Min(size1, size2), 1);
            BigSize = Math.Max(Math.Max(size1, size2), 1);
            canvas = new(height, palette);
            canvas.Clear();
            Background = new(128, height);
            Layers = new(128, height);
            Layers.AddLayer(Background);
            Layers.AddLayer(canvas);
            MainCanvas = new(128, height, Layers);
            MainCanvas.Zoom = (uint)zoom;
            drawChessGrid();
        }
        public void Load(byte[] data, int destOffset)
        {
            canvas.LoadGraphics(data, destOffset: destOffset);
        }
        private void drawChessGrid()
        {
            Background.Clear();
            ChessBackground kernel = KernelManager.GetKernel<ChessBackground>()!;
            kernel.Run(Background.Buffer.View, _chessGridColor1, _chessGridColor2, (0, 0), (SmallSize, SmallSize), Background.Size);
            Background.TriggerChangedEvent();
        }
    }
}
