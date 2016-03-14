# Microsoft Band SDK

[![Build Status][t-img]][t-link]  [![Build status][a-img]][a-link]

The Microsoft Band SDK gives developers access to the sensors available on the 
band, as well as the ability to create and send notifications to tiles. Enhance 
and extend the experience of your applications to your customers' wrists.

More information can be found on the [Microsoft website][dev].

## Downloading

There are two flavours of packaging:

 - Native Bindings
 - Portable Library

### Native Bindings

This package is available on [NuGet.org][nuget]:

```bash
 PM> Install-Package Xamarin.Microsoft.Band.Native
```

Although the Microsoft band is supported by iOS, Android and Windows, this 
package does not provide a shared / cross-platform API. Instead, it brings
you all the features available from the native libraries.

### Portable Library

This package is available on the [Xamarin Component Store][store-pcl] and 
[NuGet.org][nuget-pcl]:

```bash
 PM> Install-Package Xamarin.Microsoft.Band
```

This library provides a cross-platform API that can be used in any
iOS, Android or Windows project, as well as a cross-platform app, such
as Xamarin.Forms.

[dev]:http://developer.microsoftband.com/
[nuget]: https://www.nuget.org/packages/Xamarin.Microsoft.Band.Native
[nuget-pcl]: https://www.nuget.org/packages/Xamarin.Microsoft.Band
[store-pcl]: https://components.xamarin.com/view/microsoft-band-sdk
[t-img]: https://travis-ci.org/mattleibow/Microsoft-Band-SDK-Bindings.svg?branch=master
[t-link]: https://travis-ci.org/mattleibow/Microsoft-Band-SDK-Bindings
[a-img]: https://ci.appveyor.com/api/projects/status/d35thffd9htg4wke/branch/master?svg=true
[a-link]: https://ci.appveyor.com/project/mattleibow/microsoft-band-sdk-bindings
