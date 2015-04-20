using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using Xamarin.Forms;

using Microsoft.Band.Portable.Notifications;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class VibrationsViewModel : BaseClientViewModel
    {
        private BandNotificationManager notifiactionManager;

        private int vibrationIndex;

		public VibrationsViewModel(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            notifiactionManager = bandClient.NotificationManager;

            VibrateCommand = new Command(async () =>
            {
                await notifiactionManager.VibrateAsync((VibrationType)vibrationIndex);
            });
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
