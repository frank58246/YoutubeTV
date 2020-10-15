using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace YoutubeTV.ViewModel.Interface
{
    public interface IVolumeViewModel
    {
        public ICommand UpLevel { get; }

        public ICommand DownLevel { get; }

        public int CurrentLevel { get; set; }

        public string CurrentLevlPicture { get; }
    }
}