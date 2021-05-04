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
    /// Interaction logic for UltimateWindow.xaml
    /// </summary>
    public partial class UltimateWindow : Window
    {
        private bool playerTurn = true;
        private bool gameOver = false;
        private TicTacToeBoard gameBoard;
        private int moveCounter = 0;
        private string mode;
        private Coordinates lastMove = null;
        private TicTacToeBoard[,] theBoards;
        private TicTacToeBoard boardFromFile;

        public UltimateWindow(string gameMode)
        {
            mode = gameMode;
            InitializeComponent();
            gameBoard = new TicTacToeBoard(9, "Normal Tic-tac-toe");

            theBoards = new TicTacToeBoard[3, 3];
            theBoards[0, 0] = new TicTacToeBoard(3, "Normal Tic-tac-toe");
            theBoards[0, 1] = new TicTacToeBoard(3, "Normal Tic-tac-toe");
            theBoards[0, 2] = new TicTacToeBoard(3, "Normal Tic-tac-toe");
            theBoards[1, 0] = new TicTacToeBoard(3, "Normal Tic-tac-toe");
            theBoards[1, 1] = new TicTacToeBoard(3, "Normal Tic-tac-toe");
            theBoards[1, 2] = new TicTacToeBoard(3, "Normal Tic-tac-toe");
            theBoards[2, 0] = new TicTacToeBoard(3, "Normal Tic-tac-toe");
            theBoards[2, 1] = new TicTacToeBoard(3, "Normal Tic-tac-toe");
            theBoards[2, 2] = new TicTacToeBoard(3, "Normal Tic-tac-toe");
            // MessageBox.Show("Player X is going first.");
        }

        public UltimateWindow(string gameMode, TicTacToeBoard boardFromFile) : this(gameMode)
        {
            this.boardFromFile = boardFromFile;
            mode = gameMode;
            InitializeComponent();

        }

        //Method that sets the background and font for each button when window is loaded
        private void UltimateWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //Sets the background and foreground color of all buttons using the startup variables
            foreach (Button btn in this.UltimateGrid.Children.OfType<Button>())
            {
                btn.Background = (Brush)Application.Current.Properties["Background"];
                btn.Foreground = (Brush)Application.Current.Properties["FontColor"];
            }

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
                //TODO
                //Implement save method using a text file
                //
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

        //Method that handles when one of the buttons in the grid is clicked
        //Outcome of the button press is an X or O will display on the button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver)
            {
                MessageBox.Show("The game is over.");
                return;
            }
            //Creates a new button
            Button btn = sender as Button;

            Coordinates location = new Coordinates(Grid.GetRow(btn), Grid.GetColumn(btn));
            // if (btn.Content != null)


            if (gameBoard.isValidMove(location, mode, lastMove))
            {
                moveCounter++;
                // MessageBox.Show("Move counter is: " + moveCounter);
                gameBoard.makeMove(location, playerTurn ? 'X' : 'O', mode);
                theBoards[location.row % 3, location.col % 3].makeMove(new Coordinates(location.row % 3, location.col % 3), playerTurn ? 'X' : 'O', mode);
                btn.Content = playerTurn ? "X" : "O";

                switch (location.row % 3 + " " + location.col % 3)
                {
                    case "0 0":

                        
                        if (theBoards[0,0].isWinner(playerTurn ? 'X' : 'O', mode))
                        {
                            theBoards[0,0].makeMove(new Coordinates(location.row % 3, location.col % 3), playerTurn ? 'X' : 'O', mode);
                            if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                            {

                                MessageBox.Show((playerTurn ? "X" : "O") + " Won this board!");
                                gameOver = true;
                            }
                        }
                        break;
                    case "1 0":
                        //case "0 1":
                        if (theBoards[1, 0].isWinner(playerTurn ? 'X' : 'O', mode))
                        {
                            gameBoard.makeMove(location, playerTurn ? 'X' : 'O', mode);
                            if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                            }
                        }
                        break;
                    case "2 0":
                        // case "0 2":
                        if (theBoards[2, 0].isWinner(playerTurn ? 'X' : 'O', mode))
                        {
                            gameBoard.makeMove(location, playerTurn ? 'X' : 'O', mode);
                            if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                            }
                        }
                        break;
                    case "0 1":
                        //case "1 0":
                        if (theBoards[0, 1].isWinner(playerTurn ? 'X' : 'O', mode))
                        {
                            gameBoard.makeMove(location, playerTurn ? 'X' : 'O', mode);
                            if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                            }
                        }
                        break;
                    case "1 1":
                        if (theBoards[1,1].isWinner(playerTurn ? 'X' : 'O', mode))
                        {
                            gameBoard.makeMove(location, playerTurn ? 'X' : 'O', mode);
                            if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                            }
                        }
                        break;
                    case "2 1":
                        // case "1 2":
                        if (theBoards[2,1].isWinner(playerTurn ? 'X' : 'O', mode))
                        {
                            gameBoard.makeMove(location, playerTurn ? 'X' : 'O', mode);
                            if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                            }
                        }
                        break;
                    case "0 2":
                        // case "2 0":
                        if (theBoards[0,2].isWinner(playerTurn ? 'X' : 'O', mode))
                        {
                            gameBoard.makeMove(location, playerTurn ? 'X' : 'O', mode);
                            if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                            }
                        }
                        break;
                    case "1 2":
                        // case "2 1":
                        if (theBoards[1,2].isWinner(playerTurn ? 'X' : 'O', mode))
                        {
                            gameBoard.makeMove(location, playerTurn ? 'X' : 'O', mode);
                            if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                            }
                        }
                        break;
                    case "2 2":
                        if (theBoards[2,2].isWinner(playerTurn ? 'X' : 'O', mode))
                        {
                            gameBoard.makeMove(location, playerTurn ? 'X' : 'O', mode);
                            if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                            {
                                MessageBox.Show((playerTurn ? "X" : "O") + " Won!");
                                gameOver = true;
                            }
                        }
                        break;
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

            lastMove = location;

            //TODO
            //Use this to get exact coordinates of users play and return that for logistics
            //This section of code can be used to obtain the coordinates of the where the user has placed an X or O


            //Flips this bool so that it switches between X and O
            playerTurn = !playerTurn;
        }


        //Method that handles the event of a button click
        //Used for the settings button
        private void btn_SettingClick(object sender, RoutedEventArgs e)
        {
            //Creates a new window
            SettingWindow settingWindow = new SettingWindow(mode);

            //Shows new window
            settingWindow.Show();

            Application.Current.Properties["WindowIndex"] = this.Name;

            //Closes this window
            this.Close();

        }

        //Method that handles the event of a button click 
        //Used for the Rules button
        private void btn_RuleClick(object sender, RoutedEventArgs e)
        {
            //Pops up a message box that displays the rules of the chosen game
            //*********Replace with actual rules
            MessageBox.Show("The object of Ultimate Tic-tac-toe is to get three in a row. You play on a three by three game board of three by three game boards. The first player is known as X and the second is O. Players alternate placing Xs and Os on the each game board determined by where the last player played. Play is ended when either opponent has three in a row or all nine squares are filled. X always goes first, and in the event that no one has three in a row, the stalemate is called a cat game");
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
                foreach (Button btn in this.UltimateGrid.Children.OfType<Button>())
                {
                    btn.Content = "";
                }

                gameOver = false;
                gameBoard = null;
                gameBoard = new TicTacToeBoard(3, mode);
                playerTurn = true;
                moveCounter = 0;
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
