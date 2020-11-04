#!/usr/bin/env bash

# Example: Change bundle name of an iOS app for non-production
if [ "$APPCENTER_BRANCH" != "master" ];
then
    echo Replacing bundle values ($PRODUCT_NAME, $BUNDLE_IDENTIFIER)
    plutil -replace CFBundleName -string "\$(PRODUCT_NAME)" $APPCENTER_SOURCE_DIRECTORY/CM.ChampagneApp/Info.plist
    plutil -replace CFBundleDisplayName -string "\$(PRODUCT_NAME)" $APPCENTER_SOURCE_DIRECTORY/CM.ChampagneApp/Info.plist
    plutil -replace CFBundleIdentifier -string "\$(BUNDLE_IDENTIFIER)" $APPCENTER_SOURCE_DIRECTORY/CM.ChampagneApp/Info.plist
fi