using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoutubeTV.Controller;
using YoutubeTV.ViewModel.Interface;

namespace YoutubeTV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewController _mainViewController;

        public MainWindow(MainViewController mainViewController)
        {
            InitializeComponent();
            this._mainViewController = mainViewController;
            this.DataContext = this._mainViewController;
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // change channel
            if (e.Key == Key.Right)
            {
                this._mainViewController.ChannelViewModel.GoNext.Execute(null);
            }

            if (e.Key == Key.Left)
            {
                this._mainViewController.ChannelViewModel.GoPrivious.Execute(null);
            }

            // change volume
            if (e.Key == Key.Up)
            {
                this._mainViewController.VolumeViewModel.UpLevel.Execute(null);
            }

            if (e.Key == Key.Down)
            {
                this._mainViewController.VolumeViewModel.DownLevel.Execute(null);
            }

            //throw new NotImplementedException();
        }
    }
}