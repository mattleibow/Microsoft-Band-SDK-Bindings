using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util.Concurrent;

namespace Microsoft.Band
{
    public static class BandPendingResultExtensions
    {
        public static void RegisterResultCallback(this IBandPendingResult result, Action<Java.Lang.Object, Java.Lang.Throwable> callback)
        {
            result.RegisterResultCallback(new BandResultCallback(callback));
        }

        public static void RegisterResultCallback(this IBandPendingResult result, Action<Java.Lang.Object, Java.Lang.Throwable> callback, long value, TimeUnit unit)
        {
            result.RegisterResultCallback(new BandResultCallback(callback), value, unit);
        }

        public static Task<Java.Lang.Object> AsTask(this IBandPendingResult result)
        {
            var t = new TaskCompletionSource<Java.Lang.Object>();
            result.RegisterResultCallback((r, f) =>
            {
                if (f != null)
                    t.SetException(f);
                else
                    t.SetResult(r);
            });
            return t.Task;
        }

        public static Task<Java.Lang.Object> AsTask(this IBandPendingResult result, long timeoutMilliseconds)
        {
            var t = new TaskCompletionSource<Java.Lang.Object>();
            result.RegisterResultCallback((r, f) =>
            {
                if (f != null)
                {
                    if (f is Java.Util.Concurrent.TimeoutException)
                        t.SetCanceled();
                    else
                        t.SetException(f);
                }
                else
                {
                    t.SetResult(r);
                }
            }, timeoutMilliseconds, TimeUnit.Milliseconds);
            return t.Task;
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