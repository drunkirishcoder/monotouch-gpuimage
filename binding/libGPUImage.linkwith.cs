using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libGPUImage.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator, Frameworks = "CoreMedia CoreVideo OpenGLES AVFoundation QuartzCore", ForceLoad = true)]
