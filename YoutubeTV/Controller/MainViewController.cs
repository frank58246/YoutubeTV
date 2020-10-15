using System;
using System.Collections.Generic;
using System.Text;
using YoutubeTV.ViewModel.Interface;

namespace YoutubeTV.Controller
{
    public class MainViewController
    {
        public IChannelViewModel ChannelViewModel { get; set; }

        public IVolumeViewModel VolumeViewModel { get; set; }

        public MainViewController()
        {
        }

        public MainViewController(IChannelViewModel channelViewModel,
                                  IVolumeViewModel volumeViewModel)
        {
            this.ChannelViewModel = channelViewModel;
            this.VolumeViewModel = volumeViewModel;
        }
    }
}