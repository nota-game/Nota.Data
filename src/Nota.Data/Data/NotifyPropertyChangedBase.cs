using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nota.Data
{
    
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        private protected NotifyPropertyChangedBase()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;

        private protected virtual void FirePropertyChanged([CallerMemberName]string proeprty = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proeprty));
        }

    }
}