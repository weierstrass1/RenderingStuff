
using DyxenCanvasComponents;
using ILGPU;
using RenderLibrary;
using ResourceManager;
using SNESControllers;
using SNESRender;
using SNESRender.ColorManagement;
using WindowsFormsRender;

namespace SNESComponents
{
    public partial class SpriteGraphicsSelector4bpp : UserControl
    {
        public SNESGraphicsSelectorController<LoadSNES4bppGraphics> Controller { get; private set; }
        public SpriteGraphicsSelector4bpp()
        {
            Controller = new(128, 2, 8, 16, SingletonManager.Get<SNESPalette>()!);
            ZoomManager.Link(Controller, this);
            InitializeComponent();
        }
    }
}
