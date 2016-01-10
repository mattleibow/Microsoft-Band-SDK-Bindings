using ObjCRuntime;

[assembly: LinkWith (
    "MicrosoftBandKit_iOS-1.3.11222.1.a", 
	LinkTarget.ArmV7 | LinkTarget.Simulator | LinkTarget.Arm64 | LinkTarget.Simulator64, 
	SmartLink = true, 
	ForceLoad = true,
	Frameworks = "CoreBluetooth")]
