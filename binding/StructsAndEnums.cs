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

		public static implicit operator GPUVector3(float[] values)
		{
			if (values == null || values.Length != 3)
			{
				throw new ArgumentException(String.Format("Cannot convert array of size {0} to GPUVector3.", values.Length));
			}

			return new GPUVector3 { One = values[0], Two = values[1], Three = values[2] };
		}
	};

	public struct GPUVector4
	{
		public float One;
		public float Two;
		public float Three;
		public float Four;

		public static implicit operator GPUVector4(float[] values)
		{
			if (values == null || values.Length != 4)
			{
				throw new ArgumentException(String.Format("Cannot convert array of size {0} to GPUVector4.", values.Length));
			}

			return new GPUVector4 { One = values[0], Two = values[1], Three = values[2], Four = values[3] };
		}
	};

	public struct GPUMatrix3x3
	{
		public GPUVector3 One;
		public GPUVector3 Two;
		public GPUVector3 Three;

		public static implicit operator GPUMatrix3x3(float[,] values)
		{
			if (values == null || values.Rank != 2 || values.GetLength(0) != 3 || values.GetLength(1) != 3)
			{
				throw new ArgumentException(String.Format("Cannot convert array of size [{0}, {1}] to GPUMatrix3x3.", values.GetLength(0), values.GetLength(1)));
			}

			return new GPUMatrix3x3
			{
				One = new GPUVector3 { One = values[0, 0], Two = values[0, 1], Three = values[0, 2] },
				Two = new GPUVector3 { One = values[1, 0], Two = values[1, 1], Three = values[1, 2] },
				Three = new GPUVector3 { One = values[2, 0], Two = values[2, 1], Three = values[2, 2] }
			};
		}
	};

	public struct GPUMatrix4x4
	{
		public GPUVector4 One;
		public GPUVector4 Two;
		public GPUVector4 Three;
		public GPUVector4 Four;

		public static implicit operator GPUMatrix4x4(float[,] values)
		{
			if (values == null || values.Rank != 2 || values.GetLength(0) != 4 || values.GetLength(1) != 4)
			{
				throw new ArgumentException(String.Format("Cannot convert array of size [{0}, {1}] to GPUMatrix4x4.", values.GetLength(0), values.GetLength(1)));
			}

			return new GPUMatrix4x4
			{
				One = new GPUVector4 { One = values[0, 0], Two = values[0, 1], Three = values[0, 2], Four = values[0, 3] },
				Two = new GPUVector4 { One = values[1, 0], Two = values[1, 1], Three = values[1, 2], Four = values[1, 3] },
				Three = new GPUVector4 { One = values[2, 0], Two = values[2, 1], Three = values[2, 2], Four = values[2, 3] },
				Four = new GPUVector4 { One = values[3, 0], Two = values[3, 1], Three = values[3, 2], Four = values[3, 3] }
			};
		}
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

