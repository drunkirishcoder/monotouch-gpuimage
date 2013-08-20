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
}

