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
using Octokit;
using System.Linq;
using System.Threading.Tasks;

namespace GiTools.Views
{
    /// <summary>
    /// Logika interakcji dla klasy UserRepo.xaml
    /// </summary>
    public partial class UserRepo : Window
    {
        GitService git;
        List<Repo> dataTableData;


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
                repo.url = item.HtmlUrl;
                repo.id = item.Id;
                dataTableData.Add(repo);
            }
            this.Results.ItemsSource = dataTableData;
        }
        private async void DownloadRepo_Click(object sender, RoutedEventArgs e)
        {
            var myValue = ((Button)sender).Tag.ToString();

            await git.DownloadRepo(long.Parse(myValue), "C:\\Projekty");
            MessageBox.Show("Repository has been downloaded");
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

    }

}