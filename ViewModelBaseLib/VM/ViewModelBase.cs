using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelBaseLib.VM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string Name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public bool Set<T>(ref T field, T value, [CallerMemberName] string name = "")
        {
            if (field.Equals(value))
            {
                return false;
            }
            else
            {
                field = value;

                OnPropertyChanged(name);

                return true;
            }
        }
    }
}
