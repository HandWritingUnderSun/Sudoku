using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class CommonMethod
    {
        private delegate bool RelativeCellMethod(Cell[,] table, int i, int j, int index);


        /// <summary>
        /// 移除单元格的候选数
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="i">行</param>
        /// <param name="j">列</param>
        /// <param name="index">索引</param>
        /// <returns>返回结果</returns>
        private bool RemoveCellCandidate(Cell[,] table, int i, int j, int index)
        {
            int value = table[index / 9, index % 9].answer;
            bool flag = true;

            if (table[i, j].candidate.Contains(value))
            {
                //如果单元格候选数有此数，移除之
                table[i, j].candidate.Remove(value);

                if (table[i, j].candidate.Count == 0 && table[i, j].answer == 0)
                {
                    //如果单元格移除此候选数之后，该单元格未赋值且候选数为0，则失败，回滚
                    flag = false;
                }
            }
            else if (table[i, j].duplicateDel.ContainsKey(value))
            {
                //如果单元格候选数没有此数，且在重复删除的字典里有此数，则重复删除字典此数对应的健值的值+1
                table[i, j].duplicateDel[value]++;
            }
            else
            {
                //如果单元格候选数没有此数，且在重复删除的字典里没有此数，则重复删除字典添加此数的键值，并赋值为1
                table[i, j].DuplicateDel.Add(value, 1);
            }
            return flag;
        }

        /// <summary>
        /// 恢复单元格的候选数
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="i">行</param>
        /// <param name="j">列</param>
        /// <param name="index">索引</param>
        /// <returns>返回结果</returns>
        private bool RecoverCellCandidate(Cell[,] table, int i, int j, int index)
        {
            int value = table[index / 9, index % 9].Value;
            bool flag = true;

            if (table[i, j].DuplicateDel.ContainsKey(value))
            {
                //如果在重复删除的字典里有此数，则重复删除字典此数对应的键值的值-1
                if (--table[i, j].DuplicateDel[value] == 0)
                {
                    table[i, j].DuplicateDel.Remove(value);
                }
            }
            else if (!table[i, j].Candidate.Contains(value))
            {
                //如果单元格的候选数没有此数,添加之
                table[i, j].Candidate.Add(value);
            }

            return flag;
        }
        /// <summary>
        /// 填充单元格
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="index">索引</param>
        /// <returns>返回结果</returns>
        public bool FillCell(Cell[,] table, int index)
        {
            RelativeCellMethod removeCandidateMethod = new RelativeCellMethod(RemoveCellCandidate);
            RelativeCellMethod recoverCandidateMethod = new RelativeCellMethod(RecoverCellCandidate);

            if (index >= 81)
            {//如果索引超出范围，则表示数独已成功生成，直接返回
                return true;
            }
            if (table[index / 9, index % 9].answer != 0)
            {//如果索引的单元格已赋值，则直接跳到下一个索引赋值
                return FillCell(table, index + 1);
            }
            bool flag = true;
            List<int> nextCandidates = new List<int>();
            //预先保存好改单元格的候选数序列，如果所有候选数都不成功，则把候选数全部还原之后再返回
            nextCandidates.AddRange(table[index / 9, index % 9].candidate);

            while (table[index / 9, index % 9].candidate.Count > 0 && flag)
            {//如果单元格候选数个数大于0，且标记为真，则循环试探候选数
                SetValue(table[index / 9, index % 9]);//为单元格赋值
                flag &= DealRelativeCell(removeCandidateMethod, table, index);//移除相关单元格的对应这个值的候选数
                if (!flag)
                {//如果移除候选数失败，则恢复候选数，并继续下个循环
                    DealRelativeCell(recoverCandidateMethod, table, index);
                }
                else
                {//如果移除候选数成功，则继续试探填充下一个单元格
                    flag &= FillCell(table, index + 1);
                    if (!flag)
                    {//如果填充下一个单元格失败，则恢复候选数，并继续下个循环
                        DealRelativeCell(recoverCandidateMethod, table, index);
                    }
                    else
                    {//如果填充下一个单元格成功，则直接返回（运行到这里肯定表示整个数独已成功生成！）
                        return true;
                    }
                }
                flag = !flag;//把标志取反，继续下个循环
            }
            if (table[index / 9, index % 9].candidate.Count == 0)
            {//如果所有候选数都是过了且全部失败，恢复此单元格的候选数，并返回false
                table[index / 9, index % 9].candidate.AddRange(nextCandidates);
                return false;
            }
            return flag;
        }

        /// <summary>
        /// 显示数独
        /// </summary>
        /// <param name="table">表</param>
        private void Show(Cell[,] table)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write("{0,2}", table[i, j].answer);

                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------------------------");

        }


        /// <summary>
        /// 开始生成数独
        /// </summary>
        public void StartFillTable()
        {
            Cell[,] table = new Cell[9, 9];
            bool flag = false;
            while (!flag)
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++) //初始化数独表
                        table[i, j] = new Cell();

                flag = FillCell(table, 0);//填充数独表
                if (flag)//如果生成数独成功，则显示这个数独
                    Show(table);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 处理相关单元格
        /// 包括行，列和宫
        /// </summary>
        /// <param name="cellMethod">调用方法</param>
        /// <param name="table">表</param>
        /// <param name="index">索引</param>
        /// <returns>返回结果</returns>
        private bool DealRelativeCell(RelativeCellMethod cellMethod, Cell[,] table, int index)
        {
            bool flag = true;
            Cell cell = table[index / 9, index % 9];
            for (int i = 0; i < 9; i++)
            {
                //同列单元格
                if (i != index / 9)
                {
                    //不能等于本单元格
                    flag &= cellMethod(table, i, index % 9, index);
                }

                //同行单元格
                if (i != index % 9)
                {
                    //不能等于本单元格
                    flag &= cellMethod(table, index / 9, i, index);

                }
            }

            //宫内的其它四个单元格
            for (int i = nineCells[index / 9]; i < nineCells[index / 9] + 3; i++)
            {
                for (int j = nineCells[index % 9]; j < nineCells[index % 9] + 3; j++)
                {
                    if (i != index / 9 && j != index % 9)
                    {
                        flag &= cellMethod(table, i, j, index);
                    }
                }
            }
            if (cellMethod == RecoverCellCandidate)
            {
                cell.Value = 0;
            }
            return flag;
        }

    }
}
