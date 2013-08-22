using System;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreMedia;
using MonoTouch.CoreVideo;
using MonoTouch.CoreGraphics;

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

	public delegate void FrameProcessingCompletedHandler(GPUImageOutput sender, CMTime frameTime);

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
		NSObject TargetToIgnoreForUpdates { get; set; }		//todo: should be GPUImageInput

		[Export ("frameProcessingCompletionBlock", ArgumentSemantic.Copy)]
		FrameProcessingCompletedHandler FrameProcessingCompletionBlock { get; set; }	//todo: map this to event

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
		[Export ("redControlPoints", ArgumentSemantic.Copy)]
		NSObject [] RedControlPoints { get; set; }

		[Export ("greenControlPoints", ArgumentSemantic.Copy)]
		NSObject [] GreenControlPoints { get; set; }

		[Export ("blueControlPoints", ArgumentSemantic.Copy)]
		NSObject [] BlueControlPoints { get; set; }

		[Export ("rgbCompositeControlPoints", ArgumentSemantic.Copy)]
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
}

