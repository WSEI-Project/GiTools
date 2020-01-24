using GiTools.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiTools.ViewModels
{
    public class MainWindowViewModel
    {
        public GitService _gitService;
        MainWindowViewModel(GitService gitService)
        {
            _gitService = gitService;
        }
    }
}
