using GiTools.Views;
using GiTools;
using GiTools.Services;
using System.Windows;
using System.Windows.Controls;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TokenInputBox.Text))
                MessageBox.Show("No token");
            else
            {
                Home home = new Home(TokenInputBox.Text);
                home.Show();
                this.Close();
            }
        }
    }
}
