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
                    Url = "https://youtu.be/gPpZJlE0Ca8"
                },
                 new ChannelModel
                {
                    Name = "TVBS",
                    Number = 55,
                    Url = "https://www.youtube.com/watch?v=XDXJSr6vNdE&ab_channel=tws474"
                }
            };

            return allChannel;
        }
    }
}