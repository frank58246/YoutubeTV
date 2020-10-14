using System;
using System.Collections.Generic;
using System.Text;
using YoutubeTV.ViewModel.Interface;

namespace YoutubeTV.Controller
{
    public class MainViewController
    {
        public IChannelViewModel ChannelViewModel { get; set; }

        public IVolumnViewModel VolumnViewModel { get; set; }

        public MainViewController()
        {
        }

        public MainViewController(IChannelViewModel channelViewModel,
                                  IVolumnViewModel volumnViewModel)
        {
            this.ChannelViewModel = channelViewModel;
            this.VolumnViewModel = volumnViewModel;
        }
    }
}