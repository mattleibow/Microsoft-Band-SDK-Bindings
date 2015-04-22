using System;

using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Pages;
using Microsoft.Band.Personalization;
using Microsoft.Band.Sensors;
using Microsoft.Band.Tiles;

namespace Microsoft.Band
{
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
		bool IsPowerOn { get; }

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
	[BaseType (typeof(NSObject), Name = "MSBClient")]
	interface BandClient
	{
		// @property (readonly) NSString * name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly) NSUUID * connectionIdentifier;
		[Export ("connectionIdentifier")]
		NSUuid ConnectionIdentifier { get; }

		// @property (readonly) BOOL isDeviceConnected;
		[Export ("isDeviceConnected")]
		bool IsDeviceConnected { get; }

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
		[Export ("firmwareVersionWithCompletionHandler:"), Async]
		void GetFirmwareVersionAsync (Action<NSString, NSError> completionHandler);

		// -(void)hardwareVersionWithCompletionHandler:(void (^)(NSString *, NSError *))completionHandler;
		[Export ("hardwareVersionWithCompletionHandler:"), Async]
		void GetHardwareVersionAsyc (Action<NSString, NSError> completionHandler);
	}
}

namespace Microsoft.Band.Pages
{
	// @interface MSBPageElement : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageElement")]
	interface BandPageElement
	{
		// @property (assign, nonatomic) MSBPageElementIdentifier elementId;
		[Export ("elementId")]
		ushort ElementId { get; set; }

		// @property (nonatomic, strong) MSBRect * rect;
		[Export ("rect", ArgumentSemantic.Retain)]
		BandRect Rect { get; [NullAllowed] set; }

		// @property (nonatomic, strong) MSBMargins * margins;
		[Export ("margins", ArgumentSemantic.Retain)]
		BandMargins Margins { get; [NullAllowed] set; }

		// @property (nonatomic, strong) MSBColor * color;
		[Export ("color", ArgumentSemantic.Retain)]
		BandColor Color { get; [NullAllowed] set; }

		// @property (assign, nonatomic) MSBPageElementHorizontalAlignment horizontalAlignment;
		[Export ("horizontalAlignment", ArgumentSemantic.UnsafeUnretained)]
		BandPageElementHorizontalAlignment HorizontalAlignment { get; set; }

		// @property (assign, nonatomic) MSBPageElementVerticalAlignment verticalAlignment;
		[Export ("verticalAlignment", ArgumentSemantic.UnsafeUnretained)]
		BandPageElementVerticalAlignment VerticalAlignment { get; set; }

		// @property (assign, nonatomic) MSBPageElementVisibility visibility;
		[Export ("visibility", ArgumentSemantic.UnsafeUnretained)]
		BandPageElementVisibility Visibility { get; set; }
	}

	// @interface MSBPageElementData : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageElementData")]
	interface BandPageElementData
	{
		// @property (readonly, nonatomic) MSBPageElementIdentifier elementId;
		[Export ("elementId")]
		ushort ElementId { get; }
	}

	// @interface MSBPageTextData : MSBPageElementData
	[BaseType (typeof(BandPageElementData), Name = "MSBPageTextData")]
	interface BandPageTextData
	{
		// @property (readonly, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; }

		// +(MSBPageTextData *)pageTextDataWithElementId:(MSBPageElementIdentifier)elementId text:(NSString *)text error:(NSError **)pError;
		[Static]
		[Export ("pageTextDataWithElementId:text:error:")]
		BandPageTextData Create (ushort elementId, string text, out NSError pError);
	}

	// @interface MSBPageWrappedTextData : MSBPageElementData
	[BaseType (typeof(BandPageElementData), Name = "MSBPageWrappedTextData")]
	interface BandPageWrappedTextData
	{
		// @property (readonly, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; }

		// +(MSBPageWrappedTextData *)pageWrappedTextDataWithElementId:(MSBPageElementIdentifier)elementId text:(NSString *)text error:(NSError **)pError;
		[Static]
		[Export ("pageWrappedTextDataWithElementId:text:error:")]
		BandPageWrappedTextData Create (ushort elementId, string text, out NSError pError);
	}

	// @interface MSBPageIconData : MSBPageElementData
	[BaseType (typeof(BandPageElementData), Name = "MSBPageIconData")]
	interface BandPageIconData
	{
		// @property (readonly, nonatomic) NSUInteger iconIndex;
		[Export ("iconIndex")]
		nuint IconIndex { get; }

		// +(MSBPageIconData *)pageIconDataWithElementId:(MSBPageElementIdentifier)elementId iconIndex:(NSUInteger)iconIndex error:(NSError **)pError;
		[Static]
		[Export ("pageIconDataWithElementId:iconIndex:error:")]
		BandPageIconData Create (ushort elementId, nuint iconIndex, out NSError pError);
	}

	// @interface MSBPageData : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageData")]
	interface BandPageData
	{
		// @property (readonly, nonatomic) NSUUID * pageId;
		[Export ("pageId")]
		NSUuid PageId { get; }

		// @property (readonly, nonatomic) NSUInteger pageTemplateIndex;
		[Export ("pageTemplateIndex")]
		nuint PageTemplateIndex { get; }

		// @property (readonly, nonatomic) NSArray * values;
		[Export ("values")]
		NSObject[] Values { get; }

		// +(MSBPageData *)pageDataWithId:(NSUUID *)pageId templateIndex:(NSUInteger)templateIndex value:(NSArray *)values;
		[Static]
		[Export ("pageDataWithId:templateIndex:value:")]
		BandPageData Create (NSUuid pageId, nuint templateIndex, NSObject[] values);
	}

	// @interface MSBPageBarcodePDF417Data : MSBPageElementData
	[BaseType (typeof(BandPageElementData), Name = "MSBPageBarcodePDF417Data")]
	interface BandPageBarcodePDF417Data
	{
		// @property (readonly, nonatomic) NSString * value;
		[Export ("value")]
		string Value { get; }

		// +(MSBPageBarcodePDF417Data *)pageBarcodePDF417DataWithElementId:(MSBPageElementIdentifier)elementId value:(NSString *)value error:(NSError **)pError;
		[Static]
		[Export ("pageBarcodePDF417DataWithElementId:value:error:")]
		BandPageBarcodePDF417Data Create (ushort elementId, string value, out NSError pError);
	}

	// @interface MSBPageBarcodeCode39Data : MSBPageElementData
	[BaseType (typeof(BandPageElementData), Name = "MSBPageBarcodeCode39Data")]
	interface BandPageBarcodeCode39Data
	{
		// @property (readonly, nonatomic) NSString * value;
		[Export ("value")]
		string Value { get; }

		// +(MSBPageBarcodeCode39Data *)pageBarcodeCode39DataWithElementId:(MSBPageElementIdentifier)elementId value:(NSString *)value error:(NSError **)pError;
		[Static]
		[Export ("pageBarcodeCode39DataWithElementId:value:error:")]
		BandPageBarcodeCode39Data Create (ushort elementId, string value, out NSError pError);
	}

	// @interface MSBPagePanel : MSBPageElement
	[BaseType (typeof(BandPageElement), Name = "MSBPagePanel")]
	interface BandPagePanel
	{
		// @property (nonatomic, strong) NSMutableArray * children;
		[Internal]
		[Export ("children", ArgumentSemantic.Retain)]
		NSMutableArray ChildrenInternal { get; set; }
	}

	// @interface MSBTextElement : MSBPageElement
	[BaseType (typeof(BandPageElement), Name = "MSBTextElement")]
	interface BandTextElement
	{
		// @property (assign, nonatomic) MSBTextBlockLayoutElementWidth width;
		[Export ("width", ArgumentSemantic.UnsafeUnretained)]
		BandTextBlockLayoutElementWidth Width { get; set; }
	}

	// @interface MSBRect : NSObject
	[BaseType (typeof(NSObject), Name = "MSBRect")]
	interface BandRect
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

		// +(MSBRect *)rectwithX:(UInt16)x y:(UInt16)y width:(UInt16)width height:(UInt16)height;
		[Static]
		[Export ("rectwithX:y:width:height:")]
		BandRect Create (ushort x, ushort y, ushort width, ushort height);
	}

	// @interface MSBMargins : NSObject
	[BaseType (typeof(NSObject), Name = "MSBMargins")]
	interface BandMargins
	{
		// @property (assign, nonatomic) UInt16 left;
		[Export ("left")]
		ushort Left { get; set; }

		// @property (assign, nonatomic) UInt16 top;
		[Export ("top")]
		ushort Top { get; set; }

		// @property (assign, nonatomic) UInt16 right;
		[Export ("right")]
		ushort Right { get; set; }

		// @property (assign, nonatomic) UInt16 bottom;
		[Export ("bottom")]
		ushort Bottom { get; set; }

		// +(MSBMargins *)marginsWithLeft:(UInt16)left top:(UInt16)top right:(UInt16)right bottom:(UInt16)bottom;
		[Static]
		[Export ("marginsWithLeft:top:right:bottom:")]
		BandMargins Create (ushort left, ushort top, ushort right, ushort bottom);
	}

	// @interface MSBFilledQuad : MSBPagePanel
	[BaseType (typeof(BandPagePanel), Name = "MSBFilledQuad")]
	interface BandFilledQuad
	{
		// -(id)initWithRect:(MSBRect *)rect;
		[Export ("initWithRect:")]
		IntPtr Constructor (BandRect rect);
	}

	// @interface MSBFlowList : MSBPagePanel
	[BaseType (typeof(BandPagePanel), Name = "MSBFlowList")]
	interface BandFlowList
	{
		// @property (assign, nonatomic) MSBFlowListOrientation orientation;
		[Export ("orientation", ArgumentSemantic.UnsafeUnretained)]
		BandFlowListOrientation Orientation { get; set; }

		// -(id)initWithRect:(MSBRect *)rect orientation:(MSBFlowListOrientation)orientation;
		[Export ("initWithRect:orientation:")]
		IntPtr Constructor (BandRect rect, BandFlowListOrientation orientation);
	}

	// @interface MSBGlyph : MSBPageElement
	[BaseType (typeof(BandPageElement), Name = "MSBGlyph")]
	interface BandGlyph
	{
		// -(id)initWithRect:(MSBRect *)rect;
		[Export ("initWithRect:")]
		IntPtr Constructor (BandRect rect);
	}

	// @interface MSBPageLayout : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageLayout")]
	interface BandPageLayout
	{
		// @property (nonatomic, strong) MSBPagePanel * root;
		[Export ("root", ArgumentSemantic.Retain)]
		BandPagePanel Root { get; set; }
	}

	// @interface MSBScrollFlowList : MSBFlowList
	[BaseType (typeof(BandFlowList), Name = "MSBScrollFlowList")]
	interface BandScrollFlowList
	{
	}

	// @interface MSBTextBlock : MSBTextElement
	[BaseType (typeof(BandTextElement), Name = "MSBTextBlock")]
	interface BandTextBlock
	{
		// @property (assign, nonatomic) MSBTextBlockFont font;
		[Export ("font", ArgumentSemantic.UnsafeUnretained)]
		BandTextBlockFont Font { get; set; }

		// @property (assign, nonatomic) MSBTextBlockBaseline baseline;
		[Export ("baseline")]
		ushort Baseline { get; set; }

		// @property (assign, nonatomic) MSBTextBlockBaselineAlignment baselineAlignment;
		[Export ("baselineAlignment", ArgumentSemantic.UnsafeUnretained)]
		BandTextBlockBaselineAlignment BaselineAlignment { get; set; }

		// -(id)initWithRect:(MSBRect *)rect font:(MSBTextBlockFont)font baseline:(MSBTextBlockBaseline)baseline;
		[Export ("initWithRect:font:baseline:")]
		IntPtr Constructor (BandRect rect, BandTextBlockFont font, ushort baseline);
	}

	// @interface MSBWrappedTextBlock : MSBTextElement
	[BaseType (typeof(BandTextElement), Name = "MSBWrappedTextBlock")]
	interface BandWrappedTextBlock
	{
		// @property (assign, nonatomic) MSBWrappedTextBlockFont font;
		[Export ("font", ArgumentSemantic.UnsafeUnretained)]
		BandWrappedTextBlockFont Font { get; set; }

		// -(id)initWithRect:(MSBRect *)rect font:(MSBWrappedTextBlockFont)font;
		[Export ("initWithRect:font:")]
		IntPtr Constructor (BandRect rect, BandWrappedTextBlockFont font);
	}

	// @interface MSBBarcode : MSBPageElement
	[BaseType (typeof(BandPageElement), Name = "MSBBarcode")]
	interface BandBarcode
	{
		// @property (assign, nonatomic) MSBBarcodeType barcodeType;
		[Export ("barcodeType", ArgumentSemantic.UnsafeUnretained)]
		BandBarcodeType BarcodeType { get; set; }

		// -(id)initWithRect:(MSBRect *)rect barcodeType:(MSBBarcodeType)type;
		[Export ("initWithRect:barcodeType:")]
		IntPtr Constructor (BandRect rect, BandBarcodeType type);
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
		[iOS (7, 0)]
		[Abstract]
		[Export ("registerNotificationWithTileID:completionHandler:")]
		void RegisterPushNotificationAsync (NSUuid tileID, Action<NSError> completionHandler);

		// @required -(void)registerNotificationWithCompletionHandler:(void (^)(NSError *))completionHandler __attribute__((availability(ios, introduced=7.0)));
		[iOS (7, 0)]
		[Abstract]
		[Export ("registerNotificationWithCompletionHandler:")]
		void RegisterPushNotificationAsync (Action<NSError> completionHandler);

		// @required -(void)unregisterNotificationWithCompletionHandler:(void (^)(NSError *))completionHandler __attribute__((availability(ios, introduced=7.0)));
		[iOS (7, 0)]
		[Abstract]
		[Export ("unregisterNotificationWithCompletionHandler:")]
		void UnregisterPushNotificationAsync (Action<NSError> completionHandler);
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

	// @interface MSBSensorAccelData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorAccelData")]
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

	// @interface MSBSensorGyroData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorGyroData")]
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

	// @interface MSBSensorAccelGyroData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorAccelGyroData")]
	interface BandSensorAccelerometerGyroscopeData
	{
		// @property (readonly, nonatomic) MSBSensorAccelData * accel;
		[Export ("accel")]
		BandSensorAccelerometerData Accelerometer { get; }

		// @property (readonly, nonatomic) MSBSensorGyroData * gyro;
		[Export ("gyro")]
		BandSensorGyroscopeData Gyroscope { get; }
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
	}

	// @interface MSBSensorDistanceData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorDistanceData")]
	interface BandSensorDistanceData
	{
		// @property (readonly, nonatomic) NSUInteger totalDistance;
		[Export ("totalDistance")]
		nuint TotalDistance { get; }

		// @property (readonly, nonatomic) double speed;
		[Export ("speed")]
		double Speed { get; }

		// @property (readonly, nonatomic) double pace;
		[Export ("pace")]
		double Pace { get; }

		// @property (readonly, nonatomic) MSBPedometerMode pedometerMode;
		[Export ("pedometerMode")]
		PedometerMode PedometerMode { get; }
	}

	// @interface MSBSensorPedometerData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorPedometerData")]
	interface BandSensorPedometerData
	{
		// @property (readonly, nonatomic) int totalSteps;
		[Export ("totalSteps")]
		int TotalSteps { get; }

		// @property (readonly, nonatomic) int stepRate;
		[Export ("stepRate")]
		int StepRate { get; }

		// @property (readonly, nonatomic) int movementRate;
		[Export ("movementRate")]
		int MovementRate { get; }

		// @property (readonly, nonatomic) int totalMovements;
		[Export ("totalMovements")]
		int TotalMovements { get; }

		// @property (readonly, nonatomic) int movementMode;
		[Export ("movementMode")]
		int MovementMode { get; }
	}

	// @interface MSBSensorSkinTempData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorSkinTempData")]
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
	}

	// @interface MSBSensorBandContactData : MSBSensorData
	[BaseType (typeof(BandSensorData), Name = "MSBSensorBandContactData")]
	interface BandSensorContactData
	{
		// @property (readonly, nonatomic) MSBSensorBandContactState wornState;
		[Export ("wornState")]
		BandContactStatus WornState { get; }
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

		// @required -(BOOL)startHearRateUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorHeartRateData *, NSError *))handler;
		[Abstract]
		[Export ("startHearRateUpdatesToQueue:errorRef:withHandler:")]
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
		[Export ("theme", ArgumentSemantic.Retain)]
		BandTheme Theme { get; set; }

		// @property (getter = isBadgingEnabled, assign, nonatomic) BOOL badgingEnabled;
		[Export ("badgingEnabled")]
		bool BadgingEnabled { [Bind ("isBadgingEnabled")] get; set; }

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
		void SetPagesAsync (BandPageData[] pageData, NSUuid tileId, Action<NSError> completionHandler);

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
		[Export ("size", ArgumentSemantic.UnsafeUnretained)]
		CGSize Size { get; }

		// +(MSBIcon *)iconWithMSBImage:(MSBImage *)image error:(NSError **)pError;
		[Static]
		[Export ("iconWithMSBImage:error:")]
		BandIcon FromImage (BandImage image, out NSError pError);

		// +(MSBIcon *)iconWithUIImage:(UIImage *)image error:(NSError **)pError;
		[Static]
		[Export ("iconWithUIImage:error:")]
		BandIcon FromUIImage (UIImage image, out NSError pError);

		// -(UIImage *)UIImage;
		[Export ("UIImage")]
		UIImage UIImage { get; }
	}
}

namespace Microsoft.Band.Personalization
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

		// -(instancetype)initWithUIImage:(UIImage *)image;
		[Export ("initWithUIImage:")]
		IntPtr Constructor (UIImage image);

		// -(UIImage *)UIImage;
		[Export ("UIImage")]
		UIImage UIImage { get; }
	}

	// @interface MSBColor : NSObject
	[BaseType (typeof(NSObject), Name = "MSBColor")]
	interface BandColor
	{
		// +(id)colorWithUIColor:(UIColor *)color error:(NSError **)pError;
		[Static]
		[Export ("colorWithUIColor:error:")]
		BandColor FromUIColor (UIColor color, out NSError pError);

		// -(UIColor *)UIColor;
		[Export ("UIColor")]
		UIColor UIColor { get; }

		// +(MSBColor *)colorWithRed:(NSUInteger)red green:(NSUInteger)green blue:(NSUInteger)blue;
		[Static]
		[Export ("colorWithRed:green:blue:")]
		BandColor FromRgb (nuint red, nuint green, nuint blue);
	}

	// @interface MSBTheme : NSObject
	[BaseType (typeof(NSObject), Name = "MSBTheme")]
	interface BandTheme
	{
		// @property (nonatomic, strong) MSBColor * baseColor;
		[Export ("baseColor", ArgumentSemantic.Retain)]
		BandColor Base { get; set; }

		// @property (nonatomic, strong) MSBColor * highLightColor;
		[Export ("highLightColor", ArgumentSemantic.Retain)]
		BandColor Highlight { get; set; }

		// @property (nonatomic, strong) MSBColor * lowLightColor;
		[Export ("lowLightColor", ArgumentSemantic.Retain)]
		BandColor Lowlight { get; set; }

		// @property (nonatomic, strong) MSBColor * secondaryTextColor;
		[Export ("secondaryTextColor", ArgumentSemantic.Retain)]
		BandColor SecondaryText { get; set; }

		// @property (nonatomic, strong) MSBColor * highContrastColor;
		[Export ("highContrastColor", ArgumentSemantic.Retain)]
		BandColor HighContrast { get; set; }

		// @property (nonatomic, strong) MSBColor * mutedColor;
		[Export ("mutedColor", ArgumentSemantic.Retain)]
		BandColor Muted { get; set; }

		// +(MSBTheme *)themeWithBaseColor:(MSBColor *)baseColor highLightColor:(MSBColor *)highLightColor lowLightColor:(MSBColor *)lowLightColor secondaryTextColor:(MSBColor *)secondaryTextColor highContrastColor:(MSBColor *)highContrastColor mutedColor:(MSBColor *)mutedColor;
		[Static]
		[Export ("themeWithBaseColor:highLightColor:lowLightColor:secondaryTextColor:highContrastColor:mutedColor:")]
		BandTheme Create (BandColor baseColor, BandColor highLightColor, BandColor lowLightColor, BandColor secondaryTextColor, BandColor highContrastColor, BandColor mutedColor);
	}
}
	