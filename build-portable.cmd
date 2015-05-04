echo off

rem extract the javadocs
echo Unzipping Java Docs...
if exist Microsoft.Band\Microsoft.Band.Android\JavaDoc del /f /q /s Microsoft.Band\Microsoft.Band.Android\JavaDoc
mkdir Microsoft.Band\Microsoft.Band.Android\JavaDoc
7za x -oMicrosoft.Band\Microsoft.Band.Android\JavaDoc Microsoft.Band\Microsoft.Band.Android\microsoft-band-javadoc.jar

rem restore the NuGets
echo Restoring the NuGet packages
nuget restore Microsoft.Band.Portable.sln

rem build the solution
echo Building the solution
msbuild Microsoft.Band.Portable.sln /p:Configuration=Release /t:Rebuild

rem build the nuget
echo Packaging the NuGet
nuget pack Xamarin.Microsoft.Band.nuspec
