App Indexing helps you get your app found in Google Search. Once your app is indexed, mobile users who search for content related to your app can see an install button to your Android app in Search results. This helps you increase your install base.



Required Android API Levels
===========================

We recommend setting your app's *Target Framework* and *Target Android version* to **Android 5.0 (API Level 21)** or higher in your app project settings.

This Google Play Service SDK's requires a *Target Framework* of at least Android 4.1 (API Level 16) to compile.

You may still set a lower *Minimum Android version* (as low as Android 2.3 - API Level 9) so your app will run on older versions of Android, however you must ensure you do not use any API's at runtime that are not available on the version of Android your app is running on.



Google Developers Console Setup
=================================

Many of the Google Play Services SDK's require that you create an application inside the [Google Developers Console][1].  Visit the [Google Developers Console][1] to create a project for your application.

Once you have created a project for your Android app, enable the necessary APIs in the developer console for the Google Play Services APIs you will be using in your app.





Credentials
-----------

Some Google Play Services APIs require an *API Key* or an *OAuth 2.0 Client ID* (or both) to be setup to allow your app to make authenticated calls against the API.

In the Developers Console, in your app's Project, under the *APIs & auth* section, go to *Credentials*.





### OAuth 2.0 Client ID

This SDK/API requires an OAuth 2.0 Client ID to be created:

  1. *Add credentials* button and then *OAuth 2.0 client ID*
  2. Choose *Android* for the Application type.
  3. [Find your SHA-1 fingerprints][2]
  4. Enter the SHA-1 fingerprint of your app's debug keystore
  5. Enter your android app's package name as found in your *AndroidManifest.xml* file
  6. Check *Enable Deep Linking*
  7. Click *Create*
  8. Repeat this process with the package name and SHA-1 of the keystore file you will be signing your app's Release builds with

NOTE: Once you have created the OAuth 2.0 Client ID, you typically do not need to do anything with the generated *Client ID* value since your keystore SHA-1 fingerprints will identify the client id for you.




Samples
=======

You can find a Sample Application within each Google Play Services component.  The sample will demonstrate the necessary configuration and some basic API usages.






Learn More
==========

You can learn more about the various Google Play Services SDKs & APIs by visiting the official [Google APIs for Android][3] documentation


You can learn more about Google Play Services AppIndexing by visiting the official [App Indexing SDK for Android](https://developers.google.com/app-indexing/) documentation.



[1]: https://console.developers.google.com/ "Google Developers Console"
[2]: https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/MD5_SHA1/ "Finding your SHA-1 Fingerprints"
[3]: https://developers.google.com/android/ "Google APIs for Android"

