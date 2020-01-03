using GiTools.Services;
using GiTools.Services.Interfaces;
using GiTools.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace GiTools
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IGitService, GitService>();

            //Home homeWindow = container.Resolve<Home>();
           // MainWindow mainWindow = container.Resolve<MainWindow>();
            //mainWindow.Show();
        }
    }
}
