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

        public virtual Task Prepare()
        {
            // this fires each time the view model appears
            return Task.FromResult(true);
        }

        public virtual Task CleanUp()
        {
            // this fires each time the view model disappears
            return Task.FromResult(true);
        }

        public virtual Task Destroy()
        {
            // this fires each time the view model disappears
            return Task.FromResult(true);
        }
    }
}