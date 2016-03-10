using System;

using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Personalization;
using Microsoft.Band.Sensors;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;

// TODO: the Xamarin.Mac binding

namespace Microsoft.Band
{
    [Static]
	partial interface BandClientManagerConstants
	{
		// extern NSString *const MSBClientManagerBluetoothPowerNotification;
        [Field ("MSBClientManagerBluetoothPowerNotification", "__Internal")]
		NSString BluetoothPowerNotification { get; }

		// extern NSString *const MSBClientManagerBluetoothPowerKey;
        [Field ("MSBClientManagerBluetoothPowerKey", "__Internal")]
		NSString BluetoothPowerKey { get; }
	
		// extern NSString *const MSBErrorTypeDomain;
        [Field ("MSBErrorTypeDomain", "__Internal")]
		NSString BandErrorTypeDomain { get; }
	}

	interface IBandClientManagerDelegate
	{

	}

	// @protocol MSBClientManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBClientManagerDelegate")]
	interface BandClientManagerDelegate
	{
		// @required -(void)clientManager:(MSBClientManager *)clientManager clientDidConnect:(MSBClient *)client;
		[Abstract]
		[Export ("clientManager:clientDidConnect:"), EventArgs ("ClientManagerConnected")]
		void Connected (BandClientManager clientManager, BandClient client);

		// @required -(void)clientManager:(MSBClientManager *)clientManager clientDidDisconnect:(MSBClient *)client;
		[Abstract]
		[Export ("clientManager:clientDidDisconnect:"), EventArgs ("ClientManagerDisconnected")]
		void Disconnected (BandClientManager clientManager, BandClient client);

		// @required -(void)clientManager:(MSBClientManager *)clientManager client:(MSBClient *)client didFailToConnectWithError:(NSError *)error;
		[Abstract]
		[Export ("clientManager:client:didFailToConnectWithError:"), EventArgs ("ClientManagerFailedToConnect")]
		void ConnectionFailed (BandClientManager clientManager, BandClient client, NSError error);
	}

	// @interface MSBClientManager : NSObject
	[BaseType (typeof(NSObject), Name = "MSBClientManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof(BandClientManagerDelegate) })]
	interface BandClientManager
	{
		// @property (nonatomic, weak) id<MSBClientManagerDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IBandClientManagerDelegate Delegate { get; set; }

		// @property (readonly) BOOL isPowerOn;
		[Export ("isPowerOn")]
		bool IsBluetoothPowerOn { get; }

		// +(MSBClientManager *)sharedManager;
		[Static]
		[Export ("sharedManager")]
		BandClientManager Instance { get; }

		// -(MSBClient *)clientWithConnectionIdentifier:(NSUUID *)identifer;
		[Export ("clientWithConnectionIdentifier:")]
		BandClient FindClient (NSUuid identifer);

		// -(NSArray *)attachedClients;
		[Export ("attachedClients")]
		BandClient[] AttachedClients { get; }

		// -(void)connectClient:(MSBClient *)client;
		[Export ("connectClient:")]
		void ConnectAsync (BandClient client);

		// -(void)cancelClientConnection:(MSBClient *)client;
		[Export ("cancelClientConnection:")]
		void DisconnectAsync (BandClient client);
	}

	// @interface MSBClient : NSObject
	[BaseType (typeof(NSObject), Name = "MSBClient", Delegates = new string [] { "TileDelegate" }, Events = new Type [] { typeof(BandClientTileDelegate) })]
	interface BandClient
	{
		// @property (readonly) NSString * name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly) NSUUID * connectionIdentifier;
		[Export ("connectionIdentifier")]
		NSUuid ConnectionId { get; }

		// @property (readonly) BOOL isDeviceConnected;
		[Export ("isDeviceConnected")]
		bool IsConnected { get; }

		// @property (nonatomic, weak) id<MSBClientTileDelegate> tileDelegate;
		[NullAllowed, Export ("tileDelegate", ArgumentSemantic.Weak)]
		IBandClientTileDelegate TileDelegate { get; set; }

		// @property (readonly, nonatomic) id<MSBTileManagerProtocol> tileManager;
		[Export ("tileManager")]
		IBandTileManager TileManager { get; }

		// @property (readonly, nonatomic) id<MSBPersonalizationManagerProtocol> personalizationManager;
		[Export ("personalizationManager")]
		IBandPersonalizationManager PersonalizationManager { get; }

		// @property (readonly, nonatomic) id<MSBNotificationManagerProtocol> notificationManager;
		[Export ("notificationManager")]
		IBandNotificationManager NotificationManager { get; }

		// @property (readonly, nonatomic) id<MSBSensorManagerProtocol> sensorManager;
		[Export ("sensorManager")]
		IBandSensorManager SensorManager { get; }

		// -(void)firmwareVersionWithCompletionHandler:(void (^)(NSString *, NSError *))completionHandler;
		[Export ("firmwareVersionWithCompletionHandler:")]
		void GetFirmwareVersionAsync (Action<NSString, NSError> completionHandler);

		// -(void)hardwareVersionWithCompletionHandler:(void (^)(NSString *, NSError *))completionHandler;
		[Export ("hardwareVersionWithCompletionHandler:")]
		void GetHardwareVersionAsync (Action<NSString, NSError> completionHandler);
	}
}

namespace Microsoft.Band.Tiles.Pages
{
	// @interface MSBPageElement : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageElement")]
	interface PageElement
	{
		// @property (assign, nonatomic) MSBPageElementIdentifier elementId;
		[Export ("elementId")]
		ushort ElementId { get; set; }

		// @property (nonatomic, strong) MSBPageRect * rect;
		[Export ("rect", ArgumentSemantic.Strong)]
		PageRect Rect { get; set; }

		// @property (nonatomic, strong) MSBPageMargins * margins;
		[Export ("margins", ArgumentSemantic.Strong)]
		Margins Margins { get; set; }

		// @property (assign, nonatomic) MSBPageHorizontalAlignment horizontalAlignment;
		[Export ("horizontalAlignment", ArgumentSemantic.Assign)]
		HorizontalAlignment HorizontalAlignment { get; set; }

		// @property (assign, nonatomic) MSBPageVerticalAlignment verticalAlignment;
		[Export ("verticalAlignment", ArgumentSemantic.Assign)]
		VerticalAlignment VerticalAlignment { get; set; }
	}

	// @interface MSBPageElementData : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageElementData")]
	interface PageElementData
	{
		// @property (readonly, nonatomic) MSBPageElementIdentifier elementId;
		[Export ("elementId")]
		ushort ElementId { get; }
	}

	// @interface MSBPageTextData : MSBPageElementData
	[BaseType (typeof(PageElementData), Name = "MSBPageTextData")]
	interface TextData
	{
		// @property (readonly, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; }
	}

	// @interface MSBPageTextBlockData : MSBPageTextData
	[BaseType (typeof(TextData), Name = "MSBPageTextBlockData")]
	interface TextBlockData
	{
		// +(MSBPageTextBlockData *)pageTextBlockDataWithElementId:(MSBPageElementIdentifier)elementId text:(NSString *)text error:(NSError **)pError;
		[Static]
		[Export ("pageTextBlockDataWithElementId:text:error:")]
		TextBlockData Create (ushort elementId, string text, out NSError pError);
	}

	// @interface MSBPageWrappedTextBlockData : MSBPageElementData
	[BaseType (typeof(PageElementData), Name = "MSBPageWrappedTextBlockData")]
	interface WrappedTextBlockData
	{
		// @property (readonly, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; }

		// +(MSBPageWrappedTextData *)pageWrappedTextDataWithElementId:(MSBPageElementIdentifier)elementId text:(NSString *)text error:(NSError **)pError;
		[Static]
		[Export ("pageWrappedTextDataWithElementId:text:error:")]
		WrappedTextBlockData Create (ushort elementId, string text, out NSError pError);
	}

	// @interface MSBPageIconData : MSBPageElementData
	[BaseType (typeof(PageElementData), Name = "MSBPageIconData")]
	interface IconData
	{
		// @property (readonly, nonatomic) NSUInteger iconIndex;
		[Export ("iconIndex")]
		nuint IconIndex { get; }

		// +(MSBPageIconData *)pageIconDataWithElementId:(MSBPageElementIdentifier)elementId iconIndex:(NSUInteger)iconIndex error:(NSError **)pError;
		[Static]
		[Export ("pageIconDataWithElementId:iconIndex:error:")]
		IconData Create (ushort elementId, nuint iconIndex, out NSError pError);
	}

	// @interface MSBPageData : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageData")]
	interface PageData
	{
		// @property (readonly, nonatomic) NSUUID * pageId;
		[Export ("pageId")]
		NSUuid PageId { get; }

		// @property (readonly, nonatomic) NSUInteger pageLayoutIndex;
		[Export ("pageLayoutIndex")]
		nuint PageLayoutIndex { get; }

		// @property (readonly, nonatomic) NSArray * values;
		[Export ("values")]
		PageElementData[] Values { get; }

		// +(MSBPageData *)pageDataWithId:(NSUUID *)pageId layoutIndex:(NSUInteger)layoutIndex value:(NSArray *)values;
		[Static]
		[Export ("pageDataWithId:layoutIndex:value:")]
		PageData Create (NSUuid pageId, nuint layoutIndex, PageElementData[] values);
	}

	// @interface MSBPageRect : NSObject <NSCopying>
	[BaseType (typeof(NSObject), Name = "MSBPageRect")]
	interface PageRect : INSCopying
	{
		// @property (assign, nonatomic) UInt16 x;
		[Export ("x")]
		ushort X { get; set; }

		// @property (assign, nonatomic) UInt16 y;
		[Export ("y")]
		ushort Y { get; set; }

		// @property (assign, nonatomic) UInt16 width;
		[Export ("width")]
		ushort Width { get; set; }

		// @property (assign, nonatomic) UInt16 height;
		[Export ("height")]
		ushort Height { get; set; }

		// +(MSBPageRect *)rectWithX:(UInt16)x y:(UInt16)y width:(UInt16)width height:(UInt16)height;
		[Static]
		[Export ("rectWithX:y:width:height:")]
		PageRect Create (ushort x, ushort y, ushort width, ushort height);
	}

	// @interface MSBPageMargins : NSObject <NSCopying>
	[BaseType (typeof(NSObject), Name = "MSBPageMargins")]
	interface Margins : INSCopying
	{
		// @property (assign, nonatomic) SInt16 left;
		[Export ("left")]
		short Left { get; set; }

		// @property (assign, nonatomic) SInt16 top;
		[Export ("top")]
		short Top { get; set; }

		// @property (assign, nonatomic) SInt16 right;
		[Export ("right")]
		short Right { get; set; }

		// @property (assign, nonatomic) SInt16 bottom;
		[Export ("bottom")]
		short Bottom { get; set; }

		// +(MSBPageMargins *)marginsWithLeft:(SInt16)left top:(SInt16)top right:(SInt16)right bottom:(SInt16)bottom;
		[Static]
		[Export ("marginsWithLeft:top:right:bottom:")]
		Margins Create (short left, short top, short right, short bottom);
	}

	// @interface MSBPageBarcodeData : MSBPageElementData
	[BaseType (typeof(PageElementData), Name = "MSBPageBarcodeData")]
	interface BarcodeData
	{
		// @property (readonly, nonatomic) NSString * value;
		[Export ("value")]
		string Barcode { get; }

		// @property (readonly, nonatomic) MSBPageBarcodeType barcodeType;
		[Export ("barcodeType")]
		BarcodeType BarcodeType { get; }

		// +(MSBPageBarcodeData *)pageBarcodeDataWithElementId:(MSBPageElementIdentifier)elementId barcodeType:(MSBPageBarcodeType)type value:(NSString *)value error:(NSError **)pError;
		[Static]
		[Export ("pageBarcodeDataWithElementId:barcodeType:value:error:")]
		BarcodeData Create (ushort elementId, BarcodeType type, string value, out NSError pError);
	}

	// @interface MSBPageTextButtonData : MSBPageTextData
	[BaseType (typeof(TextData), Name = "MSBPageTextButtonData")]
	interface TextButtonData
	{
		// +(MSBPageTextButtonData *)pageTextButtonDataWithElementId:(MSBPageElementIdentifier)elementId text:(NSString *)text error:(NSError **)pError;
		[Static]
		[Export ("pageTextButtonDataWithElementId:text:error:")]
		TextButtonData Create (ushort elementId, string text, out NSError pError);
	}

	// @interface MSBPageFilledButtonData : MSBPageElementData
	[BaseType (typeof(PageElementData), Name = "MSBPageFilledButtonData")]
	interface FilledButtonData
	{
		// @property (nonatomic, strong) MSBColor * pressedColor;
		[Export ("pressedColor", ArgumentSemantic.Strong)]
		BandColor PressedColor { get; set; }

		// @property (nonatomic, strong) MSBPageElementColorSource * pressedColorSource;
        [Export ("pressedColorSource", ArgumentSemantic.Assign)]
		ElementColorSource PressedColorSource { get; set; }

		// +(MSBPageFilledButtonData *)pageFilledButtonDataWithElementId:(MSBPageElementIdentifier)elementId;
		[Static]
		[Export ("pageFilledButtonDataWithElementId:")]
		FilledButtonData Create (ushort elementId);
	}

	// @interface MSBPagePanel : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBPagePanel")]
	interface PagePanel
	{
		// -(NSArray *)elements;
		[Export ("elements")]
		PageElement[] Elements { get; }

		// -(BOOL)addElement:(MSBPageElement *)element;
		[Export ("addElement:")]
		bool AddElement (PageElement element);

		// -(BOOL)addElements:(NSArray *)elements;
		[Export ("addElements:")]
		bool AddElements (PageElement[] elements);
	}

	// @interface MSBPageFilledPanel : MSBPagePanel
	[BaseType (typeof(PagePanel), Name = "MSBPageFilledPanel")]
	interface FilledPanel
	{
		// @property (nonatomic, strong) MSBColor * backgroundColor;
		[Export ("backgroundColor", ArgumentSemantic.Strong)]
		BandColor BackgroundColor { get; set; }

		// @property (assign, nonatomic) MSBPageElementColorSource backgroundColorSource;
		[Export ("backgroundColorSource", ArgumentSemantic.Assign)]
		ElementColorSource BackgroundColorSource { get; set; }

		// -(id)initWithRect:(MSBPageRect *)rect;
		[Export ("initWithRect:")]
		IntPtr Constructor (PageRect rect);
	}

	// @interface MSBPageFlowPanel : MSBPagePanel
	[BaseType (typeof(PagePanel), Name = "MSBPageFlowPanel")]
	interface FlowPanel
	{
		// @property (assign, nonatomic) MSBFlowListOrientation orientation;
		[Export ("orientation", ArgumentSemantic.Assign)]
		FlowPanelOrientation Orientation { get; set; }

		// -(id)initWithRect:(MSBPageRect *)rect;
		[Export ("initWithRect:")]
		IntPtr Constructor (PageRect rect);
	}

	// @interface MSBPageIcon : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBPageIcon")]
	interface Icon
	{
		// @property (nonatomic, strong) MSBColor * color;
		[Export ("color", ArgumentSemantic.Strong)]
		BandColor Color { get; set; }

		// @property (assign, nonatomic) MSBPageElementColorSource colorSource;
		[Export ("colorSource", ArgumentSemantic.Assign)]
		ElementColorSource ColorSource { get; set; }

		// -(id)initWithRect:(MSBPageRect *)rect;
		[Export ("initWithRect:")]
		IntPtr Constructor (PageRect rect);
	}

	// @interface MSBPageLayout : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageLayout")]
	interface PageLayout
	{
		// @property (nonatomic, strong) MSBPagePanel * root;
		[Export ("root", ArgumentSemantic.Strong)]
		PagePanel Root { get; set; }

		// -(instancetype)initWithRoot:(MSBPagePanel *)root;
		[Export ("initWithRoot:")]
		IntPtr Constructor (PagePanel root);
	}

	// @interface MSBScrollFlowList : MSBPageFlowPanel
	[BaseType (typeof(FlowPanel), Name = "MSBPageScrollFlowPanel")]
	interface ScrollFlowPanel
	{
		// @property (nonatomic, strong) MSBColor * scrollBarColor;
		[Export ("scrollBarColor", ArgumentSemantic.Strong)]
		BandColor ScrollBarColor { get; set; }

		// @property (assign, nonatomic) MSBPageElementColorSource scrollBarColorSource;
		[Export ("scrollBarColorSource", ArgumentSemantic.Assign)]
		ElementColorSource ScrollBarColorSource { get; set; }

		// -(id)initWithRect:(MSBPageRect *)rect;
		[Export ("initWithRect:")]
		IntPtr Constructor (PageRect rect);
	}

	// @interface MSBPageTextBlock : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBPageTextBlock")]
	interface TextBlock
	{
		// @property (assign, nonatomic) MSBPageTextBlockFont font;
		[Export ("font", ArgumentSemantic.Assign)]
		TextBlockFont Font { get; set; }

		// @property (assign, nonatomic) MSBTextBlockBaseline baseline;
		[Export ("baseline")]
		ushort Baseline { get; set; }

		// @property (assign, nonatomic) MSBPageTextBlockBaselineAlignment baselineAlignment;
		[Export ("baselineAlignment", ArgumentSemantic.Assign)]
		TextBlockBaselineAlignment BaselineAlignment { get; set; }

		// @property (assign, nonatomic) BOOL autoWidth;
		[Export ("autoWidth")]
		bool AutoWidth { get; set; }

		// @property (nonatomic, strong) MSBColor * color;
		[Export ("color", ArgumentSemantic.Strong)]
		BandColor Color { get; set; }

		// @property (assign, nonatomic) MSBPageElementColorSource colorSource;
		[Export ("colorSource", ArgumentSemantic.Assign)]
		ElementColorSource ColorSource { get; set; }

		// -(id)initWithRect:(MSBPageRect *)rect font:(MSBPageTextBlockFont)font;
		[Export ("initWithRect:font:")]
		IntPtr Constructor (PageRect rect, TextBlockFont font);
	}

	// @interface MSBPageWrappedTextBlock : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBPageWrappedTextBlock")]
	interface WrappedTextBlock
	{
		// @property (assign, nonatomic) MSBPageWrappedTextBlockFont font;
		[Export ("font", ArgumentSemantic.Assign)]
		WrappedTextBlockFont Font { get; set; }

		// @property (assign, nonatomic) BOOL autoHeight;
		[Export ("autoHeight")]
		bool AutoHeight { get; set; }

		// @property (nonatomic, strong) MSBColor * color;
		[Export ("color", ArgumentSemantic.Strong)]
		BandColor Color { get; set; }

		// @property (assign, nonatomic) MSBPageElementColorSource colorSource;
		[Export ("colorSource", ArgumentSemantic.Assign)]
		ElementColorSource ColorSource { get; set; }

		// -(id)initWithRect:(MSBPageRect *)rect font:(MSBPageWrappedTextBlockFont)font;
		[Export ("initWithRect:font:")]
		IntPtr Constructor (PageRect rect, WrappedTextBlockFont font);
	}

	// @interface MSBPageBarcode : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBPageBarcode")]
	interface Barcode
	{
		// @property (readonly, nonatomic) MSBPageBarcodeType barcodeType;
		[Export ("barcodeType")]
		BarcodeType BarcodeType { get; }

		// -(id)initWithRect:(MSBPageRect *)rect barcodeType:(MSBPageBarcodeType)type;
		[Export ("initWithRect:barcodeType:")]
		IntPtr Constructor (PageRect rect, BarcodeType type);
	}

	// @interface MSBPageTextButton : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBPageTextButton")]
	interface TextButton
	{
		// @property (nonatomic, strong) MSBColor * pressedColor;
		[Export ("pressedColor", ArgumentSemantic.Strong)]
		BandColor PressedColor { get; set; }

		// @property (nonatomic, assign) MSBPageElementColorSource * pressedColorSource;
		[Export ("pressedColorSource", ArgumentSemantic.Assign)]
		ElementColorSource PressedColorSource { get; set; }

		// -(id)initWithRect:(MSBPageRect *)rect;
		[Export ("initWithRect:")]
		IntPtr Constructor (PageRect rect);
	}

	// @interface MSBPageFilledButton : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBPageFilledButton")]
	interface FilledButton
	{
		// @property (nonatomic, strong) MSBColor * backgroundColor;
		[Export ("backgroundColor", ArgumentSemantic.Strong)]
		BandColor BackgroundColor { get; set; }

		// @property (nonatomic, strong) MSBPageElementColorSource * backgroundColorSource;
		[Export ("backgroundColorSource", ArgumentSemantic.Strong)]
		ElementColorSource BackgroundColorSource { get; set; }

		// -(id)initWithRect:(MSBPageRect *)rect;
		[Export ("initWithRect:")]
		IntPtr Constructor (PageRect rect);
	}
}

namespace Microsoft.Band.Notifications
{
	interface IBandNotificationManager
	{

	}

	// @protocol MSBNotificationManagerProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBNotificationManagerProtocol")]
	interface BandNotificationManager
	{
		// @required -(void)vibrateWithType:(MSBVibrationType)vibrationType completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("vibrateWithType:completionHandler:")]
		void VibrateAsync (VibrationType vibrationType, Action<NSError> completionHandler);

		// @required -(void)showDialogWithTileID:(NSUUID *)tileID title:(NSString *)title body:(NSString *)body completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("showDialogWithTileID:title:body:completionHandler:")]
		void ShowDialogAsync (NSUuid tileID, string title, string body, Action<NSError> completionHandler);

		// @required -(void)sendMessageWithTileID:(NSUUID *)tileID title:(NSString *)title body:(NSString *)body timeStamp:(NSDate *)timeStamp flags:(MSBNotificationMessageFlags)flags completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("sendMessageWithTileID:title:body:timeStamp:flags:completionHandler:")]
		void SendMessageAsync (NSUuid tileID, string title, string body, NSDate timeStamp, MessageFlags flags, Action<NSError> completionHandler);

		// @required -(void)registerNotificationWithTileID:(NSUUID *)tileID completionHandler:(void (^)(NSError *))completionHandler __attribute__((availability(ios, introduced=7.0)));
		[Introduced (PlatformName.iOS, 7, 0)]
		[Abstract]
		[Export ("registerNotificationWithTileID:completionHandler:")]
		void RegisterNotificationAsync (NSUuid tileID, Action<NSError> completionHandler);

		// @required -(void)registerNotificationWithCompletionHandler:(void (^)(NSError *))completionHandler __attribute__((availability(ios, introduced=7.0)));
		[Introduced (PlatformName.iOS, 7, 0)]
		[Abstract]
		[Export ("registerNotificationWithCompletionHandler:")]
		void RegisterNotificationAsync (Action<NSError> completionHandler);

		// @required -(void)unregisterNotificationWithCompletionHandler:(void (^)(NSError *))completionHandler __attribute__((availability(ios, introduced=7.0)));
		[Introduced (PlatformName.iOS, 7, 0)]
		[Abstract]
		[Export ("unregisterNotificationWithCompletionHandler:")]
		void UnregisterNotificationAsync (Action<NSError> completionHandler);
	}
}

namespace Microsoft.Band.Personalization
{
	interface IBandPersonalizationManager
	{

	}

	// @protocol MSBPersonalizationManagerProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBPersonalizationManagerProtocol")]
	interface BandPersonalizationManager
	{
		// @required -(void)updateMeTileImage:(MSBImage *)image completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("updateMeTileImage:completionHandler:")]
		void SetMeTileImageAsync (BandImage image, Action<NSError> completionHandler);

		// @required -(void)meTileImageWithCompletionHandler:(void (^)(MSBImage *, NSError *))completionHandler;
		[Abstract]
		[Export ("meTileImageWithCompletionHandler:")]
		void GetMeTileImageAsync (Action<BandImage, NSError> completionHandler);

		// @required -(void)updateTheme:(MSBTheme *)theme completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("updateTheme:completionHandler:")]
		void SetThemeAsync (BandTheme theme, Action<NSError> completionHandler);

		// @required -(void)themeWithCompletionHandler:(void (^)(MSBTheme *, NSError *))completionHandler;
		[Abstract]
		[Export ("themeWithCompletionHandler:")]
		void GetThemeAsync (Action<BandTheme, NSError> completionHandler);
	}
}

namespace Microsoft.Band.Sensors
{
	// @interface MSBSensorData : NSObject
	[BaseType (typeof(NSObject), Name = "MSBSensorData")]
	interface BandSensorData
	{
	}

	// @interface MSBSensorAccelerometerData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorAccelerometerData")]
	interface BandSensorAccelerometerData
	{
		// @property (readonly, nonatomic) double x;
		[Export ("x")]
		double AccelerationX { get; }

		// @property (readonly, nonatomic) double y;
		[Export ("y")]
		double AccelerationY { get; }

		// @property (readonly, nonatomic) double z;
		[Export ("z")]
		double AccelerationZ { get; }
	}

	// @interface MSBSensorGyroscopeData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorGyroscopeData")]
	interface BandSensorGyroscopeData
	{
		// @property (readonly, nonatomic) double x;
		[Export ("x")]
		double AngularVelocityX { get; }

		// @property (readonly, nonatomic) double y;
		[Export ("y")]
		double AngularVelocityY { get; }

		// @property (readonly, nonatomic) double z;
		[Export ("z")]
		double AngularVelocityZ { get; }
	}

	// @interface MSBSensorHeartRateData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorHeartRateData")]
	interface BandSensorHeartRateData
	{
		// @property (readonly, nonatomic) NSUInteger heartRate;
		[Export ("heartRate")]
		nuint HeartRate { get; }

		// @property (readonly, nonatomic) MSBHeartRateQuality quality;
		[Export ("quality")]
		HeartRateQuality Quality { get; }
	}

	// @interface MSBSensorCaloriesData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorCaloriesData")]
	interface BandSensorCaloriesData
	{
		// @property (readonly, nonatomic) NSUInteger calories;
		[Export ("calories")]
		nuint Calories { get; }

		// @property (readonly, nonatomic) NSUInteger caloriesToday;
		[Export ("caloriesToday")]
		nuint CaloriesToday { get; }
	}

	// @interface MSBSensorDistanceData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorDistanceData")]
	interface BandSensorDistanceData
	{
		// @property (readonly, nonatomic) NSUInteger totalDistance;
		[Export ("totalDistance")]
		nuint TotalDistance { get; }

		// @property (readonly, nonatomic) NSUInteger distanceToday;
		[Export ("distanceToday")]
		nuint DistanceToday { get; }

		// @property (readonly, nonatomic) double speed;
		[Export ("speed")]
		double Speed { get; }

		// @property (readonly, nonatomic) double pace;
		[Export ("pace")]
		double Pace { get; }

		// @property (readonly, nonatomic) MSBPedometerMode pedometerMode;
		[Export ("motionType")]
		MotionType MotionType { get; }
	}

	// @interface MSBSensorPedometerData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorPedometerData")]
	interface BandSensorPedometerData
	{
		// @property (readonly, nonatomic) NSUInteger totalSteps;
		[Export ("totalSteps")]
		nuint TotalSteps { get; }

		// @property (readonly, nonatomic) NSUInteger stepsToday;
		[Export ("stepsToday")]
		nuint StepsToday { get; }

		// @property (readonly, nonatomic) int stepRate;
		[Obsolete]
		[Export ("stepRate")]
		int StepRate { get; }

		// @property (readonly, nonatomic) int movementRate;
		[Obsolete]
		[Export ("movementRate")]
		int MovementRate { get; }

		// @property (readonly, nonatomic) int totalMovements;
		[Obsolete]
		[Export ("totalMovements")]
		int TotalMovements { get; }

		// @property (readonly, nonatomic) int movementMode;
		[Obsolete]
		[Export ("movementMode")]
		int MovementMode { get; }
	}

	// @interface MSBSensorSkinTemperatureData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorSkinTemperatureData")]
	interface BandSensorSkinTemperatureData
	{
		// @property (readonly, nonatomic) double temperature;
		[Export ("temperature")]
		double Temperature { get; }
	}

	// @interface MSBSensorUVData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorUVData")]
	interface BandSensorUVData
	{
		// @property (readonly, nonatomic) MSBUVIndexLevel uvIndexLevel;
		[Export ("uvIndexLevel")]
		UVIndexLevel UVIndexLevel { get; }

		// @property (nonatomic, readonly) NSUInteger exposureToday;
		[Export ("exposureToday")]
		nuint ExposureToday { get; }
	}

	// @interface MSBSensorBandContactData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorBandContactData")]
	interface BandSensorContactData
	{
		// @property (readonly, nonatomic) MSBSensorBandContactState wornState;
		[Export ("wornState")]
		BandContactStatus WornState { get; }
	}

    // @interface MSBSensorGSRData : MSBSensorData
    [BaseType (typeof(BandSensorData), Name = "MSBSensorGSRData")]
    interface BandSensorGsrData
    {
        // @property (readonly, nonatomic) NSUInteger resistance;
        [Export ("resistance")]
        nuint Resistance { get; }
    }

    // @interface MSBSensorRRIntervalData : MSBSensorData
    [BaseType (typeof(BandSensorData), Name = "MSBSensorRRIntervalData")]
    interface BandSensorRRIntervalData
    {
        // @property (readonly, nonatomic) double interval;
        [Export ("interval")]
        double Interval { get; }
    }

    // @interface MSBSensorAmbientLightData : MSBSensorData
    [BaseType (typeof(BandSensorData), Name = "MSBSensorAmbientLightData")]
    interface BandSensorAmbientLightData
    {
        // @property (readonly, nonatomic) int brightness;
        [Export ("brightness")]
        int Brightness { get; }
    }

    // @interface MSBSensorBarometerData : MSBSensorData
    [BaseType (typeof(BandSensorData), Name = "MSBSensorBarometerData")]
    interface BandSensorBarometerData
    {
        // @property (readonly, nonatomic) double airPressure;
        [Export ("airPressure")]
        double AirPressure { get; }

        // @property (readonly, nonatomic) double temperature;
        [Export ("temperature")]
        double Temperature { get; }
    }

    // @interface MSBSensorAltimeterData : MSBSensorData
    [BaseType (typeof(BandSensorData), Name = "MSBSensorAltimeterData")]
    interface BandSensorAltimeterData
	{
		// @property (readonly, nonatomic) NSUInteger totalGain;
		[Export ("totalGain")]
		nuint TotalGain { get; }

		// @property (readonly, nonatomic) NSUInteger totalGainToday;
		[Export ("totalGainToday")]
		nuint TotalGainToday { get; }

        // @property (readonly, nonatomic) NSUInteger totalLoss;
        [Export ("totalLoss")]
        nuint TotalLoss { get; }

        // @property (readonly, nonatomic) NSUInteger steppingGain;
        [Export ("steppingGain")]
        nuint SteppingGain { get; }

        // @property (readonly, nonatomic) NSUInteger steppingLoss;
        [Export ("steppingLoss")]
        nuint SteppingLoss { get; }

        // @property (readonly, nonatomic) NSUInteger stepsAscended;
        [Export ("stepsAscended")]
        nuint StepsAscended { get; }

        // @property (readonly, nonatomic) NSUInteger stepsDescended;
        [Export ("stepsDescended")]
        nuint StepsDescended { get; }

        // @property (readonly, nonatomic) float rate;
        [Export ("rate")]
        float Rate { get; }

        // @property (readonly, nonatomic) NSUInteger flightsAscended;
        [Export ("flightsAscended")]
        nuint FlightsAscended { get; }

		// @property (readonly, nonatomic) NSUInteger flightsAscendedToday;
		[Export ("flightsAscendedToday")]
		nuint FlightsAscendedToday { get; }

        // @property (readonly, nonatomic) NSUInteger flightsDescended;
        [Export ("flightsDescended")]
        nuint FlightsDescended { get; }
    }

	interface IBandSensorManager
	{

	}

	// @protocol MSBSensorManagerProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBSensorManagerProtocol")]
	interface BandSensorManager
	{
		// @required -(BOOL)startAccelerometerUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorAccelData *, NSError *))handler;
		[Abstract]
		[Export ("startAccelerometerUpdatesToQueue:errorRef:withHandler:")]
		bool StartAccelerometerUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorAccelerometerData, NSError> handler);

		// @required -(BOOL)stopAccelerometerUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopAccelerometerUpdatesErrorRef:")]
		bool StopAccelerometerUpdates (out NSError pError);

		// @required -(BOOL)startGyroscopeUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorGyroData *, NSError *))handler;
		[Abstract]
		[Export ("startGyroscopeUpdatesToQueue:errorRef:withHandler:")]
		bool StartGyroscopeUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorGyroscopeData, NSError> handler);

		// @required -(BOOL)stopGyroscopeUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopGyroscopeUpdatesErrorRef:")]
		bool StopGyroscopeUpdates (out NSError pError);

		// @required -(BOOL)startHeartRateUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorHeartRateData *, NSError *))handler;
		[Abstract]
		[Export ("startHeartRateUpdatesToQueue:errorRef:withHandler:")]
		bool StartHeartRateUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorHeartRateData, NSError> handler);

		// @required -(BOOL)stopHeartRateUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopHeartRateUpdatesErrorRef:")]
		bool StopHeartRateUpdates (out NSError pError);

		// @required -(BOOL)startCaloriesUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorCaloriesData *, NSError *))handler;
		[Abstract]
		[Export ("startCaloriesUpdatesToQueue:errorRef:withHandler:")]
		bool StartCaloriesUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorCaloriesData, NSError> handler);

		// @required -(BOOL)stopCaloriesUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopCaloriesUpdatesErrorRef:")]
		bool StopCaloriesUpdates (out NSError pError);

		// @required -(BOOL)startDistanceUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorDistanceData *, NSError *))handler;
		[Abstract]
		[Export ("startDistanceUpdatesToQueue:errorRef:withHandler:")]
		bool StartDistanceUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorDistanceData, NSError> handler);

		// @required -(BOOL)stopDistanceUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopDistanceUpdatesErrorRef:")]
		bool StopDistanceUpdates (out NSError pError);

		// @required -(BOOL)startPedometerUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorPedometerData *, NSError *))handler;
		[Abstract]
		[Export ("startPedometerUpdatesToQueue:errorRef:withHandler:")]
		bool StartPedometerUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorPedometerData, NSError> handler);

		// @required -(BOOL)stopPedometerUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopPedometerUpdatesErrorRef:")]
		bool StopPedometerUpdates (out NSError pError);

		// @required -(BOOL)startSkinTempUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorSkinTempData *, NSError *))handler;
		[Abstract]
		[Export ("startSkinTempUpdatesToQueue:errorRef:withHandler:")]
		bool StartSkinTemperatureUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorSkinTemperatureData, NSError> handler);

		// @required -(BOOL)stopSkinTempUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopSkinTempUpdatesErrorRef:")]
		bool StopSkinTemperatureUpdates (out NSError pError);

		// @required -(BOOL)startUVUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorUVData *, NSError *))handler;
		[Abstract]
		[Export ("startUVUpdatesToQueue:errorRef:withHandler:")]
		bool StartUVUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorUVData, NSError> handler);

		// @required -(BOOL)stopUVUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopUVUpdatesErrorRef:")]
		bool StopUVUpdates (out NSError pError);

		// @required -(BOOL)startBandContactUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorBandContactData *, NSError *))handler;
		[Abstract]
		[Export ("startBandContactUpdatesToQueue:errorRef:withHandler:")]
		bool StartBandContactUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorContactData, NSError> handler);

		// @required -(BOOL)stopBandContactUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopBandContactUpdatesErrorRef:")]
		bool StopBandContactUpdates (out NSError pError);

        // @required -(BOOL)startGSRUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorGSRData *, NSError *))handler;
        [Abstract]
        [Export ("startGSRUpdatesToQueue:errorRef:withHandler:")]
        bool StartGsrUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorGsrData, NSError> handler);

        // @required -(BOOL)stopGSRUpdatesErrorRef:(NSError **)pError;
        [Abstract]
        [Export ("stopGSRUpdatesErrorRef:")]
        bool StopGsrUpdates (out NSError pError);

        // @required -(BOOL)startRRIntervalUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorRRIntervalData *, NSError *))handler;
        [Abstract]
        [Export ("startRRIntervalUpdatesToQueue:errorRef:withHandler:")]
        bool StartRRIntervalUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorRRIntervalData, NSError> handler);

        // @required -(BOOL)stopRRIntervalUpdatesErrorRef:(NSError **)pError;
        [Abstract]
        [Export ("stopRRIntervalUpdatesErrorRef:")]
        bool StopRRIntervalUpdates (out NSError pError);

        // @required -(BOOL)startAmbientLightUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorAmbientLightData *, NSError *))handler;
        [Abstract]
        [Export ("startAmbientLightUpdatesToQueue:errorRef:withHandler:")]
        bool StartAmbientLightUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorAmbientLightData, NSError> handler);

        // @required -(BOOL)stopAmbientLightUpdatesErrorRef:(NSError **)pError;
        [Abstract]
        [Export ("stopAmbientLightUpdatesErrorRef:")]
        bool StopAmbientLightUpdates (out NSError pError);

        // @required -(BOOL)startBarometerUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorBarometerData *, NSError *))handler;
        [Abstract]
        [Export ("startBarometerUpdatesToQueue:errorRef:withHandler:")]
        bool StartBarometerUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorBarometerData, NSError> handler);

        // @required -(BOOL)stopBarometerUpdatesErrorRef:(NSError **)pError;
        [Abstract]
        [Export ("stopBarometerUpdatesErrorRef:")]
        bool StopBarometerUpdates (out NSError pError);

        // @required -(BOOL)startAltimeterUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorAltimeterData *, NSError *))handler;
        [Abstract]
        [Export ("startAltimeterUpdatesToQueue:errorRef:withHandler:")]
        bool StartAltimeterUpdates ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<BandSensorAltimeterData, NSError> handler);

        // @required -(BOOL)stopAltimeterUpdatesErrorRef:(NSError **)pError;
        [Abstract]
        [Export ("stopAltimeterUpdatesErrorRef:")]
        bool StopAltimeterUpdates (out NSError pError);

		// @required -(MSBUserConsent)heartRateUserConsent;
		[Abstract]
		[Export ("heartRateUserConsent")]
		UserConsent HeartRateUserConsent { get; }

		// @required -(void)requestHRUserConsentWithCompletion:(void (^)(BOOL, NSError *))completion;
		[Abstract]
		[Export ("requestHRUserConsentWithCompletion:")]
		void RequestHeartRateUserConsentAsync (Action<bool, NSError> completion);
	}
}

namespace Microsoft.Band.Tiles
{
	// @interface MSBTile : NSObject
	[BaseType (typeof(NSObject), Name = "MSBTile")]
	interface BandTile
	{
		// @property (readonly, nonatomic) NSString * name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) NSUUID * tileId;
		[Export ("tileId")]
		NSUuid TileId { get; }

		// @property (readonly, nonatomic) MSBIcon * smallIcon;
		[Export ("smallIcon")]
		BandIcon SmallIcon { get; }

		// @property (readonly, nonatomic) MSBIcon * tileIcon;
		[Export ("tileIcon")]
		BandIcon TileIcon { get; }

		// @property (nonatomic, strong) MSBTheme * theme;
		[Export ("theme", ArgumentSemantic.Strong)]
		BandTheme Theme { get; set; }

		// @property (getter = isBadgingEnabled, assign, nonatomic) BOOL badgingEnabled;
		[Export ("badgingEnabled")]
		bool BadgingEnabled { [Bind ("isBadgingEnabled")] get; set; }

		// @property (nonatomic, assign, getter=isScreenTimeoutDisabled) BOOL screenTimeoutDisabled;
		[Export ("screenTimeoutDisabled")]
		bool ScreenTimeoutDisabled { [Bind ("isScreenTimeoutDisabled")] get; set; }

		// @property (readonly, nonatomic) NSMutableArray * pageIcons;
		[Internal]
		[Export ("pageIcons")]
		NSMutableArray PageIconsInternal { get; }

		// @property (readonly, nonatomic) NSMutableArray * pageLayouts;
		[Internal]
		[Export ("pageLayouts")]
		NSMutableArray PageLayoutsInternal { get; }

		// +(MSBTile *)tileWithId:(NSUUID *)tileId name:(NSString *)tileName tileIcon:(MSBIcon *)tileIcon smallIcon:(MSBIcon *)smallIcon error:(NSError **)pError;
		[Static]
		[Export ("tileWithId:name:tileIcon:smallIcon:error:")]
		BandTile Create (NSUuid tileId, string tileName, BandIcon tileIcon, BandIcon smallIcon, out NSError pError);

		// -(BOOL)setName:(NSString *)tileName error:(NSError **)pError;
		[Export ("setName:error:")]
		bool SetName (string tileName, out NSError pError);

		// -(BOOL)setTileIcon:(MSBIcon *)tileIcon error:(NSError **)pError;
		[Export ("setTileIcon:error:")]
		bool SetTileIcon (BandIcon tileIcon, out NSError pError);

		// -(BOOL)setSmallIcon:(MSBIcon *)smallIcon error:(NSError **)pError;
		[Export ("setSmallIcon:error:")]
		bool SetSmallIcon (BandIcon smallIcon, out NSError pError);
	}

	interface IBandTileManager
	{

	}

	// @protocol MSBTileManagerProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBTileManagerProtocol")]
	interface BandTileManager
	{
		// @required -(void)tilesWithCompletionHandler:(void (^)(NSArray *, NSError *))completionHandler;
		[Abstract]
		[Export ("tilesWithCompletionHandler:")]
		void GetTilesAsync (Action<BandTile[], NSError> completionHandler);

		// @required -(void)addTile:(MSBTile *)tile completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("addTile:completionHandler:")]
		void AddTileAsync (BandTile tile, Action<NSError> completionHandler);

		// @required -(void)removeTile:(MSBTile *)tile completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("removeTile:completionHandler:")]
		void RemoveTileAsync (BandTile tile, Action<NSError> completionHandler);

		// @required -(void)removeTileWithId:(NSUUID *)tileId completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("removeTileWithId:completionHandler:")]
		void RemoveTileAsync (NSUuid tileId, Action<NSError> completionHandler);

		// @required -(void)remainingTileCapacityWithCompletionHandler:(void (^)(NSUInteger, NSError *))completionHandler;
		[Abstract]
		[Export ("remainingTileCapacityWithCompletionHandler:")]
        void GetRemainingTileCapacityAsync (Action<nuint, NSError> completionHandler);

		// @required -(void)setPages:(NSArray *)pageData tileId:(NSUUID *)tileId completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("setPages:tileId:completionHandler:")]
		void SetPagesAsync (PageData[] pageData, NSUuid tileId, Action<NSError> completionHandler);

		// @required -(void)removePagesInTile:(NSUUID *)tileId completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("removePagesInTile:completionHandler:")]
		void RemovePagesAsync (NSUuid tileId, Action<NSError> completionHandler);
	}

	// @interface MSBIcon : NSObject
	[BaseType (typeof(NSObject), Name = "MSBIcon")]
	interface BandIcon
	{
		// @property (readonly, assign, nonatomic) CGSize size;
		[Export ("size", ArgumentSemantic.Assign)]
		CGSize Size { get; }

		// +(MSBIcon *)iconWithMSBImage:(MSBImage *)image error:(NSError **)pError;
		[Static]
		[Export ("iconWithMSBImage:error:")]
		BandIcon FromBandImage (BandImage image, out NSError pError);

#if __MAC__
		// +(MSBIcon *)iconWithNSImage:(NSImage *)image error:(NSError **)pError;
		[Static]
		[Export ("iconWithNSImage:error:")]
		BandIcon FromNSImage (NSImage image, out NSError pError);

		// -(NSImage *)NSImage;
		[Export ("NSImage")]
		NSImage NSImage { get; }
#else
		// +(MSBIcon *)iconWithUIImage:(UIImage *)image error:(NSError **)pError;
		[Static]
		[Export ("iconWithUIImage:error:")]
		BandIcon FromUIImage (UIImage image, out NSError pError);

		// -(UIImage *)UIImage;
		[Export ("UIImage")]
		UIImage UIImage { get; }
#endif
	}

	// @interface MSBTileEvent : NSObject
	[BaseType (typeof(NSObject), Name = "MSBTileEvent")]
	interface BandTileEvent
	{
		// @property (readonly, nonatomic) NSString * tileName;
		[Export ("tileName")]
		string TileName { get; }

		// @property (readonly, nonatomic) NSUUID * tileId;
		[Export ("tileId")]
		NSUuid TileId { get; }

		// @property (readonly, nonatomic) MSBTileEventType eventType;
		[Export ("eventType")]
		BandTileEventType EventType { get; }
	}

	// @interface MSBTileButtonEvent : MSBTileEvent
	[BaseType (typeof(BandTileEvent), Name = "MSBTileButtonEvent")]
	interface BandTileButtonEvent
	{
		// @property (readonly, nonatomic) NSUUID * pageId;
		[Export ("pageId")]
		NSUuid PageId { get; }

		// @property (readonly, nonatomic) MSBPageElementIdentifier buttonId;
		[Export ("buttonId")]
		ushort ElementId { get; }
	}

	interface IBandClientTileDelegate
	{

	}

	// @protocol MSBClientTileDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBClientTileDelegate")]
	interface BandClientTileDelegate
	{
		// @required -(void)client:(MSBClient *)client tileDidOpen:(MSBTileEvent *)event;
		[Abstract]
		[Export ("client:tileDidOpen:"), EventArgs ("TileOpened")]
		void TileOpened (BandClient client, BandTileEvent tileEvent);

		// @required -(void)client:(MSBClient *)client tileDidClose:(MSBTileEvent *)event;
		[Abstract]
		[Export ("client:tileDidClose:"), EventArgs ("TileClosed")]
		void TileClosed (BandClient client, BandTileEvent tileEvent);

		// @optional -(void)client:(MSBClient *)client buttonDidPress:(MSBTileButtonEvent *)event;
		[Export ("client:buttonDidPress:"), EventArgs ("TileButtonPressed")]
		void ButtonPressed (BandClient client, BandTileButtonEvent tileButtonEvent);
	}
}

namespace Microsoft.Band
{
	// @interface MSBImage : NSObject
	[BaseType (typeof(NSObject), Name = "MSBImage")]
	interface BandImage
	{
		// @property (readonly, nonatomic) CGSize size;
		[Export ("size")]
		CGSize Size { get; }

		// -(instancetype)initWithContentsOfFile:(NSString *)path;
		[Export ("initWithContentsOfFile:")]
		IntPtr Constructor (string path);

#if __MAC__
		// -(instancetype)initWithNSImage:(NSImage *)image;
		[Export ("initWithNSImage:")]
		IntPtr Constructor (NSImage image);

		// -(NSImage *)NSImage;
		[Export ("NSImage")]
		NSImage NSImage { get; }
#else
		// -(instancetype)initWithUIImage:(UIImage *)image;
		[Export ("initWithUIImage:")]
		IntPtr Constructor (UIImage image);

		// -(UIImage *)UIImage;
		[Export ("UIImage")]
		UIImage UIImage { get; }
#endif
	}

	// @interface MSBColor : NSObject
	[BaseType (typeof(NSObject), Name = "MSBColor")]
	interface BandColor : INSCopying
	{
#if __MAC__
		// +(id)colorWithNSColor:(NSColor *)color error:(NSError **)pError;
		[Static]
		[Export ("colorWithNSColor:error:")]
		BandColor FromNSColor (NSColor color, out NSError pError);

		// -(NSColor *)NSColor;
		[Export ("NSColor")]
		NSColor NSColor { get; }
#else
		// +(id)colorWithUIColor:(UIColor *)color error:(NSError **)pError;
		[Static]
		[Export ("colorWithUIColor:error:")]
		BandColor FromUIColor (UIColor color, out NSError pError);

		// -(UIColor *)UIColor;
		[Export ("UIColor")]
		UIColor UIColor { get; }
#endif
		// +(MSBColor *)colorWithRed:(NSUInteger)red green:(NSUInteger)green blue:(NSUInteger)blue;
		[Static]
		[Export ("colorWithRed:green:blue:")]
		BandColor FromRgb (nuint red, nuint green, nuint blue);

		// -(BOOL)isEqualToColor:(MSBColor *)color;
		[Export ("isEqualToColor:")]
		bool IsEqualToColor (BandColor color);
	}

	// @interface MSBTheme : NSObject
	[BaseType (typeof(NSObject), Name = "MSBTheme")]
	interface BandTheme : INSCopying
	{
		// @property (nonatomic, strong) MSBColor * baseColor;
        [Export ("baseColor", ArgumentSemantic.Strong)]
		BandColor Base { get; set; }

		// @property (nonatomic, strong) MSBColor * highLightColor;
        [Export ("highlightColor", ArgumentSemantic.Strong)]
		BandColor Highlight { get; set; }

		// @property (nonatomic, strong) MSBColor * lowLightColor;
        [Export ("lowlightColor", ArgumentSemantic.Strong)]
		BandColor Lowlight { get; set; }

		// @property (nonatomic, strong) MSBColor * secondaryTextColor;
        [Export ("secondaryTextColor", ArgumentSemantic.Strong)]
		BandColor SecondaryText { get; set; }

		// @property (nonatomic, strong) MSBColor * highContrastColor;
        [Export ("highContrastColor", ArgumentSemantic.Strong)]
		BandColor HighContrast { get; set; }

		// @property (nonatomic, strong) MSBColor * mutedColor;
        [Export ("mutedColor", ArgumentSemantic.Strong)]
		BandColor Muted { get; set; }

		// +(MSBTheme *)themeWithBaseColor:(MSBColor *)baseColor highlightColor:(MSBColor *)highlightColor lowlightColor:(MSBColor *)lowlightColor secondaryTextColor:(MSBColor *)secondaryTextColor highContrastColor:(MSBColor *)highContrastColor mutedColor:(MSBColor *)mutedColor;
		[Static]
		[Export ("themeWithBaseColor:highlightColor:lowlightColor:secondaryTextColor:highContrastColor:mutedColor:")]
		BandTheme Create (BandColor baseColor, BandColor highLightColor, BandColor lowLightColor, BandColor secondaryTextColor, BandColor highContrastColor, BandColor mutedColor);
	}
}
	