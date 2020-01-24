using GiTools.Services;
using Octokit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GiTools.Views
{
    /// <summary>
    /// Logika interakcji dla klasy SearchRepo.xaml
    /// </summary>

    public partial class SearchRepo : Window
    {
        readonly GitService git;
        List<Repo> dataTableData;
        public SearchRepo(GitService git)
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

        #region Window : BtnSearchRepo
        private async void BtnSearchRepo_Click(object sender, RoutedEventArgs e)
        {
            var req = new SearchRepositoriesRequest();
            req.Archived = (bool)Archieved.IsChecked; // checkbox
            if (DateFrom.SelectedDate.HasValue && DateTo.SelectedDate.HasValue)
            {
                req.Created = new DateRange(DateFrom.SelectedDate.Value, DateTo.SelectedDate.Value);
            }
            if (Language.SelectedItem != null)
            {
                req.Language = (Octokit.Language)Language.SelectedItem;
            }
            if (!string.IsNullOrEmpty(StarsFrom.Text) && !string.IsNullOrEmpty(StarsTo.Text))
            {
                int from = 0;
                int to = 0;
                int.TryParse(StarsFrom.Text, out from);
                int.TryParse(StarsTo.Text, out to);
                if (from > to)
                {
                    MessageBox.Show("Stars from must be smaller than Stars to");
                }

                req.Stars = new Octokit.Range(from, to);
            }
            if (!string.IsNullOrEmpty(Owner.Text))
            {
                req.User = Owner.Text;
            }

            var result = await git.SearchRepositories(req);
            Amount.Text = result.TotalCount.ToString();
            for (int i = 0; i < 5; i++)
            {
                var repo = new Repo();
                repo.name = result.Items[i].Name;
                repo.url = result.Items[i].Url;
                dataTableData.Add(repo);
            }
            this.Results.ItemsSource = dataTableData;
        }
        #endregion
    }
    #region Window : Class Repo
    public class Repo
    {
        public string name { get; set; }
        public string url { get; set; }
    }
    #endregion
}
