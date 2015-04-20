using System;

using Microsoft.Band.Portable.Sample.ViewModels;

namespace Microsoft.Band.Portable.Sample
{
	public partial class VibrationsPage : BaseClientContentPage
	{
		public VibrationsPage(BandDeviceInfo info, BandClient bandClient)
			: base(info, bandClient)
		{
			InitializeComponent();

			ViewModel = new VibrationsViewModel(info, bandClient);

			foreach (var type in VibrationsViewModel.GetVibrationTypes())
			{
				vibrationTypesPicker.Items.Add(type);
			}
		}
	}
}
	