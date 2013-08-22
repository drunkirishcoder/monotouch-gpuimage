using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace MonoTouch.GpuImage
{
	public partial class GPUImageView : UIView
	{
		public GPUImageView(RectangleF frame) : base(frame)
		{
		}
	}

	public partial class GPUImageMovieWriter
	{
		public GPUImageMovieWriter(string newMovieUrl, SizeF newSize)
			: this(NSUrl.FromFilename(newMovieUrl), newSize)
		{
		}
	}
}

