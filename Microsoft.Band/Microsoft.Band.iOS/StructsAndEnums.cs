using System;
using ObjCRuntime;

namespace Microsoft.Band
{
	[Native]
	public enum BandNSErrorCodes : long
	{
		// Band errors
		BandNotConnected = 100L,
		BandError,

		// Validation errors
		NullArgument = 200L,
		ValueEmpty,
		InvalidImage,
		InvalidFilePath,
		TileNameInvalidLength,
		SDKUnsupported,
		InvalidArgument,
		UserDeclinedHR,
		UserConsentRequiredHR,
        SensorUnavailable,
        BarcodeInvalidLength,
		UnsupportedPageElement,

		// Tile errors
		InvalidTile = 300L,
		InvalidTileId,
		UserDeclinedTile,
		MaxTiles,
		TileAlreadyExist,
		TileNotFound,
		PageElementAlreadyExist,
        PageElementIllegalIdentifier,
        PageDataTooLarge,

		// Unknown
		Unknown = 900L
	}

	[Native]
	public enum BandTileEventType : ulong
	{
		Opened,
		ButtonPressed,
		Closed
	}

	[Native]
	public enum UserConsent : ulong
	{
		NotSpecified,
		Granted,
		Declined
	}
}

namespace Microsoft.Band.Tiles.Pages
{
	[Native]
	public enum HorizontalAlignment : ulong
	{
		Left = 0uL,
		Center,
		Right
	}

	[Native]
	public enum VerticalAlignment : ulong
	{
		Top = 100uL,
		Center,
		Bottom
	}

	[Native]
	public enum FlowPanelOrientation : ulong
	{
		Vertical = 300uL,
		Horizontal
	}

	[Native]
	public enum TextBlockBaselineAlignment : ulong
	{
		Automatic = 400uL,
		Absolute,
		Relative
	}

	[Native]
	public enum TextBlockFont : ulong
	{
		/// <summary>
		/// Smallest font, contains all characters supported by the device.
		/// </summary>
		Small = 500uL,

		/// <summary>
		/// Medium sized font, contains alphanumeric characters as well as some symbols.
		/// </summary>
		Medium,

		/// <summary>
		/// Large font, contains numeric and some symbols.
		/// </summary>
		Large,

		/// <summary>
		/// Extra large font contains numeric characters and a very small set of symbols.
		/// </summary>
		ExtraLargeNumbers,

		/// <summary>
		/// Extra Large Bold contains numbers and a very small subset of symbols.
		/// </summary>
		ExtraLargeNumbersBold
	}

	public enum WrappedTextBlockFont : uint
	{
		/// <summary>
		/// Smallest font, contains all characters supported by the device.
		/// </summary>
		Small = 600u,

		/// <summary>
		/// Medium sized font, contains alphanumeric characters as well as some symbols.
		/// </summary>
		Medium
	}

	public enum BarcodeType : ushort
	{
		Pdf417 = 800,
		Code39
	}

	public enum ElementColorSource : ushort
	{
		Custom = 0,

		BandBase,
		BandHighlight,
		BandLowlight,
		BandSecondaryText,
		BandHighContrast,
		BandMuted,

		TileBase,
		TileHighlight,
		TileLowlight,
		TileSecondaryText,
		TileHighContrast,
		TileMuted,

		Max
	}
}

namespace Microsoft.Band.Notifications
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

	public enum MessageFlags : byte
	{
		None = (byte)0uL,
		ShowDialog = (byte)1uL
	}
}

namespace Microsoft.Band.Sensors
{
	[Native]
	public enum HeartRateQuality : ulong
	{
		Acquiring,
		Locked
	}

	[Native]
	public enum MotionType : ulong
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

	[Native]
	public enum BandContactStatus : ulong
	{
		NotWorn,
		Worn,
		Unknown
	}
}
