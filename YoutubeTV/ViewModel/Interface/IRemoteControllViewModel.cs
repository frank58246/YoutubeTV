using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Windows.Input;
using YoutubeTV.Model;

namespace YoutubeTV.ViewModel.Interface
{
    public interface IChannelViewModel
    {
        public ICommand GoPrivious { get; }

        public ICommand GoNext { get; }

        /// <summary>
        /// Switch between two channel
        /// </summary>
        public ICommand Switch { get; }

        public ChannelModel CurrentChannel { get; set; }

        public string ChangingChannel { get; set; }

        public void HandleNumKeyDown(int num);

        public void HandleEnterKeyDown();
    }
}