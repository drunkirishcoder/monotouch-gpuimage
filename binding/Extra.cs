using System;
using System.Drawing;
using System.Linq;
using Foundation;
using UIKit;
using CoreGraphics;

namespace MonoTouch.GpuImage
{
	public partial class GPUImageView : UIView
	{
		public GPUImageView(CGRect frame) : base(frame)
		{
		}
	}

	public partial class GPUImageMovieWriter
	{
		public GPUImageMovieWriter(string filename, CGSize newSize)
			: this(NSUrl.FromFilename(filename), newSize)
		{
		}
	}

	public partial class GPUImageToneCurveFilter
	{
		public CGPoint[] RedControlPoints
		{
			get { return GPUImageRedControlPoints.ToPoints(); }
			set { GPUImageRedControlPoints = value.ToNSValues(); }
		}

		public CGPoint[] GreenControlPoints
		{
			get { return GPUImageGreenControlPoints.ToPoints(); }
			set { GPUImageGreenControlPoints = value.ToNSValues(); }
		}

		public CGPoint[] BlueControlPoints
		{
			get { return GPUImageBlueControlPoints.ToPoints(); }
			set { GPUImageBlueControlPoints = value.ToNSValues(); }
		}

		public CGPoint[] RgbCompositeControlPoints
		{
			get { return GPUImageRgbCompositeControlPoints.ToPoints(); }
			set { GPUImageRgbCompositeControlPoints = value.ToNSValues(); }
		}

		public CGPoint[] GetPreparedSplineCurve(CGPoint[] points)
		{
			return GPUImageGetPreparedSplineCurve(points.ToNSValues()).ToPoints();
		}

		public CGPoint[] GetSplineCurve(CGPoint[] points)
		{
			return GPUImageGetSplineCurve(points.ToNSValues()).ToPoints();
		}

		public CGPoint[] GetSecondDerivative(CGPoint[] cgPoints)
		{
			return GPUImageGetSecondDerivative(cgPoints.ToNSValues()).ToPoints();
		}
	}

	internal static class Extensions
	{
		public static CGPoint[] ToPoints(this NSValue[] @this)
		{
			return @this.Select(x => x.CGPointValue).ToArray();
		}

		public static NSValue[] ToNSValues(this CGPoint[] @this)
		{
			return @this.Select(x => NSValue.FromCGPoint(x)).ToArray();
		}
	}
}

