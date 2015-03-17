using System;
using System.Drawing;

using ObjCRuntime;
using Foundation;
using UIKit;
using CoreGraphics;

namespace Microsoft.Band
{
	interface IClientManagerDelegate
	{

	}

	// @protocol MSBClientManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBClientManagerDelegate")]
	interface ClientManagerDelegate
	{
		// @required -(void)clientManager:(MSBClientManager *)clientManager clientDidConnect:(MSBClient *)client;
		[Abstract]
		[Export ("clientManager:clientDidConnect:"), EventArgs ("ClientManagerConnected")]
		void Connected (ClientManager clientManager, Client client);

		// @required -(void)clientManager:(MSBClientManager *)clientManager clientDidDisconnect:(MSBClient *)client;
		[Abstract]
		[Export ("clientManager:clientDidDisconnect:"), EventArgs ("ClientManagerDisconnected")]
		void Disconnected (ClientManager clientManager, Client client);

		// @required -(void)clientManager:(MSBClientManager *)clientManager client:(MSBClient *)client didFailToConnectWithError:(NSError *)error;
		[Abstract]
		[Export ("clientManager:client:didFailToConnectWithError:"), EventArgs ("ClientManagerFailedToConnect")]
		void FailedToConnect (ClientManager clientManager, Client client, NSError error);
	}

	// @interface MSBClientManager : NSObject
	[BaseType (typeof(NSObject), Name = "MSBClientManager", Delegates = new string [] { "Delegate" }, 
		Events = new Type [] { typeof(ClientManagerDelegate) })]
	interface ClientManager
	{
		// @property (nonatomic, weak) id<MSBClientManagerDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IClientManagerDelegate Delegate { get; set; }

		// @property (readonly) BOOL isPowerOn;
		[Export ("isPowerOn")]
		bool IsPowerOn { get; }

		// +(MSBClientManager *)sharedManager;
		[Static]
		[Export ("sharedManager")]
		ClientManager SharedManager { get; }

		// -(MSBClient *)clientWithConnectionIdentifier:(NSUUID *)identifer;
		[Export ("clientWithConnectionIdentifier:")]
		Client ClientWithConnectionIdentifier (NSUuid identifer);

		// -(NSArray *)attachedClients;
		[Export ("attachedClients")]
		Client[] AttachedClients { get; }

		// -(void)connectClient:(MSBClient *)client;
		[Export ("connectClient:")]
		void ConnectClient (Client client);

		// -(void)cancelClientConnection:(MSBClient *)client;
		[Export ("cancelClientConnection:")]
		void CancelClientConnection (Client client);
	}

	// @interface MSBClient : NSObject
	[BaseType (typeof(NSObject), Name = "MSBClient")]
	interface Client
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
		ITileManagerProtocol TileManager { get; }

		// @property (readonly, nonatomic) id<MSBPersonalizationManagerProtocol> personalizationManager;
		[Export ("personalizationManager")]
		IPersonalizationManagerProtocol PersonalizationManager { get; }

		// @property (readonly, nonatomic) id<MSBNotificationManagerProtocol> notificationManager;
		[Export ("notificationManager")]
		INotificationManagerProtocol NotificationManager { get; }

		// @property (readonly, nonatomic) id<MSBSensorManagerProtocol> sensorManager;
		[Export ("sensorManager")]
		ISensorManagerProtocol SensorManager { get; }

		// -(void)firmwareVersionWithCompletionHandler:(void (^)(NSString *, NSError *))completionHandler;
		[Export ("firmwareVersionWithCompletionHandler:")]
		void FirmwareVersionWithCompletionHandler (Action<NSString, NSError> completionHandler);

		// -(void)hardwareVersionWithCompletionHandler:(void (^)(NSString *, NSError *))completionHandler;
		[Export ("hardwareVersionWithCompletionHandler:")]
		void HardwareVersionWithCompletionHandler (Action<NSString, NSError> completionHandler);
	}

	// @interface MSBTile : NSObject
	[BaseType (typeof(NSObject), Name = "MSBTile")]
	interface Tile
	{
		// @property (readonly, nonatomic) NSString * name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) NSUUID * tileId;
		[Export ("tileId")]
		NSUuid TileId { get; }

		// @property (readonly, nonatomic) MSBIcon * smallIcon;
		[Export ("smallIcon")]
		Icon SmallIcon { get; }

		// @property (readonly, nonatomic) MSBIcon * tileIcon;
		[Export ("tileIcon")]
		Icon TileIcon { get; }

		// @property (nonatomic, strong) MSBTheme * theme;
		[Export ("theme", ArgumentSemantic.Retain)]
		Theme Theme { get; set; }

		// @property (getter = isBadgingEnabled, assign, nonatomic) BOOL badgingEnabled;
		[Export ("badgingEnabled")]
		bool BadgingEnabled { [Bind ("isBadgingEnabled")] get; set; }

		// @property (readonly, nonatomic) NSMutableArray * pageIcons;
		[Export ("pageIcons")]
		NSMutableArray PageIcons { get; }

		// @property (readonly, nonatomic) NSMutableArray * pageLayouts;
		[Export ("pageLayouts")]
		NSMutableArray PageLayouts { get; }

		// +(MSBTile *)tileWithId:(NSUUID *)tileId name:(NSString *)tileName tileIcon:(MSBIcon *)tileIcon smallIcon:(MSBIcon *)smallIcon error:(NSError **)pError;
		[Static]
		[Export ("tileWithId:name:tileIcon:smallIcon:error:")]
		Tile TileWithId (NSUuid tileId, string tileName, Icon tileIcon, Icon smallIcon, out NSError pError);

		// -(BOOL)setName:(NSString *)tileName error:(NSError **)pError;
		[Export ("setName:error:")]
		bool SetName (string tileName, out NSError pError);

		// -(BOOL)setTileIcon:(MSBIcon *)tileIcon error:(NSError **)pError;
		[Export ("setTileIcon:error:")]
		bool SetTileIcon (Icon tileIcon, out NSError pError);

		// -(BOOL)setSmallIcon:(MSBIcon *)smallIcon error:(NSError **)pError;
		[Export ("setSmallIcon:error:")]
		bool SetSmallIcon (Icon smallIcon, out NSError pError);
	}

	interface ITileManagerProtocol
	{

	}

	// @protocol MSBTileManagerProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBTileManagerProtocol")]
	interface TileManagerProtocol
	{
		// @required -(void)tilesWithCompletionHandler:(void (^)(NSArray *, NSError *))completionHandler;
		[Abstract]
		[Export ("tilesWithCompletionHandler:")]
		void TilesWithCompletionHandler (Action<NSArray, NSError> completionHandler);

		// @required -(void)addTile:(MSBTile *)tile completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("addTile:completionHandler:")]
		void AddTile (Tile tile, Action<NSError> completionHandler);

		// @required -(void)removeTile:(MSBTile *)tile completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("removeTile:completionHandler:")]
		void RemoveTile (Tile tile, Action<NSError> completionHandler);

		// @required -(void)removeTileWithId:(NSUUID *)tileId completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("removeTileWithId:completionHandler:")]
		void RemoveTileWithId (NSUuid tileId, Action<NSError> completionHandler);

		// @required -(void)remainingTileCapacityWithCompletionHandler:(void (^)(NSUInteger, NSError *))completionHandler;
		[Abstract]
		[Export ("remainingTileCapacityWithCompletionHandler:")]
		void RemainingTileCapacityWithCompletionHandler (Action<nuint, NSError> completionHandler);

		// @required -(void)setPages:(NSArray *)pageData tileId:(NSUUID *)tileId completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("setPages:tileId:completionHandler:")]
		void SetPages (NSObject[] pageData, NSUuid tileId, Action<NSError> completionHandler);

		// @required -(void)removePagesInTile:(NSUUID *)tileId completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("removePagesInTile:completionHandler:")]
		void RemovePagesInTile (NSUuid tileId, Action<NSError> completionHandler);
	}

	interface IPersonalizationManagerProtocol
	{

	}

	// @protocol MSBPersonalizationManagerProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBPersonalizationManagerProtocol")]
	interface PersonalizationManagerProtocol
	{
		// @required -(void)updateMeTileImage:(MSBImage *)image completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("updateMeTileImage:completionHandler:")]
		void UpdateMeTileImage (Image image, Action<NSError> completionHandler);

		// @required -(void)meTileImageWithCompletionHandler:(void (^)(MSBImage *, NSError *))completionHandler;
		[Abstract]
		[Export ("meTileImageWithCompletionHandler:")]
		void MeTileImageWithCompletionHandler (Action<Image, NSError> completionHandler);

		// @required -(void)updateTheme:(MSBTheme *)theme completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("updateTheme:completionHandler:")]
		void UpdateTheme (Theme theme, Action<NSError> completionHandler);

		// @required -(void)themeWithCompletionHandler:(void (^)(MSBTheme *, NSError *))completionHandler;
		[Abstract]
		[Export ("themeWithCompletionHandler:")]
		void ThemeWithCompletionHandler (Action<Theme, NSError> completionHandler);
	}

	// @interface MSBSensorData : NSObject
	[BaseType (typeof(NSObject), Name = "MSBSensorData")]
	interface SensorData
	{
	}

	// @interface MSBSensorAccelData : MSBSensorData
	[BaseType (typeof(SensorData), Name = "MSBSensorAccelData")]
	interface SensorAccelData
	{
		// @property (readonly, nonatomic) double x;
		[Export ("x")]
		double X { get; }

		// @property (readonly, nonatomic) double y;
		[Export ("y")]
		double Y { get; }

		// @property (readonly, nonatomic) double z;
		[Export ("z")]
		double Z { get; }
	}

	// @interface MSBSensorGyroData : MSBSensorData
	[BaseType (typeof(SensorData), Name = "MSBSensorGyroData")]
	interface SensorGyroData
	{
		// @property (readonly, nonatomic) double x;
		[Export ("x")]
		double X { get; }

		// @property (readonly, nonatomic) double y;
		[Export ("y")]
		double Y { get; }

		// @property (readonly, nonatomic) double z;
		[Export ("z")]
		double Z { get; }
	}

	// @interface MSBSensorAccelGyroData : MSBSensorData
	[BaseType (typeof(SensorData), Name = "MSBSensorAccelGyroData")]
	interface SensorAccelGyroData
	{
		// @property (readonly, nonatomic) MSBSensorAccelData * accel;
		[Export ("accel")]
		SensorAccelData Accel { get; }

		// @property (readonly, nonatomic) MSBSensorGyroData * gyro;
		[Export ("gyro")]
		SensorGyroData Gyro { get; }
	}

	// @interface MSBSensorHeartRateData : MSBSensorData
	[BaseType (typeof(SensorData), Name = "MSBSensorHeartRateData")]
	interface SensorHeartRateData
	{
		// @property (readonly, nonatomic) NSUInteger heartRate;
		[Export ("heartRate")]
		nuint HeartRate { get; }

		// @property (readonly, nonatomic) MSBHeartRateQuality quality;
		[Export ("quality")]
		HeartRateQuality Quality { get; }
	}

	// @interface MSBSensorCaloriesData : MSBSensorData
	[BaseType (typeof(SensorData), Name = "MSBSensorCaloriesData")]
	interface SensorCaloriesData
	{
		// @property (readonly, nonatomic) NSUInteger calories;
		[Export ("calories")]
		nuint Calories { get; }
	}

	// @interface MSBSensorDistanceData : MSBSensorData
	[BaseType (typeof(SensorData), Name = "MSBSensorDistanceData")]
	interface SensorDistanceData
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
	[BaseType (typeof(SensorData), Name = "MSBSensorPedometerData")]
	interface SensorPedometerData
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
	[BaseType (typeof(SensorData), Name = "MSBSensorSkinTempData")]
	interface SensorSkinTempData
	{
		// @property (readonly, nonatomic) double temperature;
		[Export ("temperature")]
		double Temperature { get; }
	}

	// @interface MSBSensorUVData : MSBSensorData
	[BaseType (typeof(SensorData), Name = "MSBSensorUVData")]
	interface SensorUVData
	{
		// @property (readonly, nonatomic) MSBUVIndexLevel uvIndexLevel;
		[Export ("uvIndexLevel")]
		UVIndexLevel UvIndexLevel { get; }
	}

	// @interface MSBSensorBandContactData : MSBSensorData
	[BaseType (typeof(SensorData), Name = "MSBSensorBandContactData")]
	interface SensorBandContactData
	{
		// @property (readonly, nonatomic) MSBSensorBandContactState wornState;
		[Export ("wornState")]
		SensorBandContactState WornState { get; }
	}

	interface ISensorManagerProtocol
	{

	}

	// @protocol MSBSensorManagerProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBSensorManagerProtocol")]
	interface SensorManagerProtocol
	{
		// @required -(BOOL)startAccelerometerUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorAccelData *, NSError *))handler;
		[Abstract]
		[Export ("startAccelerometerUpdatesToQueue:errorRef:withHandler:")]
		bool StartAccelerometerUpdatesToQueue ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<SensorAccelData, NSError> handler);

		// @required -(BOOL)stopAccelerometerUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopAccelerometerUpdatesErrorRef:")]
		bool StopAccelerometerUpdatesErrorRef (out NSError pError);

		// @required -(BOOL)startGyroscopeUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorGyroData *, NSError *))handler;
		[Abstract]
		[Export ("startGyroscopeUpdatesToQueue:errorRef:withHandler:")]
		bool StartGyroscopeUpdatesToQueue ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<SensorGyroData, NSError> handler);

		// @required -(BOOL)stopGyroscopeUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopGyroscopeUpdatesErrorRef:")]
		bool StopGyroscopeUpdatesErrorRef (out NSError pError);

		// @required -(BOOL)startHearRateUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorHeartRateData *, NSError *))handler;
		[Abstract]
		[Export ("startHearRateUpdatesToQueue:errorRef:withHandler:")]
		bool StartHearRateUpdatesToQueue ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<SensorHeartRateData, NSError> handler);

		// @required -(BOOL)stopHeartRateUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopHeartRateUpdatesErrorRef:")]
		bool StopHeartRateUpdatesErrorRef (out NSError pError);

		// @required -(BOOL)startCaloriesUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorCaloriesData *, NSError *))handler;
		[Abstract]
		[Export ("startCaloriesUpdatesToQueue:errorRef:withHandler:")]
		bool StartCaloriesUpdatesToQueue ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<SensorCaloriesData, NSError> handler);

		// @required -(BOOL)stopCaloriesUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopCaloriesUpdatesErrorRef:")]
		bool StopCaloriesUpdatesErrorRef (out NSError pError);

		// @required -(BOOL)startDistanceUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorDistanceData *, NSError *))handler;
		[Abstract]
		[Export ("startDistanceUpdatesToQueue:errorRef:withHandler:")]
		bool StartDistanceUpdatesToQueue ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<SensorDistanceData, NSError> handler);

		// @required -(BOOL)stopDistanceUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopDistanceUpdatesErrorRef:")]
		bool StopDistanceUpdatesErrorRef (out NSError pError);

		// @required -(BOOL)startPedometerUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorPedometerData *, NSError *))handler;
		[Abstract]
		[Export ("startPedometerUpdatesToQueue:errorRef:withHandler:")]
		bool StartPedometerUpdatesToQueue ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<SensorPedometerData, NSError> handler);

		// @required -(BOOL)stopPedometerUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopPedometerUpdatesErrorRef:")]
		bool StopPedometerUpdatesErrorRef (out NSError pError);

		// @required -(BOOL)startSkinTempUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorSkinTempData *, NSError *))handler;
		[Abstract]
		[Export ("startSkinTempUpdatesToQueue:errorRef:withHandler:")]
		bool StartSkinTempUpdatesToQueue ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<SensorSkinTempData, NSError> handler);

		// @required -(BOOL)stopSkinTempUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopSkinTempUpdatesErrorRef:")]
		bool StopSkinTempUpdatesErrorRef (out NSError pError);

		// @required -(BOOL)startUVUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorUVData *, NSError *))handler;
		[Abstract]
		[Export ("startUVUpdatesToQueue:errorRef:withHandler:")]
		bool StartUVUpdatesToQueue ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<SensorUVData, NSError> handler);

		// @required -(BOOL)stopUVUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopUVUpdatesErrorRef:")]
		bool StopUVUpdatesErrorRef (out NSError pError);

		// @required -(BOOL)startBandContactUpdatesToQueue:(NSOperationQueue *)queue errorRef:(NSError **)pError withHandler:(void (^)(MSBSensorBandContactData *, NSError *))handler;
		[Abstract]
		[Export ("startBandContactUpdatesToQueue:errorRef:withHandler:")]
		bool StartBandContactUpdatesToQueue ([NullAllowed] NSOperationQueue queue, out NSError pError, Action<SensorBandContactData, NSError> handler);

		// @required -(BOOL)stopBandContactUpdatesErrorRef:(NSError **)pError;
		[Abstract]
		[Export ("stopBandContactUpdatesErrorRef:")]
		bool StopBandContactUpdatesErrorRef (out NSError pError);
	}

	// @interface MSBIcon : NSObject
	[BaseType (typeof(NSObject), Name = "MSBIcon")]
	interface Icon
	{
		// @property (readonly, assign, nonatomic) CGSize size;
		[Export ("size", ArgumentSemantic.UnsafeUnretained)]
		CGSize Size { get; }

		// +(MSBIcon *)iconWithMSBImage:(MSBImage *)image error:(NSError **)pError;
		[Static]
		[Export ("iconWithMSBImage:error:")]
		Icon IconWithMSBImage (Image image, out NSError pError);

		// +(MSBIcon *)iconWithUIImage:(UIImage *)image error:(NSError **)pError;
		[Static]
		[Export ("iconWithUIImage:error:")]
		Icon IconWithUIImage (UIImage image, out NSError pError);

		// -(UIImage *)UIImage;
		[Export ("UIImage")]
		UIImage UIImage { get; }
	}

	// @interface MSBImage : NSObject
	[BaseType (typeof(NSObject), Name = "MSBImage")]
	interface Image
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
	interface Color
	{
		// +(id)colorWithUIColor:(UIColor *)color error:(NSError **)pError;
		[Static]
		[Export ("colorWithUIColor:error:")]
		NSObject ColorWithUIColor (UIColor color, out NSError pError);

		// -(UIColor *)UIColor;
		[Export ("UIColor")]
		UIColor UIColor { get; }

		// +(MSBColor *)colorWithRed:(NSUInteger)red green:(NSUInteger)green blue:(NSUInteger)blue;
		[Static]
		[Export ("colorWithRed:green:blue:")]
		Color ColorWithRed (nuint red, nuint green, nuint blue);
	}

	// @interface MSBTheme : NSObject
	[BaseType (typeof(NSObject), Name = "MSBTheme")]
	interface Theme
	{
		// @property (nonatomic, strong) MSBColor * baseColor;
		[Export ("baseColor", ArgumentSemantic.Retain)]
		Color BaseColor { get; set; }

		// @property (nonatomic, strong) MSBColor * highLightColor;
		[Export ("highLightColor", ArgumentSemantic.Retain)]
		Color HighLightColor { get; set; }

		// @property (nonatomic, strong) MSBColor * lowLightColor;
		[Export ("lowLightColor", ArgumentSemantic.Retain)]
		Color LowLightColor { get; set; }

		// @property (nonatomic, strong) MSBColor * secondaryTextColor;
		[Export ("secondaryTextColor", ArgumentSemantic.Retain)]
		Color SecondaryTextColor { get; set; }

		// @property (nonatomic, strong) MSBColor * highContrastColor;
		[Export ("highContrastColor", ArgumentSemantic.Retain)]
		Color HighContrastColor { get; set; }

		// @property (nonatomic, strong) MSBColor * mutedColor;
		[Export ("mutedColor", ArgumentSemantic.Retain)]
		Color MutedColor { get; set; }

		// +(MSBTheme *)themeWithBaseColor:(MSBColor *)baseColor highLightColor:(MSBColor *)highLightColor lowLightColor:(MSBColor *)lowLightColor secondaryTextColor:(MSBColor *)secondaryTextColor highContrastColor:(MSBColor *)highContrastColor mutedColor:(MSBColor *)mutedColor;
		[Static]
		[Export ("themeWithBaseColor:highLightColor:lowLightColor:secondaryTextColor:highContrastColor:mutedColor:")]
		Theme ThemeWithBaseColor (Color baseColor, Color highLightColor, Color lowLightColor, Color secondaryTextColor, Color highContrastColor, Color mutedColor);
	}

	interface INotificationManagerProtocol
	{

	}

	// @protocol MSBNotificationManagerProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBNotificationManagerProtocol")]
	interface NotificationManagerProtocol
	{
		// @required -(void)vibrateWithType:(MSBVibrationType)vibrationType completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("vibrateWithType:completionHandler:")]
		void VibrateWithType (VibrationType vibrationType, Action<NSError> completionHandler);

		// @required -(void)showDialogWithTileID:(NSUUID *)tileID title:(NSString *)title body:(NSString *)body completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("showDialogWithTileID:title:body:completionHandler:")]
		void ShowDialogWithTileID (NSUuid tileID, string title, string body, Action<NSError> completionHandler);

		// @required -(void)sendMessageWithTileID:(NSUUID *)tileID title:(NSString *)title body:(NSString *)body timeStamp:(NSDate *)timeStamp flags:(MSBNotificationMessageFlags)flags completionHandler:(void (^)(NSError *))completionHandler;
		[Abstract]
		[Export ("sendMessageWithTileID:title:body:timeStamp:flags:completionHandler:")]
		void SendMessageWithTileID (NSUuid tileID, string title, string body, NSDate timeStamp, NotificationMessageFlags flags, Action<NSError> completionHandler);

		// @required -(void)registerNotificationWithTileID:(NSUUID *)tileID completionHandler:(void (^)(NSError *))completionHandler __attribute__((availability(ios, introduced=7.0)));
		[iOS (7, 0)]
		[Abstract]
		[Export ("registerNotificationWithTileID:completionHandler:")]
		void RegisterNotificationWithTileID (NSUuid tileID, Action<NSError> completionHandler);

		// @required -(void)registerNotificationWithCompletionHandler:(void (^)(NSError *))completionHandler __attribute__((availability(ios, introduced=7.0)));
		[iOS (7, 0)]
		[Abstract]
		[Export ("registerNotificationWithCompletionHandler:")]
		void RegisterNotificationWithCompletionHandler (Action<NSError> completionHandler);

		// @required -(void)unregisterNotificationWithCompletionHandler:(void (^)(NSError *))completionHandler __attribute__((availability(ios, introduced=7.0)));
		[iOS (7, 0)]
		[Abstract]
		[Export ("unregisterNotificationWithCompletionHandler:")]
		void UnregisterNotificationWithCompletionHandler (Action<NSError> completionHandler);
	}

	// @interface MSBPageElement : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageElement")]
	interface PageElement
	{
		// @property (assign, nonatomic) MSBPageElementIdentifier elementId;
		[Export ("elementId")]
		ushort ElementId { get; set; }

		// @property (nonatomic, strong) MSBRect * rect;
		[Export ("rect", ArgumentSemantic.Retain)]
		Rect Rect { get; set; }

		// @property (nonatomic, strong) MSBMargins * margins;
		[Export ("margins", ArgumentSemantic.Retain)]
		Margins Margins { get; set; }

		// @property (nonatomic, strong) MSBColor * color;
		[Export ("color", ArgumentSemantic.Retain)]
		Color Color { get; set; }

		// @property (assign, nonatomic) MSBPageElementHorizontalAlignment horizontalAlignment;
		[Export ("horizontalAlignment", ArgumentSemantic.UnsafeUnretained)]
		PageElementHorizontalAlignment HorizontalAlignment { get; set; }

		// @property (assign, nonatomic) MSBPageElementVerticalAlignment verticalAlignment;
		[Export ("verticalAlignment", ArgumentSemantic.UnsafeUnretained)]
		PageElementVerticalAlignment VerticalAlignment { get; set; }

		// @property (assign, nonatomic) MSBPageElementVisibility visibility;
		[Export ("visibility", ArgumentSemantic.UnsafeUnretained)]
		PageElementVisibility Visibility { get; set; }
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
	interface PageTextData
	{
		// @property (readonly, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; }

		// +(MSBPageTextData *)pageTextDataWithElementId:(MSBPageElementIdentifier)elementId text:(NSString *)text error:(NSError **)pError;
		[Static]
		[Export ("pageTextDataWithElementId:text:error:")]
		PageTextData PageTextDataWithElementId (ushort elementId, string text, out NSError pError);
	}

	// @interface MSBPageWrappedTextData : MSBPageElementData
	[BaseType (typeof(PageElementData), Name = "MSBPageWrappedTextData")]
	interface PageWrappedTextData
	{
		// @property (readonly, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; }

		// +(MSBPageWrappedTextData *)pageWrappedTextDataWithElementId:(MSBPageElementIdentifier)elementId text:(NSString *)text error:(NSError **)pError;
		[Static]
		[Export ("pageWrappedTextDataWithElementId:text:error:")]
		PageWrappedTextData PageWrappedTextDataWithElementId (ushort elementId, string text, out NSError pError);
	}

	// @interface MSBPageIconData : MSBPageElementData
	[BaseType (typeof(PageElementData), Name = "MSBPageIconData")]
	interface PageIconData
	{
		// @property (readonly, nonatomic) NSUInteger iconIndex;
		[Export ("iconIndex")]
		nuint IconIndex { get; }

		// +(MSBPageIconData *)pageIconDataWithElementId:(MSBPageElementIdentifier)elementId iconIndex:(NSUInteger)iconIndex error:(NSError **)pError;
		[Static]
		[Export ("pageIconDataWithElementId:iconIndex:error:")]
		PageIconData PageIconDataWithElementId (ushort elementId, nuint iconIndex, out NSError pError);
	}

	// @interface MSBPageData : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageData")]
	interface PageData
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
		PageData PageDataWithId (NSUuid pageId, nuint templateIndex, NSObject[] values);
	}

	// @interface MSBPageBarcodePDF417Data : MSBPageElementData
	[BaseType (typeof(PageElementData), Name = "MSBPageBarcodePDF417Data")]
	interface PageBarcodePDF417Data
	{
		// @property (readonly, nonatomic) NSString * value;
		[Export ("value")]
		string Value { get; }

		// +(MSBPageBarcodePDF417Data *)pageBarcodePDF417DataWithElementId:(MSBPageElementIdentifier)elementId value:(NSString *)value error:(NSError **)pError;
		[Static]
		[Export ("pageBarcodePDF417DataWithElementId:value:error:")]
		PageBarcodePDF417Data PageBarcodePDF417DataWithElementId (ushort elementId, string value, out NSError pError);
	}

	// @interface MSBPageBarcodeCode39Data : MSBPageElementData
	[BaseType (typeof(PageElementData), Name = "MSBPageBarcodeCode39Data")]
	interface PageBarcodeCode39Data
	{
		// @property (readonly, nonatomic) NSString * value;
		[Export ("value")]
		string Value { get; }

		// +(MSBPageBarcodeCode39Data *)pageBarcodeCode39DataWithElementId:(MSBPageElementIdentifier)elementId value:(NSString *)value error:(NSError **)pError;
		[Static]
		[Export ("pageBarcodeCode39DataWithElementId:value:error:")]
		PageBarcodeCode39Data PageBarcodeCode39DataWithElementId (ushort elementId, string value, out NSError pError);
	}

	// @interface MSBPagePanel : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBPagePanel")]
	interface PagePanel
	{
		// @property (nonatomic, strong) NSMutableArray * children;
		[Export ("children", ArgumentSemantic.Retain)]
		NSMutableArray Children { get; set; }
	}

	// @interface MSBTextElement : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBTextElement")]
	interface TextElement
	{
		// @property (assign, nonatomic) MSBTextBlockLayoutElementWidth width;
		[Export ("width", ArgumentSemantic.UnsafeUnretained)]
		TextBlockLayoutElementWidth Width { get; set; }
	}

	// @interface MSBRect : NSObject
	[BaseType (typeof(NSObject), Name = "MSBRect")]
	interface Rect
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
		Rect RectwithX (ushort x, ushort y, ushort width, ushort height);
	}

	// @interface MSBMargins : NSObject
	[BaseType (typeof(NSObject), Name = "MSBMargins")]
	interface Margins
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
		Margins MarginsWithLeft (ushort left, ushort top, ushort right, ushort bottom);
	}

	// @interface MSBFilledQuad : MSBPagePanel
	[BaseType (typeof(PagePanel), Name = "MSBFilledQuad")]
	interface FilledQuad
	{
		// -(id)initWithRect:(MSBRect *)rect;
		[Export ("initWithRect:")]
		IntPtr Constructor (Rect rect);
	}

	// @interface MSBFlowList : MSBPagePanel
	[BaseType (typeof(PagePanel), Name = "MSBFlowList")]
	interface FlowList
	{
		// @property (assign, nonatomic) MSBFlowListOrientation orientation;
		[Export ("orientation", ArgumentSemantic.UnsafeUnretained)]
		FlowListOrientation Orientation { get; set; }

		// -(id)initWithRect:(MSBRect *)rect orientation:(MSBFlowListOrientation)orientation;
		[Export ("initWithRect:orientation:")]
		IntPtr Constructor (Rect rect, FlowListOrientation orientation);
	}

	// @interface MSBGlyph : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBGlyph")]
	interface Glyph
	{
		// -(id)initWithRect:(MSBRect *)rect;
		[Export ("initWithRect:")]
		IntPtr Constructor (Rect rect);
	}

	// @interface MSBPageLayout : NSObject
	[BaseType (typeof(NSObject), Name = "MSBPageLayout")]
	interface PageLayout
	{
		// @property (nonatomic, strong) MSBPagePanel * root;
		[Export ("root", ArgumentSemantic.Retain)]
		PagePanel Root { get; set; }
	}

	// @interface MSBScrollFlowList : MSBFlowList
	[BaseType (typeof(FlowList), Name = "MSBScrollFlowList")]
	interface ScrollFlowList
	{
	}

	// @interface MSBTextBlock : MSBTextElement
	[BaseType (typeof(TextElement), Name = "MSBTextBlock")]
	interface TextBlock
	{
		// @property (assign, nonatomic) MSBTextBlockFont font;
		[Export ("font", ArgumentSemantic.UnsafeUnretained)]
		TextBlockFont Font { get; set; }

		// @property (assign, nonatomic) MSBTextBlockBaseline baseline;
		[Export ("baseline")]
		ushort Baseline { get; set; }

		// @property (assign, nonatomic) MSBTextBlockBaselineAlignment baselineAlignment;
		[Export ("baselineAlignment", ArgumentSemantic.UnsafeUnretained)]
		TextBlockBaselineAlignment BaselineAlignment { get; set; }

		// -(id)initWithRect:(MSBRect *)rect font:(MSBTextBlockFont)font baseline:(MSBTextBlockBaseline)baseline;
		[Export ("initWithRect:font:baseline:")]
		IntPtr Constructor (Rect rect, TextBlockFont font, ushort baseline);
	}

	// @interface MSBWrappedTextBlock : MSBTextElement
	[BaseType (typeof(TextElement), Name = "MSBWrappedTextBlock")]
	interface WrappedTextBlock
	{
		// @property (assign, nonatomic) MSBWrappedTextBlockFont font;
		[Export ("font", ArgumentSemantic.UnsafeUnretained)]
		WrappedTextBlockFont Font { get; set; }

		// -(id)initWithRect:(MSBRect *)rect font:(MSBWrappedTextBlockFont)font;
		[Export ("initWithRect:font:")]
		IntPtr Constructor (Rect rect, WrappedTextBlockFont font);
	}

	// @interface MSBBarcode : MSBPageElement
	[BaseType (typeof(PageElement), Name = "MSBBarcode")]
	interface MSBBarcode
	{
		// @property (assign, nonatomic) MSBBarcodeType barcodeType;
		[Export ("barcodeType", ArgumentSemantic.UnsafeUnretained)]
		BarcodeType BarcodeType { get; set; }

		// -(id)initWithRect:(MSBRect *)rect barcodeType:(MSBBarcodeType)type;
		[Export ("initWithRect:barcodeType:")]
		IntPtr Constructor (Rect rect, BarcodeType type);
	}

}


