using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class FrmMain : Form
    {
        /// <summary>
        /// 记录标准数独
        /// </summary>
        

        /// <summary>
        /// 记录解题过程中的数独
        /// </summary>
        int[,] ProcessNineSudoku = new int[9, 9];

        /// <summary>
        /// 记录答案数独
        /// </summary>
        int[,] AnswerNineSudoku = new int[9, 9];

        /// <summary>
        /// 记录解题过程
        /// 例如[i,j]->9
        /// </summary>
        string txtlog = "";

        Button[,] buttons = new Button[9, 9];

        private CommonMethod CellMethod = new CommonMethod();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            NineBoard nineBoard = new NineBoard();
            this.splitContainer1.IsSplitterFixed = true;
            splitContainer1.SplitterDistance = 900;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            panel1.Width = 610;
            panel1.Height = 610;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    buttons[i, j] = new Button();
                    panel1.Controls.Add(buttons[i, j]);
                    buttons[i, j].Width = 64;
                    buttons[i, j].Height = 64;
                    buttons[i, j].Location = new Point(i * 64, j * 64);
                    buttons[i, j].Click += new EventHandler(Button_Click);
                    buttons[i, j].KeyPress += new KeyPressEventHandler(Button_KeyPress);
                }
            }
            initNineBoard();
            //InitShudu();
        }

        /// <summary>
        /// 初始化9*9数独面板
        /// </summary>
        private void initNineBoard()
        {
            StandardSudoku OrignalStandardSudoku = new StandardSudoku(9);

            bool flag = CellMethod.FillCell(OrignalStandardSudoku.Cells, 0);//填充数独表
            if (flag)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        buttons[i, j].Text = OrignalStandardSudoku.Cells[i, j].answer.ToString();

                        //初始化颜色
                        if (i <= 2)
                        {
                            if (j <= 2 || j >= 6)
                                buttons[i, j].BackColor = Color.BurlyWood;
                        }
                        else if (i >= 6)
                        {
                            if (j <= 2 || j >= 6)
                                buttons[i, j].BackColor = Color.BurlyWood;
                        }
                        else if ((i >= 3 && i <= 5))
                        {
                            if (j >= 3 && j <= 5)
                                buttons[i, j].BackColor = Color.BurlyWood;
                        }
                    }
                }
            }
        }

        private void Button_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && Char.IsDigit(e.KeyChar))
            {
                ((Button)sender).Text = e.KeyChar.ToString();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
        }

        private void 新的开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定开始新的游戏", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                initNineBoard();
            }
        }
    }
}
