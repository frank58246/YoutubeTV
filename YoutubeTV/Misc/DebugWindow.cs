using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YoutubeTV.Misc
{
    public class DebugWindow
    {
        public Label label;

        public DebugWindow()
        {
            this.label = new Label();
#if DEBUG
            this.label.Visibility = Visibility.Visible;
#else
            this.label.Visibility = Visibility.Hidden;
#endif
        }

        public void Init()
        {
            var mainWindow = Application.Current.MainWindow;
        }

        public void Log(string s)
        {
            var content = this.label.Content.ToString();
            content += "\r\n";
            content += s;
            this.label.Content = content;
        }
    }
}