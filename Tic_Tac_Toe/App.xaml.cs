using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Application.Current.Properties["Background"] = Brushes.White;
            Application.Current.Properties["FontColor"] = Brushes.Black;
            Application.Current.Properties["WindowIndex"] = "";
            Application.Current.Properties["BoardSize"] = 0;
        }
    }
}
