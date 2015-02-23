using System;
using ObjCRuntime;

[assembly: LinkWith ("libGPUImage.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64 | LinkTarget.Simulator, Frameworks = "CoreMedia CoreVideo OpenGLES AVFoundation QuartzCore", ForceLoad = true)]
