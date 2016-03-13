#!/bin/bash

# #
# # Remove the custom keychain
# #
# echo Remove the custom keychain
# security delete-keychain ios-build.keychain

#
# Remove the provisioning profile
#
# echo Remove the provisioning profile
rm -f "~/Library/MobileDevice/Provisioning Profiles/iOSDeveloper.mobileprovision"

# #
# # Deactivate the Xamarin license
# # 
# echo Deactivate the Xamarin license
# mono downloads/XamarinActivator/tools/XamarinActivator.exe deactivate -x ios -e "${XamarinEmail}" -p "${XamarinPassword}" -k "${XamarinApiKey}" -u "TravisCI"
