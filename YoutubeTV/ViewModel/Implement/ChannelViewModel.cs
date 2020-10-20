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
        private IChannelProvider _channelProvider;

        private List<ChannelModel> _allChannel;

        private ChannelModel _currentChannel;

        private int _lastChannelIndex;

        private int _currentIndex;

        private string _changingChannel = "";

        public ChannelViewModel()
        {
        }

        public Action OnChangingChannel { get; set; }

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

        public ChannelViewModel(IChannelProvider channelProvider)
        {
            this._allChannel = channelProvider.GetAllChannel();
            this._channelProvider = channelProvider;

            // Get last viewd channel
            var lastChnnel = channelProvider.GetLastChannel();
            for (int i = 0; i < _allChannel.Count(); i++)
            {
                if (_allChannel[i].Number == lastChnnel)
                {
                    CurrentIndex = i;
                    break;
                }
            }
            _currentChannel = _allChannel[CurrentIndex];
        }

        public ChannelModel CurrentChannel
        {
            get { return this._currentChannel; }
            set
            {
                this._currentChannel = value;
                this._channelProvider.SetLastChannel(this._currentChannel.Number);
                OnPropertyChanged();
                if (this.OnChangingChannel != null)
                {
                    OnChangingChannel.Invoke();
                }
            }
        }

        public string ChangingChannel
        {
            get { return this._changingChannel; }
            set
            {
                this._changingChannel = value;
                OnPropertyChanged();
                if (this.OnChangingChannel != null)
                {
                    OnChangingChannel.Invoke();
                }
            }
        }

        public ICommand GoPrivious => new RelayCommand(GoPreviousChannel, CanDo);

        public ICommand GoNext => new RelayCommand(GoNextChannel, CanDo);

        public ICommand Switch => new RelayCommand(SwitchChannel, CanDo);

        public void HandleNumKeyDown(int num)
        {
            if (this.ChangingChannel.Count() >= 3)
            {
                var target = int.Parse(this.ChangingChannel);
                this.ChangeChannel(target);
                this.ChangingChannel = num.ToString();
            }
            else
            {
                this.ChangingChannel += num.ToString();
            }
        }

        public void HandleEnterKeyDown()
        {
            if (int.TryParse(this.ChangingChannel, out var target))
            {
                this.ChangeChannel(target);
            }
            else
            {
                this.ChangingChannel = "";
            }
        }

        private void ChangeChannel(int target)
        {
            var channel = this._allChannel.Where(x => x.Number == target).FirstOrDefault();
            if (channel != null)
            {
                var index = _allChannel.IndexOf(channel);
                this.CurrentIndex = index;
                this.CurrentChannel = this._allChannel[CurrentIndex];
            }

            this.ChangingChannel = "";
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