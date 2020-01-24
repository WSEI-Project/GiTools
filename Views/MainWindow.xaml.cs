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
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("start https://github.com/settings/tokens");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
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
                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

                Home home = new Home(token);
                home.Show();
                this.Close();
            }
        }
        #endregion


    }
}
