using System.ComponentModel;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual async Task Prepare()
        {
            // this fires each time the view model appears
        }

        public virtual async Task CleanUp()
        {
            // this fires each timethe view model disappears
        }

        public virtual async Task Destroy()
        {
            // this fires each timethe view model disappears
        }
    }
}