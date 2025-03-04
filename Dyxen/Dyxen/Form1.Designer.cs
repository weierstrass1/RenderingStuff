namespace Dyxen;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        spriteGrid1 = new DyxenCanvasComponents.SpriteGrid();
        spriteGraphicsSelector4bpp2 = new SNESComponents.SpriteGraphicsSelector4bpp();
        snesPaletteSelector1 = new SNESComponents.SNESPaletteSelector();
        SuspendLayout();
        // 
        // spriteGrid1
        // 
        spriteGrid1.BackgroundImage = (Image)resources.GetObject("spriteGrid1.BackgroundImage");
        spriteGrid1.Location = new Point(370, 12);
        spriteGrid1.MaximumSize = new Size(384, 306);
        spriteGrid1.Name = "spriteGrid1";
        spriteGrid1.Size = new Size(384, 306);
        spriteGrid1.TabIndex = 0;
        // 
        // spriteGraphicsSelector4bpp2
        // 
        spriteGraphicsSelector4bpp2.BackgroundImage = (Image)resources.GetObject("spriteGraphicsSelector4bpp2.BackgroundImage");
        spriteGraphicsSelector4bpp2.Location = new Point(27, 14);
        spriteGraphicsSelector4bpp2.MaximumSize = new Size(256, 256);
        spriteGraphicsSelector4bpp2.Name = "spriteGraphicsSelector4bpp2";
        spriteGraphicsSelector4bpp2.Size = new Size(256, 256);
        spriteGraphicsSelector4bpp2.TabIndex = 1;
        // 
        // snesPaletteSelector1
        // 
        snesPaletteSelector1.BackgroundImage = (Image)resources.GetObject("snesPaletteSelector1.BackgroundImage");
        snesPaletteSelector1.Location = new Point(802, 14);
        snesPaletteSelector1.MaximumSize = new Size(256, 256);
        snesPaletteSelector1.Name = "snesPaletteSelector1";
        snesPaletteSelector1.Size = new Size(256, 256);
        snesPaletteSelector1.TabIndex = 2;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1152, 450);
        Controls.Add(snesPaletteSelector1);
        Controls.Add(spriteGraphicsSelector4bpp2);
        Controls.Add(spriteGrid1);
        Name = "Form1";
        Text = "Form1";
        ResumeLayout(false);
    }

    #endregion

    private DyxenCanvasComponents.SpriteGrid spriteGrid1;
    private SNESComponents.SpriteGraphicsSelector4bpp spriteGraphicsSelector4bpp2;
    private SNESComponents.SNESPaletteSelector snesPaletteSelector1;
}
