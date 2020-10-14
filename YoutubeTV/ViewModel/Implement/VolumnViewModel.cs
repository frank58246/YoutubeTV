using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using YoutubeTV.Command;
using YoutubeTV.Providers.Interface;
using YoutubeTV.ViewModel.Interface;

namespace YoutubeTV.ViewModel.Implement
{
    public class VolumnViewModel : ViewModelBase, IVolumnViewModel
    {
        private IUserConfigProvider _userConfigProvider;

        private List<int> _allVolumns;

        private int _currentIndex;

        private int _currentVolumn;

        public VolumnViewModel(IUserConfigProvider userConfigProvider)
        {
            this._userConfigProvider = userConfigProvider;
            this._currentIndex = this._userConfigProvider.GetLastVolumnIndex();
            this._currentVolumn = this._allVolumns[_currentIndex];
            // TODO consider use iProvider??
            this._allVolumns = new List<int>()
            {
                0,1,2
            };
            //OnPropertyChanged();
        }

        public ICommand UpLevel => new RelayCommand(this.AddLevel, this.CanDo);

        public ICommand DownLevel => new RelayCommand(this.MinusLevel, this.CanDo);

        private void AddLevel()
        {
            if (this._currentIndex < this._allVolumns.Count - 1)
            {
                this._currentIndex++;
            }
            this.CurrentLevel = this._allVolumns[this._currentIndex];
        }

        private void MinusLevel()
        {
            if (this._currentIndex > 0)
            {
                this._currentIndex++;
            }
            this.CurrentLevel = this._allVolumns[this._currentIndex];
        }

        private bool CanDo()
        {
            return _allVolumns.Count > 0;
        }

        public int CurrentLevel
        {
            get { return this._currentVolumn; }
            set
            {
                this._currentVolumn = value;
                OnPropertyChanged();
            }
        }
    }
}