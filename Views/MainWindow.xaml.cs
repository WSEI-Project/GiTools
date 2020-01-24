using GiTools.Views;
using GiTools;
using GiTools.Services;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using System;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;

namespace GiTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            CloseButton.Click += (s, e) => Close();
        }
        public void Token_Click(object sender, RoutedEventArgs e)
        {
            
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Internet Explorer\iexplore.exe", "https://github.com/settings/tokens");
        }

        #region Window: Moving

        private void GridOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.Grid;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

        #endregion

        #region Window : Button1
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TokenInputBox.Text))
                MessageBox.Show("No token");
            else
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://api.github.com");
                var token = TokenInputBox.Text;
                var service = new GitService(token);
                if (service.AuthenticateUser(token))
                {
                    Home home = new Home(token);
                    home.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Bad Token");
                }
            
            }
        }
        #endregion


    }
}
