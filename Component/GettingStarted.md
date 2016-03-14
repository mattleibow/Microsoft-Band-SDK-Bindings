# Getting Started with Microsoft Band SDK

The Microsoft Band SDK gives developers access to the sensors available on the 
band, as well as the ability to create and send notifications to tiles. Enhance 
and extend the experience of your applications to your customers' wrists.

## Amazing App Experiences
Extend the experience of your application to your users' wrists via a new 
dimension of interaction. Create an app that can send UI content to the band, 
keeping users engaged when they're in motion. Your app can also receive data 
directly from the band sensors, giving your users more reasons to interact 
with it. Create a personalized, data-rich, custom experience and enhanced 
scenarios that will engage users in ways only possible with Microsoft Band.

### Access Sensors
Use a range of sensors including heart rate, UV, accelerometer, gyroscope, and 
skin temperature, as well as fitness data, to design cutting-edge user 
experiences:

 - **Accelerometer**  
   Provides X, Y, and Z acceleration in meters per second squared (m/s²) units.
 - **Gyroscope**  
   Provides X, Y, and Z angular velocity in degrees per second (°/sec) units.
 - **Distance**  
   Provides the total distance in centimeters, current speed in centimeters 
   per second (cm/s), current pace in milliseconds per meter (ms/m), and 
   the current pedometer mode (such as walking or running).
 - **Heart Rate**  
   Provides the number of beats per minute, also indicates if the heart rate 
   sensor is fully locked onto the wearer’s heart rate.
 - **Pedometer**  
   Provides the total number of steps the wearer has taken.
 - **Skin Temperature**  
   Provides the current skin temperature of the wearer in degrees Celsius.
 - **UV**  
   Provides the current ultra violet radiation exposure intensity.
 - **Device Contact**  
   Provides a way to let the developer know if someone is currently wearing 
   the device.
 - **Calories**  
   Provides the total number of calories the wearer has burned.
 - **Altimeter** _(Microsoft Band 2 only)_  
   Provides current elevation data like total gain/loss, steps 
   ascended/descended, flights ascended/descended, and elevation rate. 
 - **Ambient Light** _(Microsoft Band 2 only)_  
   Provides the current light intensity (illuminance) in lux (Lumes/m²).
 - **Barometer** _(Microsoft Band 2 only)_  
   Provides the current raw air pressure in hPa (hectopascals) and raw 
   temperature in degrees Celsius.
 - **Galvanic Skin Response (GSR)** _(Microsoft Band 2 only)_  
   Provides the current skin resistance of the wearer in kohms. 
 - **RR Interval** _(Microsoft Band 2 only)_  
   Provides the interval in seconds between the last two continuous heart beats. 

### Create App Tiles
Keep users engaged and extend your app experience to Microsoft Band. Create 
tiles for the band that send glance-able data and notifications from your app 
to your users.

#### Tile Notifications

Each app tile is visually represented on the Start Strip by an icon, and when a 
new notification arrives, the icon is scaled down and a number badge appears on 
the tile. App notifications come in two flavors:

 - **Dialogs**  
   Dialog notifications are popups meant to quickly display information to the 
   user. Once the user dismisses the dialog, the information contained therein 
   does not persist on the Band.
 - **Messages**  
   Message notifications are sent and stored in a specific tile, and a tile can 
   keep up to 8 messages at a time. Messages may display a dialog as well.
   Both notifications types contain a title text and a body text.

#### Custom Tile Pages

Custom tiles have application defined layouts and custom content, which includes 
multiple icons, buttons, text blocks, and barcodes. With custom tiles, developers
can define unique experiences for their applications. The developers control 
exactly how many pages to show inside of a tile as well as the content of 
individual pages. 

They can update the contents of a page that has been created using custom layout
at any point, unlike messaging tiles where every new message results in the 
creation of a new page inside the tile. In addition, a developer can choose to 
add additional pages inside the tile. If the total number of pages goes past the 
maximum pages allowed inside the tile, the right most page is dropped out when a 
new page is added.

#### Tile Events

It is also possible to register for tile events. This allows a developer to know
when the user has entered and exited their tile. In addition, they can receive 
events when a user taps on a button in one of their custom tiles.

### Personalize Device
Monetize your app by offering users ways to customize the band. Change the color 
theme, or bring the Me Tile to life by changing the wallpaper.

## SDK API Usage

More advanced documentation can be found on the 
[Microsoft Band Developers Page][1].

### Requirements

All operations with the Band API are performed using the types and members of 
the `Microsoft.Band.Portable` namespace. This makes it easier to share and reuse 
code across platforms. But, the original `Microsoft.Band` namespace can still be 
used on the specific platforms. 

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
    <!-- Used by the Microsoft Band SDK -->
    <Function Type="serviceId:A502CA9A-2BA5-413C-A4E0-13804E47B38F" />
    <!-- Used by the Microsoft Band SDK -->
    <Function Type="serviceId:C742E1A2-6320-5ABC-9643-D206C677E580" />
  </Device>
</m2:DeviceCapability>
```

#### iOS 7+

1. Automatically adds the CoreBluetooth framework.
2. In order for the app to communicate with the Band in the background, 
"Use Bluetooth LE Accessories" must be enabled in Background Modes.

### Connecting to a Band

Connecting to a Band device is very easy and only requires two steps:

1. Get a list of the paired Band devices
2. Connect to a specific Band device

```
var bandClientManager = BandClientManager.Instance;
// query the service for paired devices
var pairedBands = await bandClientManager.GetPairedBandsAsync();
// connect to the first device
var bandInfo = pairedBands.FirstOrDefault();
var bandClient = await bandClientManager.ConnectAsync(bandInfo);
```

### Connecting to a Sensor

Connecting to a sensor on the Band requires the use of the Sensor Manager.

```
var sensorManager = bandClient.SensorManager;
// get the accelerometer sensor
var accelerometer = sensorManager.Accelerometer;
// add a handler
accelerometer.ReadingChanged += (o, args) => {
    var yReading = args.SensorReading.AccelerationY;
};
// start reading, with the interval
await accelerometer.StartReadingsAsync(SampleRate.Ms16);
// stop reading
await accelerometer.StopReadingsAsync();
```

Some sensors require user consent before use. One of these sensors, and 
currently the only one, is the heart rate sensor.

```
var sensorManager = bandClient.SensorManager;
// get the heart rate sensor
var heartRate = sensorManager.HeartRate;
// add a handler
heartRate.ReadingChanged += (o, args) => {
    var quality= args.SensorReading.Quality;
};
if (heartRate.UserConsented == UserConsent.Unspecified) {
    bool granted = await heartRate.RequestUserConsent();
}
if (heartRate.UserConsented == UserConsent.Granted) {
    // start reading, with the interval
    await heartRate.StartReadingsAsync(SampleRate.Ms16);
} else {
    // user declined
}
// stop reading
await heartRate.StopReadingsAsync();
```

### Adding Tiles

Another feature of the Band is the ability to provide the user with custom, 
interactive tiles.

```
var tileManager = bandClient.TileManager;
// get the current set of tiles
var tiles = await tileManager.GetTilesAsync();
// get the number of tiles we can add
var capacity = await tileManager.GetRemainingTileCapacityAsync();
// create a new tile
var tile = new BandTile(tileId) {
    Icon = await BandImage.FromStreamAsync(tileImageStream),
    Name = "Tile Name",
    SmallIcon = await BandImage.FromStreamAsync(tileBadgeImageStream)
};
// add the tile
await tileManager.AddTileAsync(tile);
// remove the tile
await tileManager.RemoveTileAsync(tile);
```

### Adding Pages to Tiles

Tiles are made up of the actual tile as well as pages. Each page can consist of 
several page elements with which to provide the user information. Buttons can be 
added to allow the user to communicate back to the app on the device.

```
// define page/element IDs
Guid tileId = Guid.NewGuid();
Guid pageId = Guid.NewGuid();
int pageIndex = 0;
short titleId = 1;
short buttonId = 2;
short imageId = 3;
// create the new tile
var tile = new BandTile(tileId) { ... };
// define the page layout
var pageLayout = new PageLayout {
    Root = new ScrollFlowPanel {
        Rectangle = new Rectangle(0, 0, 245, 105),
        Orientation = Orientation.Vertical,
        Elements = {
            new TextBlock {
                ElementId = titleId,
                Rectangle = new Rectangle(0, 0, 229, 30),
                TextColorSource = ElementColorSource.BandBase,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom
            },
            new TextButton {
                ElementId = buttonId,
                Rectangle = new Rectangle(0, 0, 229, 43),
                PressedColor = new BandColor(0, 127, 0)
            },
            new Image {
                ElementId = imageId,
                Rectangle = new Rectangle(0, 0, 229, 46),
                Color = new BandColor(127, 127, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            }
        }
    }
};
// add the page layout to the tile
tile.PageLayouts.Add(pageLayout);
// add additional images
tile.PageImages.Add(await BandImage.FromStreamAsync(additionalImageStream));
// add the tile to the Band
await tileManager.AddTileAsync(tile);
// declare the data for the page
var pageData = new PageData {
    PageId = pageId,
    PageLayoutIndex = pageIndex,
    Data = {
        new TextBlockData {
            ElementId = titleId,
            Text = "Buttons"
        },
        new TextButtonData {
            ElementId = buttonId,
            Text = "Press Me!"
        },
        new ImageData {
            ElementId = imageId,
            ImageIndex = 0
        }
    }
};
// apply the data to the tile
await tileManager.SetTilePageDataAsync(tileId, pageData);
```

### Subscribing to Band Page Events

Events can be received from the Band when a tile is opened or close, as well as 
when the user presses a button on a page. 

```
var tileManager = bandClient.TileManager;
// attach a handler to the button pressed event
tileManager.TileButtonPressed += (sender, e) => {
    var buttonId = e.ElementId;
    var pageId = e.PageId;
    var tileId = e.TileId;
};
// attach a handler to the tile events
tileManager.TileOpened += (sender, e) => {
    var tileId = e.TileId;
};
tileManager.TileClosed += (sender, e) => {
    var tileId = e.TileId;
};
// start listening to events from the Band
await tileManager.StartEventListenersAsync();
// stop listening
await BandClient.TileManager.StopEventListenersAsync();
```

### Send a Message

Along with pages, a tile can be used to show notifications or dialogs.

```
var notifictionManager = bandClient.NotificationManager;
// send a notification to the Band with a dialog as well as a page
await notifictionManager.SendMessageAsync(
    tileId, 
    "Message Title", 
    "This is the message body...", 
    DateTime.Now, 
    true);
```

[1]:http://developer.microsoftband.com/
[2]:https://cdn.rawgit.com/mattleibow/Microsoft-Band-SDK-Bindings/v1.3.6/Images/capabilities.png
