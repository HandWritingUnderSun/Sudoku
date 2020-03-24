using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            FourLevelPanel fourLevelPanel = new FourLevelPanel();
            //fourLevelPanel.DigHole(5);
            fourLevelPanel.OutPrint();
        }
    }
}
