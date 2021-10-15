using System;
using System.Windows;

namespace xTwitchChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _channelName = "";
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            // First we load the channel window for getting channel name.
            Channel channel = new Channel();
            channel.ShowDialog();

            // We grab the channel name.
            _channelName = channel.channelName;

            // If is null we exit the app.
            if (string.IsNullOrEmpty(_channelName))
                this.Close();

            // Loading chat.
            LoadWeb();
        }

        /// <summary>
        /// Load web twitch chat with the provided channel name.
        /// </summary>
        private async void LoadWeb()
        {
            twitch_web.Source = new Uri($"https://www.twitch.tv/popout/{_channelName}/chat");
            await twitch_web.EnsureCoreWebView2Async(null);
        }


        /// <summary>
        /// Change webview2 size to window one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                twitch_web.Height = SystemParameters.WorkArea.Height - 24;
                twitch_web.Width = SystemParameters.WorkArea.Width;
                await twitch_web.EnsureCoreWebView2Async(null);
                return;
            }
            twitch_web.Height = this.Height - 30;
            twitch_web.Width = this.Width - 10;
        }
    }
}
