using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for Rules.xaml
    /// </summary>
    public partial class RuleWindow : Window
    {
        private string ruleMessage;

        public RuleWindow(string msg)
        {
            ruleMessage = msg;
            InitializeComponent();
        }

        private void Rules_OnLoaded(object sender, RoutedEventArgs e)
        {
            ruleTextBlock.Text = ruleMessage;
            ruleGrid.Background = (Brush)Application.Current.Properties["Background"];
            ruleTextBlock.Foreground = (Brush)Application.Current.Properties["FontColor"];

        }

        private void btn_CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
