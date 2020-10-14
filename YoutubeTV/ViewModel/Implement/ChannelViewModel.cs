using System;
using System.Collections.Generic;
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

        private int _currentIndex;

        public ChannelViewModel()
        {
        }

        public ChannelViewModel(IChannelProvider channelProvider)
        {
            this._allChannel = channelProvider.GetAllChannel();
            _currentChannel = _allChannel[_currentIndex];
        }

        public ChannelModel CurrentChannel
        {
            get { return this._currentChannel; }
            set
            {
                this._currentChannel = value;
                OnPropertyChanged();
                //OnChannelChanged(_currentChannel);
            }
        }

        public ICommand GoPrivious => new RelayCommand(GoPreviousChannel, CanDo);

        public ICommand GoNext => new RelayCommand(GoNextChannel, CanDo);

        private void GoPreviousChannel()
        {
            _currentIndex -= 1;
            if (_currentIndex < 0)
            {
                _currentIndex += _allChannel.Count;
            }

            CurrentChannel = _allChannel[_currentIndex];
        }

        private void GoNextChannel()
        {
            _currentIndex += 1;
            _currentIndex %= _allChannel.Count;
            CurrentChannel = _allChannel[_currentIndex];
        }

        private bool CanDo()
        {
            return _allChannel != null && _allChannel.Count > 0;
        }
    }
}