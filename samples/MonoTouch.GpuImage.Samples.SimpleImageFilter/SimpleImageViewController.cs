using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.GpuImage;

namespace MonoTouch.GpuImage.Samples.SimpleImageFilter
{
	public partial class SimpleImageViewController : UIViewController
	{
		public SimpleImageViewController() : base (null, null)
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

			SetupDisplayFiltering();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void UpdateSliderValue(object sender, EventArgs e)
		{
		}

		private void SetupDisplayFiltering()
		{
			UIImage image = new UIImage("comcast-tower.jpg");
			var sourcePicture = new GPUImagePicture(image, true);

			// edge detection
			//var filter = new GPUImageSobelEdgeDetectionFilter();

			// sepia
			//var filter = new GPUImageSepiaFilter();
			//filter.Intensity = 0.5f;

			// grayscale
			//var filter = new GPUImageGrayscaleFilter();

			// Amatorka
			//var filter = new GPUImageAmatorkaFilter();

			// Ms. Etikate
			//var filter = new GPUImageMissEtikateFilter();

			// soft elegance
			//var filter = new GPUImageSoftEleganceFilter();

			// haze
			var filter = new GPUImageHazeFilter();
			filter.Distance = 0.3f;
			filter.Slope = -0.3f;

			var imageView = (GPUImageView)this.View;
			filter.ForceProcessingAtSize(imageView.SizeInPixels);

			sourcePicture.AddTarget(filter);
			filter.AddTarget(imageView);

			sourcePicture.ProcessImage();
		}
	}
}

