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

        public CreateRepo(GitService git)
        {
            InitializeComponent();
            this.git = git;
            //Tu masz konstruktor, do którego wrzucasz ten gitservice, a który jest 
            // stworzony w Home.xaml.cs. Przypisujesz do pola git, ktore jest tam wyzej
            // i mozesz sobie z tego caly czas korzystac. analogicznie we wszystkich widokach
        }
  
       private async void Crte_ClickAsync(object sender, RoutedEventArgs e)
       {
           if (string.IsNullOrEmpty(RepoNameInputBox.Text))
               MessageBox.Show("Repository name is empty");
           else
           {
                long createdRepoId = await git.CreateRepo(RepoNameInputBox.Text, (bool)Private.IsChecked ? true : false);
                MessageBox.Show(string.Format("Repository has beed created with id {0}", createdRepoId));
           }
       }
       
    }
}
