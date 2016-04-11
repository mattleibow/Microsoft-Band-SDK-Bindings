#tool nuget:?package=XamarinComponent

#addin nuget:?package=Octokit
#addin nuget:?package=Cake.AppVeyor
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

#reference "tools/Addins/Octokit/lib/net45/Octokit.dll"

using System.Net;
using Octokit;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

// special logic to tweak builds for platform limitations

var ForMacOnly = target.EndsWith("-Mac");
var ForWindowsOnly = target.EndsWith("-Windows");
var ForEverywhere = !ForMacOnly && !ForWindowsOnly;

var ForWindows = ForEverywhere || !ForMacOnly;
var ForMac = ForEverywhere || !ForWindowsOnly;

target = target.Replace("-Mac", string.Empty).Replace("-Windows", string.Empty);

Information("Building target '{0}' for {1}.", target, ForEverywhere ? "everywhere" : (ForWindowsOnly ? "Windows only" : "Mac only"));

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

FilePath XamarinComponentPath = "./tools/XamarinComponent/tools/xamarin-component.exe";

DirectoryPath outDir = "./output/";
FilePath outputZip = "output.zip";
if (!DirectoryExists(outDir)) {
    CreateDirectory(outDir);
}

var sha = EnvironmentVariable("APPVEYOR_REPO_COMMIT") ?? EnvironmentVariable("TRAVIS_COMMIT");
var branch = EnvironmentVariable("APPVEYOR_REPO_BRANCH") ?? EnvironmentVariable("TRAVIS_BRANCH");
var tag = EnvironmentVariable("APPVEYOR_REPO_TAG_NAME") ?? EnvironmentVariable("TRAVIS_TAG");
var pull = EnvironmentVariable("APPVEYOR_PULL_REQUEST_NUMBER") ?? EnvironmentVariable("TRAVIS_PULL_REQUEST");

var buildType = "COMMIT";
if (!string.IsNullOrEmpty(pull) && !string.Equals(pull, "false", StringComparison.OrdinalIgnoreCase)) {
    buildType = "PULL" + pull;
} else if (!string.IsNullOrEmpty(tag)) {
    buildType = "TAG";
}
var tagOrBranch = branch;
if (!string.IsNullOrEmpty(tag)) {
    tagOrBranch = tag;
}

var GitHubToken = EnvironmentVariable("GitHubToken");
var GitHubUser = "mattleibow";
var GitHubRepository = "Microsoft-Band-SDK-Bindings";
var GitHubBuildTag = "CI";
var GitHubUploadFilename = string.Format("{0}_{1}_{2}.zip", buildType, tagOrBranch, sha);

var AppVeyorToken = EnvironmentVariable("AppVeyorToken");

var Build = new Action<string>((solution) =>
{
    if (IsRunningOnWindows()) {
        MSBuild(solution, s => s.SetConfiguration(configuration).SetMSBuildPlatform(MSBuildPlatform.x86));
    } else {
        XBuild(solution, s => s.SetConfiguration(configuration));
    }
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    var dirs = new [] { 
        "./output",
        // source
        "./packages",
        "./Microsoft.Band/*/bin", 
        "./Microsoft.Band/*/obj", 
        "./Microsoft.Band.Portable/*/bin", 
        "./Microsoft.Band.Portable/*/obj", 
        // samples
        "./Demos/*/packages",
        "./Demos/*/*/bin",
        "./Demos/*/*/obj",
    };
    foreach (var dir in dirs) {
        CleanDirectories(dir);
    }
});

Task("RestorePackages")
    .Does(() =>
{
    var solutions = new [] { 
        // source
        "./Microsoft.Band.sln", 
        "./Microsoft.Band.Portable.sln",
        // samples
        "./Demos/Microsoft.Band.Sample/Microsoft.Band.Sample.sln",
        "./Demos/Microsoft.Band.Portable.Sample/Microsoft.Band.Portable.Sample.sln",
        "./Demos/RotatingHand/RotatingHand.sln",
    };
    foreach (var solution in solutions) {
        Information("Restoring {0}...", solution);
        NuGetRestore(solution, new NuGetRestoreSettings {
            Source = new [] { ForWindows ? "https://api.nuget.org/v3/index.json" : "https://www.nuget.org/api/v2/" },
            Verbosity = NuGetVerbosity.Detailed
        });
    }
});

Task("Build")
    .IsDependentOn("RestorePackages")
    .Does(() =>
{
    var solutions = new [] { 
        ForEverywhere ? "./Microsoft.Band.sln" : (ForWindowsOnly ? "./Microsoft.Band.Windows.sln" : "./Microsoft.Band.Mac.sln"),
        ForEverywhere ? "./Microsoft.Band.Portable.sln" : (ForWindowsOnly ? "./Microsoft.Band.Portable.Windows.sln" : "./Microsoft.Band.Portable.Mac.sln"),
    };
    foreach (var solution in solutions) {
        Information("Building {0}...", solution);
        Build(solution);
    }
    
    var outputs = new Dictionary<string, string>();
    if (ForWindows) {
        // native
        outputs.Add("./Microsoft.Band/Microsoft.Band.Android/bin/{0}/Microsoft.Band.Android.dll", "android/Microsoft.Band.Android.dll");
        // portable
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.Android/bin/{0}/Microsoft.Band.Portable.dll", "android/Microsoft.Band.Portable.dll");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.Android/bin/{0}/Microsoft.Band.Portable.xml", "android/Microsoft.Band.Portable.xml");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.Phone/bin/{0}/Microsoft.Band.Portable.dll", "wpa81/Microsoft.Band.Portable.dll");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.Phone/bin/{0}/Microsoft.Band.Portable.xml", "wpa81/Microsoft.Band.Portable.xml");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.Windows/bin/{0}/Microsoft.Band.Portable.dll", "netcore451/Microsoft.Band.Portable.dll");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.Windows/bin/{0}/Microsoft.Band.Portable.xml", "netcore451/Microsoft.Band.Portable.xml");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.UWP/bin/{0}/Microsoft.Band.Portable.dll", "uap10.0/Microsoft.Band.Portable.dll");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.UWP/bin/{0}/Microsoft.Band.Portable.xml", "uap10.0/Microsoft.Band.Portable.xml");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.UWP/bin/{0}/Microsoft.Band.Portable.pri", "uap10.0/Microsoft.Band.Portable.pri");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable/bin/{0}/Microsoft.Band.Portable.dll", "pcl/Microsoft.Band.Portable.dll");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable/bin/{0}/Microsoft.Band.Portable.xml", "pcl/Microsoft.Band.Portable.xml");
    }
    if (ForMac) {
        // native
        outputs.Add("./Microsoft.Band/Microsoft.Band.iOS/bin/{0}/Microsoft.Band.iOS.dll", "ios/Microsoft.Band.iOS.dll");
        // portable
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.iOS/bin/{0}/Microsoft.Band.Portable.dll", "ios/Microsoft.Band.Portable.dll");
        outputs.Add("./Microsoft.Band.Portable/Microsoft.Band.Portable.iOS/bin/{0}/Microsoft.Band.Portable.xml", "ios/Microsoft.Band.Portable.xml");
    }
    foreach (var output in outputs) {
        var dest = outDir.CombineWithFilePath(string.Format(output.Value, configuration));
        var dir = dest.GetDirectory();
        if (!DirectoryExists(dir)) {
            CreateDirectory(dir);
        }
        CopyFile(string.Format(output.Key, configuration), dest);
    }
});

Task("BuildSamples")
    .IsDependentOn("RestorePackages")
    .Does(() =>
{
    var solutions = new List<string> { 
        ForEverywhere ? "./Demos/Microsoft.Band.Sample/Microsoft.Band.Sample.sln" : (ForWindowsOnly ? "./Demos/Microsoft.Band.Sample/Microsoft.Band.Android.Sample.sln" : "./Demos/Microsoft.Band.Sample/Microsoft.Band.iOS.Sample.sln"),
        ForEverywhere ? "./Demos/Microsoft.Band.Portable.Sample/Microsoft.Band.Portable.Sample.sln" : (ForWindowsOnly ? "./Demos/Microsoft.Band.Portable.Sample/Microsoft.Band.Portable.Sample.Windows.sln" : "./Demos/Microsoft.Band.Portable.Sample/Microsoft.Band.Portable.Sample.Mac.sln"),
    };
    if (ForWindows) {
        solutions.Add("./Demos/RotatingHand/RotatingHand.sln");
    }
    foreach (var solution in solutions) {
        Information("Building {0}...", solution);
        Build(solution);
    }
});

Task("PackageNuGet")
    .WithCriteria(ForWindows)
    .IsDependentOn("Build")
    .Does(() =>
{
    var nugets = new [] {
        "./NuGet/Xamarin.Microsoft.Band.Native.nuspec",
        "./NuGet/Xamarin.Microsoft.Band.nuspec",
    };
    foreach (var nuget in nugets) {
        Information("Packing (NuGet) {0}...", nuget);
        NuGetPack(nuget, new NuGetPackSettings {
            OutputDirectory = outDir,
            Verbosity = NuGetVerbosity.Detailed,
            BasePath = IsRunningOnUnix() ? "././" : "./",
        });
    }
});

Task("PackageComponent")
    .WithCriteria(ForWindows)
    .IsDependentOn("Build")
    .IsDependentOn("PackageNuGet")
    .Does(() =>
{
    Information("Packing Component...");
        
    DeleteFiles("./Component/*.xam");
    PackageComponent("./Component/", new XamarinComponentSettings { ToolPath = XamarinComponentPath });
    
    DeleteFiles("./output/*.xam");
    MoveFiles("./Component/*.xam", outDir);
});

Task("Package")
    .IsDependentOn("Download")
    .IsDependentOn("PackageNuGet")
    .IsDependentOn("PackageComponent")
    .Does(() =>
{
});

Task("Download")
    .WithCriteria(!string.IsNullOrEmpty(sha))
    .WithCriteria(!ForEverywhere)
    .Does(() =>
{
    if (ForWindowsOnly) {
        Information("Connecting to GitHub...");
        var client = new GitHubClient(new ProductHeaderValue("msband-sdk-ci"));
        client.Credentials = new Credentials(GitHubToken);
        
        Information("Loading releases...");
        var releases = client.Release.GetAll(GitHubUser, GitHubRepository).Result;
        var releaseId = releases.Single(r => r.TagName == GitHubBuildTag).Id;
        
        Information("Loading CI release...");
        Release release = null;
        ReleaseAsset asset = null;
        var waitSeconds = 0;
        while (asset == null) {
            release = client.Release.Get(GitHubUser, GitHubRepository, releaseId).Result;
            Information("Loading asset...");
            asset = release.Assets.SingleOrDefault(a => a.Name == GitHubUploadFilename);
            if (asset == null) {
                // only try for 15 minutes
                if (waitSeconds > 15 * 60) {
                    throw new Exception("Unable to download assets, maybe the build has failed.");
                }
                Information("Asset not found, waiting another 30 seconds.");
                waitSeconds += 30;
                System.Threading.Thread.Sleep(1000 * 30);
            }
        }
        Information("Found asset: {0}", asset.Id);
        Information("Url: {0}", asset.BrowserDownloadUrl);
        
        Information("Downloading asset...");
        if (FileExists(outputZip)) {
            DeleteFile(outputZip);
        }
        var url = string.Format("https://api.github.com/repos/{0}/{1}/releases/assets/{2}?access_token={3}", GitHubUser, GitHubRepository, asset.Id, GitHubToken);
        var wc = new WebClient();
        wc.Headers.Add("Accept", "application/octet-stream");
        wc.Headers.Add("User-Agent", "msband-sdk-ci");
        wc.DownloadFile(url, outputZip.FullPath);
        
        Information("Extracting output...");
        CleanDirectory(outDir);
        Unzip(outputZip, outDir);
    }
});

Task("Upload")
    .WithCriteria(!string.IsNullOrEmpty(sha))
    .WithCriteria(!ForEverywhere)
    .IsDependentOn("Build")
    .IsDependentOn("Package")
    .Does(() =>
{
    Information("Connecting to GitHub...");
    var client = new GitHubClient(new ProductHeaderValue("msband-sdk"));
    client.Credentials = new Credentials(GitHubToken);

    Information("Loading releases...");
    var releases = client.Release.GetAll(GitHubUser, GitHubRepository).Result;
    var releaseId = releases.Single(r => r.TagName == GitHubBuildTag).Id;

    Information("Loading CI release...");
    var release = client.Release.Get(GitHubUser, GitHubRepository, releaseId).Result;

    Information("Loading asset...");
    var asset = release.Assets.SingleOrDefault(a => a.Name == GitHubUploadFilename);
    
    if (asset != null) {
        Information("Deleting asset...");
        client.Release.DeleteAsset(GitHubUser, GitHubRepository, asset.Id).Wait();
    } else {
        Information("Asset not found.");
    }

    if (ForMacOnly) {
        Information("Compressing output...");
        if (FileExists(outputZip)) {
            DeleteFile(outputZip);
        }
        Zip(outDir, outputZip);

        Information("Creating asset...");
        var archiveContents = System.IO.File.OpenRead(outputZip.FullPath);
        var assetUpload = new ReleaseAssetUpload {
            FileName = GitHubUploadFilename,
            ContentType = "application/zip",
            RawData = archiveContents
        };
        
        Information("Uploading asset...");
        asset = client.Release.UploadAsset(release, assetUpload).Result;
        Information("Uploaded asset: {0}", asset.Id);
        Information("Url: {0}", asset.BrowserDownloadUrl);
        
        // Information("Triggering AppVeyor build...");
        // AppVeyorStartBuildSpecificCommit(AppVeyorToken, GitHubUser, GitHubRepository, sha, branch);
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Download")
    .IsDependentOn("Build")
    .IsDependentOn("Package")
    .IsDependentOn("BuildSamples")
    .IsDependentOn("Upload");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
