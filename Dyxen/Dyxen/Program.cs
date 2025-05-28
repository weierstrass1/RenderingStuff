using ResourceManager;
using SNESRender.ColorManagement;

namespace Dyxen;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        SNESPalette palette = new(256);
        if (File.Exists("Resources/green.pal"))
            palette.Load(File.ReadAllBytes("Resources/green.pal"), SnesPaletteFormat.Pal, 0, 0);

        SingletonManager.Add(palette);
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}