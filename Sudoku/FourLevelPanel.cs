using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class FourLevelPanel
    {
        private int rank = 4;
        Cell[,] cells =new Cell[4, 4];
        Cell[,] answer = new Cell[4, 4];



        private void digHole(int holeCnt)
        {
            int[] idx=new int[16];
            int i, k;
            for (i = 0; i < 16; i++)
            {
                answer[i / 4,i % 4].answer = 0;
                idx[i] = i;
            }
            for (i = 0; i < holeCnt; i++)    //随机挖洞位置  
            {
                Random r = new Random(16);
                k = r.Next();
                int tmp = idx[k];
                idx[k] = idx[i];
                idx[i] = tmp;
            }
            for (i = 0; i < holeCnt; i++)
            {
                answer[idx[i] / 9,idx[i] % 9].answer = 1;
            }
        }
    }
}
