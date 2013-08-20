using System;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreMedia;
using MonoTouch.CoreGraphics;

namespace MonoTouch.GpuImage
{
	[Model, BaseType (typeof (NSObject))]
	public partial interface GPUImageInput
	{
		[Export ("newFrameReadyAtTime:atIndex:")]
		void NewFrameReadyAtTime (CMTime frameTime, int textureIndex);

		[Export ("setInputTexture:atIndex:")]
		void SetInputTexture (uint newInputTexture, int textureIndex);

		[Export ("setTextureDelegate:atIndex:")]
		void SetTextureDelegate (GPUImageTextureDelegate newTextureDelegate, int textureIndex);

		[Export ("nextAvailableTextureIndex")]
		int NextAvailableTextureIndex { get; }

		[Export ("setInputSize:atIndex:")]
		void SetInputSize (SizeF newSize, int textureIndex);

		[Export ("setInputRotation:atIndex:")]
		void SetInputRotation (GPUImageRotationMode newInputRotation, int textureIndex);

		[Export ("maximumOutputSize")]
		SizeF MaximumOutputSize { get; }

		[Export ("endProcessing")]
		void EndProcessing ();

		[Export ("shouldIgnoreUpdatesToThisTarget")]
		bool ShouldIgnoreUpdatesToThisTarget { get; }

		[Export ("enabled")]
		bool Enabled { get; }

		[Export ("conserveMemoryForNextFrame")]
		void ConserveMemoryForNextFrame ();

		[Export ("wantsMonochromeInput")]
		bool WantsMonochromeInput { get; }

		[Export ("currentlyReceivingMonochromeInput")]
		bool CurrentlyReceivingMonochromeInput { set; }
	}

	[Model, BaseType (typeof (NSObject))]
	public partial interface GPUImageTextureDelegate
	{
		[Export ("textureNoLongerNeededForTarget:")]
		void TextureNoLongerNeededForTarget (GPUImageInput textureTarget);
	}

	[BaseType (typeof (UIView))]
	public partial interface GPUImageView : GPUImageInput
	{
		[Export ("fillMode")]
		GPUImageFillModeType FillMode { get; set; }

		[Export ("sizeInPixels")]
		SizeF SizeInPixels { get; }

		//[Export ("enabled")]
		//bool Enabled { get; set; }

		[Export ("setBackgroundColorRed:green:blue:alpha:")]
		void SetBackgroundColorRed(float redComponent, float greenComponent, float blueComponent, float alphaComponent);
	}

	public delegate void FrameProcessingCompletionBlock(GPUImageOutput imageOutput, CMTime frameTime);

	[BaseType (typeof (NSObject))]
	public partial interface GPUImageOutput : GPUImageTextureDelegate
	{
		[Export ("shouldSmoothlyScaleOutput")]
		bool ShouldSmoothlyScaleOutput { get; set; }

		[Export ("shouldIgnoreUpdatesToThisTarget")]
		bool ShouldIgnoreUpdatesToThisTarget { get; set; }

		//[Export ("audioEncodingTarget", ArgumentSemantic.Retain)]
		//GPUImageMovieWriter AudioEncodingTarget { get; set; }

		[Export ("targetToIgnoreForUpdates", ArgumentSemantic.Assign)]
		GPUImageInput TargetToIgnoreForUpdates { get; set; }

		[Export ("frameProcessingCompletionBlock", ArgumentSemantic.Copy)]
		FrameProcessingCompletionBlock FrameProcessingCompletionBlock { get; set; }

		[Export ("enabled")]
		bool Enabled { get; set; }

		[Export ("outputTextureOptions")]
		GPUTextureOptions OutputTextureOptions { get; set; }

		[Export ("setInputTextureForTarget:atIndex:")]
		void SetInputTextureForTarget (GPUImageInput target, int inputTextureIndex);

		[Export ("textureForOutput")]
		uint TextureForOutput { get; }

		[Export ("notifyTargetsAboutNewOutputTexture")]
		void NotifyTargetsAboutNewOutputTexture ();

		[Export ("targets")]
		GPUImageInput [] Targets { get; }

		[Export ("addTarget:")]
		void AddTarget (NSObject newTarget);	//todo: should be GPUImageInput

		[Export ("addTarget:atTextureLocation:")]
		void AddTarget (NSObject newTarget, int textureLocation);	//todo: should be GPUImageInput

		[Export ("removeTarget:")]
		void RemoveTarget (NSObject targetToRemove);	//todo: should be GPUImageInput

		[Export ("removeAllTargets")]
		void RemoveAllTargets ();

		[Export ("initializeOutputTextureIfNeeded")]
		void InitializeOutputTextureIfNeeded ();

		[Export ("deleteOutputTexture")]
		void DeleteOutputTexture ();

		[Export ("forceProcessingAtSize:")]
		void ForceProcessingAtSize (SizeF frameSize);

		[Export ("forceProcessingAtSizeRespectingAspectRatio:")]
		void ForceProcessingAtSizeRespectingAspectRatio (SizeF frameSize);

		[Export ("cleanupOutputImage")]
		void CleanupOutputImage ();

		[Export ("newCGImageFromCurrentlyProcessedOutput")]
		CGImage NewCGImageFromCurrentlyProcessedOutput { get; }

		[Export ("newCGImageFromCurrentlyProcessedOutputWithOrientation:")]
		CGImage NewCGImageFromCurrentlyProcessedOutputWithOrientation (UIImageOrientation imageOrientation);

		[Export ("newCGImageByFilteringCGImage:")]
		CGImage NewCGImageByFilteringCGImage (CGImage imageToFilter);

		[Export ("newCGImageByFilteringCGImage:orientation:")]
		CGImage NewCGImageByFilteringCGImage (CGImage imageToFilter, UIImageOrientation orientation);

		[Export ("imageFromCurrentlyProcessedOutput")]
		UIImage ImageFromCurrentlyProcessedOutput { get; }

		[Export ("imageFromCurrentlyProcessedOutputWithOrientation:")]
		UIImage ImageFromCurrentlyProcessedOutputWithOrientation (UIImageOrientation imageOrientation);

		[Export ("imageByFilteringImage:")]
		UIImage ImageByFilteringImage (UIImage imageToFilter);

		[Export ("newCGImageByFilteringImage:")]
		CGImage NewCGImageByFilteringImage (UIImage imageToFilter);

		[Export ("providesMonochromeOutput")]
		bool ProvidesMonochromeOutput { get; }

		[Export ("prepareForImageCapture")]
		void PrepareForImageCapture ();

		[Export ("conserveMemoryForNextFrame")]
		void ConserveMemoryForNextFrame ();
	}

	[BaseType (typeof (GPUImageOutput))]
	public partial interface GPUImagePicture
	{
		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("initWithImage:")]
		IntPtr Constructor (UIImage newImageSource);

		[Export ("initWithCGImage:")]
		IntPtr Constructor (CGImage newImageSource);

		[Export ("initWithImage:smoothlyScaleOutput:")]
		IntPtr Constructor (UIImage newImageSource, bool smoothlyScaleOutput);

		[Export ("initWithCGImage:smoothlyScaleOutput:")]
		IntPtr Constructor (CGImage newImageSource, bool smoothlyScaleOutput);

		[Export ("processImage")]
		void ProcessImage ();

		[Export ("outputImageSize")]
		SizeF OutputImageSize { get; }

		[Export ("processImageWithCompletionHandler:")]
		bool ProcessImageWithCompletionHandler (Action completion);
	}
}

