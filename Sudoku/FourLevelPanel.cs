using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 如果候选数为0，说明方案是有问题的，产生了不符合要求的解
/// </summary>

namespace Sudoku
{
    public class FourLevelPanel
    {
        /// <summary>
        /// 数独级数
        /// </summary>
        private int rank = 4;
        /// <summary>
        /// 速度难度，暂时设置与hole挂钩
        /// </summary>
        int holeCount = 4;

        Cell[,] cells =new Cell[4, 4];

        public FourLevelPanel()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cells[i, j] = new Cell(4);
                }
            }
            GenerateCells2();
            DigHole();
        }



        private void DigHole()
        {
            List<int> holePos = new List<int>();
            
            int i, k;
            for (i = 0; i < holeCount; i++)    //随机挖洞位置  
            {
                Random r = new Random();
                k = r.Next(15);
                while (holePos.Contains(k))
                {
                    k = r.Next(15);
                }
                holePos.Add(k);
                cells[k / 4, k % 4].show = 0;
            }
        }

        private void GenerateCells()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cells[i, j].show = GenerateCell(i,j);
                    cells[i, j].answer = cells[i, j].show;
                    System.Console.WriteLine("cells[{0},{1}]"+cells[i, j].answer,i,j);
                }
            }
        }

        /// <summary>
        /// 产生题干方法2
        /// 通过随机移动行列产生题板
        /// </summary>
        private void GenerateCells2()
        {
            /*
             * 1,2,3,4
             * 3,4,1,2
             * 2,1,4,3
             * 4,3,2,1
             */
            int[,] arr = new int[4, 4] { { 1, 2, 3, 4 }, { 3, 4, 1, 2 }, { 2, 1, 4, 3 }, { 4, 3, 2, 1 } };

            //随机0和1，0变换位置，1不变换位置
            Random random1 = new Random();
            Random random2 = new Random();
            Random random3 = new Random();

            #region 行变换
            if (random1.Next(0, 2) == 1)
            {
                if (random2.Next(0, 2) == 1)
                {
                    ExchangeRow(arr, 0, 3, 1);
                    ExchangeRow(arr, 1, 2, 1);
                    if(random3.Next(0,2)==1)
                    {
                        ExchangeRow(arr, 0, 1, 1);
                    }
                }
                else
                {
                    ExchangeRow(arr, 0, 2, 1);
                    ExchangeRow(arr, 1, 3, 1);
                    if(random3.Next(0, 2) == 1)
                    {
                        ExchangeRow(arr, 0, 1, 1);
                    }
                }
            }
            else {
                if (random2.Next(0, 2) == 1)
                {
                    ExchangeRow(arr, 0, 1, 1);
                }
                if (random3.Next(0, 2) == 1)
                {
                    ExchangeRow(arr, 2, 3, 1);
                }
            }
            #endregion

            #region 列变换
            if (random1.Next(0, 2) == 1)
            {
                if (random2.Next(0, 2) == 1)
                {
                    ExchangeRow(arr, 0, 3, 0);
                    ExchangeRow(arr, 1, 2, 0);
                    if (random3.Next(0, 2) == 1)
                    {
                        ExchangeRow(arr, 0, 1, 0);
                    }
                }
                else
                {
                    ExchangeRow(arr, 0, 2, 0);
                    ExchangeRow(arr, 1, 3, 0);
                    if (random3.Next(0, 2) == 1)
                    {
                        ExchangeRow(arr, 0, 1, 0);
                    }
                }
            }
            else
            {
                if (random2.Next(0, 2) == 1)
                {
                    ExchangeRow(arr, 0, 1, 0);
                }
                if (random3.Next(0, 2) == 1)
                {
                    ExchangeRow(arr, 2, 3, 0);
                }
            }
            #endregion


            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cells[i, j].show = int.Parse(arr[i,j].ToString());
                    cells[i, j].answer = cells[i, j].show;
                    System.Console.WriteLine("cells[{0},{1}]" + cells[i, j].answer, i, j);
                }
            }
        }

        private void ExchangeRow(int[,] temp, int startindex, int endindex, int type)
        {
            int tempInt = 0;
            //行转换
            if (type == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    tempInt = temp[startindex, i];
                    temp[startindex, i] = temp[endindex, i];
                    temp[endindex, i] = tempInt;
                }
            }//列转换
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    tempInt = temp[i,startindex];
                    temp[i, startindex] = temp[i, endindex];
                    temp[i, endindex] = tempInt;
                }
            }
        }


        /// <summary>
        /// 生成一个cell
        /// </summary>
        /// <returns></returns>
        private int GenerateCell(int i,int j)
        {
            Random random = new Random();
            //
            int rtnValue = cells[i, j].candidate[random.Next(0,cells[i,j].candidate.Count-1)];
            cells[i, j].candidate.Remove(rtnValue);
            //清除列候选数
            for (int rowRule = 0; rowRule < 4; rowRule++)
            {
                cells[rowRule, j].candidate.Remove(rtnValue);
            }
            //清除行候选数
            for (int colRule = 0; colRule < 4; colRule++)
            {
                cells[i, colRule].candidate.Remove(rtnValue);
            }
            //清除宫候选数
            for (int row = (i / 2) * 2; row < ((i / 2) + 1) * 2; row++)
            {
                for (int col = (j / 2) * 2; col < ((j / 2) + 1) * 2; col++)
                {
                    cells[row, col].candidate.Remove(rtnValue);
                }
            }
            return rtnValue;
        }

        public void OutPrint()
        {
            System.Console.WriteLine("题干：");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    System.Console.Write(cells[i, j].show+"    ");
                }
                System.Console.WriteLine();
            }

            System.Console.WriteLine("答案：");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    System.Console.Write(cells[i, j].answer + "    ");
                }
                System.Console.WriteLine();
            }


            System.Console.ReadKey();
        }
    }
}
