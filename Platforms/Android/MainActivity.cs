﻿using Android.App;
using Android.Content.PM;
using Android.OS;

namespace SimpleCompass
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait )]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}
