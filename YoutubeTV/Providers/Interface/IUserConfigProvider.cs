using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeTV.Providers.Interface
{
    public interface IUserConfigProvider
    {
        int GetLastVolumeIndex();

        void SetLastVolumeIndex(int val);
    }
}