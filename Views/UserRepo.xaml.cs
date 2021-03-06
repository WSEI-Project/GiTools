﻿using GiTools.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace GiTools.Views
{
    /// <summary>
    /// Logika interakcji dla klasy UserRepo.xaml
    /// </summary>
    public partial class UserRepo : Window
    {
        GitService git;
        List<Repo> dataTableData;

        private readonly string savingPath = "C:\\Projects";
        public UserRepo(GitService git)
        {
            InitializeComponent();
            this.git = git;
            dataTableData = new List<Repo>();
            CloseButton.Click += (s, e) => Close();

        }
        #region Window: Moving

        private void GridOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.Grid;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

        #endregion


        #region Window : Button
        private async void ShowRepo_Click(object sender, RoutedEventArgs e)
        {
            
            var repoas = await git.GetUsersRepo();
            Amount.Text = repoas.Count.ToString();
            foreach (var item in repoas)
            {
                var repo = new Repo();
                repo.name = item.Name;
                repo.url = item.Url;
                repo.id = item.Id;               
                dataTableData.Add(repo);
                
            }
            this.Results.ItemsSource = dataTableData;
        }
        private async void DownloadRepo_Click(object sender, RoutedEventArgs e)
        {
            var myValue = ((Button)sender).Tag.ToString();
            if(!System.IO.Directory.Exists(savingPath))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(savingPath);
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Repository could not be saved due to error: {ex.Message}");
                }
                
            }
            try
            {
                await git.DownloadRepo(long.Parse(myValue), savingPath);
                MessageBox.Show($"Repository has been downloaded to {savingPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Repository could not be saved due to error: {ex.Message}");
            }

        }
       
        private async void ShowContributors_Click(object sender, RoutedEventArgs e)
        {
            var myValue = ((Button)sender).Tag.ToString();
            var com = await git.GetContributors(long.Parse(myValue));
            
            foreach (var name in com)
            {
                var con = new Contributor();
                con.login = name.Login;
                con.contributors = name.Contributions.ToString();
                MessageBox.Show("Login: " + con.login + " Amount of contributors: " + con.contributors) ;
            }
        }
        
        #endregion

        #region Repo Class
        public class Repo
        {
            public string name { get; set; }
            public string url { get; set; }
            public long id { get; set; }           
        }
        #endregion

        public class Contributor
        {
            public string login{ get; set; }
            public string contributors { get; set; }
        }

        private void Results_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}