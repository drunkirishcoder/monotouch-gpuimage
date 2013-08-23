using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
using MonoTouch.GpuImage;

namespace MonoTouch.GpuImage.Samples.FilterShowcase
{
	public partial class DetailViewController : UIViewController
	{
		private UIImage _image;
		private FilterType _type;

		public DetailViewController() : base (null, null)
		{
			_image = new UIImage("comcast-tower.jpg");
		}

		public void SetDetailItem(FilterType newType)
		{
			_type = newType;
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void LoadView()
		{
			base.LoadView();

			var mainScreenFrame = UIScreen.MainScreen.ApplicationFrame;
			UIImageView primaryView = new UIImageView(mainScreenFrame);
			this.View = primaryView;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			Title = _type.ToString();

			GPUImageOutput filter = null;

			switch (_type)
			{
				case FilterType.Sepia:
					filter = new GPUImageSepiaFilter();
					break;
				case FilterType.Pixellate:
					filter = new GPUImagePixellateFilter();
					break;
				case FilterType.PolarPixellate:
					filter = new GPUImagePolarPixellateFilter();
					break;
				case FilterType.PixellatePosition:
					filter = new GPUImagePixellatePositionFilter();
					break;
				case FilterType.PolkaDot:
					filter = new GPUImagePolkaDotFilter();
					break;
				case FilterType.Halftone:
					filter = new GPUImageHalftoneFilter();
					break;
				case FilterType.Crosshatch:
					filter = new GPUImageCrosshatchFilter();
					break;
				case FilterType.ColorInvert:
					filter = new GPUImageColorInvertFilter();
					break;
				case FilterType.Grayscale:
					filter = new GPUImageGrayscaleFilter();
					break;
				case FilterType.Monochrome:
					var monochrome = new GPUImageMonochromeFilter();
					monochrome.Color = new [] { 0f, 0f, 1f, 1f };
					filter = monochrome;
					break;
				case FilterType.FalseColor:
					filter = new GPUImageFalseColorFilter();
					break;
				case FilterType.SoftElegance:
					filter = new GPUImageSoftEleganceFilter();
					break;
				case FilterType.MissEtikate:
					filter = new GPUImageMissEtikateFilter();
					break;
				case FilterType.Amatorka:
					filter = new GPUImageAmatorkaFilter();
					break;
				case FilterType.Saturation:
					var saturation = new GPUImageSaturationFilter();
					saturation.Saturation = 2;
					filter = saturation;
					break;
				case FilterType.Contrast:
					var contrast = new GPUImageContrastFilter();
					contrast.Contrast = 3;
					filter = contrast;
					break;
				case FilterType.Brightness:
					var brightness = new GPUImageBrightnessFilter();
					brightness.Brightness = -0.33f;
					filter = brightness;
					break;
				case FilterType.Levels:
					var levels = new GPUImageLevelsFilter();
					levels.SetRed(0.25f, 0.5f, 0.75f);
					filter = levels;
					break;
				case FilterType.RGB:
					var rgb = new GPUImageRGBFilter();
					rgb.Blue = 0.33f;
					filter = rgb;
					break;
				case FilterType.Hue:
					filter = new GPUImageHueFilter();
					break;
				case FilterType.WhiteBalance:
					var balance = new GPUImageWhiteBalanceFilter();
					balance.Temperature = 6500;
					filter = balance;
					break;
				case FilterType.Exposure:
					var exposure = new GPUImageExposureFilter();
					exposure.Exposure = 1;
					filter = exposure;
					break;
				case FilterType.Sharpen:
					var sharpen = new GPUImageSharpenFilter();
					sharpen.Sharpness = 3;
					filter = sharpen;
					break;
				case FilterType.UnsharpMask:
					var unsharp = new GPUImageUnsharpMaskFilter();
					unsharp.BlurSize = 5;
					unsharp.Intensity = 5;
					filter = unsharp;
					break;
				case FilterType.Gamma:
					var gamma = new GPUImageGammaFilter();
					gamma.Gamma = 2;
					filter = gamma;
					break;
				case FilterType.ToneCurve:
					var tonecurve = new GPUImageToneCurveFilter();
					tonecurve.BlueControlPoints =
						new NSObject[]
						{
							NSValue.FromPointF(new PointF(0, 0)),
							NSValue.FromPointF(new PointF(0.25f, 0.25f)),
							NSValue.FromPointF(new PointF(0.5f, 0.5f))
						};
					filter = tonecurve;
					break;
				case FilterType.HighlightsAndShadows:
					var highlight = new GPUImageHighlightShadowFilter();
					highlight.Highlights = 0;
					highlight.Shadows = 1;
					filter = highlight;
					break;
				case FilterType.Haze:
					var haze = new GPUImageHazeFilter();
					haze.Distance = 0.3f;
					haze.Slope = 0.3f;
					filter = haze;
					break;
				//case FilterType.AverageColor:
				//	return;
				//case FilterType.Luminosity:
				//	return;
				//case FilterType.Histogram:
				//	return;
				case FilterType.LuminanceThreshold:
					filter = new GPUImageLuminanceThresholdFilter();
					break;
				case FilterType.AdaptiveThreshold:
					filter = new GPUImageAdaptiveThresholdFilter();
					break;
				case FilterType.AverageLuminanceThreshold:
					filter = new GPUImageAverageLuminanceThresholdFilter();
					break;
				case FilterType.Crop:
					filter = new GPUImageCropFilter(new RectangleF(0, 0, 1, 0.25f));
					break;
				//case FilterType.Mask:
				//	return;
				case FilterType.Transform2D:
					var transform2d = new GPUImageTransformFilter();
					transform2d.AffineTransform = CGAffineTransform.MakeRotation(2);
					filter = transform2d;
					break;
				case FilterType.Transform3D:
					var transform3d = new GPUImageTransformFilter();
					CATransform3D perspective = CATransform3D.Identity;
					perspective.m34 = 0.4f;
					perspective.m33 = 0.4f;
					perspective = perspective.Scale(0.75f, 0.75f, 0.75f);
					perspective = perspective.Rotate(0.75f, 0.0f, 1.0f, 0.0f);
					transform3d.Transform3D = perspective;
					filter = transform3d;
					break;
				case FilterType.SobelEdgeDetection:
					filter = new GPUImageSobelEdgeDetectionFilter();
					break;
				case FilterType.XYDerivative:
					filter = new GPUImageXYDerivativeFilter();
					break;
				case FilterType.HarrisCornerDetection:
					var harris = new GPUImageHarrisCornerDetectionFilter();
					harris.Threshold = 0.2f;
					filter = harris;
					break;
				case FilterType.NobleCornerDetection:
					var noble = new GPUImageNobleCornerDetectionFilter();
					noble.Threshold = 0.2f;
					filter = noble;
					break;
				case FilterType.ShiTomasiFeatureDetection:
					var shi = new GPUImageShiTomasiFeatureDetectionFilter();
					shi.Threshold = 0.2f;
					filter = shi;
					break;
				case FilterType.LineDetection:
					var line = new GPUImageHoughTransformLineDetector();
					line.LineDetectionThreshold = 0.6f;
					filter = line;
					break;
				case FilterType.PrewittEdgeDetection:
					filter = new GPUImagePrewittEdgeDetectionFilter();
					break;
				case FilterType.CannyEdgeDetection:
					filter = new GPUImageCannyEdgeDetectionFilter();
					break;
				case FilterType.ThresholdEdgeDetection:
					filter = new GPUImageThresholdEdgeDetectionFilter();
					break;
				case FilterType.LocalBinaryPattern:
					filter = new GPUImageLocalBinaryPatternFilter();
					break;
				//case FilterType.LowPass:
				//	return;
				//case FilterType.HighPass:
				//	return;
				case FilterType.Sketch:
					filter = new GPUImageSketchFilter();
					break;
				case FilterType.ThresholdSketch:
					filter = new GPUImageThresholdSketchFilter();
					break;
				case FilterType.Toon:
					filter = new GPUImageToonFilter();
					break;
				case FilterType.SmoothToon:
					filter = new GPUImageSmoothToonFilter();
					break;
				case FilterType.TiltShift:
					var tilt = new GPUImageTiltShiftFilter();
					tilt.TopFocusLevel = 0.4f;
					tilt.BottomFocusLevel = 0.6f;
					tilt.FocusFallOffRate = 0.2f;
					filter = tilt;
					break;
				case FilterType.CGA:
					filter = new GPUImageCGAColorspaceFilter();
					break;
				case FilterType.Convolution3x3:
					var convo = new GPUImage3x3ConvolutionFilter();
					convo.ConvolutionKernel = new [,] {
						{-1f, 0f, 1f},
						{-2f, 0f, 2f},
						{-1f, 0f, 1f}
					};
					filter = convo;
					break;
				case FilterType.Emboss:
					filter = new GPUImageEmbossFilter();
					break;
				case FilterType.Laplacian:
					filter = new GPUImageLaplacianFilter();
					break;
				case FilterType.Posterize:
					filter = new GPUImagePosterizeFilter();
					break;
				case FilterType.Swirl:
					filter = new GPUImageSwirlFilter();
					break;
				case FilterType.Bulge:
					filter = new GPUImageBulgeDistortionFilter();
					break;
				case FilterType.SphereRefraction:
					filter = new GPUImageSphereRefractionFilter();
					break;
				case FilterType.GlassSphere:
					filter = new GPUImageGlassSphereFilter();
					break;
				case FilterType.Pinch:
					filter = new GPUImagePinchDistortionFilter();
					break;
				case FilterType.Stretch:
					filter = new GPUImageStretchDistortionFilter();
					break;
				case FilterType.Dilation:
					filter = new GPUImageDilationFilter();
					break;
				case FilterType.DilationRGB:
					filter = new GPUImageRGBDilationFilter();
					break;
				case FilterType.Erosion:
					filter = new GPUImageErosionFilter();
					break;
				case FilterType.ErosionRGB:
					filter = new GPUImageRGBErosionFilter();
					break;
				case FilterType.Opening:
					filter = new GPUImageOpeningFilter();
					break;
				case FilterType.OpeningRGB:
					filter = new GPUImageRGBOpeningFilter();
					break;
				case FilterType.Closing:
					filter = new GPUImageClosingFilter();
					break;
				case FilterType.ClosingRGB:
					filter = new GPUImageRGBClosingFilter();
					break;
				case FilterType.PerlinNoise:
					filter = new GPUImagePerlinNoiseFilter();
					break;
				//case FilterType.Voronoi:
				//	return;
				case FilterType.Mosaic:
					var mosaic = new GPUImageMosaicFilter();
					mosaic.ColorOn = true;
					mosaic.TileSet = "squares.png";
					filter = mosaic;
					break;
				case FilterType.Kuwahara:
					filter = new GPUImageKuwaharaFilter();
					break;
				case FilterType.KuwaharaRadius3:
					filter = new GPUImageKuwaharaRadius3Filter();
					break;
				case FilterType.Vignette:
					filter = new GPUImageVignetteFilter();
					break;
				case FilterType.GaussianBlur:
					var gaussian = new GPUImageGaussianBlurFilter();
					gaussian.BlurSize = 5;
					filter = gaussian;
					break;
				case FilterType.FastBlur:
					var fast = new GPUImageFastBlurFilter();
					fast.BlurSize = 5;
					filter = fast;
					break;
				case FilterType.BoxBlur:
					var box = new GPUImageBoxBlurFilter();
					box.BlurSize = 5;
					filter = box;
					break;
				case FilterType.Median:
					filter = new GPUImageMedianFilter();
					break;
				case FilterType.MotionBlur:
					filter = new GPUImageMotionBlurFilter();
					break;
				case FilterType.ZoomBlur:
					filter = new GPUImageZoomBlurFilter();
					break;
				case FilterType.GaussianSelectiveBlur:
					var selective = new GPUImageGaussianSelectiveBlurFilter();
					selective.BlurSize = 5;
					selective.ExcludeCircleRadius = 80.0f / 320.0f;
					filter = selective;
					break;
				case FilterType.GaussianPositionBlur:
					var position = new GPUImageGaussianBlurPositionFilter();
					position.BlurSize = 5;
					position.BlurRadius = 160.0f / 320.0f;
					filter = position;
					break;
				case FilterType.BilateralBlur:
					var bilateral = new GPUImageBilateralFilter();
					bilateral.BlurSize = 5;
					filter = bilateral;
				case FilterType.Custom:
					filter = new GPUImageFilter("CustomFilter");
					break;
				default:
					return;
			}

			((UIImageView)View).Image = filter.ImageByFilteringImage(_image);

			filter.Dispose();
		}
	}
}

