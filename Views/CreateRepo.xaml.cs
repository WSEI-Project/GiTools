﻿using GiTools.Services;
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
            CloseButton.Click += (s, e) => Close();

        }
        #region Window : Moving
        private void GridOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.Grid;
            var win = Window.GetWindow(move);
            win.DragMove();
        }
        #endregion

        #region Window : Crte_Click
        private async void Crte_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(RepoNameInputBox.Text))
                MessageBox.Show("Repository name is empty");
            else
            {
                try
                {
                    long createdRepoId = await git.CreateRepo(RepoNameInputBox.Text, (bool)Private.IsChecked ? true : false);
                    MessageBox.Show(string.Format("Repository has beed created with id {0}", createdRepoId));
                }
                catch(Octokit.ApiException ex)
                {
                    if(ex.Message == "Not Found")
                    {
                        MessageBox.Show($"Github has refused to create repository, perhaps you limited token abilities to create repositories?");
                    }
                    else MessageBox.Show($"Repository could not be saved due to error: {ex.Message}");
                }
               
            }
        }
        #endregion


    }
}
