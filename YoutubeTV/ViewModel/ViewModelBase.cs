using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace YoutubeTV.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string PropertyName = null)
        {
            if (PropertyName != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}