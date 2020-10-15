using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using YoutubeTV.Command;
using YoutubeTV.Providers.Interface;
using YoutubeTV.ViewModel.Interface;

namespace YoutubeTV.ViewModel.Implement
{
    public class VolumeViewModel : ViewModelBase, IVolumeViewModel
    {
        private IUserConfigProvider _userConfigProvider;

        private List<int> _allVolumes;

        private int _currentIndex;

        private int _currentVolume;

        private string _currentLevlPicture;

        public VolumeViewModel(IUserConfigProvider userConfigProvider)
        {
            // TODO consider use iProvider??
            this._allVolumes = new List<int>()
            {
                0,1,2
            };

            this._userConfigProvider = userConfigProvider;
            this._currentIndex = this._userConfigProvider.GetLastVolumeIndex();
            this._currentVolume = this._allVolumes[_currentIndex];
            this._currentLevlPicture = $"../../Images_{CurrentLevel}.jpg";
        }

        public ICommand UpLevel => new RelayCommand(this.AddLevel, this.CanDo);

        public ICommand DownLevel => new RelayCommand(this.MinusLevel, this.CanDo);

        private void AddLevel()
        {
            if (this._currentIndex < this._allVolumes.Count - 1)
            {
                this._currentIndex++;
            }
            this.CurrentLevel = this._allVolumes[this._currentIndex];
            this.CurrentLevlPicture = $"../../Images/volume_{CurrentLevel}.jpg";
        }

        private void MinusLevel()
        {
            if (this._currentIndex > 0)
            {
                this._currentIndex--;
            }
            this.CurrentLevel = this._allVolumes[this._currentIndex];
            this.CurrentLevlPicture = $"../../Images/volume_{CurrentLevel}.jpg";
        }

        private bool CanDo()
        {
            return _allVolumes.Count > 0;
        }

        public int CurrentLevel
        {
            get { return this._currentVolume; }
            set
            {
                this._currentVolume = value;
                OnPropertyChanged();
            }
        }

        public string CurrentLevlPicture
        {
            get { return this._currentLevlPicture; }
            set
            {
                this._currentLevlPicture = value;
                OnPropertyChanged();
            }
        }
    }
}