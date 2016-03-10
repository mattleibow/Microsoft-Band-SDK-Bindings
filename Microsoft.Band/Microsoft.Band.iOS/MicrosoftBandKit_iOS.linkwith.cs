using ObjCRuntime;

[assembly: LinkWith (
    "MicrosoftBandKit_iOS-1.3.20217.2.a", 
	LinkTarget.ArmV7 | LinkTarget.Simulator | LinkTarget.Arm64 | LinkTarget.Simulator64, 
	ForceLoad = true,
	Frameworks = "CoreBluetooth")]
