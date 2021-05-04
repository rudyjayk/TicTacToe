using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Interaction logic for CustomWindow1.xaml
    /// </summary>
    public partial class CustomWindow : Window
    {

        private bool playerTurn = true;
        private bool gameOver = false;
        private TicTacToeBoard gameBoard;
        private int moveCounter = 0;
        private string mode;
        private bool oneMoreTurn = false;
        private bool loadFromSave = false;

        public CustomWindow(string gameMode)
        {
            mode = gameMode;
            InitializeComponent();
            gameBoard = new TicTacToeBoard((int)Application.Current.Properties["BoardSize"], mode);
        }

        public CustomWindow(string gameMode, TicTacToeBoard boardFromFile) : this(gameMode)
        {
            mode = gameMode;
            gameBoard = boardFromFile;
            loadFromSave = true;
            InitializeComponent();


        }

        private List<Button> board = new List<Button>();
        private TicTacToeBoard boardFromFile;

        //private bool playerTurn = false;


        public void createWindow(CustomWindow w)
        {
            CustomWindow customWindow = w;
            StackPanel stackPanel = new StackPanel();
            Grid dynamicGrid = new Grid();

            dynamicGrid.Height = 500;
            dynamicGrid.Width = 500;
            dynamicGrid.VerticalAlignment = VerticalAlignment.Bottom;

            //Goes through and adds the correct number of rows and columns to grid
            for (int i = 0; i < (int)Application.Current.Properties["BoardSize"]; i++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                RowDefinition gridRow = new RowDefinition();

                dynamicGrid.ColumnDefinitions.Add(gridCol);
                dynamicGrid.RowDefinitions.Add(gridRow);
            }

            //Creates a button and sets the button to a section in the grid
            //Then adds the button to the grid children
            for (int i = 0; i < (int)Application.Current.Properties["BoardSize"]; i++)
            {
                for (int j = 0; j < (int)Application.Current.Properties["BoardSize"]; j++)
                {

                    Button gridBtn = new Button();

                    Grid.SetRow(gridBtn, i);
                    Grid.SetColumn(gridBtn, j);
                    gridBtn.Click += Button_Click;
                    gridBtn.FontSize = 40;

                    if (loadFromSave)
                    {
                        if (gameBoard.Grid[j,i].symbol != '\0')
                        {
                            gridBtn.Content = gameBoard.Grid[i, j].symbol;
                        }
                    }

                    board.Add(gridBtn);
                    dynamicGrid.Children.Add(gridBtn);
                }
            }

            createMenu(stackPanel);
            stackPanel.Children.Add(dynamicGrid);

            //Adds grid to the next window about to be opened
            customWindow.Content = stackPanel;


            customWindow.Show();
        }

        private void createMenu(StackPanel s)
        {
            Menu customMenu = new Menu();
            var combo = new ComboBox();
            var btn1 = new Button();
            var btn2 = new Button();

            customMenu.HorizontalAlignment = HorizontalAlignment.Left;
            customMenu.VerticalAlignment = VerticalAlignment.Top;
            customMenu.Height = 29;
            customMenu.Width = 500;

            combo.FontWeight = FontWeights.Bold;
            combo.FontSize = 18;
            combo.Loaded += regularComboBox;
            combo.SelectionChanged += regularComboBox_SelectionChanged;

            btn1.Content = "Rules";
            btn1.FontWeight = FontWeights.Bold;
            btn1.FontSize = 18;
            btn1.Click += btn_RuleClick;

            btn2.Content = "Settings";
            btn2.FontWeight = FontWeights.Bold;
            btn2.FontSize = 18;
            btn2.Click += btn_SettingClick;

            customMenu.Items.Add(combo);
            customMenu.Items.Add(btn1);
            customMenu.Items.Add(btn2);


            s.Children.Add(customMenu);


        }

        //Method that fills the combobox with elements
        //This is called when window loads
        private void regularComboBox(object sender, RoutedEventArgs e)
        {
            //List which has elements that we want in the combo box
            List<string> data = new List<string>();
            data.Add("File");
            data.Add("Save");
            data.Add("Quit");

            //Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            //Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            //Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        //Method that handles the event of a selection changed within the combo box
        //Another window should open when a selection is picked
        private void regularComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get the ComboBox.
            var comboBox = sender as ComboBox;

            // Set SelectedItem as Window Title.
            string value = comboBox.SelectedItem as string;

            if (value == "File")
            {
                return;
            }
            else if (value == "Save")
            {
                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fStream = new FileStream(DateTime.Now.ToFileTime() + ".dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, gameBoard);
                }
            }
            else if (value == "Quit")
            {
                MainWindow mainWindow = new MainWindow();

                mainWindow.Show();

                //Sets background and foreground color
                mainWindow.mainGrid.Background = (Brush)Application.Current.Properties["Background"];
                mainWindow.title.Foreground = (Brush)Application.Current.Properties["FontColor"];
                mainWindow.boardLabel.Foreground = (Brush)Application.Current.Properties["FontColor"];

                this.Close();
            }
        }

        //Method that loads the colors for the window
        //Method is called when the window opens
        private void Colors_OnLoaded(object sender, RoutedEventArgs e)
        {
            //Sets the background and foreground color of all buttons using the startup variables
            foreach (Button btn in board)
            {
                btn.Background = (Brush)Application.Current.Properties["Background"];
                btn.Foreground = (Brush)Application.Current.Properties["FontColor"];
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver)
            {
                MessageBox.Show("The game is over.");
                playAgainMessage();
                return;
            }
            //Creates a new button
            Button btn = sender as Button;

            Coordinates location = new Coordinates(Grid.GetRow(btn), Grid.GetColumn(btn));
            // if (btn.Content != null)


            if (gameBoard.isValidMove(location, mode))
            {
                moveCounter++;
                // MessageBox.Show("Move counter is: " + moveCounter);
                if (mode == "Nokato Tic-tac-toe")
                {
                    gameBoard.makeMove(location, 'X', mode);
                    btn.Content = "X";
                }
                else if (mode == "Wild Tic-tac-toe (Your Choice Tic-tac-toe)" || mode == "Devil's Tic-tac-toe") //This else-if statement is used for player choice variants of tic-tac-toe
                {
                    btn.Content = playerChoiceMessage();
                }
                else
                {
                    gameBoard.makeMove(location, playerTurn ? 'X' : 'O', mode);
                    btn.Content = playerTurn ? "X" : "O";
                }
                if (mode == "Misere Tic-tac-toe (Avoidance Tic-tac-toe)")
                {
                    if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                    {

                        MessageBox.Show((playerTurn ? "X" : "O") + " Lost!");
                        gameOver = true;
                        playAgainMessage();
                        return;
                    }
                }
                else if (mode == "Nokato Tic-tac-toe")
                {

                    if (gameBoard.isWinner('X', mode))
                    {
                        MessageBox.Show((playerTurn ? "Player 1" : "Player 2") + " Lost!");
                        gameOver = true;
                        playAgainMessage();
                        return;
                    }
                }
                else
                {
                    if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                    {
                        if (mode == "Revenge Tic-tac-toe")
                        {
                            if (oneMoreTurn)
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                                playAgainMessage();
                                return;
                            }
                            if (moveCounter == 9)
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                                playAgainMessage();
                                return;
                            }
                            else
                            {
                                MessageBox.Show(playerTurn ? "X" : "O" + " has one more chance to win or else they lose.");
                                playerTurn = !playerTurn;
                                oneMoreTurn = true;
                                return;
                            }
                        }
                        else
                        {
                            if (moveCounter == (int)Application.Current.Properties["BoardSize"] * (int)Application.Current.Properties["BoardSize"])
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                                playAgainMessage();
                                return;
                            }
                            else
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                                playAgainMessage();
                                return;
                            }
                        }
                    }
                    else if (oneMoreTurn)
                    {
                        MessageBox.Show((!playerTurn ? "X" : "O") + " Won!");
                        gameOver = true;
                        playAgainMessage();
                        return;
                    }
                }
                if ((int)Application.Current.Properties["BoardSize"] * (int)Application.Current.Properties["BoardSize"] == moveCounter)
                {
                    MessageBox.Show("Cats game, nobody won!");
                    gameOver = true;
                    playAgainMessage();
                }
            }
            else
            {
                MessageBox.Show("That is not a valid move!");
                return;
            }
            //MessageBox.Show(location.ToString());

            //If btn already has an X or O in it then it will just return

            //Prints an X or O to the button that is pressed



            //TODO
            //Use this to get exact coordinates of users play and return that for logistics
            //This section of code can be used to obtain the coordinates of the where the user has placed an X or O


            //Flips this bool so that it switches between X and O

            if (mode == "Random Tic-tac-toe" && !gameOver)
            {
                Random random = new Random();
                playerTurn = Convert.ToBoolean(random.Next(0, 2));
                MessageBox.Show("It is " + (playerTurn ? "X" : "O") + "'s turn.");
            }
            else
            {
                playerTurn = !playerTurn;
            }
        }

        //Method that handles the event of a button click 
        //Used for the Rules button
        private void btn_RuleClick(object sender, RoutedEventArgs e)
        {
            //Pops up a message box that displays the rules of the chosen game
            //*********Replace with actual rules
            MessageBox.Show("The object of Tic Tac Toe is to get three in a row. You play on a three by three game board. The first player is known as X and the second is O. Players alternate placing Xs and Os on the game board until either oppent has three in a row or all nine squares are filled. X always goes first, and in the event that no one has three in a row, the stalemate is called a cat game");
        }

        //Method that handles the event of a button click
        //Used for the settings button
        private void btn_SettingClick(object sender, RoutedEventArgs e)
        {
            //Creates a new window
            SettingWindow settingWindow = new SettingWindow();

            //Shows new window
            settingWindow.Show();

            Application.Current.Properties["WindowIndex"] = this.Name;

            //Closes this window
            this.Close();

        }

        private string playerChoiceMessage()
        {
            //Creates a message box with buttons
            MessageBoxResult playAgainBoxResult = MessageBox.Show("Select |Yes| for X and |No| for O", "X or O", MessageBoxButton.YesNo);


            if (playAgainBoxResult == MessageBoxResult.Yes)
            {
                return "X";
            }

            return "O";
        }

        //Method that creates a message box with buttons
        //Used to ask the user if they want to play again or not
        private void playAgainMessage()
        {
            //Creates a message box with buttons
            MessageBoxResult playAgainBoxResult =
                MessageBox.Show("Do you want to play again?", "Play Again", MessageBoxButton.YesNo);

            //If user says yes then it clears the board
            if (playAgainBoxResult == MessageBoxResult.Yes)
            {
                foreach (Button btn in board)
                {
                    btn.Content = "";
                }

                gameOver = false;
                gameBoard = null;
                gameBoard = new TicTacToeBoard((int)Application.Current.Properties["BoardSize"], mode);
                playerTurn = true;
                moveCounter = 0;
                oneMoreTurn = false;
            }
            else if (playAgainBoxResult == MessageBoxResult.No) //If user says no then it closes window and opens main window
            {
                MainWindow mainWindow = new MainWindow();

                mainWindow.Show();

                //Sets background and foreground color
                mainWindow.mainGrid.Background = (Brush)Application.Current.Properties["Background"];
                mainWindow.title.Foreground = (Brush)Application.Current.Properties["FontColor"];
                mainWindow.boardLabel.Foreground = (Brush)Application.Current.Properties["FontColor"];

                this.Close();
            }
        }
    }
}

