using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Microsoft.Band.Portable.Sample
{
	public class BasePickerPage : BaseContentPage
	{
		public BasePickerPage()
		{
			var doneButton = new ToolbarItem
			{
				Text = "Done",
                Icon = OnPlatform(
                    iOS: (FileImageSource)FileImageSource.FromFile("Icons/Done.png"),
                    Android: (FileImageSource)FileImageSource.FromFile("Done.png"),
                    Windows: (FileImageSource)FileImageSource.FromFile("Assets/Icons/Done.png"))
            };
			doneButton.Clicked += PickButtonClicked;
			ToolbarItems.Add(doneButton);

			var cancelButton = new ToolbarItem
			{
				Text = "Cancel",
                Icon = OnPlatform(
                    iOS: (FileImageSource)FileImageSource.FromFile("Icons/Cancel.png"),
                    Android: (FileImageSource)FileImageSource.FromFile("Cancel.png"),
                    Windows: (FileImageSource)FileImageSource.FromFile("Assets/Icons/Cancel.png"))
            };
			cancelButton.Clicked += CancelButtonClicked;
			ToolbarItems.Add(cancelButton);
		}

		protected override bool OnBackButtonPressed()
		{
			OnCanceled();

			return base.OnBackButtonPressed();
		}

		public event EventHandler Picked;

		protected virtual void OnPicked()
		{
			var handler = Picked;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		public event EventHandler Canceled;

		protected virtual void OnCanceled()
		{
			var handler = Canceled;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		protected async void PickButtonClicked(object sender, EventArgs e)
		{
			OnPicked();

			await PopAsync();
		}

		protected async void CancelButtonClicked(object sender, EventArgs e)
		{
			OnCanceled();

			await PopAsync();
		}

		private async Task PopAsync()
		{
			if (Navigation.ModalStack.Count > 0 &&
				Navigation.ModalStack[Navigation.ModalStack.Count - 1] == this)
			{
				await Navigation.PopModalAsync();
			}
			else
			{
				await Navigation.PopAsync();
			}
		}
	}
}
