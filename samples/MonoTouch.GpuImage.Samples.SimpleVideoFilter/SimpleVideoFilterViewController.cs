using System;
using System.Drawing;
using System.IO;
using System.Threading;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AVFoundation;
using MonoTouch.GpuImage;

namespace MonoTouch.GpuImage.Samples.SimpleVideoFilter
{
	public partial class SimpleVideoFilterViewController : UIViewController
	{
		private GPUImageVideoCamera _videoCamera;
		private GPUImageOutput _filter;

		public SimpleVideoFilterViewController() : base (null, null)
		{
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
			GPUImageView primaryView = new GPUImageView(mainScreenFrame);
			this.View = primaryView;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_videoCamera = new GPUImageVideoCamera(AVCaptureSession.Preset640x480, AVCaptureDevicePosition.Back);
			//_videoCamera = new GPUImageVideoCamera(AVCaptureSession.Preset640x480, AVCaptureDevicePosition.Front);
			//_videoCamera = new GPUImageVideoCamera(AVCaptureSession.Preset1280x720, AVCaptureDevicePosition.Back);

			_videoCamera.OutputImageOrientation = UIInterfaceOrientation.Portrait;
			_videoCamera.HorizontallyMirrorFrontFacingCamera = false;
			_videoCamera.HorizontallyMirrorRearFacingCamera = false;

			_filter = new GPUImageSepiaFilter();

			_videoCamera.AddTarget(_filter);
			var filterView = (GPUImageView)this.View;
			_filter.AddTarget(filterView);

			var documentsPath = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0];
			string pathToMovie = Path.Combine(documentsPath, "Movie.m4v");
			if (File.Exists(pathToMovie))
			{
				File.Delete(pathToMovie);
			}

			var movieWriter = new GPUImageMovieWriter(pathToMovie, new CGSize(480, 640));
			_filter.AddTarget(movieWriter);

			_videoCamera.StartCameraCapture();

			ThreadPool.RegisterWaitForSingleObject(
				new AutoResetEvent(false),
				(o, t) => {
					Console.WriteLine("Start recording");
					_videoCamera.AudioEncodingTarget = movieWriter;
					movieWriter.StartRecording();

					ThreadPool.RegisterWaitForSingleObject(
						new AutoResetEvent(false),
						(o1, t1) => {
							_filter.RemoveTarget(movieWriter);
							_videoCamera.AudioEncodingTarget = null;
							movieWriter.FinishRecording();
							Console.WriteLine("Movie completed.");
						},
						null,
						TimeSpan.FromSeconds(15),
						true);
				},
				null,
				TimeSpan.FromSeconds(5),
				true);
		}

		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			UIInterfaceOrientation orient = UIInterfaceOrientation.Portrait;

			switch (UIDevice.CurrentDevice.Orientation)
			{
				case UIDeviceOrientation.LandscapeLeft:
					orient = UIInterfaceOrientation.LandscapeLeft;
					break;

				case UIDeviceOrientation.LandscapeRight:
					orient = UIInterfaceOrientation.LandscapeRight;
					break;

				case UIDeviceOrientation.Portrait:
					orient = UIInterfaceOrientation.Portrait;
					break;

				case UIDeviceOrientation.PortraitUpsideDown:
					orient = UIInterfaceOrientation.PortraitUpsideDown;
					break;

				default:
					// When in doubt, stay the same.
					orient = fromInterfaceOrientation;
					break;
			}

			_videoCamera.OutputImageOrientation = orient;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.All;
		}
	}
}

