using System.Collections.Generic;

namespace Sudoku
{
    /// <summary>
    /// 格子类
    /// </summary>
    public class Cell
    {
        public Cell(int count)
        {
            for (int i = 1; i < count + 1; i++)
            {
                candidate.Add(i);
            }
        }

        /// <summary>
        /// 候选数
        /// </summary>
        private List<int> _candidate;
        public List<int> candidate
        {
            get
            {
                if (_candidate == null)
                    _candidate = new List<int>();
                return _candidate;
            }
            set { _candidate = value; }
        }

        private Dictionary<int, int> _duplicateDel;
        /// <summary>
        /// 重复删除候选数次数的记录，以便恢复
        /// </summary>
        public Dictionary<int, int> duplicateDel
        {
            get
            {
                if (_duplicateDel == null)
                    _duplicateDel = new Dictionary<int, int>();
                return _duplicateDel;
            }
            set
            {
                _duplicateDel = value;
            }

        }

        /// <summary>
        /// 单元格的值
        /// </summary>
        private int _answer;
        public int answer
        {
            get;
            set;
        }
    }
}
