@echo off

rem restore the NuGets
echo Restoring the NuGet packages
nuget restore Microsoft.Band.Portable.sln

rem build the solution
echo Building the solution
msbuild Microsoft.Band.Portable.sln /p:Configuration=Release /t:Rebuild

rem build the nuget
echo Packaging the NuGet
nuget pack Xamarin.Microsoft.Band.Native.nuspec
nuget pack Xamarin.Microsoft.Band.nuspec

rem package the component
echo Packaging the Component
xamarin-component package 
