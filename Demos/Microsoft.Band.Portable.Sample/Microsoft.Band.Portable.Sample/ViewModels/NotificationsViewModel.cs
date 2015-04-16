using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

using Media.Plugin;
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

        private int vibrationIndex;
        private string title;
        private string body;

        public NotificationsViewModel(BandDeviceInfo info, BandClient bandClient, BandTile tile)
            : base(info, bandClient)
        {
            notifiactionManager = bandClient.NotificationManager;
            this.tile = tile;

            VibrateCommand = new Command(async () =>
            {
                await notifiactionManager.VibrateAsync((VibrationType)vibrationIndex);
            });
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
            get { return title; }
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
            get { return body; }
            set
            {
                if (body != value)
                {
                    body = value;
                    OnPropertyChanged("Body");
                }
            }
        }

        public int VibrationIndex
        {
            get { return vibrationIndex; }
            set
            {
                if (vibrationIndex != value)
                {
                    vibrationIndex = value;
                    OnPropertyChanged("VibrationIndex");
                }
            }
        }

        public ICommand VibrateCommand { get; private set; }
        public ICommand SendMessageCommand { get; private set; }
        public ICommand SendMessageWithDialogCommand { get; private set; }
        public ICommand ShowDialogCommand { get; private set; }

        public static ObservableCollection<string> GetVibrationTypes()
        {
            var names = Enum.GetNames(typeof(VibrationType));
            var split = names.Select(n =>
                string.Concat(n.ToCharArray().Select(c =>
                    char.IsUpper(c) ? " " + c : c.ToString())));
            return new ObservableCollection<string>(split.ToList());
        }
    }
}
