namespace Sudoku.Model
{
    public class Cell
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public string Number { get; set; }
        public string GamePath { get; set; }

        public Cell(int row, int column, string number, string gamePath)
        {
            this.Column = column;
            this.Row = row;
            this.Number = number;
            this.GamePath = gamePath;
        }
        
    }
}
