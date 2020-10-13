using System;
using System.Collections.Generic;
using System.Text;
using YoutubeTV.ViewModel.Interface;

namespace YoutubeTV.Controller
{
    public class MainViewController
    {
        public IRemoteControllViewModel _remoteControllViewModel { get; set; }

        public MainViewController()
        {
        }

        public MainViewController(IRemoteControllViewModel remoteControllViewModel)
        {
            this._remoteControllViewModel = remoteControllViewModel;
        }
    }
}