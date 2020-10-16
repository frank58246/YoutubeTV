using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Shapes;
using YoutubeTV.Model;
using YoutubeTV.Providers.Interface;

namespace YoutubeTV.Providers.Implement
{
    public class ChannelProvider : IChannelProvider
    {
        public List<ChannelModel> GetAllChannel()
        {
            string line;
            var result = new List<ChannelModel>();
            using (var file = new StreamReader(@"File\allChannel.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    var contents = line.Split(";");
                    if (contents.Length == 3)
                    {
                        var channel = new ChannelModel
                        {
                            Number = int.Parse(contents[0]),
                            Url = contents[1],
                            Name = contents[2]
                        };

                        result.Add(channel);
                    }
                }
            }
            result.ForEach(x => x.Url += "?controls=0&autoplay=1");

            return result;
        }
    }
}