using DyxenCanvasComponents;
using ResourceManager;
using SNESControllers;
using SNESRender.ColorManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNESComponents
{
    public partial class SNESPaletteSelector : UserControl
    {
        public SNESPaletteSelectorController Controller { get; private set; }
        public SNESPaletteSelector()
        {
            Controller = new(Width, Height, 16, SingletonManager.Get<SNESPalette>()!);
            ZoomManager.Link(Controller, this);
            InitializeComponent();
        }
    }
}
