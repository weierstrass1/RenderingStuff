using DyxenCanvasControllers;
using ILGPU;
using RenderLibrary;
using WindowsFormsRender;

namespace DyxenCanvasComponents
{
    public partial class SpriteGrid : UserControl
    {
        private SpriteGridController controller;
        public SpriteGrid()
        {
            controller = new(256, 256, (16, 16));
            ZoomManager.Link(controller, this);
            InitializeComponent();
        }
    }
}
