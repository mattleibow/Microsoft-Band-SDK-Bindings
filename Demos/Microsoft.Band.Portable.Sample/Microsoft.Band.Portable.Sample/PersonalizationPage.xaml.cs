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
		private PersonalizationViewModel personalizationViewModel;

        public PersonalizationPage(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            InitializeComponent();

			personalizationViewModel = new PersonalizationViewModel(info, bandClient);
			ViewModel = personalizationViewModel;
		}

		public async void ChangeThemeButtonClicked(object sender, EventArgs e)
		{
			var picker = new ThemePickerPage { Theme = personalizationViewModel.BandTheme };
			picker.Picked += delegate { personalizationViewModel.BandTheme = picker.Theme; };

			await Navigation.PushAsync(picker);
		}
    }
}
