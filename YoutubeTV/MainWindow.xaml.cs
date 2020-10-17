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

        private int controllCountDown = 5;

        private int channelCountDown = 5;

        private MainViewController _mainViewController;

        public MainWindow(MainViewController mainViewController)
        {
            // binding
            InitializeComponent();
            this._mainViewController = mainViewController;
            this.DataContext = this._mainViewController;

            // event
            this.KeyDown += MainWindow_KeyDown;
            this.MouseEnter += ShowControllPannel;
            this.controllPannel.MouseMove += ShowControllPannel;
            this.label.SourceUpdated += Label_SourceUpdated;

            this.StartTimer();
        }

        private void Label_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            this.channelCountDown = 5;
            this.lblChangeChannel.Visibility = Visibility.Visible;
        }

        private void ShowControllPannel(object sender, MouseEventArgs e)
        {
            this.controllCountDown = 5;
            this.controllPannel.Visibility = Visibility.Visible;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                // change volume
                case Key.Up:
                    this._mainViewController.VolumeViewModel.UpLevel.Execute(null);
                    break;

                case Key.Down:
                    this._mainViewController.VolumeViewModel.DownLevel.Execute(null);
                    break;

                // channel by direction
                case Key.Right:
                    this._mainViewController.ChannelViewModel.GoNext.Execute(null);
                    break;

                case Key.Left:
                    this._mainViewController.ChannelViewModel.GoPrivious.Execute(null);
                    break;

                case Key.RightShift:
                    this._mainViewController.ChannelViewModel.Switch.Execute(null);
                    break;

                // change channel by num
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                    // NumPad0 value = 74
                    var intValue = (int)e.Key - 74;
                    this._mainViewController.ChannelViewModel.HandleNumKeyDown(intValue);
                    break;

                case Key.Enter:
                    this._mainViewController.ChannelViewModel.HandleEnterKeyDown();
                    break;

                default:
                    break;
            }
        }

        private void StartTimer()
        {
            this._timer = new TimersTimer();
            this._timer.Interval = 1000;

            Action hideControllPannel = () =>
            {
                if (this.controllCountDown < 0)
                {
                    return;
                }

                this.controllCountDown--;
                if (this.controllCountDown == 0)
                {
                    this.controllPannel.Visibility = Visibility.Hidden;
                }
            };

            Action hideChangeChannel = () =>
            {
                if (this.channelCountDown < 0)
                {
                    return;
                }
                this.channelCountDown--;
                if (this.channelCountDown == 0)
                {
                    this.lblChangeChannel.Visibility = Visibility.Hidden;
                }
            };

            Action<object, ElapsedEventArgs> action = (obj, args) =>
            {
                if (!CheckAccess())
                {
                    Dispatcher.Invoke(hideControllPannel);
                    Dispatcher.Invoke(hideChangeChannel);
                }
            };

            this._timer.Elapsed += new ElapsedEventHandler(action);
            this._timer.Start();
        }
    }
}