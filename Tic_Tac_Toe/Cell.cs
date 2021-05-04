using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    // Cell Class: contains the coordinates and symbol for the tic tac toe board
    [Serializable]
    public class Cell : Coordinates
    {
        public Coordinates location { get; set; }
        public char symbol { get; set; }

        public Cell()
        {

        }

        public Cell(Coordinates location, char playerSymbol)
        {
            this.location = location;
            this.symbol = playerSymbol;
        }



        public override string ToString()
        {
            String cellStr = "";
            cellStr += this.symbol;
            return cellStr;
        }
    }
}
