
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for regularWindow.xaml
    /// </summary>
    public partial class RegularWindow : Window
    {
        private bool playerTurn = true;
        private bool gameOver = false;
        private TicTacToeBoard gameBoard;
        private int moveCounter = 0;
        private string mode;
        private bool oneMoreTurn = false;

        public RegularWindow(string gameMode)
        {
            mode = gameMode;
            InitializeComponent();
            gameBoard = new TicTacToeBoard(3, gameMode);
            // MessageBox.Show("Player X is going first.");
        }

        public RegularWindow(string gameMode, TicTacToeBoard board)
        {

            InitializeComponent();

            mode = gameMode;
            gameBoard = board;
            Button btn;
            if (gameBoard.Grid[0,0].symbol != '\0')
            {
                // Button btn = (Button)Application.
                btn = btn1;
                btn.Content = gameBoard.Grid[0, 0].symbol;
            }
            if (gameBoard.Grid[1, 0].symbol != '\0')
            {
                btn =  btn2;
                btn.Content = gameBoard.Grid[1, 0].symbol;
            }
            if (gameBoard.Grid[2, 0].symbol != '\0')
            {
                btn = btn3;
                btn.Content = gameBoard.Grid[2, 0].symbol;
            }
            if (gameBoard.Grid[0, 1].symbol != '\0')
            {
                btn = btn4;
                btn.Content = gameBoard.Grid[0, 1].symbol;
            }
            if (gameBoard.Grid[1, 1].symbol != '\0')
            {
                btn = btn5;
                btn.Content = gameBoard.Grid[1,1].symbol;
            }
            if (gameBoard.Grid[2, 1].symbol != '\0')
            {
                btn = btn6;
                btn.Content = gameBoard.Grid[2,1].symbol;
            }
            if (gameBoard.Grid[0, 2].symbol != '\0')
            {
                btn = btn7;
                btn.Content = gameBoard.Grid[0, 2].symbol;
            }
            if (gameBoard.Grid[1, 2].symbol != '\0')
            {
                btn = btn8;
                btn.Content = gameBoard.Grid[1,2].symbol;
            }
            if (gameBoard.Grid[2, 2].symbol != '\0')
            {
                btn = btn9;
                btn.Content = gameBoard.Grid[2,2].symbol;
            }

            // MessageBox.Show("Player X is going first.");
        }

        //TODO
        //Need to return an array of something
        //Logisitcs - Carter
        //Implement method for Save button
        //  Possibly a Load button as well

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
                // Saves the game in a binary format to restore at a later time
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
                mainWindow.title.Foreground = (Brush) Application.Current.Properties["FontColor"];
                mainWindow.boardLabel.Foreground = (Brush)Application.Current.Properties["FontColor"];

                this.Close();
            }
        }

        //Method that handles the event of a button click
            //This whole window is a game board and it uses buttons for where players can choose to put either an X or O
            //  The game board is a 3X3 board
            //This method is for the game board
        private void Button_Click(object sender, RoutedEventArgs e)
        {


            if(gameOver) {
                MessageBox.Show("The game is over.");
                playAgainMessage();
                return;
            }
            //Creates a new button
            Button btn = sender as Button;


            // Start of game loop, passes in the coordinates from the button
            Coordinates location = new Coordinates(Grid.GetRow(btn), Grid.GetColumn(btn));
            // if (btn.Content != null)


            // Checks if the chosen location is a valid move, if not valid than stops and doesnt place a piece
            if (gameBoard.isValidMove(location, mode))
            {
                moveCounter++;
                // MessageBox.Show("Move counter is: " + moveCounter);
                if (mode == "Nokato Tic-tac-toe") {
                    gameBoard.makeMove(location, 'X' , mode);
                    btn.Content = "X";
                }else if (mode == "Wild Tic-tac-toe (Your Choice Tic-tac-toe)" || mode == "Devil's Tic-tac-toe") //This else-if statement is used for player choice variants of tic-tac-toe
                {
                     btn.Content = playerChoiceMessage();
                } 
                else
                {
                    // Executes chosen move, place current players piece on the TicTacToeBoard object and the window
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
                } else if (mode == "Nokato Tic-tac-toe") {

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
                    // checks if the player won, if so, ends the game
                    if (gameBoard.isWinner(playerTurn ? 'X' : 'O', mode))
                    {
                        if (mode == "Revenge Tic-tac-toe")
                        {
                            if(oneMoreTurn)
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
                            if (moveCounter == 9)
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
                    } else if(oneMoreTurn)
                    {
                        MessageBox.Show((!playerTurn ? "X" : "O") + " Won!");
                        gameOver = true;
                        playAgainMessage();
                        return;
                    }
                } if(3*3 == moveCounter)
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

        //Method that loads the colors for the window
        //Method is called when the window opens
        private void Colors_OnLoaded(object sender, RoutedEventArgs e)
        {
            //Sets the background and foreground color of all buttons using the startup variables
            foreach (Button btn in this.RegularGrid.Children.OfType<Button>())
            {
                btn.Background = (Brush)Application.Current.Properties["Background"];
                btn.Foreground = (Brush) Application.Current.Properties["FontColor"];
            }
            
        }

        //Method that handles the event of a button click 
        //Used for the Rules button
        private void btn_RuleClick(object sender, RoutedEventArgs e)
        {

            RuleWindow ruleWindow;
            //Pops up a message box that displays the rules of the chosen game
            //*********Replace with actual rules
            switch (mode)
            {
                case "Normal Tic-tac-toe":
                    ruleWindow = new RuleWindow("The object of Tic Tac Toe is to get three in a row. You play on a three by three game board. The first player is known as X and the second is O. Players alternate placing Xs and Os on the game board until either opponent has three in a row or all nine squares are filled. X always goes first, and in the event that no one has three in a row, the stalemate is called a cat game");
                    break;
                case "Misere Tic-tac-toe (Avoidance Tic-tac-toe)":
                    ruleWindow = new RuleWindow("The object of Misere Tic-tac-toe (Avoidance Tic-tac-toe) is to avoid getting three in a row. You play on a three by three game board. The first player is known as X and the second is O. Players alternate placing Xs and Os on the game board until either opponent has three in a row or all nine squares are filled. X always goes first, and in the event that no one has three in a row, the stalemate is called a cat game");
                    break;
                case "Nokato Tic-tac-toe":
                    ruleWindow = new RuleWindow("The object of Nokato Tic-tac-toe is to avoid getting three in a row. You play on a three by three game board. Both players are X. Players alternate placing Xs on the game board until one of the players gets three in a row and loses.");
                    break;
                case "Wild Tic-tac-toe (Your Choice Tic-tac-toe)":
                    ruleWindow = new RuleWindow("The object of Wild Tic-tac-toe (Your Choice Tic-tac-toe) is to get three in a row. You play on a three by three game board. Player 1 an player 2 get to choose wether to place an X or an O each turn. Players placing Xs or Os on the game board until either an opponent has three in a row or all nine squares are filled. Player 1 goes first, and in the event that no one has three in a row, the stalemate is called a cat game");
                    break;
                case "Devil's Tic-tac-toe":
                    ruleWindow = new RuleWindow("The object of Devil's Tic-tac-toe is to avoid getting three in a row. You play on a three by three game board. Player 1 an player 2 get to choose wether to place an X or an O each turn. Players placing Xs or Os on the game board until either an opponent has three in a row or all nine squares are filled. Player 1 goes first, and in the event that no one has three in a row, the stalemate is called a cat game");
                    break;
                case "Revenge Tic-tac-toe":
                    ruleWindow = new RuleWindow("The object of Revenge Tic-tac-toe is to get three in a row without your opponent getting three-in-a-row in the next turn. If a player gets three in a row, their opponent has one more turn to try and get three in a row and if they are able to, they win. You play on a three by three game board. The first player is known as X and the second is O. Players alternate placing Xs and Os on the game board until either opponent has three in a row or all nine squares are filled. X always goes first, and in the event that no one has three in a row, the stalemate is called a cat game");
                    break;
                case "Random Tic-tac-toe":
                    ruleWindow = new RuleWindow("The object of Random Tic-tac-toe is to get three in a row. A random player is chosen to play each turn. You play on a three by three game board. The first player is known as X and the second is O. Players alternate placing Xs and Os on the game board until either opponent has three in a row or all nine squares are filled. In the event that no one has three in a row, the stalemate is called a cat game");
                    break;
                default:
                    ruleWindow = new RuleWindow("No Game mode detected");
                    break;
            }

            ruleWindow.Show();
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

        //Method that creates a message box with buttons for the user to select if they want to place and X or O
        //It is mainly used for player choice variants
        //Returns a string with the player choice which is either an X or O
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
                foreach (Button btn in this.RegularGrid.Children.OfType<Button>())
                {
                    btn.Content = "";
                }

                gameOver = false;
                gameBoard = null;
                gameBoard = new TicTacToeBoard(3, mode);
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
