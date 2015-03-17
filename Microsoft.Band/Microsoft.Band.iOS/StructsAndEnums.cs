using System;
using ObjCRuntime;

namespace Microsoft.Band
{
	[Native]
	public enum VibrationType : ulong
	{
		NotificationOneTone = 7uL,
		NotificationTwoTone = 16uL,
		NotificationAlarm = 17uL,
		NotificationTimer = 18uL,
		OneToneHigh = 27uL,
		TwoToneHigh = 29uL,
		ThreeToneHigh = 28uL,
		RampUp = 5uL,
		RampDown = 4uL
	}
	public enum NotificationMessageFlags : byte
	{
		None = (byte)0uL,
		ShowDialog = (byte)1uL
	}
	[Native]
	public enum HeartRateQuality : ulong
	{
		Acquiring,
		Locked
	}
	[Native]
	public enum PedometerMode : ulong
	{
		Unknown,
		Idle,
		Walking,
		Jogging,
		Running
	}
	[Native]
	public enum UVIndexLevel : ulong
	{
		None,
		Low,
		Medium,
		High,
		VeryHigh
	}
	public enum SensorBandContactState : uint
	{
		NotWorn,
		Worn,
		Unknown
	}
	[Native]
	public enum NSErrorCodes : long
	{
		BandNotConnected = 100L,
		BandError,
		NullArgument = 200L,
		ValueEmpty,
		InvalidImage,
		InvalidFilePath,
		TileNameInvalidLength,
		SDKUnsupported,
		InvalidArgument,
		InvalidTile = 300L,
		InvalidTileID,
		UserDeclinedTile,
		MaxTiles,
		TileAlreadyExist,
		TileNotFound,
		Unknown = 900L
	}
	[Native]
	public enum PageElementHorizontalAlignment : ulong
	{
		None = 0uL,
		Left,
		Center,
		Right
	}
	[Native]
	public enum PageElementVerticalAlignment : ulong
	{
		None = 100uL,
		Top,
		Center,
		Bottom
	}
	[Native]
	public enum PageElementVisibility : ulong
	{
		Hidden = 200uL,
		Visible
	}
	[Native]
	public enum FlowListOrientation : ulong
	{
		Horizontal = 300uL,
		Vertical
	}
	[Native]
	public enum TextBlockBaselineAlignment : ulong
	{
		Absolute = 400uL,
		Relative
	}
	[Native]
	public enum TextBlockFont : ulong
	{
		Small = 500uL,
		Medium,
		Large,
		ExtraLargeNumbers,
		ExtraLargeNumbersBold
	}
	public enum WrappedTextBlockFont : uint
	{
		Small = 600u,
		Medium
	}
	public enum TextBlockLayoutElementWidth : uint
	{
		Fixed = 700u,
		Auto
	}
	public enum BarcodeType : ushort
	{
		PDF417 = (ushort)800u,
		CODE39
	}

}

