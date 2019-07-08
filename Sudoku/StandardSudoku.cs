namespace Sudoku
{
    public class StandardSudoku
    {
        const int col = 9;
        const int row = 9;

        /// <summary>
        /// 初始化标准9*9数独
        /// </summary>
        Cell[,] Cells = new Cell[row, col];
    }
}
