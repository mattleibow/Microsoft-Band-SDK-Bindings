# Microsoft Band SDK Preview

The Microsoft Band SDK Preview gives developers access to the sensors available on the band, as well as the ability to create and send notifications to tiles. Enhance and extend the experience of your applications to your customers' wrists.

## Amazing App Experiences
Extend the experience of your application to your users' wrists via a new dimension of interaction. Create an app that can send UI content to the band, keeping users engaged when they're in motion. Your app can also receive data directly from the band sensors, giving your users more reasons to interact with it. Create a personalized, data-rich, custom experience and enhanced scenarios that will engage users in ways only possible with Microsoft Band.

### Access Sensors
Use a range of sensors including heart rate, UV, accelerometer, gyroscope, and skin temperature, as well as fitness data, to design cutting-edge user experiences:

 - **Accelerometer**  
   Provides X, Y, and Z acceleration in meters per second squared (m/s2) units.
 - **Gyroscope**  
   Provides X, Y, and Z angular velocity in degrees per second (°/sec) units.
 - **Distance**  
   Provides the total distance in centimeters, current speed in centimeters per second (cm/s), current pace in milliseconds per meter (ms/m), and the current pedometer mode (such as walking or running).
 - **Heart Rate**  
   Provides the number of beats per minute, also indicates if the heart rate sensor is fully locked onto the wearer’s heart rate.
 - **Pedometer**  
   Provides the total number of steps the wearer has taken.
 - **Skin Temperature**  
   Provides the current skin temperature of the wearer in degrees Celius.
 - **UV**  
   Provides the current ultra violet radition exposure intensity.
 - **Device Contact**  
   Provides a way to let the developer know if someone is currently wearing the device.

### Create App Tiles
Keep users engaged and extend your app experience to Microsoft Band. Create tiles for the band that send glanceable notifications from your app to your users.

Each app tile is visually represented on the Start Strip by an icon, and when a new notification arrives, the icon is scaled down and a number badge appears on the tile. App notifications come in two flavors:

 - **Dialogs**  
   Dialog notifications are popups meant to quickly display information to the user. Once the user dismisses the dialog, the information contained therein does not persist on the Band.
 - **Messages**  
   Message notifications are sent and stored in a specific tile, and a tile can keep up to 8
messages at a time. Messages may display a dialog as well.
Both notifications types contain a title text and a body text.

### Personalize Device
Monetize your app by offering users ways to customize the band. Change the color theme, or bring the Me Tile to life by changing the wallpaper.

## SDK API Usage

More advanced documentation can be found on the [Microsoft Band Developers Page][1].

### Requirements

#### Android 4.2+
1. Permission to access Bluetooth:  
   `[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]`
2. Permission to access the Band service:  
   `[assembly: UsesPermission(Microsoft.Band.BandClientManager.BindBandService)]`

#### Windows Phone 8.1+ & Windows 8.1+ (Windows Runtime)
1. You will also need to add the Proximity capability:  
![Capabilities][2]
2. Permission to access Bluetooth:  

```
<DeviceCapability Name="bluetooth.rfcomm">
  <Device Id="any">
    <!-- Used by the Microsoft Band SDK Preview -->
    <Function Type="serviceId:A502CA9A-2BA5-413C-A4E0-13804E47B38F" />
    <!-- Used by the Microsoft Band SDK Preview -->
    <Function Type="serviceId:C742E1A2-6320-5ABC-9643-D206C677E580" />
  </Device>
</m2:DeviceCapability>
```

#### iOS 7+

1. Automatically adds the CoreBluetooth framework.
2. In order for the app to communicate with the Band in the background, "Use Bluetooth LE Accessories" must be enabled in Background Modes.

### Connecting to a Band


#### Android

```
var pairedBands = BandClientManager.Instance.GetPairedBands();
var bandClient = BandClientManager.Instance.Create(this, pairedBands[0]);
await bandClient.ConnectTaskAsync();
```

#### Windows 

```
var pairedBands = await BandClientManager.Instance.GetBandsAsync();
var bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);
```

#### iOS

```
var manager = BandClientManager.SharedManager;
var client = manager.AttachedClients.FirstOrDefault ();
if (client == null) {
  // error: No Bands attached.
} else {
  manager.Connect(client);
}
```

### Connecting the a Sensor


#### Android

```
// get the sensor
var accelerometer = bandClient.SensorManager.CreateAccelerometerSensor();
// add a handler
accelerometer.ReadingChanged += (o, args) => {
  var yReading = args.SensorReading.AccelerationY;
};
// start reading, with the interval
await accelerometer.StartReadingsTaskAsync(SampleRate.Ms16);
// stop reading
await accelerometer.StopReadingsTaskAsync();
```

#### Windows

```
// get the sensor
var accelerometer = bandclient.SensorManager.Accelerometer;
// set the interval
accelerometer.ReportingInterval = TimeSpan.FromMilliseconds(16);
// add a handler
accelerometer.ReadingChanged += (o, args) => {
  var yReading = args.SensorReading.AccelerationY;
};
// start reading
await accelerometer.StartReadingsAsync();
// stop reading
await accelerometer.StopReadingsAsync();
```

#### iOS

```
// start listening to the sensor with a callback
NSError queueError;
client.SensorManager.StartAccelerometerUpdates(null, out queueError, (data, error) => {
  var yReading = data.Y;
});
```

### Working with Tiles

#### Android

```
// get the tiles
var tiles = await bandClient.TileManager.GetTilesTaskAsync();
// the the number of tiles we can add
var capacity = await bandClient.TileManager.GetRemainingTileCapacityTaskAsync();
// create a new tile
var tile = 
  new BandTile.Builder(tileUuid, "TileName", tileIcon)
  .SetTileSmallIcon(smallIcon)
  .Build();
// add the tile
await bandClient.TileManager.AddTileTaskAsync(this, tile);
// remove the tile
await bandClient.TileManager.RemoveTileTaskAsync(tile);
```

#### Windows

```
// get the tiles
var tiles = await bandClient.TileManager.GetTilesAsync();
// the the number of tiles we can add
var capacity = await bandClient.TileManager.GetRemainingTileCapacityAsync();
// create a new tile
var tile = new BandTile(tileGuid) {
  IsBadgingEnabled = true,
  Name = "TileName",
  SmallIcon = smallIcon,
  TileIcon = tileIcon
};
// add the tile
await bandClient.TileManager.AddTileAsync(tile);
// remove the tile
await bandClient.TileManager.RemoveTileAsync(tile);
```

#### iOS

```
// get the tiles
client.TileManager.GetTiles ((tiles, tileError) => {
  // tiles contains the collection of app tiles
});
client.TileManager.RemainingTileCapacity((capacity, error) => {
  // capacity contains the value of the remaining spaces
});
// create the tile
NSError operationError;
var tile = BandTile.Create(
  new NSUuid ("DCBABA9F-12FD-47A5-83A9-E7270A4399BB",
  "B tile",
  BandIcon.FromUIImage (UIImage.FromBundle ("B.png"), out operationError), 
  BandIcon.FromUIImage (UIImage.FromBundle ("Bb.png"), out operationError), 
  out operationError);
// add the tile
client.TileManager.AddTile(tile, error => { });
// remove the tile
client.TileManager.RemoveTile (tileId, error => { });
```

### Send a Message

#### Android

```
await bandClient.NotificationManager.SendMessageTaskAsync(
  tileGuid, 
  "Message title", 
  "Message body", 
  DateTime.Now,
  MessageFlags.ShowDialog);
```

#### Windows 

```
await bandClient.NotificationManager.SendMessageAsync(
  tileGuid, 
  "Message title", 
  "Message body", 
  DateTimeOffset.Now, 
  MessageFlags.ShowDialog);
```

#### iOS

```
client.NotificationManager.SendMessage(
  tileId, 
  "Message title", 
  "Message body", 
  NSDate.Now, 
  BandNotificationMessageFlags.ShowDialog, 
  error => { });
```


[1]:http://developer.microsoftband.com/
[2]:https://raw.githubusercontent.com/mattleibow/Microsoft-Band-SDK-Bindings/master/Images/capabilities.png