
rem build the solution

msbuild Microsoft.Band.Portable.sln /p:Configuration=Release /t:Rebuild

rem build the nuget

nuget pack Xamarin.Microsoft.Band.nuspec