using ObjCRuntime;

[assembly: LinkWith (
	"MicrosoftBandKit_iOS.a", 
	LinkTarget.ArmV7 | LinkTarget.Simulator | LinkTarget.Arm64 | LinkTarget.Simulator64, 
	SmartLink = true, 
	ForceLoad = true,
	Frameworks = "CoreBluetooth")]
