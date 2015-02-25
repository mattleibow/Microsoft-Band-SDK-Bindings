using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Microsoft.Band
{
    public static class BandPendingResultExtensions
    {
        public static void RegisterResultCallback(this IBandPendingResult result, Action<Java.Lang.Object, Java.Lang.Throwable> callback)
        {
            result.RegisterResultCallback(new BandResultCallback(callback));
        }
    }

    internal class BandResultCallback : Java.Lang.Object, IBandResultCallback
    {
        private readonly Action<Java.Lang.Object, Java.Lang.Throwable> callback;

        public BandResultCallback(Action<Java.Lang.Object, Java.Lang.Throwable> callback)
        {
            this.callback = callback;
        }

        public void OnResult(Java.Lang.Object p0, Java.Lang.Throwable p1)
        {
            callback(p0, p1);
        }
    }
}