using GiTools.Views;
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
            //pobierasz tekst z inputa
            //przechodzisz do następnego widoku, przekazując tam token
            Home home= new Home(TokenInputBox.Text);
            home.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
