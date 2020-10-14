using System;
using System.Collections.Generic;
using System.Text;
using YoutubeTV.Providers.Interface;

namespace YoutubeTV.Providers.Implement
{
    // todo use file to config

    public class UserConfigProvider : IUserConfigProvider
    {
        public int GetLastVolumnIndex()
        {
            return 1;
        }

        public void SetLastVolumnIndex(int index)
        {
            Console.WriteLine($"{index} is saved");
        }
    }
}