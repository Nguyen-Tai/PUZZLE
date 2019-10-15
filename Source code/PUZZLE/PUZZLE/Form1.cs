using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUZZLE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string path = "";
      
        private int[,] board = new int[10, 10];
        private Button[,] BtnArr = new Button[10, 10];
        private Image[,] ImgArr = new Image[10, 10];
        Stack<move> stUndo= new Stack<move>();
        Stack<move> slt = new Stack<move>();
        private int time;
        private bool TT_game;
   
        void initButton(int n)
        {
            int dem = 1;
            if(path!="")
               CropImage(comboBox1.SelectedIndex + 3);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    BtnArr[i, j] = new Button();
                    BtnArr[i, j].SetBounds(pnl.Width / n * j, pnl.Height / n * i,
                                        pnl.Width / n, pnl.Height / n);
                    BtnArr[i, j].Click += new EventHandler(this.bt_Click);
                    BtnArr[i, j].BackColor = Color.White;
                    BtnArr[i, j].Cursor = Cursors.Hand;
                    BtnArr[i, j].Text = dem++.ToString();
                    if(path=="")
                        BtnArr[i, j].Font = new Font("Consolas", BtnArr[i, j].Size.Height / 3, FontStyle.Regular);
                    else
                    {
                        BtnArr[i, j].Image = ImgArr[i, j];
                        BtnArr[i, j].Font = new Font("Consolas", 15, FontStyle.Regular);
                        BtnArr[i, j].TextAlign = ContentAlignment.TopLeft;
                        BtnArr[i, j].ForeColor = Color.White;
                    }
                    BtnArr[i, j].FlatStyle = FlatStyle.Flat;
                    BtnArr[i, j].FlatAppearance.BorderSize = 0;
                    BtnArr[i, j].FlatAppearance.CheckedBackColor = Color.Black;
                    pnl.Controls.Add(BtnArr[i, j]);
                }
            }
            BtnArr[n - 1, n - 1].Text = " ";
            BtnArr[n - 1, n - 1].Image = null;
        }
        public void swap(Button a, Button b)
        {

                string temp = a.Text;
                a.Text = b.Text;
                b.Text = temp;     
                Image i = a.Image;
                a.Image = b.Image;
                b.Image = i;
        }
        public void go_right(int n, int push)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
   
                        if (BtnArr[i, j].Text == " " && j + 1 < n)
                        {
                            swap(BtnArr[i, j], BtnArr[i, j + 1]);
                            if (push == 1)
                                stUndo.Push(move.Right);
                            return;
                        }                    
                }
        }
        public void go_left(int n, int push)
        {
       
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        if (BtnArr[i, j].Text == " " && j - 1 >= 0)
                        {
                            swap(BtnArr[i, j], BtnArr[i, j - 1]);
                            if (push == 1)
                                stUndo.Push(move.Left);
                            return;
                        }
                    }
            
      
        }
        public void go_up(int n, int push)
        {
           
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        if (BtnArr[i, j].Text == " " && i - 1 >= 0)
                        {
                            swap(BtnArr[i, j], BtnArr[i - 1, j]);
                            if (push == 1)
                                stUndo.Push(move.Up);
                            return;
                        }
                    }
            
        }
        public void go_down(int n, int push)
        {
          
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        if (BtnArr[i, j].Text == " " && i + 1 < n)
                        {
                            swap(BtnArr[i, j], BtnArr[i + 1, j]);
                            if (push == 1)
                                stUndo.Push(move.Down);
                            return;
                        }
                    }
            
        
        }
        public void init(int n)
        {
            stUndo.Clear();
            slt.Clear();
            for (int i = 0; i < 500; i++)
            {
                move rd = (move)MyRandom.Next(0, 4);
                switch (rd)
                {
                    case move.Up: go_up(n, 0); break;
                    case move.Right: go_right(n, 0); break;
                    case move.Down: go_down(n, 0); break;
                    case move.Left: go_left(n, 0); break;
                }
            }
        }
        private void bt_Click(object sender, EventArgs e)
        {
            if (TT_game == true)
            {
                Button b = sender as Button;
                for (int i = 0; i < comboBox1.SelectedIndex + 3; i++)
                    for (int j = 0; j < comboBox1.SelectedIndex + 3; j++)
                    {             
                            if (BtnArr[i, j] == b && BtnArr[i, j].Text != " " && i - 1 >= 0 && BtnArr[i - 1, j].Text == " ")//bottom
                            {
                                swap(b, BtnArr[i - 1, j]);
                                stUndo.Push(move.Down);
                                lbsobuocdi.Text = (stUndo.Count).ToString();
                            }
                            else if (BtnArr[i, j] == b && BtnArr[i, j].Text != " " && i + 1 < comboBox1.SelectedIndex + 3
                                       && BtnArr[i + 1, j].Text == " ")//top
                            {
                                swap(b, BtnArr[i + 1, j]);
                                stUndo.Push(move.Up);
                                lbsobuocdi.Text = (stUndo.Count).ToString();
                            }
                            else if (BtnArr[i, j] == b && BtnArr[i, j].Text != " " && j - 1 >= 0 && BtnArr[i, j - 1].Text == " ")//right
                            {
                                swap(b, BtnArr[i, j - 1]);
                                stUndo.Push(move.Right);
                                lbsobuocdi.Text = (stUndo.Count()).ToString();
                            }
                            else if (BtnArr[i, j] == b && BtnArr[i, j].Text != " " && j + 1 < comboBox1.SelectedIndex + 3
                                        && BtnArr[i, j + 1].Text == " ")//left
                            {
                                swap(b, BtnArr[i, j + 1]);
                                stUndo.Push(move.Left);
                                lbsobuocdi.Text = (stUndo.Count).ToString();
                            }
                            if (ktWin() == true)
                            {
                                timer.Stop();
                                MessageBox.Show("Bạn đã chiến thắng với thời gian là: " + labelTime.Text,
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnnewGame_Click(sender, e);
                            }                      
                    }
            }
        }
        public bool ktWin()
        {
                int dem = 1;
                for (int i = 0; i < comboBox1.SelectedIndex + 3; i++)
                    for (int j = 0; j < comboBox1.SelectedIndex + 3; j++)
                    {
                        if (i != comboBox1.SelectedIndex + 3 - 1 || j != comboBox1.SelectedIndex + 3 - 1)
                            if (BtnArr[i, j].Text != dem.ToString())
                                return false;
                        dem++;
                    }
                return true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (TT_game == true)
            {
                timer.Stop();
                timer1.Stop();
                TT_game = false;
                comboBox1.Enabled = true;
                btnPause.Text = "Chơi tiếp";
            }
            else
            {
                timer.Start();
                timer1.Start();
                TT_game = true;
                btnPause.Text = "Tạm dừng";
            }
        }
        private void btnnewGame_Click(object sender, EventArgs e)
        {
            timer.Stop();
            timer1.Stop();
            labelTime.Text = "00:00";
            time = 0;
            comboBox1.Enabled = true;
            TT_game = false;
            pnl.Controls.Clear();
            initButton(comboBox1.SelectedIndex + 3);
            init(comboBox1.SelectedIndex + 3);
            btnPause.Text = "Bắt đầu";
            lbsobuocdi.Text = "0";
          //  if (comboBox1.SelectedIndex + 3 > 3) btnslt.Enabled = false;
            //else btnslt.Enabled = true;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            labelTime.Text = (time / 60).ToString() + ":" + (time % 60).ToString() + "s";
            time++;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Undo(move a)
        {
            if (a == move.Up)
            {
                go_down(comboBox1.SelectedIndex + 3, 0);
                lbsobuocdi.Text = (stUndo.Count()).ToString();
            }
            if (a == move.Right)
            {
                go_left(comboBox1.SelectedIndex + 3, 0);
                lbsobuocdi.Text = (stUndo.Count()).ToString();
            }
            if (a == move.Down)
            {
                go_up(comboBox1.SelectedIndex + 3, 0);
                lbsobuocdi.Text = (stUndo.Count()).ToString();
            }
            if (a == move.Left)
            {
                go_right(comboBox1.SelectedIndex + 3, 0);
                lbsobuocdi.Text = (stUndo.Count()).ToString();
            }
        }
        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (stUndo.Count != 0)
                Undo(stUndo.Pop());

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Focus();
            btnnewGame_Click(sender, e);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
        private void Choose_Click(object sender, EventArgs e)
        {
            OpenFileDialog choose = new OpenFileDialog();
            choose.Filter =
                "Image Files (JPEG, GIF, PNG , JPG) |*.jpeg; *.gif; *.png; *.jpg";

            if (choose.ShowDialog() == DialogResult.OK)
            {
                picture.Image = resize(Image.FromFile(choose.FileName), picture.Size);
                path = choose.FileName;
                CropImage(comboBox1.SelectedIndex + 3);
                btnnewGame_Click(sender, e);
            }
        }
        private static Image resize(Image img, Size newsize)
        {
            Image newImage = new Bitmap(newsize.Width, newsize.Height);
            using (Graphics g = Graphics.FromImage((Bitmap)newImage))
            {
                g.DrawImage(img, new Rectangle(Point.Empty, newsize));
            }
            return newImage;
        }
        private void CropImage(int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ImgArr[i, j] = new Bitmap(pnl.Width/n, pnl.Height/n);
                    var graphics = Graphics.FromImage(ImgArr[i, j]);
                    graphics.DrawImage(resize(Image.FromFile(path), pnl.Size), new Rectangle(0, 0, pnl.Width / n, pnl.Height / n),
                        new Rectangle(j * pnl.Width / n, i * pnl.Height / n, pnl.Width/n, pnl.Height/n), GraphicsUnit.Pixel);
                    graphics.Dispose();             
                }
            }
        }
        private void GetSolver()
        {         
            GetBoard(BtnArr);     
            Best_First_Search Solver = new Best_First_Search();
            slt= Solver.timKetQua(board,comboBox1.SelectedIndex + 3);
            if (slt != null)
            {
                if (MessageBox.Show("   Tìm thấy lời giải với " + slt.Count.ToString() + " bước.\n\n   Giải ngay ?", "Thông báo",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    timer1.Start();
                    comboBox1.Enabled = false;
                }
            }
            else
                MessageBox.Show("Không tìm thấy lời giải", "Thông báo");
            
        }
        private void Btnslt_Click(object sender, EventArgs e)
        {
             Thread th = new Thread(GetSolver);
            th.Start();
            //GetSolver();
        }
        private void GetBoard(Button[,] btn)
        {
            for (int i = 0; i < comboBox1.SelectedIndex + 3; i++)
                for (int j = 0; j < comboBox1.SelectedIndex + 3; j++)
                    if (btn[i, j].Text == " ")
                        board[i, j] = 0;
                    else board[i, j] = Convert.ToInt32(btn[i, j].Text.ToString());
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 1001 - trackBar1.Value * 100;
        }
        private void MoveSlt(move a)
        {
            if (a == move.Down)
            {
                go_down(comboBox1.SelectedIndex + 3, 0);
                lbsobuocdi.Text = (stUndo.Count()).ToString();
            }
            if (a == move.Left)
            {
                go_left(comboBox1.SelectedIndex + 3, 0);
                lbsobuocdi.Text = (stUndo.Count()).ToString();
            }
            if (a == move.Up)
            {
                go_up(comboBox1.SelectedIndex + 3, 0);
                lbsobuocdi.Text = (stUndo.Count()).ToString();
            }
            if (a == move.Right)
            {
                go_right(comboBox1.SelectedIndex + 3, 0);
                lbsobuocdi.Text = (stUndo.Count()).ToString();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {     
                if (slt.Count != 0)
                    MoveSlt(slt.Pop());
                if (ktWin() == true)
                {
                    timer1.Stop();
                    timer.Stop();
                    MessageBox.Show("Bạn đã chiến thắng",
                        "Chúc mừng", MessageBoxButtons.OK);
                    btnnewGame_Click(sender, e);
                }
        }
    }
}
