#!/bin/bash

# build everything
xamarin-component package

# package binding
nuget pack Xamarin.Microsoft.Band.nuspec
