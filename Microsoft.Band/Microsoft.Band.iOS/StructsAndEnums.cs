using System;
using ObjCRuntime;

namespace Microsoft.Band
{
	[Native]
	public enum BandVibrationType : ulong
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
	public enum BandNotificationMessageFlags : byte
	{
		None = (byte)0uL,
		ShowDialog = (byte)1uL
	}
	[Native]
	public enum BandHeartRateQuality : ulong
	{
		Acquiring,
		Locked
	}
	[Native]
	public enum BandPedometerMode : ulong
	{
		Unknown,
		Idle,
		Walking,
		Jogging,
		Running
	}
	[Native]
	public enum BandUVIndexLevel : ulong
	{
		None,
		Low,
		Medium,
		High,
		VeryHigh
	}
	public enum BandSensorBandContactState : uint
	{
		NotWorn,
		Worn,
		Unknown
	}
	[Native]
	public enum BandNSErrorCodes : long
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
	public enum BandPageElementHorizontalAlignment : ulong
	{
		None = 0uL,
		Left,
		Center,
		Right
	}
	[Native]
	public enum BandPageElementVerticalAlignment : ulong
	{
		None = 100uL,
		Top,
		Center,
		Bottom
	}
	[Native]
	public enum BandPageElementVisibility : ulong
	{
		Hidden = 200uL,
		Visible
	}
	[Native]
	public enum BandFlowListOrientation : ulong
	{
		Horizontal = 300uL,
		Vertical
	}
	[Native]
	public enum BandTextBlockBaselineAlignment : ulong
	{
		Absolute = 400uL,
		Relative
	}
	[Native]
	public enum BandTextBlockFont : ulong
	{
		Small = 500uL,
		Medium,
		Large,
		ExtraLargeNumbers,
		ExtraLargeNumbersBold
	}
	public enum BandWrappedTextBlockFont : uint
	{
		Small = 600u,
		Medium
	}
	public enum BandTextBlockLayoutElementWidth : uint
	{
		Fixed = 700u,
		Auto
	}
	public enum BandBarcodeType : ushort
	{
		PDF417 = (ushort)800u,
		CODE39
	}

}

