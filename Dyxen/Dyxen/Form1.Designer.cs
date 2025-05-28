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
        spriteGraphicsSelector8bpp1 = new SNESComponents.SpriteGraphicsSelector8bpp();
        SuspendLayout();
        // 
        // spriteGraphicsSelector8bpp1
        // 
        spriteGraphicsSelector8bpp1.BackgroundImage = (Image)resources.GetObject("spriteGraphicsSelector8bpp1.BackgroundImage");
        spriteGraphicsSelector8bpp1.Location = new Point(12, 12);
        spriteGraphicsSelector8bpp1.MaximumSize = new Size(225, 225);
        spriteGraphicsSelector8bpp1.Name = "spriteGraphicsSelector8bpp1";
        spriteGraphicsSelector8bpp1.Size = new Size(225, 225);
        spriteGraphicsSelector8bpp1.TabIndex = 3;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1152, 450);
        Controls.Add(spriteGraphicsSelector8bpp1);
        Name = "Form1";
        Text = "Form1";
        ResumeLayout(false);
    }

    #endregion
    private SNESComponents.SpriteGraphicsSelector8bpp spriteGraphicsSelector8bpp1;
}
