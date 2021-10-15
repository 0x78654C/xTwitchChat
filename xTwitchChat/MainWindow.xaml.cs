using System;
using System.Windows;
using System.Net.NetworkInformation;
using Microsoft.Web.WebView2.Core;

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

            // Check Inernet connection
            if (PingHost("8.8.8.8") == false)
            {
                MessageBox.Show("No internet connection!", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }

            // First we load the channel window for getting channel name.
            Channel channel = new Channel();
            channel.ShowDialog();

            // We grab the channel name.
            _channelName = channel.channelName;

            // We set the tile including the channel.
            this.Title = this.Title + $" : Connected at {_channelName}";

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

        /// <summary>
        /// Verifies if IP is up or not
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>verifies if IP is up or not</returns>
        public static bool PingHost(string ipAddress)
        {
            bool pingable = false;
            Ping pinger = null;
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(ipAddress);
                pingable = reply.Status == IPStatus.Success;

            }
            catch
            {
                // We handle erros in other functions.
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }

            }
            return pingable;
        }
    }
}
