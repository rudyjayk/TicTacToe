using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private string gameMode;

        public SettingWindow()
        {
            InitializeComponent();
        }

        public SettingWindow(string mode)
        {
            gameMode = mode;
            InitializeComponent();
        }

        //Method that sets the settings window background and font color when the window is loaded
        private void colors_OnLoaded(object sender, RoutedEventArgs e)
        {
            settingGrid.Background = (Brush) Application.Current.Properties["Background"];
            settingLabel.Foreground = (Brush) Application.Current.Properties["FontColor"];
            backLabel.Foreground = (Brush)Application.Current.Properties["FontColor"];
            fontLabel.Foreground = (Brush)Application.Current.Properties["FontColor"];
        }

        //Method that is used for when the window is loaded
        //Used to load combo boxes
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            //Creates a list
            List<string> data = new List<string>();

            //Creat a variable that is set as the combo box
            var combo = sender as ComboBox;

            //Use LINQ to find all names of the brushes (colors)
            var colorNamesQ = typeof(Brushes)
                .GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Select(x => x.Name);

            //Using the query add all names of colors to the list
            foreach (var colorName in colorNamesQ)
            {
                data.Add((string)colorName);
            }

            //Make the source of the items in the combo box the list that was created
            combo.ItemsSource = data;

           
        }

        //Method that handles the event of the selection changing in a combo box
        //Changes the background and font color based on which combo box is used
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //Get the ComboBox.
            var comboBox = sender as ComboBox;

            //Set SelectedItem as Window Title.
            string value = comboBox.SelectedItem as string;

            if (comboBox.Name == "Background")
            {
                //Changes background to what the user has selected
                settingGrid.Background = new BrushConverter().ConvertFromString(value) as SolidColorBrush;

            }
            else
            {
                //Changes the font color to what the user has selected
                settingLabel.Foreground = new BrushConverter().ConvertFromString(value) as SolidColorBrush;
                backLabel.Foreground = new BrushConverter().ConvertFromString(value) as SolidColorBrush;
                fontLabel.Foreground = new BrushConverter().ConvertFromString(value) as SolidColorBrush;
            }
        }

        //Method that handles a button click event
        //Mainly used for the close button
        private void btn_CloseClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            RegularWindow regularWindow = new RegularWindow(gameMode);
            UltimateWindow ultimateWindow = new UltimateWindow(String.Empty);
            CustomWindow customWindow = new CustomWindow(gameMode);
            ThreeDWindow threeDWindow = new ThreeDWindow(String.Empty);

            //This conditional is used to open the previous window that was open before the settings window
            if ((string)Application.Current.Properties["WindowIndex"] == mainWindow.Name)
            {
                mainWindow.Show();

                //Sets background and foreground color
                mainWindow.mainGrid.Background = settingGrid.Background;
                mainWindow.title.Foreground = settingLabel.Foreground;
                mainWindow.boardLabel.Foreground = settingLabel.Foreground;
            }
            else if ((string)Application.Current.Properties["WindowIndex"] == regularWindow.Name)
            {
                //Setting these variables will set the background and foreground color for this window
                Application.Current.Properties["Background"] = settingGrid.Background;
                Application.Current.Properties["FontColor"] = settingLabel.Foreground;

                regularWindow.Show();

            }
            else if ((string) Application.Current.Properties["WindowIndex"] == ultimateWindow.Name)
            {
                //Setting these variables will set the background and foreground color for this window
                Application.Current.Properties["Background"] = settingGrid.Background;
                Application.Current.Properties["FontColor"] = settingLabel.Foreground;

                ultimateWindow.Show();

            }
            else if ((string)Application.Current.Properties["WindowIndex"] == customWindow.Name)
            {
                //Setting these variables will set the background and foreground color for this window
                Application.Current.Properties["Background"] = settingGrid.Background;
                Application.Current.Properties["FontColor"] = settingLabel.Foreground;

                customWindow.createWindow(customWindow);
            }
            else if ((string)Application.Current.Properties["WindowIndex"] == threeDWindow.Name)
            {
                //Setting these variables will set the background and foreground color for this window
                Application.Current.Properties["Background"] = settingGrid.Background;
                Application.Current.Properties["FontColor"] = settingLabel.Foreground;

                threeDWindow.Show();
            }

            //Closes the setting window
            this.Close();

        }


        /*This code was commented out just so I could use it as a reference.
            First it was made where you would just press a button and change
            the colors randomly but I changed it so that the user can pick
            from the entire Brushes library*/

        /*private void btn_ColorClick(object sender, RoutedEventArgs e)
        {

            var r = new Random();
            Button btn = sender as Button;

            var properties = typeof(Brushes).GetProperties();
            var count = properties.Count();

            var colour = properties
                .Select(x => new { Property = x, Index = r.Next(count) })
                .OrderBy(x => x.Index)
                .First();

            if (btn.Name == "back")
                settingGrid.Background = (SolidColorBrush) colour.Property.GetValue(colour, null);
            else
                settingLabel.Foreground = (SolidColorBrush) colour.Property.GetValue(colour, null);
        }*/
    }
}
