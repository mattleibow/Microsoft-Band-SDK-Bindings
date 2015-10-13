@echo off

rem restore the NuGets
echo Restoring the NuGet packages
nuget restore Microsoft.Band.sln

rem build the solution
echo Building the solution
msbuild Microsoft.Band.sln /p:Configuration=Release /t:Rebuild

rem build the nuget
echo Packaging the NuGet
nuget pack Xamarin.Microsoft.Band.Native.nuspec
