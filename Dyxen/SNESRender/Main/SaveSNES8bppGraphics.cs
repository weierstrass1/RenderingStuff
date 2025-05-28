using ILGPU;
using ILGPU.Runtime;
using RenderLibrary.Drawing;
using SNESRender.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNESRender
{
    public partial class SaveSNES8bppGraphics : IKernel, ISaveSnesGraphics
	{
		public static int BPPs { get => 8; }
		public Accelerator Accelerator { get; private set; }
		public static IKernel? CreateInstance(Accelerator accel)
		{
			return new SaveSNES8bppGraphics(accel);
		}
		public SaveSNES8bppGraphics(Accelerator accel)
		{
			Accelerator = accel;
			kernel = accel
				.LoadAutoGroupedStreamKernel<Index1D, ArrayView<byte>, ArrayView<byte>, int, int>
				(saveSNES8bppGraphics);
		}
		public void Run(ArrayView<byte> src, ArrayView<byte> dest, int srcOffset, int destOffset)
		{
			if (Accelerator == null)
				return;
			Accelerator.Synchronize();
			kernel(dest.IntExtent / 64, src, dest, srcOffset, destOffset);
		}
	}
}
