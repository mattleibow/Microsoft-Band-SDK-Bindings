using ObjCRuntime;

[assembly: LinkWith (
    "MicrosoftBandKit_iOS-1.3.20419.1.a", 
	LinkTarget.ArmV7 | LinkTarget.Simulator | LinkTarget.Arm64 | LinkTarget.Simulator64, 
	ForceLoad = true,
	Frameworks = "CoreBluetooth")]
