using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelBaseLib.VM
{
    public class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public virtual string this[string columnName] => throw new NotImplementedException();

        public virtual string Error => throw new NotImplementedException();

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

        public bool[] m_ValidArray;

        public bool CheckValidArray(int start, int end)
        {
            for (; start < end; start++)
            {
                if (!m_ValidArray[start])
                {
                    return false;
                }
            }

            return true;
        }


    }
}
