using DyxenCanvasControllers.Interfaces;
using ILGPU;
using RenderLibrary;
using RenderLibrary.Decorators;
using RenderLibrary.Drawing;
using System.Drawing;
using Grid = RenderLibrary.Drawing.Grid;

namespace DyxenCanvasControllers;

public class SpriteGridController : IZoom
{
    public Index2D MaxSize { get; private set; }
    public uint Zoom
    {
        get => Canvas.Zoom;
        set
        {
            Canvas.Zoom = value;
            int x = (int)(BaseCanvas.Size.X * Canvas.Zoom);
            int y = (int)(BaseCanvas.Size.Y * Canvas.Zoom);
            MaxSize = (x, y);
            MaxSizeChanged?.Invoke(MaxSize);
        }
    }
    public Index2D ZoomedCellSize
    {
        get => new((int)(CellSize.X * Zoom), (int)(CellSize.Y * Zoom));
    }
    public Index2D Offset
    {
        get => Canvas.Offset;
        set
        {
            Canvas.Offset = value;
            drawChessGrid();
        }
    }
    public Index2D CellSize
    {
        get => _cellsize;
        set
        {
            _cellsize = value;
            drawChessGrid();
        }
    }
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
    public Color GridColor
    {
        get => _gridColor;
        set
        {
            if (!ShowGrid)
                return;
            _gridColor = value;
            refreshCanvas();
        }
    }
    public GridConfig GridConfig
    {
        get => _gridConfig;
        set
        {
            if (!ShowGrid)
                return;
            _gridConfig = value;
            refreshCanvas();
        }
    }
    public bool ShowGrid
    {
        get => _showGrid;
        set
        {
            if (value == _showGrid)
                return;
            _showGrid = value;
            refreshCanvas();
        }
    }

    public ZoomeableDrawingCanvas ZoomedCanvas { get => Canvas; }

    public ZoomeableDrawingCanvas Canvas;
    public MultilayerCanvas BaseCanvas;
    public DrawingCanvas ChessCanvas;
    public event Action<Index2D>? MaxSizeChanged;
    public event Action<BaseDrawingCanvas>? CanvasChanged
    {
        add
        {
            Canvas.Changed += value;
        }
        remove
        {
            Canvas.Changed -= value;
        }
    }
    private bool _showGrid = true;
    private GridConfig _gridConfig = GridConfig.Dashed;
    private Index2D _cellsize;
    private Color _chessGridColor1 = Color.LightGray;
    private Color _chessGridColor2 = Color.Gray;
    private Color _gridColor = Color.Black;
    public SpriteGridController(int width, int height, Index2D cellSize)
    {
        BaseCanvas = new(width, height);
        ChessCanvas = new(width, height);
        BaseCanvas.AddLayer(ChessCanvas);
        Canvas = new(width, height, BaseCanvas);
        Canvas.Changed += canvas => drawGrid();
        Offset = (0, 0);
        CellSize = cellSize;
        Zoom = 2;
        drawGrid();
    }
    private void drawChessGrid()
    {
        ChessBackground chess = KernelManager.GetKernel<ChessBackground>()!;
        chess.Run(ChessCanvas.Buffer.View, ChessGridColor1, ChessGridColor2, (0, 0), CellSize, ChessCanvas.Size);
        ChessCanvas.TriggerChangedEvent();
    }
    private void drawGrid()
    {
        if (!ShowGrid)
            return;
        Grid grid = KernelManager.GetKernel<Grid>()!;
        grid.Run(Canvas.View, GridColor, GridConfig.LineLength, GridConfig.Spacing, (0, 0), ZoomedCellSize, Canvas.Offset, Canvas.Size);
    }
    private void refreshCanvas()
    {
        Canvas.Refresh();
        drawGrid();
    }
}
