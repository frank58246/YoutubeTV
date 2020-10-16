using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
using TimersTimer = System.Timers.Timer;

namespace YoutubeTV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimersTimer _timer;

        private int countDown = 5;

        private MainViewController _mainViewController;

        public MainWindow(MainViewController mainViewController)
        {
            InitializeComponent();
            this._mainViewController = mainViewController;
            this.DataContext = this._mainViewController;
            this.KeyDown += MainWindow_KeyDown;
            this.MouseEnter += ShowControllPannel;
            this.controllPannel.MouseMove += ShowControllPannel;
            this.StartTimer();
        }

    

        private void ShowControllPannel(object sender, MouseEventArgs e)
        {
            this.countDown = 5;
            this.controllPannel.Visibility = Visibility.Visible;
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

            if (e.Key == Key.RightShift)
            {
                this._mainViewController.ChannelViewModel.Switch.Execute(null);
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
        }

        private void StartTimer()
        {
            this._timer = new TimersTimer();
            this._timer.Interval = 1000;

            Action hideControllPannel = () =>
            {
                if (this.countDown < 0)
                {
                    return;
                }

                this.countDown--;
                if (this.countDown == 0)
                {
                    this.controllPannel.Visibility = Visibility.Hidden;
                }
            };

            Action<object, ElapsedEventArgs> action = (y, x) =>
            {
                if (!CheckAccess())
                {
                    Dispatcher.Invoke(hideControllPannel);
                }
            };

            this._timer.Elapsed += new ElapsedEventHandler(action);
            this._timer.Start();
        }
    }
}