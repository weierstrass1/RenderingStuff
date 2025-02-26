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
        spriteGrid1 = new DyxenCanvasComponents.SpriteGrid();
        SuspendLayout();
        // 
        // spriteGrid1
        // 
        spriteGrid1.Location = new Point(202, 23);
        spriteGrid1.Name = "spriteGrid1";
        spriteGrid1.Size = new Size(384, 384);
        spriteGrid1.TabIndex = 0;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(spriteGrid1);
        Name = "Form1";
        Text = "Form1";
        ResumeLayout(false);
    }

    #endregion

    private DyxenCanvasComponents.SpriteGrid spriteGrid1;
}
