using System;

namespace MonoTouch.GpuImage
{
	public enum GPUImageRotationMode
	{
		NoRotation,
		RotateLeft,
		RotateRight,
		FlipVertical,
		FlipHorizonal,
		RotateRightFlipVertical,
		Rotate180
	}

	public enum GPUImageFillModeType
	{
		Stretch,
		PreserveAspectRatio,
		PreserveAspectRatioAndFill
	}

	public struct GPUTextureOptions
	{
		public int MinFilter;
		public int MagFilter;
		public int WrapS;
		public int WrapT;
		public int InternalFormat;
		public int Format;
		public int Type;
	}
}

