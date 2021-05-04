using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    // Coordinates class: defines an obect to store coordinates, including rows, cols and depth
    [Serializable]
    public class Coordinates
    {
        public int row { get; set; }
        public int col { get; set; }
        public int depth { get; set; }

        /**
         * Coordinates default value constructor sets the value for the current Column coordinate
         */
        public Coordinates()
        {
            row = 0;
            col = 0;
            depth = -1;
        }

        /**
         * Coordinates explicit value constructor sets the value for the current Column coordinate
         */
        public Coordinates(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.depth = -1;
        }

        public Coordinates(int row, int col, int depth)
        {
            this.row = row;
            this.col = col;
            this.depth = depth;
        }

        /**
         * overridden toString function returns coordinates
         */
        public override string ToString()
        {
            string coordinatesStr = " ";
            if (depth != -1)
            {
                coordinatesStr += "(" + this.row + ", " + this.col + ", " + this.depth + ")";
            } else
            {
                coordinatesStr += "(" + this.row + ", " + this.col + ")";
            }
            return coordinatesStr;
        }


    }
}
