using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using YoutubeTV.Model;

namespace YoutubeTV.Providers.Interface
{
    public interface IChannelProvider
    {
        List<ChannelModel> GetAllChannel();
    }
}