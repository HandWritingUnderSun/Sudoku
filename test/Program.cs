using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku;
using Common;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string a = "FF";
                int b = Convert.ToInt32(a);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message.ToString(), ex);
            }
            //FourLevelPanel fourLevelPanel = new FourLevelPanel();
            ////fourLevelPanel.DigHole(5);
            //fourLevelPanel.OutPrint();
        }
    }
}
