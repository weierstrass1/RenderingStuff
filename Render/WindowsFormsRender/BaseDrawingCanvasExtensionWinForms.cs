using System.Drawing;
using System.Drawing.Imaging;
using RenderLibrary;
using ILGPU.Runtime;
using System.Runtime.InteropServices;

namespace WindowsFormsRender;

public static class BaseDrawingCanvasExtensionWinForms
{
    public static void FromBitmap(this BaseDrawingCanvas canvas, Bitmap bmp)
    {
        BitmapData bmpdata = bmp.LockBits(new(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

        int numbytes = bmpdata.Stride * bmp.Height;
        byte[] data = new byte[numbytes];
        IntPtr ptr = bmpdata.Scan0;

        Marshal.Copy(ptr, data, 0, numbytes);
        canvas.Buffer.CopyFromCPU(data);

        bmp.UnlockBits(bmpdata);
    }
    public static Bitmap ToBitmap(this BaseDrawingCanvas canvas)
    {
        Bitmap bmp = new(canvas.Width, canvas.Height);
        BitmapData bmpdata = bmp.LockBits(new(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

        byte[] data = new byte[canvas.Width * canvas.Height * 4];
        canvas.Buffer.CopyToCPU(data);

        IntPtr ptr = bmpdata.Scan0;
        Marshal.Copy(data, 0, ptr, data.Length);

        bmp.UnlockBits(bmpdata);
        return bmp;
    }
}
