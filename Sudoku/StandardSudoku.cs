namespace Sudoku
{
    public class StandardSudoku
    {
        private int _Rank = 3;
        /// <summary>
        /// 初始化数独
        /// </summary>
        private Cell[,] _Cells;
        public StandardSudoku(int r)
        {
            _Rank = r;
            _Cells = new Cell[r,r];
        }
    }
}
