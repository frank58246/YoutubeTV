using System;
using System.Collections.Generic;
using System.Text;
using YoutubeTV.Model;
using YoutubeTV.Providers.Interface;

namespace YoutubeTV.Providers.Implement
{
    public class ChannelProvider : IChannelProvider
    {
        public List<ChannelModel> GetAllChannel()
        {
            var allChannel = new List<ChannelModel>()
            {
                new ChannelModel
                {
                    Name = "公視",
                    Number = 13,
                    Url = "https://www.youtube.com/embed/ED4QXd5xAco?controls=0"
                },
                 new ChannelModel
                {
                    Name = "TVBS",
                    Number = 55,
                    Url = "https://www.youtube.com/embed/A4FbB8UhNRs?controls=0"
                },
                 new ChannelModel
                 {
                    Name = "東森",
                    Number = 51,
                    Url = "https://www.youtube.com/embed/wUPPkSANpyo?controls=0"
                 }
            };

            allChannel.ForEach(x => x.Url += "&autoplay=1");

            return allChannel;
        }
    }
}