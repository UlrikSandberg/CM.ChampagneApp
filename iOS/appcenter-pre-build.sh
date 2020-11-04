#!/usr/bin/env bash


if [ "$APPCENTER_BRANCH" != "master" ];
then
    echo "Replacing bundle values $PRODUCT_NAME, $BUNDLE_IDENTIFIER"
    plutil -replace CFBundleName -string "$PRODUCT_NAME" Info.plist
    plutil -replace CFBundleDisplayName -string "$PRODUCT_NAME" Info.plist
    plutil -replace CFBundleIdentifier -string "$BUNDLE_IDENTIFIER" Info.plist
fi