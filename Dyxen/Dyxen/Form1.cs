using RenderLibrary.Drawing;
using SNESComponents;
using SNESGraphicsProcess;
using SNESRender;

namespace Dyxen;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        var bytes = File
            .ReadAllBytes(Path
            .Combine("Resources", "ExGFXF34.bin"));
        spriteGraphicsSelector8bpp1.Controller.ChessGridColor1 = Color.Black;
        spriteGraphicsSelector8bpp1.Controller.ChessGridColor2 = Color.Black;
        spriteGraphicsSelector8bpp1.Controller.Load(bytes, 0);
        var kernel = KernelManager.GetKernel<PaletteRemap<LoadSNES8bppGraphics>>();
        //var res = kernel.Run<SaveSNES8bppGraphics>(bytes, 0x90, 0xF0);
        var res = kernel.Run<SaveSNES8bppGraphics>(bytes, 0, 0xE1);
        File.WriteAllBytes("ExGFXF34.bin", res);
    }
}
