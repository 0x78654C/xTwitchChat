using System.Windows;
using System.Windows.Input;

namespace xTwitchChat
{
    /// <summary>
    /// Interaction logic for Channel.xaml
    /// </summary>
    public partial class Channel : Window
    {
        public string channelName = "";
        public Channel()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        /// <summary>
        /// Set the channel name for main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setchannel_btn_Click(object sender, RoutedEventArgs e)
        {
            channelName = channel_name.Text;
            if (string.IsNullOrEmpty(channelName))
            {
                MessageBox.Show("You must enter the Twitch channel name!");
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Close lable button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeLBL_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Drag window with mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        /// <summary>
        /// Minimize lable button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miniMizeLBL_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
