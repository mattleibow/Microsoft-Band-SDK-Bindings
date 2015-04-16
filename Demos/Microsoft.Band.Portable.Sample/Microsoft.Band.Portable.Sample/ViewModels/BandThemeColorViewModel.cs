using System;

using Microsoft.Band.Portable.Personalization;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class BandThemeColorViewModel : BaseViewModel
    {
        private Func<BandColor> getter;
        private Action<BandColor> setter;

        private string name;

        public BandThemeColorViewModel(string name, Func<BandColor> getter, Action<BandColor> setter)
        {
            this.getter = getter;
            this.setter = setter;

            Name = name;
            Color = getter();
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public BandColor Color
        {
            get { return getter(); }
            set
            {
                setter(value);
                OnPropertyChanged("Color");
            }
        }
    }
}
