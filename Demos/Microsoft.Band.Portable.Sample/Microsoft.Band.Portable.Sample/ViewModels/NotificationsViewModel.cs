using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

using Plugin.Media;
using Xamarin.Forms;

using Microsoft.Band.Portable.Notifications;
using Microsoft.Band.Portable.Personalization;
using Microsoft.Band.Portable.Tiles;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class NotificationsViewModel : BaseClientViewModel
    {
        private BandNotificationManager notifiactionManager;
        private BandTile tile;

        private string title;
        private string body;

        public NotificationsViewModel(BandDeviceInfo info, BandClient bandClient, BandTile tile)
            : base(info, bandClient)
        {
            notifiactionManager = bandClient.NotificationManager;
            this.tile = tile;

            SendMessageCommand = new Command(async () =>
            {
                await notifiactionManager.SendMessageAsync(tile.Id, Title, Body, DateTime.Now);
            });
            SendMessageWithDialogCommand = new Command(async () =>
            {
                await notifiactionManager.SendMessageAsync(tile.Id, Title, Body, DateTime.Now, true);
            });
            ShowDialogCommand = new Command(async () =>
            {
                await notifiactionManager.ShowDialogAsync(tile.Id, Title, Body);
            });
        }

        public string Title
        {
            get { return title ?? "<title>"; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string Body
        {
            get { return body ?? "<body>"; }
            set
            {
                if (body != value)
                {
                    body = value;
                    OnPropertyChanged("Body");
                }
            }
        }

        public ICommand SendMessageCommand { get; private set; }
        public ICommand SendMessageWithDialogCommand { get; private set; }
        public ICommand ShowDialogCommand { get; private set; }
    }
}
