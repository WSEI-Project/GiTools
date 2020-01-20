using GiTools.Services;
using GiTools.ViewModels;
using GiTools.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;

namespace GiTools.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private GitService GitService;
        [Dependency]
        public MainWindowViewModel ViewModel
        {
        
            set { this.GitService = value._gitService; }
        }
        public Home(string token)
        {
            InitializeComponent();
            GitService = new GitService(token);
        }

        
        private void BtnCreateRepo_Click (object sender, RoutedEventArgs e)
        {
            //tu jak widac przekazuje ten stworzony gitService do konstruktora widoku CreateRepo
            CreateRepo repo = new CreateRepo(GitService);
            repo.Show();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchRepo searchRepo = new SearchRepo(GitService);
            searchRepo.Show();
        }

    }
}
