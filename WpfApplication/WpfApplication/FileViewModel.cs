using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WpfApplication
{
    public class FileViewModel : INotifyPropertyChanged
    {
        public FileViewModel()
        {
            FileModel mwm = new FileModel();
            OC = mwm;
        }

        private ObservableCollection<Files> _OC = null;

        public ObservableCollection<Files> OC
        {
            get { return _OC; }
            set
            {
                _OC = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
