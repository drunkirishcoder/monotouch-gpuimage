using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

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
		public GPUImageMovieWriter(string filename, SizeF newSize)
			: this(NSUrl.FromFilename(filename), newSize)
		{
		}
	}

	public partial class GPUImageToneCurveFilter
	{
		public PointF[] RedControlPoints
		{
			get { return GPUImageRedControlPoints.ToPoints(); }
			set { GPUImageRedControlPoints = value.ToNSValues(); }
		}

		public PointF[] GreenControlPoints
		{
			get { return GPUImageGreenControlPoints.ToPoints(); }
			set { GPUImageGreenControlPoints = value.ToNSValues(); }
		}

		public PointF[] BlueControlPoints
		{
			get { return GPUImageBlueControlPoints.ToPoints(); }
			set { GPUImageBlueControlPoints = value.ToNSValues(); }
		}

		public PointF[] RgbCompositeControlPoints
		{
			get { return GPUImageRgbCompositeControlPoints.ToPoints(); }
			set { GPUImageRgbCompositeControlPoints = value.ToNSValues(); }
		}

		public PointF[] GetPreparedSplineCurve(PointF[] points)
		{
			return GPUImageGetPreparedSplineCurve(points.ToNSValues()).ToPoints();
		}

		public PointF[] GetSplineCurve(PointF[] points)
		{
			return GPUImageGetSplineCurve(points.ToNSValues()).ToPoints();
		}

		public PointF[] GetSecondDerivative(PointF[] cgPoints)
		{
			return GPUImageGetSecondDerivative(cgPoints.ToNSValues()).ToPoints();
		}
	}

	internal static class Extensions
	{
		public static PointF[] ToPoints(this NSValue[] @this)
		{
			return @this.Select(x => x.PointFValue).ToArray();
		}

		public static NSValue[] ToNSValues(this PointF[] @this)
		{
			return @this.Select(x => NSValue.FromPointF(x)).ToArray();
		}
	}
}

