using DyxenCanvasComponents;
using ResourceManager;
using SNESControllers;
using SNESRender.ColorManagement;
using SNESRender;

namespace SNESComponents
{
    public partial class SpriteGraphicsSelector8bpp : UserControl
    {
        public SNESGraphicsSelectorController<LoadSNES8bppGraphics> Controller { get; private set; }
        public SpriteGraphicsSelector8bpp()
        {
            Controller = new(128, 2, 8, 16, SingletonManager.Get<SNESPalette>()!);
            ZoomManager.Link(Controller, this);
            InitializeComponent();
        }
    }
}
