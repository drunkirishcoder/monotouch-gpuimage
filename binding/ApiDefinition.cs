using System;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreMedia;
using MonoTouch.CoreVideo;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
using MonoTouch.AVFoundation;

namespace MonoTouch.GpuImage
{
	[BaseType (typeof (NSObject))]
	public partial interface GLProgram
	{
		[Export ("initialized")]
		bool Initialized { get; set; }

		[Export ("initWithVertexShaderString:fragmentShaderString:")]
		IntPtr Constructor (string vertexShader, string fragmentShader);

		//[Export ("initWithVertexShaderString:fragmentShaderFilename:")]
		//IntPtr Constructor (string vShaderString, string fShaderFilename);

		//[Export ("initWithVertexShaderFilename:fragmentShaderFilename:")]
		//IntPtr Constructor (string vShaderFilename, string fShaderFilename);

		[Export ("addAttribute:")]
		void AddAttribute (string attributeName);

		[Export ("attributeIndex:")]
		uint AttributeIndex (string attributeName);

		[Export ("uniformIndex:")]
		uint UniformIndex (string uniformName);

		[Export ("link")]
		bool Link ();

		[Export ("use")]
		void Use ();

		[Export ("vertexShaderLog")]
		string VertexShaderLog { get; }

		[Export ("fragmentShaderLog")]
		string FragmentShaderLog { get; }

		[Export ("programLog")]
		string ProgramLog { get; }

		[Export ("validate")]
		void Validate ();
	}

	[Model, BaseType (typeof (NSObject))]
	public partial interface GPUImageInput
	{
		[Export ("newFrameReadyAtTime:atIndex:")]
		void NewFrameReady (CMTime frameTime, int textureIndex);

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
		void SetBackgroundColor(float redComponent, float greenComponent, float blueComponent, float alphaComponent);
	}

	public delegate void GPUImageFrameProcessingCompletedCallback(GPUImageOutput sender, CMTime frameTime);

	[BaseType (typeof (NSObject))]
	public partial interface GPUImageOutput : GPUImageTextureDelegate
	{
		[Export ("shouldSmoothlyScaleOutput")]
		bool ShouldSmoothlyScaleOutput { get; set; }

		[Export ("shouldIgnoreUpdatesToThisTarget")]
		bool ShouldIgnoreUpdatesToThisTarget { get; set; }

		[Export ("audioEncodingTarget", ArgumentSemantic.Retain), NullAllowed]
		GPUImageMovieWriter AudioEncodingTarget { get; set; }

		[Export ("targetToIgnoreForUpdates", ArgumentSemantic.Assign)]
		NSObject TargetToIgnoreForUpdates { get; set; }		//todo: should be GPUImageInput

		[Export ("frameProcessingCompletionBlock", ArgumentSemantic.Copy), NullAllowed]
		GPUImageFrameProcessingCompletedCallback FrameProcessingCompletionBlock { get; set; }	//todo: map this to event

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
		NSObject [] Targets { get; }	//todo: should be GPUImageInput

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
		CGImage NewCGImageFromCurrentlyProcessedOutput ();

		[Export ("newCGImageFromCurrentlyProcessedOutputWithOrientation:")]
		CGImage NewCGImageFromCurrentlyProcessedOutput (UIImageOrientation imageOrientation);

		[Export ("newCGImageByFilteringCGImage:")]
		CGImage NewCGImageByFilteringCGImage (CGImage imageToFilter);

		[Export ("newCGImageByFilteringCGImage:orientation:")]
		CGImage NewCGImageByFilteringCGImage (CGImage imageToFilter, UIImageOrientation orientation);

		[Export ("imageFromCurrentlyProcessedOutput")]
		UIImage ImageFromCurrentlyProcessedOutput ();

		[Export ("imageFromCurrentlyProcessedOutputWithOrientation:")]
		UIImage ImageFromCurrentlyProcessedOutput (UIImageOrientation imageOrientation);

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
		bool ProcessImage (NSAction completion);
	}

	[Model, BaseType (typeof (NSObject))]
	public partial interface GPUImageVideoCameraDelegate
	{
		[Export ("willOutputSampleBuffer:")]
		void WillOutputSampleBuffer (CMSampleBuffer sampleBuffer);
	}

	[BaseType (typeof (GPUImageOutput))]
	public partial interface GPUImageVideoCamera //: AVCaptureVideoDataOutputSampleBufferDelegate, AVCaptureAudioDataOutputSampleBufferDelegate
	{
		[Export ("captureSession", ArgumentSemantic.Retain)]
		AVCaptureSession CaptureSession { get; }

		[Export ("captureSessionPreset", ArgumentSemantic.Copy)]
		string CaptureSessionPreset { get; set; }

		[Export ("frameRate")]
		int FrameRate { get; set; }

		[Export ("frontFacingCameraPresent")]
		bool FrontFacingCameraPresent { [Bind ("isFrontFacingCameraPresent")] get; }

		[Export ("backFacingCameraPresent")]
		bool BackFacingCameraPresent { [Bind ("isBackFacingCameraPresent")] get; }

		[Export ("runBenchmark")]
		bool RunBenchmark { get; set; }

		[Export ("inputCamera")]
		AVCaptureDevice InputCamera { get; }

		[Export ("outputImageOrientation")]
		UIInterfaceOrientation OutputImageOrientation { get; set; }

		[Export ("horizontallyMirrorFrontFacingCamera")]
		bool HorizontallyMirrorFrontFacingCamera { get; set; }

		[Export ("horizontallyMirrorRearFacingCamera")]
		bool HorizontallyMirrorRearFacingCamera { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign), NullAllowed]
		GPUImageVideoCameraDelegate Delegate { get; set; }

		[Export ("initWithSessionPreset:cameraPosition:")]
		IntPtr Constructor (string sessionPreset, AVCaptureDevicePosition cameraPosition);

		[Export ("removeInputsAndOutputs")]
		void RemoveInputsAndOutputs ();

		[Export ("startCameraCapture")]
		void StartCameraCapture ();

		[Export ("stopCameraCapture")]
		void StopCameraCapture ();

		[Export ("pauseCameraCapture")]
		void PauseCameraCapture ();

		[Export ("resumeCameraCapture")]
		void ResumeCameraCapture ();

		[Export ("processVideoSampleBuffer:")]
		void ProcessVideoSampleBuffer (CMSampleBuffer sampleBuffer);

		[Export ("processAudioSampleBuffer:")]
		void ProcessAudioSampleBuffer (CMSampleBuffer sampleBuffer);

		[Export ("cameraPosition")]
		AVCaptureDevicePosition CameraPosition { get; }

		[Export ("videoCaptureConnection")]
		AVCaptureConnection VideoCaptureConnection { get; }

		[Export ("rotateCamera")]
		void RotateCamera ();

		[Export ("averageFrameDurationDuringCapture")]
		float AverageFrameDurationDuringCapture { get; }

		//[Export ("captureOutput:didOutputSampleBuffer:fromConnection:")]
		//void DidOutputSampleBuffer(AVCaptureOutput captureOutput, CMSampleBuffer sampleBuffer, AVCaptureConnection connection);
	}

	public delegate void GPUImageCaptureCompletionUIImageHandler(UIImage processedImage, NSError error);

	public delegate void GPUImageCaptureCompletionNSDataHandler(NSData processedImage, NSError error);

	[BaseType (typeof (GPUImageVideoCamera))]
	public partial interface GPUImageStillCamera
	{
		[Export ("jpegCompressionQuality")]
		float JpegCompressionQuality { get; set; }

		[Export ("currentCaptureMetadata")]
		NSDictionary CurrentCaptureMetadata { get; }

		//[Export ("capturePhotoAsSampleBufferWithCompletionHandler:")]
		//void CapturePhotoAsSampleBuffer (AVCaptureCompletionHandler completionHandler);

		[Export ("capturePhotoAsImageProcessedUpToFilter:withCompletionHandler:")]
		void CapturePhotoAsImage (GPUImageOutput finalFilterInChain, GPUImageCaptureCompletionUIImageHandler completionHandler);

		[Export ("capturePhotoAsJPEGProcessedUpToFilter:withCompletionHandler:")]
		void CapturePhotoAsJpeg (GPUImageOutput finalFilterInChain, GPUImageCaptureCompletionNSDataHandler completionHandler);

		[Export ("capturePhotoAsPNGProcessedUpToFilter:withCompletionHandler:")]
		void CapturePhotoAsPng (GPUImageOutput finalFilterInChain, GPUImageCaptureCompletionNSDataHandler completionHandler);
	}

	[Model, BaseType (typeof (NSObject))]
	public partial interface GPUImageMovieWriterDelegate
	{
		[Export ("movieRecordingCompleted")]
		void MovieRecordingCompleted ();

		[Export ("movieRecordingFailedWithError:")]
		void MovieRecordingFailed (NSError error);
	}

	public delegate void NSErrorHandler(NSError error);

	[BaseType (typeof (NSObject))]
	public partial interface GPUImageMovieWriter : GPUImageInput
	{
		[Export ("hasAudioTrack")]
		bool HasAudioTrack { get; set; }

		[Export ("shouldPassthroughAudio")]
		bool ShouldPassthroughAudio { get; set; }

		[Export ("shouldInvalidateAudioSampleWhenDone")]
		bool ShouldInvalidateAudioSampleWhenDone { get; set; }

		[Export ("completionBlock", ArgumentSemantic.Copy), NullAllowed]
		NSAction CompletionBlock { get; set; }

		[Export ("failureBlock", ArgumentSemantic.Copy), NullAllowed]
		NSErrorHandler FailureBlock { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign), NullAllowed]
		GPUImageMovieWriterDelegate Delegate { get; set; }

		[Export ("encodingLiveVideo")]
		bool EncodingLiveVideo { get; set; }

		[Export ("videoInputReadyCallback", ArgumentSemantic.Copy), NullAllowed]
		NSAction VideoInputReadyCallback { get; set; }

		[Export ("audioInputReadyCallback", ArgumentSemantic.Copy), NullAllowed]
		NSAction AudioInputReadyCallback { get; set; }

		//[Export ("enabled")]
		//bool Enabled { get; set; }

		[Export ("initWithMovieURL:size:")]
		IntPtr Constructor (NSUrl newMovieURL, SizeF newSize);

		[Export ("initWithMovieURL:size:fileType:outputSettings:")]
		IntPtr Constructor (NSUrl newMovieURL, SizeF newSize, string newFileType, NSMutableDictionary outputSettings);

		[Export ("setHasAudioTrack:audioSettings:")]
		void SetHasAudioTrack (bool hasAudioTrack, NSDictionary audioOutputSettings);

		[Export ("startRecording")]
		void StartRecording ();

		[Export ("startRecordingInOrientation:")]
		void StartRecordingInOrientation (CGAffineTransform orientationTransform);

		[Export ("finishRecording")]
		void FinishRecording ();

		[Export ("finishRecordingWithCompletionHandler:")]
		void FinishRecordingWithCompletionHandler (NSAction handler);

		[Export ("cancelRecording")]
		void CancelRecording ();

		[Export ("processAudioBuffer:")]
		void ProcessAudioBuffer (CMSampleBuffer audioBuffer);

		[Export ("enableSynchronizationCallbacks")]
		void EnableSynchronizationCallbacks ();
	}

	// todo: change to event
	[Model, BaseType (typeof (NSObject))]
	public partial interface GPUImageMovieDelegate
	{
		[Export ("didCompletePlayingMovie")]
		void DidCompletePlayingMovie ();
	}

	[BaseType (typeof (GPUImageOutput))]
	public partial interface GPUImageMovie
	{
		[Export ("asset", ArgumentSemantic.Retain)]
		AVAsset Asset { get; set; }

		[Export ("url", ArgumentSemantic.Retain)]
		NSUrl Url { get; set; }

		[Export ("runBenchmark")]
		bool RunBenchmark { get; set; }

		[Export ("playAtActualSpeed")]
		bool PlayAtActualSpeed { get; set; }

		[Export ("shouldRepeat")]
		bool ShouldRepeat { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign), NullAllowed]
		GPUImageMovieDelegate Delegate { get; set; }

		[Export ("initWithAsset:")]
		IntPtr Constructor (AVAsset asset);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("textureCacheSetup")]
		void TextureCacheSetup ();

		[Export ("enableSynchronizedEncodingUsingMovieWriter:")]
		void EnableSynchronizedEncodingUsingMovieWriter (GPUImageMovieWriter movieWriter);

		[Export ("readNextVideoFrameFromOutput:")]
		void ReadNextVideoFrameFromOutput (AVAssetReaderTrackOutput readerVideoTrackOutput);

		[Export ("readNextAudioSampleFromOutput:")]
		void ReadNextAudioSampleFromOutput (AVAssetReaderTrackOutput readerAudioTrackOutput);

		[Export ("startProcessing")]
		void StartProcessing ();

		[Export ("endProcessing")]
		void EndProcessing ();

		[Export ("cancelProcessing")]
		void CancelProcessing ();

		[Export ("processMovieFrame:")]
		void ProcessMovieFrame (CMSampleBuffer movieSampleBuffer);
	}

	[BaseType (typeof (GPUImageOutput))]
	public partial interface GPUImageFilter : GPUImageInput
	{
		//[Export ("renderTarget")]
		//CVPixelBuffer RenderTarget { get; }

		[Export ("preventRendering")]
		bool PreventRendering { get; set; }

		//[Export ("currentlyReceivingMonochromeInput")]
		//bool CurrentlyReceivingMonochromeInput { get; set; }

		[Export ("initWithVertexShaderFromString:fragmentShaderFromString:")]
		IntPtr Constructor (string vertexShader, string fragmentShader);

		[Export ("initWithFragmentShaderFromString:")]
		IntPtr Constructor (string fragmentShader);

		//[Export ("initWithFragmentShaderFromFile:")]
		//IntPtr Constructor (string fragmentShaderFilename);

		[Export ("initializeAttributes")]
		void InitializeAttributes ();

		[Export ("setupFilterForSize")]
		void SetupFilterForSize (SizeF filterFrameSize);

		[Export ("rotatedSize:forIndex:")]
		SizeF RotatedSize (SizeF sizeToRotate, int textureIndex);

		[Export ("rotatedPoint:forRotation:")]
		PointF RotatedPoint (PointF pointToRotate, GPUImageRotationMode rotation);

		[Export ("recreateFilterFBO")]
		void RecreateFilterFBO ();

		[Export ("sizeOfFBO")]
		SizeF SizeOfFBO { get; }

		[Export ("createFilterFBOofSize:")]
		void CreateFilterFBO (SizeF currentFBOSize);

		[Export ("destroyFilterFBO")]
		void DestroyFilterFBO ();

		[Export ("setFilterFBO")]
		void SetFilterFBO ();

		[Export ("setOutputFBO")]
		void SetOutputFBO ();

		[Export ("releaseInputTexturesIfNeeded")]
		void ReleaseInputTexturesIfNeeded ();

		[Static, Export ("textureCoordinatesForRotation:")]
		IntPtr TextureCoordinatesForRotation (GPUImageRotationMode rotationMode);

		[Export ("renderToTextureWithVertices:textureCoordinates:sourceTexture:")]
		void RenderToTexture (IntPtr vertices, IntPtr textureCoordinates, uint sourceTexture);

		[Export ("informTargetsAboutNewFrameAtTime:")]
		void InformTargetsAboutNewFrame (CMTime frameTime);

		[Export ("outputFrameSize")]
		SizeF OutputFrameSize { get; }

		[Export ("setBackgroundColorRed:green:blue:alpha:")]
		void SetBackgroundColor (float redComponent, float greenComponent, float blueComponent, float alphaComponent);

		[Export ("setInteger:forUniformName:")]
		void SetInteger (int newInteger, string uniformName);

		[Export ("setFloat:forUniformName:")]
		void SetFloat (float newFloat, string uniformName);

		[Export ("setSize:forUniformName:")]
		void SetSize (SizeF newSize, string uniformName);

		[Export ("setPoint:forUniformName:")]
		void SetPoint (PointF newPoint, string uniformName);

		[Export ("setFloatVec3:forUniformName:")]
		void SetFloatVec3 (GPUVector3 newVec3, string uniformName);

		[Export ("setFloatVec4:forUniform:")]
		void SetFloatVec4 (GPUVector4 newVec4, string uniformName);

		[Export ("setFloatArray:length:forUniform:")]
		void SetFloatArray (IntPtr array, int count, string uniformName);

		[Export ("setMatrix3f:forUniform:program:")]
		void SetMatrix3f (GPUMatrix3x3 matrix, int uniform, GLProgram shaderProgram);

		[Export ("setMatrix4f:forUniform:program:")]
		void SetMatrix4f (GPUMatrix4x4 matrix, int uniform, GLProgram shaderProgram);

		[Export ("setFloat:forUniform:program:")]
		void SetFloat (float floatValue, int uniform, GLProgram shaderProgram);

		[Export ("setPoint:forUniform:program:")]
		void SetPoint (PointF pointValue, int uniform, GLProgram shaderProgram);

		[Export ("setSize:forUniform:program:")]
		void SetSize (SizeF sizeValue, int uniform, GLProgram shaderProgram);

		[Export ("setVec3:forUniform:program:")]
		void SetVec3 (GPUVector3 vectorValue, int uniform, GLProgram shaderProgram);

		[Export ("setVec4:forUniform:program:")]
		void SetVec4 (GPUVector4 vectorValue, int uniform, GLProgram shaderProgram);

		[Export ("setFloatArray:length:forUniform:program:")]
		void SetFloatArray (IntPtr arrayValue, int arrayLength, int uniform, GLProgram shaderProgram);

		[Export ("setInteger:forUniform:program:")]
		void SetInteger (int intValue, int uniform, GLProgram shaderProgram);

		[Export ("setAndExecuteUniformStateCallbackAtIndex:forProgram:toBlock:")]
		void SetAndExecuteUniformStateCallback (int uniform, GLProgram shaderProgram, NSAction uniformStateBlock);

		[Export ("uniformsForProgramAtIndex")]
		void SetUniformsForProgram (uint programIndex);
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageTwoPassFilter
	{
		[Export ("initWithFirstStageVertexShaderFromString:firstStageFragmentShaderFromString:secondStageVertexShaderFromString:secondStageFragmentShaderFromString:")]
		IntPtr Constructor (string firstStageVertexShaderString, string firstStageFragmentShaderString, string secondStageVertexShaderString, string secondStageFragmentShaderString);

		[Export ("initWithFirstStageFragmentShaderFromString:secondStageFragmentShaderFromString:")]
		IntPtr Constructor (string firstStageFragmentShaderString, string secondStageFragmentShaderString);

		[Export ("initializeSecondaryAttributes")]
		void InitializeSecondaryAttributes ();

		[Export ("initializeSecondOutputTextureIfNeeded")]
		void InitializeSecondOutputTextureIfNeeded ();

		[Export ("createSecondFilterFBOofSize:")]
		void CreateSecondFilterFBO (SizeF currentFBOSize);
	}

	[BaseType (typeof (GPUImageOutput))]
	public partial interface GPUImageFilterGroup : GPUImageInput
	{
		[Export ("terminalFilter", ArgumentSemantic.Retain)]
		GPUImageOutput TerminalFilter { get; set; }

		[Export ("initialFilters", ArgumentSemantic.Retain)]
		GPUImageOutput [] InitialFilters { get; set; }

		[Export ("inputFilterToIgnoreForUpdates", ArgumentSemantic.Retain)]
		GPUImageOutput InputFilterToIgnoreForUpdates { get; set; }

		[Export ("addFilter:")]
		void AddFilter (GPUImageOutput newFilter);

		[Export ("filterAtIndex:")]
		GPUImageOutput FilterAtIndex (uint filterIndex);

		[Export ("filterCount")]
		uint FilterCount { get; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageBrightnessFilter
	{
		[Export ("brightness")]
		float Brightness { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageExposureFilter
	{
		[Export ("exposure")]
		float Exposure { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageContrastFilter
	{
		[Export ("contrast")]
		float Contrast { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageSaturationFilter
	{
		[Export ("saturation")]
		float Saturation { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageGammaFilter
	{
		[Export ("gamma")]
		float Gamma { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageLevelsFilter
	{
		[Export ("setRedMin:gamma:max:minOut:maxOut:")]
		void SetRed (float min, float mid, float max, float minOut, float maxOut);

		[Export ("setRedMin:gamma:max:")]
		void SetRed (float min, float mid, float max);

		[Export ("setGreenMin:gamma:max:minOut:maxOut:")]
		void SetGreen (float min, float mid, float max, float minOut, float maxOut);

		[Export ("setGreenMin:gamma:max:")]
		void SetGreen (float min, float mid, float max);

		[Export ("setBlueMin:gamma:max:minOut:maxOut:")]
		void SetBlue (float min, float mid, float max, float minOut, float maxOut);

		[Export ("setBlueMin:gamma:max:")]
		void SetBlue (float min, float mid, float max);

		[Export ("setMin:gamma:max:minOut:maxOut:")]
		void SetAll (float min, float mid, float max, float minOut, float maxOut);

		[Export ("setMin:gamma:max:")]
		void SetAll (float min, float mid, float max);
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageColorMatrixFilter
	{
		[Export ("colorMatrix")]
		GPUMatrix4x4 ColorMatrix { get; set; }

		[Export ("intensity")]
		float Intensity { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageRGBFilter
	{
		[Export ("red")]
		float Red { get; set; }

		[Export ("green")]
		float Green { get; set; }

		[Export ("blue")]
		float Blue { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageHueFilter
	{
		[Export ("hue")]
		float Hue { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageToneCurveFilter
	{
		[Export ("redControlPoints", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] RedControlPoints { get; set; }

		[Export ("greenControlPoints", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] GreenControlPoints { get; set; }

		[Export ("blueControlPoints", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] BlueControlPoints { get; set; }

		[Export ("rgbCompositeControlPoints", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] RgbCompositeControlPoints { get; set; }

		[Export ("initWithACVData:")]
		IntPtr Constructor (NSData data);

		[Export ("initWithACV:")]
		IntPtr Constructor (string curveFilename);

		[Export ("initWithACVURL:")]
		IntPtr Constructor (NSUrl curveFileUrl);

		[Export ("pointsWithACV")]
		void SetPoints (string curveFilename);

		[Export ("pointsWithACVURL")]
		void SetPoints (NSUrl curveFileUrl);

		[Export ("getPreparedSplineCurve:")]
		NSMutableArray GetPreparedSplineCurve (NSObject [] points);

		[Export ("splineCurve:")]
		NSMutableArray GetSplineCurve (NSObject [] points);

		[Export ("secondDerivative:")]
		NSMutableArray GetSecondDerivative (NSObject [] cgPoints);

		[Export ("updateToneCurveTexture")]
		void UpdateToneCurveTexture ();
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageHighlightShadowFilter
	{
		[Export ("shadows")]
		float Shadows { get; set; }

		[Export ("highlights")]
		float Highlights { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageTwoInputFilter
	{
		[Export ("disableFirstFrameCheck")]
		void DisableFirstFrameCheck ();

		[Export ("disableSecondFrameCheck")]
		void DisableSecondFrameCheck ();
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageLookupFilter
	{
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageGrayscaleFilter
	{
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageAmatorkaFilter
	{
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageMissEtikateFilter
	{
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageSoftEleganceFilter
	{
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageColorInvertFilter
	{
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageMonochromeFilter
	{
		[Export ("intensity")]
		float Intensity { get; set; }

		[Export ("color")]
		GPUVector4 Color { get; set; }

		[Export ("setColorRed:green:blue:")]
		void SetColor (float redComponent, float greenComponent, float blueComponent);
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageFalseColorFilter
	{
		[Export ("firstColor")]
		GPUVector4 FirstColor { get; set; }

		[Export ("secondColor")]
		GPUVector4 SecondColor { get; set; }

		[Export ("setFirstColorRed:green:blue:")]
		void SetFirstColor (float redComponent, float greenComponent, float blueComponent);

		[Export ("setSecondColorRed:green:blue:")]
		void SetSecondColor (float redComponent, float greenComponent, float blueComponent);
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageHazeFilter
	{
		[Export ("distance")]
		float Distance { get; set; }

		[Export ("slope")]
		float Slope { get; set; }
	}

	[BaseType (typeof (GPUImageColorMatrixFilter))]
	public partial interface GPUImageSepiaFilter
	{
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageOpacityFilter
	{
		[Export ("opacity")]
		float Opacity { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageSolidColorGenerator
	{
		[Export ("color")]
		GPUVector4 Color { get; set; }

		[Export ("useExistingAlpha")]
		bool UseExistingAlpha { get; set; }

		[Export ("setColorRed:green:blue:alpha:")]
		void SetColor (float redComponent, float greenComponent, float blueComponent, float alphaComponent);
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageLuminanceThresholdFilter
	{
		[Export ("threshold")]
		float Threshold { get; set; }
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageAdaptiveThresholdFilter
	{
		[Export ("blurSize")]
		float BlurSize { get; set; }
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageAverageLuminanceThresholdFilter
	{
		[Export ("thresholdMultiplier")]
		float ThresholdMultiplier { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageHistogramFilter
	{
		[Export ("downsamplingFactor")]
		uint DownsamplingFactor { get; set; }

		[Export ("initWithHistogramType:")]
		IntPtr Constructor (GPUImageHistogramType newHistogramType);

		[Export ("initializeSecondaryAttributes")]
		void InitializeSecondaryAttributes ();

		[Export ("generatePointCoordinates")]
		void GeneratePointCoordinates ();
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageHistogramGenerator
	{
	}

	public delegate void GPUImageColorAverageProcessingFinishedCallback(float redComponent, float greenComponent, float blueComponent, float alphaComponent, CMTime frameTime);

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageAverageColor
	{
		[Export ("colorAverageProcessingFinishedBlock", ArgumentSemantic.Copy), NullAllowed]
		GPUImageColorAverageProcessingFinishedCallback ColorAverageProcessingFinished { get; set; }

		[Export ("extractAverageColorAtFrameTime:")]
		void ExtractAverageColor (CMTime frameTime);
	}

	public delegate void GPUImageLuminosityProcessingFinishedCallback(float luminosity, CMTime frameTime);

	[BaseType (typeof (GPUImageAverageColor))]
	public partial interface GPUImageLuminosity
	{
		[Export ("luminosityProcessingFinishedBlock", ArgumentSemantic.Copy), NullAllowed]
		GPUImageLuminosityProcessingFinishedCallback LuminosityProcessingFinished { get; set; }

		[Export ("extractLuminosityAtFrameTime:")]
		void ExtractLuminosity (CMTime frameTime);

		[Export ("initializeSecondaryAttributes")]
		void InitializeSecondaryAttributes ();
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageChromaKeyFilter
	{
		[Export ("thresholdSensitivity")]
		float ThresholdSensitivity { get; set; }

		[Export ("smoothing")]
		float Smoothing { get; set; }

		[Export ("setColorToReplaceRed:green:blue:")]
		void SetColorToReplace (float redComponent, float greenComponent, float blueComponent);
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageTransformFilter
	{
		[Export ("affineTransform")]
		CGAffineTransform AffineTransform { get; set; }

		[Export ("transform3D")]
		CATransform3D Transform3D { get; set; }

		[Export ("ignoreAspectRatio")]
		bool IgnoreAspectRatio { get; set; }

		[Export ("anchorTopLeft")]
		bool AnchorTopLeft { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageCropFilter
	{
		[Export ("cropRegion")]
		RectangleF CropRegion { get; set; }

		[Export ("initWithCropRegion:")]
		IntPtr Constructor (RectangleF newCropRegion);
	}

	[BaseType (typeof (GPUImageTwoPassFilter))]
	public partial interface GPUImageTwoPassTextureSamplingFilter
	{
		[Export ("verticalTexelSpacing")]
		float VerticalTexelSpacing { get; set; }

		[Export ("horizontalTexelSpacing")]
		float HorizontalTexelSpacing { get; set; }
	}

	[BaseType (typeof (GPUImageTwoPassTextureSamplingFilter))]
	public partial interface GPUImageLanczosResamplingFilter
	{
		[Export ("originalImageSize")]
		SizeF OriginalImageSize { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageSharpenFilter
	{
		[Export ("sharpness")]
		float Sharpness { get; set; }
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageUnsharpMaskFilter
	{
		[Export ("blurSize")]
		float BlurSize { get; set; }

		[Export ("intensity")]
		float Intensity { get; set; }
	}

	[BaseType (typeof (GPUImageTwoPassTextureSamplingFilter))]
	public partial interface GPUImageFastBlurFilter
	{
		[Export ("blurPasses")]
		uint BlurPasses { get; set; }

		[Export ("blurSize")]
		float BlurSize { get; set; }

		[Export ("initWithFragmentShaderFromString:")]
		IntPtr Constructor (string fragmentShaderString);
	}

	[BaseType (typeof (GPUImageFastBlurFilter))]
	public partial interface GPUImageSingleComponentFastBlurFilter
	{
	}

	[BaseType (typeof (GPUImageTwoPassTextureSamplingFilter))]
	public partial interface GPUImageGaussianBlurFilter
	{
		[Export ("blurSize")]
		float BlurSize { get; set; }
	}

	[BaseType (typeof (GPUImageGaussianBlurFilter))]
	public partial interface GPUImageSingleComponentGaussianBlurFilter
	{
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageGaussianSelectiveBlurFilter
	{
		[Export ("excludeCircleRadius")]
		float ExcludeCircleRadius { get; set; }

		[Export ("excludeCirclePoint")]
		PointF ExcludeCirclePoint { get; set; }

		[Export ("excludeBlurSize")]
		float ExcludeBlurSize { get; set; }

		[Export ("blurSize")]
		float BlurSize { get; set; }

		[Export ("aspectRatio")]
		float AspectRatio { get; set; }
	}
	
	[BaseType (typeof (GPUImageTwoPassTextureSamplingFilter))]
	public partial interface GPUImageGaussianBlurPositionFilter
	{
		[Export ("blurSize")]
		float BlurSize { get; set; }

		[Export ("blurCenter")]
		PointF BlurCenter { get; set; }

		[Export ("blurRadius")]
		float BlurRadius { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImage3x3TextureSamplingFilter
	{
		[Export ("texelWidth")]
		float TexelWidth { get; set; }

		[Export ("texelHeight")]
		float TexelHeight { get; set; }
	}

	[BaseType (typeof (GPUImage3x3TextureSamplingFilter))]
	public partial interface GPUImageMedianFilter
	{
	}

	[BaseType (typeof (GPUImageGaussianBlurFilter))]
	public partial interface GPUImageBilateralFilter
	{
		[Export ("distanceNormalizationFactor")]
		float DistanceNormalizationFactor { get; set; }
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageTiltShiftFilter
	{
		[Export ("blurSize")]
		float BlurSize { get; set; }

		[Export ("topFocusLevel")]
		float TopFocusLevel { get; set; }

		[Export ("bottomFocusLevel")]
		float BottomFocusLevel { get; set; }

		[Export ("focusFallOffRate")]
		float FocusFallOffRate { get; set; }
	}

	[BaseType (typeof (GPUImageTwoPassTextureSamplingFilter))]
	public partial interface GPUImageBoxBlurFilter
	{
		[Export ("blurSize")]
		float BlurSize { get; set; }
	}

	[BaseType (typeof (GPUImage3x3TextureSamplingFilter))]
	public partial interface GPUImage3x3ConvolutionFilter
	{
		[Export ("convolutionKernel")]
		GPUMatrix3x3 ConvolutionKernel { get; set; }
	}

	[BaseType (typeof (GPUImageTwoPassFilter))]
	public partial interface GPUImageSobelEdgeDetectionFilter
	{
		[Export ("texelWidth")]
		float TexelWidth { get; set; }

		[Export ("texelHeight")]
		float TexelHeight { get; set; }

		[Export ("edgeStrength")]
		float EdgeStrength { get; set; }
	}

	[BaseType (typeof (GPUImageSobelEdgeDetectionFilter))]
	public partial interface GPUImagePrewittEdgeDetectionFilter
	{
	}

	[BaseType (typeof (GPUImageSobelEdgeDetectionFilter))]
	public partial interface GPUImageThresholdEdgeDetectionFilter
	{
		[Export ("threshold")]
		float Threshold { get; set; }
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageCannyEdgeDetectionFilter
	{
		[Export ("texelWidth")]
		float TexelWidth { get; set; }

		[Export ("texelHeight")]
		float TexelHeight { get; set; }

		[Export ("blurSize")]
		float BlurSize { get; set; }

		[Export ("upperThreshold")]
		float UpperThreshold { get; set; }

		[Export ("lowerThreshold")]
		float LowerThreshold { get; set; }
	}

	public delegate void GPUImageCornersDetectedCallback(IntPtr cornerArray, uint cornersDetected, CMTime frameTime);

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageHarrisCornerDetectionFilter
	{
		[Export ("blurSize")]
		float BlurSize { get; set; }

		[Export ("sensitivity")]
		float Sensitivity { get; set; }

		[Export ("threshold")]
		float Threshold { get; set; }

		[Export ("cornersDetectedBlock", ArgumentSemantic.Copy), NullAllowed]
		GPUImageCornersDetectedCallback CornersDetected { get; set; }

		[Export ("intermediateImages", ArgumentSemantic.Retain)]
		NSMutableArray IntermediateImages { get; }

		[Export ("initWithCornerDetectionFragmentShader:")]
		IntPtr Constructor (string cornerDetectionFragmentShader);
	}

	[BaseType (typeof (GPUImageHarrisCornerDetectionFilter))]
	public partial interface GPUImageNobleCornerDetectionFilter
	{
	}

	[BaseType (typeof (GPUImageHarrisCornerDetectionFilter))]
	public partial interface GPUImageShiTomasiFeatureDetectionFilter
	{
	}

	[BaseType (typeof (GPUImage3x3TextureSamplingFilter))]
	public partial interface GPUImageNonMaximumSuppressionFilter
	{
	}

	[BaseType (typeof (GPUImageSobelEdgeDetectionFilter))]
	public partial interface GPUImageXYDerivativeFilter
	{
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageCrosshairGenerator
	{
		[Export ("crosshairWidth")]
		float CrosshairWidth { get; set; }

		[Export ("setCrosshairColorRed:green:blue:")]
		void SetCrosshairColor (float redComponent, float greenComponent, float blueComponent);

		[Export ("renderCrosshairsFromArray:count:frameTime:")]
		void RenderCrosshairs (IntPtr crosshairCoordinates, uint numberOfCrosshairs, CMTime frameTime);
	}

	[BaseType (typeof (GPUImageTwoPassTextureSamplingFilter))]
	public partial interface GPUImageDilationFilter
	{
		[Export ("initWithRadius:")]
		IntPtr Constructor (uint dilationRadius);
	}

	[BaseType (typeof (GPUImageTwoPassTextureSamplingFilter))]
	public partial interface GPUImageRgbDilationFilter
	{
		[Export ("initWithRadius:")]
		IntPtr Constructor (uint dilationRadius);
	}

	[BaseType (typeof (GPUImageTwoPassTextureSamplingFilter))]
	public partial interface GPUImageErosionFilter
	{
		[Export ("initWithRadius:")]
		IntPtr Constructor (uint erosionRadius);
	}

	[BaseType (typeof (GPUImageTwoPassTextureSamplingFilter))]
	public partial interface GPUImageRgbErosionFilter
	{
		[Export ("initWithRadius:")]
		IntPtr Constructor (uint erosionRadius);
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageOpeningFilter
	{
		[Export ("verticalTexelSpacing")]
		float VerticalTexelSpacing { get; set; }

		[Export ("horizontalTexelSpacing")]
		float HorizontalTexelSpacing { get; set; }

		[Export ("initWithRadius:")]
		IntPtr Constructor (uint radius);
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageRgbOpeningFilter
	{
		[Export ("initWithRadius:")]
		IntPtr Constructor (uint radius);
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageClosingFilter
	{
		[Export ("verticalTexelSpacing")]
		float VerticalTexelSpacing { get; set; }

		[Export ("horizontalTexelSpacing")]
		float HorizontalTexelSpacing { get; set; }

		[Export ("initWithRadius:")]
		IntPtr Constructor (uint radius);
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageRgbClosingFilter
	{
		[Export ("initWithRadius:")]
		IntPtr Constructor (uint radius);
	}

	[BaseType (typeof (GPUImage3x3TextureSamplingFilter))]
	public partial interface GPUImageLocalBinaryPatternFilter
	{
		[Export ("initWithRadius:")]
		IntPtr Constructor (uint radius);
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageLowPassFilter
	{
		[Export ("filterStrength")]
		float FilterStrength { get; set; }
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageHighPassFilter
	{
		[Export ("filterStrength")]
		float FilterStrength { get; set; }
	}

	public delegate void GPUImageMotionDetectionCallback(PointF motionCentroid, float motionIntensity, CMTime frameTime);

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageMotionDetector
	{
		[Export ("lowPassFilterStrength")]
		float LowPassFilterStrength { get; set; }

		[Export ("motionDetectionBlock", ArgumentSemantic.Copy), NullAllowed]
		GPUImageMotionDetectionCallback Delegate { get; set; }
	}

	public delegate void GPUImageLinesDetectedCallback(IntPtr lineArray, uint linesDetected, CMTime frameTime);

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageHoughTransformLineDetector
	{
		[Export ("edgeThreshold")]
		float EdgeThreshold { get; set; }

		[Export ("lineDetectionThreshold")]
		float LineDetectionThreshold { get; set; }

		[Export ("linesDetectedBlock", ArgumentSemantic.Copy), NullAllowed]
		GPUImageLinesDetectedCallback LinesDetected { get; set; }

		[Export ("intermediateImages", ArgumentSemantic.Retain)]
		NSMutableArray IntermediateImages { get; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageLineGenerator
	{
		[Export ("lineWidth")]
		float LineWidth { get; set; }

		[Export ("setLineColorRed:green:blue:")]
		void SetLineColor (float redComponent, float greenComponent, float blueComponent);

		[Export ("renderLinesFromArray:count:frameTime:")]
		void RenderLines (IntPtr lineSlopeAndIntercepts, uint numberOfLines, CMTime frameTime);
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageMotionBlurFilter
	{
		[Export ("blurSize")]
		float BlurSize { get; set; }

		[Export ("blurAngle")]
		float BlurAngle { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageZoomBlurFilter
	{
		[Export ("blurSize")]
		float BlurSize { get; set; }

		[Export ("blurCenter")]
		PointF BlurCenter { get; set; }
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageChromaKeyBlendFilter
	{
		[Export ("thresholdSensitivity")]
		float ThresholdSensitivity { get; set; }

		[Export ("smoothing")]
		float Smoothing { get; set; }

		[Export ("setColorToReplaceRed:green:blue:")]
		void SetColorToReplace (float redComponent, float greenComponent, float blueComponent);
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageDissolveBlendFilter
	{
		[Export ("mix")]
		float Mix { get; set; }
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageMultiplyBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageAddBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageSubtractBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageDivideBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageOverlayBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageDarkenBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageLightenBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageColorBurnBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageColorDodgeBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageScreenBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageExclusionBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageDifferenceBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageHardLightBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageSoftLightBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageAlphaBlendFilter
	{
		[Export ("mix")]
		float Mix { get; set; }
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageSourceOverBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageNormalBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageColorBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageHueBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageSaturationBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageLuminosityBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageLinearBurnBlendFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageTwoInputCrossTextureSamplingFilter
	{
		[Export ("texelWidth")]
		float TexelWidth { get; set; }

		[Export ("texelHeight")]
		float TexelHeight { get; set; }
	}

	[BaseType (typeof (GPUImageTwoInputCrossTextureSamplingFilter))]
	public partial interface GPUImagePoissonBlendFilter
	{
		[Export ("mix")]
		float Mix { get; set; }

		[Export ("numIterations")]
		uint NumIterations { get; set; }
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageMaskFilter
	{
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImagePixellateFilter
	{
		[Export ("fractionalWidthOfAPixel")]
		float FractionalWidthOfAPixel { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImagePolarPixellateFilter
	{
		[Export ("center")]
		PointF Center { get; set; }

		[Export ("pixelSize")]
		SizeF PixelSize { get; set; }
	}

	[BaseType (typeof (GPUImagePixellateFilter))]
	public partial interface GPUImagePolkaDotFilter
	{
		[Export ("dotScaling")]
		float DotScaling { get; set; }
	}

	[BaseType (typeof (GPUImagePixellateFilter))]
	public partial interface GPUImageHalftoneFilter
	{
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageCrosshatchFilter
	{
		[Export ("crossHatchSpacing")]
		float CrossHatchSpacing { get; set; }

		[Export ("lineWidth")]
		float LineWidth { get; set; }
	}

	[BaseType (typeof (GPUImageSobelEdgeDetectionFilter))]
	public partial interface GPUImageSketchFilter
	{
	}

	[BaseType (typeof (GPUImageThresholdEdgeDetectionFilter))]
	public partial interface GPUImageThresholdSketchFilter
	{
	}

	[BaseType (typeof (GPUImage3x3TextureSamplingFilter))]
	public partial interface GPUImageToonFilter
	{
		[Export ("threshold")]
		float Threshold { get; set; }

		[Export ("quantizationLevels")]
		float QuantizationLevels { get; set; }
	}

	[BaseType (typeof (GPUImageFilterGroup))]
	public partial interface GPUImageSmoothToonFilter
	{
		[Export ("texelWidth")]
		float TexelWidth { get; set; }

		[Export ("texelHeight")]
		float TexelHeight { get; set; }

		[Export ("blurSize")]
		float BlurSize { get; set; }

		[Export ("threshold")]
		float Threshold { get; set; }

		[Export ("quantizationLevels")]
		float QuantizationLevels { get; set; }
	}

	[BaseType (typeof (GPUImage3x3ConvolutionFilter))]
	public partial interface GPUImageEmbossFilter
	{
		[Export ("intensity")]
		float Intensity { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImagePosterizeFilter
	{
		[Export ("colorLevels")]
		uint ColorLevels { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageSwirlFilter
	{
		[Export ("center")]
		PointF Center { get; set; }

		[Export ("radius")]
		float Radius { get; set; }

		[Export ("angle")]
		float Angle { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageBulgeDistortionFilter
	{
		[Export ("center")]
		PointF Center { get; set; }

		[Export ("radius")]
		float Radius { get; set; }

		[Export ("scale")]
		float Scale { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImagePinchDistortionFilter
	{
		[Export ("center")]
		PointF Center { get; set; }

		[Export ("radius")]
		float Radius { get; set; }

		[Export ("scale")]
		float Scale { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageStretchDistortionFilter
	{
		[Export ("center")]
		PointF Center { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageSphereRefractionFilter
	{
		[Export ("center")]
		PointF Center { get; set; }

		[Export ("radius")]
		float Radius { get; set; }

		[Export ("refractiveIndex")]
		float RefractiveIndex { get; set; }
	}

	[BaseType (typeof (GPUImageSphereRefractionFilter))]
	public partial interface GPUImageGlassSphereFilter
	{
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageVignetteFilter
	{
		[Export ("vignetteCenter")]
		PointF VignetteCenter { get; set; }

		[Export ("vignetteColor")]
		GPUVector3 VignetteColor { get; set; }

		[Export ("vignetteStart")]
		float VignetteStart { get; set; }

		[Export ("vignetteEnd")]
		float VignetteEnd { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageKuwaharaFilter
	{
		[Export ("radius")]
		uint Radius { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageKuwaharaRadius3Filter
	{
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImagePerlinNoiseFilter
	{
		[Export ("colorStart")]
		GPUVector4 ColorStart { get; set; }

		[Export ("colorFinish")]
		GPUVector4 ColorFinish { get; set; }

		[Export ("scale")]
		float Scale { get; set; }
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageCGAColorspaceFilter
	{
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageMosaicFilter
	{
		[Export ("inputTileSize")]
		SizeF InputTileSize { get; set; }

		[Export ("numTiles")]
		float NumTiles { get; set; }

		[Export ("displayTileSize")]
		SizeF DisplayTileSize { get; set; }

		[Export ("colorOn")]
		bool ColorOn { get; set; }

		[Export ("setTileSet")]
		void SetTileSet (string tileSet);
	}

	[BaseType (typeof (GPUImageFilter))]
	public partial interface GPUImageJFAVoronoiFilter
	{
		[Export ("sizeInPixels")]
		SizeF SizeInPixels { get; set; }
	}

	[BaseType (typeof (GPUImageTwoInputFilter))]
	public partial interface GPUImageVoronoiConsumerFilter
	{
		[Export ("sizeInPixels")]
		SizeF SizeInPixels { get; set; }
	}
}

