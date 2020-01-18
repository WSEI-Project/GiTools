using GiTools.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GiTools.Views
{
    /// <summary>
    /// Logika interakcji dla klasy CreateRepo.xaml
    /// </summary>
    public partial class CreateRepo : Window
    {
        GitService git;

        public CreateRepo()
        {
            InitializeComponent();
        }

        
  
       private async System.Threading.Tasks.Task Crte_ClickAsync(object sender, RoutedEventArgs e)
       {
           if (string.IsNullOrEmpty(RepoNameInputBox.Text))
               MessageBox.Show("No repository name");
           else
           {
                var 
                _ = await git.CreateRepo(RepoNameInputBox.Text, false);
           }
       }
       
    }
}
