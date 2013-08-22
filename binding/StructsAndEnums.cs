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

	public struct GPUVector3
	{
		public float One;
		public float Two;
		public float Three;
	};

	public struct GPUVector4
	{
		public float One;
		public float Two;
		public float Three;
		public float Four;
	};

	public struct GPUMatrix3x3
	{
		public GPUVector3 One;
		public GPUVector3 Two;
		public GPUVector3 Three;
	};

	public struct GPUMatrix4x4
	{
		public GPUVector4 One;
		public GPUVector4 Two;
		public GPUVector4 Three;
		public GPUVector4 Four;
	};

	public enum GPUImageHistogramType
	{
		Red,
		Green,
		Blue,
		RGB,
		Luminance
	}
}

