using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows.Shapes;
using YoutubeTV.Constants;
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

        public int GetLastChannel()
        {
            var key = ProjectConstants.UserSetting.LastChannel;
            var value = ConfigurationManager.AppSettings.Get(key);

            int.TryParse(value, out var lastChannel);
            return lastChannel;
        }

        public void SetLastChannel(int value)
        {
            var key = ProjectConstants.UserSetting.LastChannel;

            var oConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            oConfig.AppSettings.Settings["LastChannel"].Value = value.ToString();
            ConfigurationManager.RefreshSection("appSettings");
            oConfig.Save(ConfigurationSaveMode.Modified);
        }
    }
}