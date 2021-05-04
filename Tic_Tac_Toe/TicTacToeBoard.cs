using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    [Serializable]
    public class TicTacToeBoard : Cell
    {
        /*
         * 0: Normal Tic-tac-toe
         *      NxN, N in a row, X and O
         * 1: Misere Tic-tac-toe (Avoidance Tic-tac-toe)
         *      NxN, N in a row, X and O
         *      The player who get N in a row loses
         * 2: Nokato Tic-tac-toe
         *      NxN, N in a row, X
         *      The player who get N in a row loses
         * 3: Wild Tic-tac-toe (Your Choice Tic-tac-toe)
         *      NxN, N in a row, X and Order
         *      Player chooses X or O each turn
         * 4: Devil's Tic-tac-toe
         *      NxN, N in a row, X and O
         *      Player chooses X or O each turn
         *      The player who get N in a row loses
         * 5: Revenge Tic-tac-toe
         *      NxN, N in a row, X and O
         *      Player who gets N-in-a-row unless opponent can create n-in-a-row in the next turn
         * 6: Random Tic-tac-toe
         *      NxN, N in a row, X and O
         *      Random turn each turn
         * 7: Ultimate Tic-tac-toe
         *      9x9, N in a row, X and O
         *      each 3x3 square is a normal Tic-tac-toe game
         *      Forced to play on a board based on last move
         * 8: 3D Tic-tac-toe
         *      NxNxN, N in a row, X and O
         */
        public string gameMode;
        public int N { get; set; }
        public Cell[,] Grid { get; set; }
        public Cell[,,] ThreeDGrid { get; set; }

        // Tic Tac Toe Board stores a two dimensional array of cells
        public TicTacToeBoard()
        {
            Grid = new Cell[N, N];
            this.N = 3;
            this.Grid = new Cell[this.N, this.N];
            this.gameMode = "Normal Tic-tac-toe";
        }

        public TicTacToeBoard(bool threeD)
        {
            this.gameMode = "3D";
            N = 3;

            ThreeDGrid = new Cell[3, 3, 3];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        ThreeDGrid[i, j, k] = new Cell();
                    }
                }
            }
            this.ThreeDGrid = ThreeDGrid;
        }

        public TicTacToeBoard(int size, string mode)
        {
            this.gameMode = mode;
            N = size;

            if (gameMode == "3D")
            {
                ThreeDGrid = new Cell[3, 3, 3];
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        for (int k = 0; k < N; k++)
                        {
                            ThreeDGrid[i, j, k] = new Cell();
                        }
                    }
                }
                this.ThreeDGrid = ThreeDGrid;
            }
            else
            {
                Grid = new Cell[N, N];
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        Grid[i, j] = new Cell();
                    }
                }
                this.Grid = Grid;
            }


            
        }

        public TicTacToeBoard(string mode)
        {
            switch (mode)
            {
                case "Normal Tic-tac - toe":
                    N = 3;
                    Grid = new Cell[N, N];
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            Grid[i, j] = new Cell();
                        }
                    }
                    break;
            }

        }

        public override string ToString()
        {
            string ticTacToeBoardStr = " ";
            for (int k = 0; k < N; k++)
            {
                ticTacToeBoardStr += " " + k;
            }

            ticTacToeBoardStr += "\n";
            for (int i = 0; i < N; i++)
            {
                ticTacToeBoardStr += i;
                for (int j = 0; j < N; j++)
                {
                    ticTacToeBoardStr += " " + Grid[i, j];
                }

                ticTacToeBoardStr += "\n";
            }

            return ticTacToeBoardStr;
        }

        // Class to check if current move location contains an X or O
        public bool isValidMove(Coordinates location, string mode)
        {
            if (gameMode == "3D")
            {
                if(ThreeDGrid[location.row, location.col, location.depth].symbol != '\0')
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (Grid[location.row, location.col].symbol != '\0')
                {
                    return false;
                }
                return true;
            }
        }

        // Class to check if current move location contains an X or O for Ultimate TicTacToe Game mode
        public bool isValidMove(Coordinates location, string mode, Coordinates lastPlayerMove)
        {

            if(lastPlayerMove == null) { return true; }
            if (Grid[location.row, location.col].symbol != '\0' )
            {
                return false;
            }

            if(lastPlayerMove != null)
            {
                switch(lastPlayerMove.row % 3 + " " + lastPlayerMove.col % 3)
                {
                    case "0 0":
                        if(location.row >= 0 && location.row <= 2 && location.col >= 0 && location.col <= 2)
                        {
                            return true;
                        } else
                        {
                            return false;
                        }
                        break;
                    case "1 0":
                    //case "0 1":
                        if (location.row >= 3 && location.row <= 5 && location.col >= 0 && location.col <= 2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "2 0":
                    // case "0 2":
                        if (location.row >= 6 && location.row <= 8 && location.col >= 0 && location.col <= 2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "0 1":
                    //case "1 0":
                        if (location.row >= 0 && location.row <= 2 && location.col >= 3 && location.col <= 5)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "1 1":
                        if (location.row >= 3 && location.row <= 5 && location.col >= 3 && location.col <= 5)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "2 1":
                    // case "1 2":
                        if (location.row >= 6 && location.row <= 8 && location.col >= 3 && location.col <= 5)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "0 2":
                    // case "2 0":
                        if (location.row >= 0 && location.row <= 2 && location.col >= 6 && location.col <= 8)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "1 2":
                    // case "2 1":
                        if (location.row >= 3 && location.row <= 5 && location.col >= 6 && location.col <= 8)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "2 2":
                        if (location.row >= 6 && location.row <= 8 && location.col >= 6 && location.col <= 8)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        public void makeMove(Coordinates location, char playerSymbol, string mode)
        {
            if (gameMode == "3D")
            {
                Cell currCell = ThreeDGrid[location.row, location.col, location.depth];
                currCell.symbol = playerSymbol;
            }
            else
            {
                Cell currCell = Grid[location.row, location.col];
                currCell.symbol = playerSymbol;
            }
        }

        // Checks win conditions based on current board state, returns true if the current player won
        public bool isWinner(char playerSymbol, string mode)
        {
            if (gameMode == "3D")
            {
                // Depth
                for(int i = 0; i < N; i++)
                {
                    int count1 = 0;
                    for(int j = 0; j < N; j++)
                    {
                        int count2 = 0;
                        for(int k = 0; k < N; k++)
                        {
                            if(ThreeDGrid[i,j,k].symbol == playerSymbol)
                            {
                                count2++;
                            }
                        }
                        if(count2 == N)
                        {
                            return true;
                        }
                    }
                }
                // Width
                for (int i = 0; i < N; i++)
                {
                    int count1 = 0;
                    for (int j = 0; j < N; j++)
                    {
                        int count2 = 0;
                        for (int k = 0; k < N; k++)
                        {
                            if (ThreeDGrid[j, k, i].symbol == playerSymbol)
                            {
                                count2++;
                            }
                        }
                        if (count2 == N)
                        {
                            return true;
                        }
                    }
                }
                // Height
                for (int i = 0; i < N; i++)
                {
                    int count1 = 0;
                    for (int j = 0; j < N; j++)
                    {
                        int count2 = 0;
                        for (int k = 0; k < N; k++)
                        {
                            if (ThreeDGrid[k, i, j].symbol == playerSymbol)
                            {
                                count2++;
                            }
                        }
                        if (count2 == N)
                        {
                            return true;
                        }
                    }
                }

                for (int j = 0; j< N; j++) {
                    int counter = 0;
                    for (int i = 0; i < N; i++)
                    {
                        if (ThreeDGrid[i, i, j].symbol == playerSymbol)
                        {
                            counter++;
                        }
                    }
                    if (counter == N)
                    {
                        return true;
                    } else
                    {
                        counter = 0;
                    }
                }
                for (int j = 0; j < N; j++)
                {
                    int counter = 0;
                    for (int i = 0; i < N; i++)
                    {
                        if (ThreeDGrid[i, (N - 1) - i, j].symbol == playerSymbol)
                        {
                            counter++;
                        }
                    }
                    if (counter == N)
                    {
                        return true;
                    }

                }
                for (int j = 0; j < N; j++)
                {
                    int counter = 0;
                    for (int i = 0; i < N; i++)
                    {
                        if (ThreeDGrid[j, i, i].symbol == playerSymbol)
                        {
                            counter++;
                        }
                    }
                    if (counter == N)
                    {
                        return true;
                    }
                    else
                    {
                        counter = 0;
                    }
                }
                for (int j = 0; j < N; j++)
                {
                    int counter = 0;
                    for (int i = 0; i < N; i++)
                    {
                        if (ThreeDGrid[j, i, (N - 1) - i].symbol == playerSymbol)
                        {
                            counter++;
                        }
                    }
                    if (counter == N)
                    {
                        return true;
                    }
                }
                for (int j = 0; j < N; j++)
                {
                    int counter = 0;
                    for (int i = 0; i < N; i++)
                    {
                        if (ThreeDGrid[i, j, i].symbol == playerSymbol)
                        {
                            counter++;
                        }
                    }
                    if (counter == N)
                    {
                        return true;
                    }
                    else
                    {
                        counter = 0;
                    }
                }
                for (int j = 0; j < N; j++)
                {
                    int counter = 0;
                    for (int i = 0; i < N; i++)
                    {
                        if (ThreeDGrid[(N - 1) - i, j, i].symbol == playerSymbol)
                        {
                            counter++;
                        }
                    }
                    if (counter == N)
                    {
                        return true;
                    }

                }
                if ((ThreeDGrid[0, 0, 0].symbol == playerSymbol && ThreeDGrid[1, 1, 1].symbol == playerSymbol && ThreeDGrid[2, 2, 2].symbol == playerSymbol) ||
                   (ThreeDGrid[2, 0, 0].symbol == playerSymbol && ThreeDGrid[1, 1, 1].symbol == playerSymbol && ThreeDGrid[0, 2, 2].symbol == playerSymbol) ||
                   (ThreeDGrid[0, 2, 0].symbol == playerSymbol && ThreeDGrid[1, 1, 1].symbol == playerSymbol && ThreeDGrid[2, 0, 2].symbol == playerSymbol) ||
                   (ThreeDGrid[2, 2, 0].symbol == playerSymbol && ThreeDGrid[1, 1, 1].symbol == playerSymbol && ThreeDGrid[0, 0, 2].symbol == playerSymbol))
                {
                    return true;
                }

                return false;
            } 
            else
            {
                for(int i = 0; i < N; i++)
                {
                    int vertcounter = 0;
                    for(int j = 0; j < N; j++)
                    {
                        if (Grid[i,j].symbol == playerSymbol)
                        {
                            vertcounter++;
                        }
                    }
                    if(vertcounter == N)
                    {
                        return true;
                    }
                }

                for (int i = 0; i < N; i++)
                {
                    int horizCounter = 0;
                    for (int j = 0; j < N; j++)
                    {
                        if (Grid[j, i].symbol == playerSymbol)
                        {
                            horizCounter++;
                        }
                    }
                    if (horizCounter == N)
                    {
                        return true;
                    }
                }

                int counter = 0;
                for (int i = 0; i < N; i++)
                {
                    if (Grid[i, i].symbol == playerSymbol)
                    {
                        counter++;
                    }
                }
                if (counter == N)
                {
                    return true;
                } else
                {
                    counter = 0;
                }

                for (int i = 0; i < N; i++)
                {
                    if (Grid[i, (N- 1) - i].symbol == playerSymbol)
                    {
                        counter++;
                    }
                }
                if (counter == N)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                
            }

        }

    }
}
