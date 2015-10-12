using System;
using System.Threading.Tasks;

namespace Microsoft.Band
{
    public static class BandClientExtensions
    {
        public static async Task<ConnectionState> ConnectTaskAsync(this IBandClient client)
        {
			return (ConnectionState)await client.ConnectAsync().AsTask();
        }

        public static Task DisconnectTaskAsync(this IBandClient client)
        {
            return client.DisconnectAsync().AsTask();
        }

        public static async Task<string> GetFirmwareVersionTaskAsync(this IBandClient client)
        {
            return (string)await client.GetFirmwareVersionAsync().AsTask();
        }

        public static async Task<string> GetHardwareVersionTaskAsync(this IBandClient client)
        {
            return (string)await client.GetHardwareVersionAsync().AsTask();
        }

        public static IBandConnectionCallback RegisterConnectionCallback(this IBandClient client, Action<ConnectionState> callback)
        {
            var instance = new BandConnectionCallback(callback);
            client.RegisterConnectionCallback(instance);
            return instance;
        }

        internal sealed class BandConnectionCallback : Java.Lang.Object, IBandConnectionCallback
        {
            private readonly Action<ConnectionState> callback;

            public BandConnectionCallback(Action<ConnectionState> callback)
            {
                this.callback = callback;
            }

            public void OnStateChanged(ConnectionState connectionState)
            {
                if (callback != null)
                {
                    callback(connectionState);
                }
            }
        }
    }
}
