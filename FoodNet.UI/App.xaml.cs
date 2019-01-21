using Autofac;
using FoodNet.UI.Startup;
using System;
using System.Windows;

namespace FoodNet.UI
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstraper = new Bootstraper();
            var container = bootstraper.Bootstrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
