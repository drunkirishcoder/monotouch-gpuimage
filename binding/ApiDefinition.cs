using System;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreMedia;

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
		void SetBackgroundColorRed (float redComponent, float greenComponent, float blueComponent, float alphaComponent);
	}
}

