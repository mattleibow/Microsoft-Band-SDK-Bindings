using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
    public partial class PersonalizationPage : BaseClientContentPage
    {
        public PersonalizationPage(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            InitializeComponent();

            ViewModel = new PersonalizationViewModel(info, bandClient);
        }

        public async void ChangeColorButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var themeColor = (BandThemeColorViewModel)button.CommandParameter;

            var picker = new ColorPickerPage { Color = themeColor.Color };
            picker.Picked += delegate { themeColor.Color = picker.Color; };

            await Navigation.PushModalAsync(picker);
        }
    }
}
