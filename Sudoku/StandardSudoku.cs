namespace Sudoku
{
    public class StandardSudoku
    {
        public StandardSudoku(int count)
        {
            Cells = new Cell[count,count];
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Cells[i, j] = new Cell(9);
                }
            }
        }
        /// <summary>
        /// 初始化数独
        /// </summary>
        public Cell[,] Cells;
    }
}
