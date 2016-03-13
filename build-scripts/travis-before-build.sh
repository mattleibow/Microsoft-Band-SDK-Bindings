#!/bin/bash

#
# Variables
#
export MonoVersion=4.3.2
export MonoTouchVersion=9.4.2.27
export ActivatorVersion=1.0.0

#
# Encrypting the certificates and profiles
# 
#openssl aes-256-cbc -k "${CertificatePassword}" -in build-scripts/iOSDeveloper.mobileprovision -out build-scripts/iOSDeveloper.mobileprovision.enc -a
#openssl aes-256-cbc -k "${CertificatePassword}" -in build-scripts/iOSDeveloper.cer -out build-scripts/iOSDeveloper.cer.enc -a
#openssl aes-256-cbc -k "${CertificatePassword}" -in build-scripts/iOSDeveloper.p12 -out build-scripts/iOSDeveloper.p12.enc -a

#
# Decrypting the certificates and profiles
#
echo Decrypting the certificates and profiles
openssl aes-256-cbc -k "${CertificatePassword}" -in build-scripts/iOSDeveloper.mobileprovision.enc -d -a -out build-scripts/iOSDeveloper.mobileprovision
openssl aes-256-cbc -k "${CertificatePassword}" -in build-scripts/iOSDeveloper.cer.enc -d -a -out build-scripts/iOSDeveloper.cer
openssl aes-256-cbc -k "${CertificatePassword}" -in build-scripts/iOSDeveloper.p12.enc -d -a -out build-scripts/iOSDeveloper.p12

#
# Add the certificates to the keychain
#
echo Add the certificates to the keychain
# # Create a custom keychain
# security create-keychain -p "${CertificatePassword}" ios-build.keychain
# # Make the custom keychain default, so xcodebuild will use it for signing
# security default-keychain -s ios-build.keychain
# # Unlock the keychain
# security unlock-keychain -p "${CertificatePassword}" ios-build.keychain
# Set keychain timeout to 1 hour for long builds
security set-keychain-settings -t 3600 -l ~/Library/Keychains/login.keychain
# Add certificates to keychain and allow codesign to access them
#security import ./apple.cer -k ~/Library/Keychains/ios-build.keychain -T /usr/bin/codesign
security import ./build-scripts/iOSDeveloper.cer -k ~/Library/Keychains/login.keychain -T /usr/bin/codesign
security import ./build-scripts/iOSDeveloper.p12 -k ~/Library/Keychains/login.keychain -T /usr/bin/codesign -P "${CertificatePassword}"

#
# Put the provisioning profile in place
#
echo Put the provisioning profile in place
mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
cp ./build-scripts/iOSDeveloper.mobileprovision ~/Library/MobileDevice/Provisioning\ Profiles/
[ -f ~/Library/MobileDevice/Provisioning\ Profiles/iOSDeveloper.mobileprovision ] && echo "Added provisioning profile" || echo "Error adding provisioning profile"

#
# Download and install Mono and Xamarin.iOS
#
echo Download and install Mono and Xamarin.iOS
wget -nc -P downloads "http://download.mono-project.com/archive/${MonoVersion}/macos-10-universal/MonoFramework-MDK-${MonoVersion}.macos10.xamarin.universal.pkg"
sudo installer -pkg "downloads/MonoFramework-MDK-${MonoVersion}.macos10.xamarin.universal.pkg" -target / 
wget -nc -P downloads "http://download.xamarin.com/MonoTouch/Mac/monotouch-${MonoTouchVersion}.pkg"
sudo installer -pkg "downloads/monotouch-${MonoTouchVersion}.pkg" -target /

#
# Activate the Xamarin license
#
echo Activate the Xamarin license
wget -nc -O "downloads/XamarinActivator-${ActivatorVersion}.nupkg" "https://www.nuget.org/api/v2/package/XamarinActivator/${ActivatorVersion}"
unzip -o -d downloads/XamarinActivator "downloads/XamarinActivator-${ActivatorVersion}.nupkg"
mono downloads/XamarinActivator/tools/XamarinActivator.exe activate -x ios -e "${XamarinEmail}" -p "${XamarinPassword}" -k "${XamarinApiKey}" -u "TravisCI" 
