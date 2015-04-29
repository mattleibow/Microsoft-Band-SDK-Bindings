# Getting Started with Microsoft Band Native SDK (Preview)

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
   Provides the current skin temperature of the wearer in degrees Celcius.
 - **UV**  
   Provides the current ultra violet radition exposure intensity.
 - **Device Contact**  
   Provides a way to let the developer know if someone is currently wearing the device.
 - **Calories**  
   Provides the total number of calories the wearer has burned.

### Create App Tiles
Keep users engaged and extend your app experience to Microsoft Band. Create tiles for the band that send glanceable data and notifications from your app to your users.

#### Tile Notifications

Each app tile is visually represented on the Start Strip by an icon, and when a new notification arrives, the icon is scaled down and a number badge appears on the tile. App notifications come in two flavors:

 - **Dialogs**  
   Dialog notifications are popups meant to quickly display information to the user. Once the user dismisses the dialog, the information contained therein does not persist on the Band.
 - **Messages**  
   Message notifications are sent and stored in a specific tile, and a tile can keep up to 8
messages at a time. Messages may display a dialog as well.
Both notifications types contain a title text and a body text.


#### Custom Tile Pages

Custom tiles have application defined layouts and custom content, which includes multiple icons, buttons, text blocks, and barcodes. With custom tiles, developers can define unique experiences for their applications. The developers control exactly how many pages to show inside of a tile as well as the content of individual pages. 

They can update the contents of a page that has been created using custom layout at any point, unlike messaging tiles where every new message results in the creation of a new page inside the tile. In addition, a developer can choose to add additional pages inside the tile. If the total number of pages goes past the maximum pages allowed inside the tile, the right most page is dropped out when a new page is added.

#### Tile Events
It is also possible to register for tile events. This allows a developer to know when the user has entered and exited their tile. In addition, they can receive events when a user taps on a button in one of their custom tiles.

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
var manager = BandClientManager.Instance;
var pairedBands = manager.GetPairedBands();
var bandClient = manager.Create(this, pairedBands[0]);
await bandClient.ConnectTaskAsync();
```

#### Windows 

```
var manager = BandClientManager.Instance;
var pairedBands = await manager.GetBandsAsync();
var bandClient = await manager.ConnectAsync(pairedBands[0]);
```

#### iOS

```
var manager = BandClientManager.Instance;
var pairedBands = await manager.AttachedClients;
var bandClient = pairedBands[0];
await manager.ConnectTaskAsync(bandClient);
```

### Connecting to a Sensor


#### Android

```
// get the sensor
var accelerometer = bandClient.SensorManager.CreateAccelerometerSensor();
// add a handler
accelerometer.ReadingChanged += (o, args) => {
    var yReading = args.SensorReading.AccelerationY;
};
// start reading, with the interval
accelerometer.StartReadings(SampleRate.Ms16);
// stop reading
accelerometer.StopReadings();
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
// get the sensor
var accelerometer = bandClient.SensorManager.CreateAccelerometerSensor();
// add a handler
accelerometer.ReadingChanged += (o, args) => {
    var yReading = args.SensorReading.AccelerationY;
};
// start reading
accelerometer.StartReadings();
// stop reading
accelerometer.StopReadings();
```

### Adding Tiles

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
var tiles = await bandClient.TileManager.GetTilesTaskAsync();
// the the number of tiles we can add
var capacity = await bandClient.TileManager.GetRemainingTileCapacityTaskAsync();
// create the tile
NSError operationError;
var tile = BandTile.Create(
    new NSUuid ("DCBABA9F-12FD-47A5-83A9-E7270A4399BB",
    "B tile",
    BandIcon.FromUIImage (UIImage.FromBundle ("B.png"), out operationError), 
    BandIcon.FromUIImage (UIImage.FromBundle ("Bb.png"), out operationError), 
    out operationError);
// add the tile
await bandClient.TileManager.AddTileTaskAsync (tile);
// remove the tile
await bandClient.TileManager.RemoveTileTaskAsync (tileId);
```

### Adding Pages to Tiles

#### Android

```
// create a barcode element
Barcode barcode = new Barcode(new PageRect(0, 0, 221, 70), BarcodeType.Code39);
barcode.Margins = new Margins(3, 0, 0, 0);
barcode.ElementId = 11;
// create a text block element
var textBlock = new TextBlock(new PageRect(0, 0, 230, 30), TextBlockFont.Small, 0);
textBlock.Color = Color.Red;
textBlock.ElementId = 21;
// add both to a flow panel
var flowPanel = new FlowPanel(new PageRect(15, 0, 245, 105), FlowPanelOrientation.Vertical);
flowPanel.AddElements(barcode);
flowPanel.AddElements(textBlock);
// create the page
PageLayout pageLayout = new PageLayout(flowPanel1);
// add the page to the tile
var tile = 
    new BandTile.Builder(tileUuid, "TileName", tileIcon)
    .SetPageLayouts(pageLayout);
    .Build();
// add the tile to the band
await bandClient.TileManager.AddTileTaskAsync(Activity, tile);
// create the data for the page
var barcodeValue = "MK12345509";
var pageData = new PageData(Java.Util.UUID.RandomUUID(), 0);
pageData.Update(new BarcodeData(barcode.ElementId, barcodeValue, BarcodeType.Code39));
pageData.Update(new TextButtonData(textBlock.ElementId, barcodeValue));
// set the data for the page
await bandClient.TileManager.SetPagesTaskAsync(tile.TileId, pageData);
```

#### Windows

```
// create a flow panel element
var panel = new ScrollFlowPanel{    Rect = new PageRect(0, 0, 245, 102),    Orientation = FlowListOrientation.Vertical,    ColorSource = ElementColorSource.BandBase};
// add a text block element
panel.Elements.Add(new WrappedTextBlock{    ElementId = 11,    Rect = new PageRect(0, 0, 245, 102),    Margins = new Margins(15, 0, 15, 0),    Color = new BandColor(0xFF, 0xFF, 0xFF),    Font = WrappedTextBlockFont.Small});
// add another text block element
panel.Elements.Add(new WrappedTextBlock{    ElementId = 21,    Rect = new PageRect(0, 0, 245, 102),    Margins = new Margins (15, 0, 15, 0),    Color = new BandColor(0xFF, 0xFF, 0xFF),    Font = WrappedTextBlockFont.Small});
// create the page
var pageLayout = new PageLayout(panel);
// add the page to the tile
tile.PageLayouts.Add(pageLayout);
// add the tile to the band
await bandClient.TileManager.AddTileAsync (tile);
// create the data for the page
var pageData = new PageData(Guid.NewGuid(), 0,
    new WrappedTextBlockData(11, "The first message"),
    new WrappedTextBlockData(21, "The second message"));
// set the data for the page
await client.TileManager.SetPagesAsync(tileId, pageData);
```

#### iOS

```
// create a text block element
textBlock = new TextBlock (PageRect.Create (0, 0, 230, 40), TextBlockFont.Small);
textBlock.ElementId = 10;
textBlock.Baseline = 25;
textBlock.HorizontalAlignment = HorizontalAlignment.Center;
textBlock.BaselineAlignment = TextBlockBaselineAlignment.Relative;
textBlock.AutoWidth = false;
// create a barcode element
var barcode = new Barcode (PageRect.Create (0, 5, 230, 95), BarcodeType.Code39);
barcode.ElementId = 11;
// add both to a flow panel
var flowPanel = new FlowPanel (PageRect.Create (15, 0, 260, 105));
flowPanel.AddElement (textBlock);
flowPanel.AddElement (barcode);
// create the page
var pageLayout = new PageLayout ();
pageLayout.Root = flowPanel;
// add the page to the tile
tile.PageLayouts.Add (pageLayout);
// add the tile to the band
await bandClient.TileManager.AddTileTaskAsync (tile);
// create the data for the page
var pageValues = new PageElementData [] {
    TextBlockData.Create (textBlock.ElementId, "Barcode value: A1 B", out error),
    BarcodeData.Create (barcode.ElementId, BarcodeType.Code39, "A1 B", out error)
};
var page = PageData.Create (barcodePageId, 0, pageValues);
// set the data for the page
await client.TileManager.SetPagesTaskAsync (new[]{ page }, tileId);
```

### Subscribing to Band Page Events

#### Android

```
// create a broadcast receiver
private class ButtonTileBroadcastReceiver : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        if (intent.Action == TileEvent.ActionTileOpened) 
        {
            var data = (TileEvent)intent.GetParcelableExtra(TileEvent.TileEventData);
        }
        else if (intent.Action == TileEvent.ActionTileButtonPressed) 
        {
            var data = (TileButtonEvent)intent.GetParcelableExtra(TileEvent.TileEventData);
        }
        else if (intent.Action == TileEvent.ActionTileClosed) 
        {
            var data = (TileEvent)intent.GetParcelableExtra(TileEvent.TileEventData);
        }
    }
}
// create the intent filter for the events
var filter = new IntentFilter();
filter.AddAction(TileEvent.ActionTileOpened);
filter.AddAction(TileEvent.ActionTileButtonPressed);
filter.AddAction(TileEvent.ActionTileClosed);
// register the receiver
var receiver = new ButtonTileBroadcastReceiver();
RegisterReceiver(receiver, filter);
```

#### Windows

```
// attach event handlers to the Band client
bandClient.TileManager.TileButtonPushed += (_, e) => {
    var data = e.TileEvent;
};
bandClient.TileManager.TileOpened += (_, e) => {
    var data = e.TileEvent;
};
bandClient.TileManager.TileClosed += (_, e) => {
    var data = e.TileEvent;
};
```

#### iOS

```
// attach event handlers to the Band client
bandClient.ButtonPressed += (_, e) => {
    var data = e.TileButtonEvent;
};
bandClient.TileOpened += (_, e) => {
    var data = e.TileEvent;
};
bandClient.TileClosed += (_, e) => {
    var data = e.TileEvent;
};
```


### Send a Message

#### Android

```
await bandClient.NotificationManager.SendMessageTaskAsync(
  tileId, 
  "Message title", 
  "Message body", 
  DateTime.Now,
  MessageFlags.ShowDialog);
```

#### Windows 

```
await bandClient.NotificationManager.SendMessageAsync(
  tileId, 
  "Message title", 
  "Message body", 
  DateTimeOffset.Now, 
  MessageFlags.ShowDialog);
```

#### iOS

```
bandClient.NotificationManager.SendMessageTaskAsync(
  tileId, 
  "Message title", 
  "Message body", 
  NSDate.Now, 
  MessageFlags.ShowDialog);
```


[1]:http://developer.microsoftband.com/
[2]:https://raw.githubusercontent.com/mattleibow/Microsoft-Band-SDK-Bindings/master/Images/capabilities.png