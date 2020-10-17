using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Windows.Input;
using YoutubeTV.Command;
using YoutubeTV.Model;
using YoutubeTV.Providers.Interface;
using YoutubeTV.ViewModel.Interface;

namespace YoutubeTV.ViewModel.Implement
{
    /// <summary>
    /// RemoteControllViewModel
    /// </summary>
    public class ChannelViewModel : ViewModelBase, IChannelViewModel

    {
        private List<ChannelModel> _allChannel;

        private ChannelModel _currentChannel;

        private int _lastChannelIndex;

        private int _currentIndex;

        private string _changingChannel = "";

        public int CurrentIndex
        {
            get { return this._currentIndex; }
            set
            {
                if (this._currentIndex == value)
                {
                    return;
                }

                this._lastChannelIndex = this._currentIndex;
                this._currentIndex = value;
            }
        }

        public ChannelViewModel()
        {
        }

        public ChannelViewModel(IChannelProvider channelProvider)
        {
            _allChannel = channelProvider.GetAllChannel();
            _currentChannel = _allChannel[CurrentIndex];
        }

        public ChannelModel CurrentChannel
        {
            get { return this._currentChannel; }
            set
            {
                this._currentChannel = value;
                OnPropertyChanged();
            }
        }

        public string ChangingChannel
        {
            get { return this._changingChannel; }
            set
            {
                this._changingChannel = value;
                OnPropertyChanged();
            }
        }

        public ICommand GoPrivious => new RelayCommand(GoPreviousChannel, CanDo);

        public ICommand GoNext => new RelayCommand(GoNextChannel, CanDo);

        public ICommand Switch => new RelayCommand(SwitchChannel, CanDo);

        public void HandleNumKeyDown(int num)
        {
            this.ChangingChannel += num.ToString();
        }

        public void HandleEnterKeyDown()
        {
            this.ChangingChannel = "0";
        }

        private void GoPreviousChannel()
        {
            CurrentIndex -= 1;
            if (CurrentIndex < 0)
            {
                CurrentIndex += _allChannel.Count;
            }

            CurrentChannel = _allChannel[CurrentIndex];
        }

        private void GoNextChannel()
        {
            CurrentIndex += 1;
            CurrentIndex %= _allChannel.Count;
            CurrentChannel = _allChannel[CurrentIndex];
        }

        private void SwitchChannel()
        {
            if (this._lastChannelIndex < 0)
            {
                return;
            }

            // swap
            var temp = this._currentIndex;
            this._currentIndex = this._lastChannelIndex;
            this._lastChannelIndex = temp;

            this.CurrentChannel = _allChannel[CurrentIndex];
        }

        private bool CanDo()
        {
            return _allChannel != null && _allChannel.Count > 0;
        }
    }
}