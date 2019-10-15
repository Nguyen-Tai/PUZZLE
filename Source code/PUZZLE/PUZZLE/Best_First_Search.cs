using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUZZLE
{
    class Best_First_Search
    {
        public Stack<move> timKetQua(int[,] MaTran, int n)
        {

            Stack<move> stkKetQua = new Stack<move>();
            List<Node> Close = new List<Node>();
            List<Node> Open = new List<Node>();
            //khai báo và khởi tạo cho node đầu tiên
            Node tSo = new Node();
            tSo.MaTran = MaTran;
            tSo.hn = Somiengsai(MaTran, n);
            tSo.Parent = null;
            tSo.MoveDirection = move.None;
            //tSo.Cha = -1;
            //cho trạng thái đầu tiên vào Open;
            Open.Add(tSo);
            int t = 0;
            while (Open.Count > 0)
            {
                #region chọn node tốt nhất trong tập Open và chuyển nó sang Close
                tSo = new Node();
                tSo = Open[t];
                Open.Remove(tSo);
                Close.Add(tSo);
                #endregion
                //nếu node có số mảnh sai là 0, tức là đích thì thoát
                if (tSo.hn == 0)
                {
                   stkKetQua = truyVetKetQua(tSo);
                    return stkKetQua;
                }
                else
                {
                    //sinh hướng đi của node hiện tại
                    List<Node> lstHuongDi = new List<Node>();
                    lstHuongDi = sinhHuongDi(tSo, n);
                    for (int i = 0; i < lstHuongDi.Count; i++)
                    {
                        //hướng đi không thuộc Open và Close
                        if (!haiNodeTrungNhau(lstHuongDi[i], Open, n) && !haiNodeTrungNhau(lstHuongDi[i], Close, n))
                        {
                            Open.Add(lstHuongDi[i]);
                        }                       
                    }
                    //chọn vị trí có phí tốt nhất trong Open
                    t = viTriTotNhatOpen(Open);
                }
            }
            return null;
        }
        Stack<move> truyVetKetQua(Node kq)
        {
            Stack<move> ketQua = new Stack<move>();

            Node current = kq;
            ketQua.Push(current.MoveDirection);
            while (current.Parent.MoveDirection != move.None)
            {
                current = current.Parent;
                ketQua.Push(current.MoveDirection);
            }
            return ketQua;
        }
        bool haiMaTranBangNhau(int[,] a, int[,] b, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    if (a[i, j] != b[i, j])
                        return false;
            }
            return true;
        }
        public int Somiengsai(int[,] a, int n)
        {
            int dem = 1;
            int dung = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i != n - 1 || j != n - 1)
                        if (a[i, j] == dem)
                            dung++;

                    dem++;
                }
            return n * n - 1 - dung;
        }
        public bool haiNodeTrungNhau(Node tSo, List<Node> lst8So, int n)
        {
            for (int i = 0; i < lst8So.Count; i++)
                if (haiMaTranBangNhau(lst8So[i].MaTran, tSo.MaTran, n))
                    return true;

            return false;
        }
        public void copy(int[,] a, int[,] b, int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    b[i, j] = a[i, j];
        }
        List<Node> sinhHuongDi(Node tSo, int n)
        {
            List<Node> lstHuongDi = new List<Node>();

            #region  Xác định vị trí mảnh chống, có giá trị là 0
            int h = 0;
            int c = 0;
            bool ok = false;
            for (h = 0; h < n; h++)
            {
                for (c = 0; c < n; c++)
                    if (tSo.MaTran[h, c] == 0)
                    {
                        ok = true;
                        break;
                    }

                if (ok) break;
            }
            #endregion

            //  MessageBox.Show(h.ToString(), c.ToString());
            Node Temp = new Node();
            Temp.MaTran = new int[n, n];
            //Copy mảng Ma trận sang mảng ma trận tạm
            copy(tSo.MaTran, Temp.MaTran, n);   

            #region Xét các hướng đi theo 4 hướng: trên, dưới, phải, trái 
            if (h > 0 && h <= n - 1)            // trên
            {
                // MessageBox.Show("tren");
                // thay đổi hướng đi của ma trận
                Temp.MaTran[h, c] = Temp.MaTran[h - 1, c];
                Temp.MaTran[h - 1, c] = 0;

                //  for (int i = 0; i < n; i++)
                //     for (int j = 0; j < n; j++)
                //      MessageBox.Show(Temp.MaTran[i, j].ToString(),"ma tran len");
                //cập nhật lại thông số của node
                Temp.hn = Somiengsai(Temp.MaTran, n);
                Temp.MoveDirection = move.Up;
                Temp.Parent = tSo;
                lstHuongDi.Add(Temp);
                //sau khi thay đổi ma trận thì copy lại ma trận cha cho MaTran để xét trường hợp tiếp theo
                Temp = new Node();
                Temp.MaTran = new int[n, n];
                copy(tSo.MaTran, Temp.MaTran, n);
            }
            if (h < n - 1 && h >= 0)// xuống
            {
                // MessageBox.Show("xuong");

                // thay đổi hướng đi của ma trận
                Temp.MaTran[h, c] = Temp.MaTran[h + 1, c];
                Temp.MaTran[h + 1, c] = 0;

                //cập nhật lại thông số của node
                Temp.hn = Somiengsai(Temp.MaTran, n);
                Temp.MoveDirection = move.Down;
                Temp.Parent = tSo;
                lstHuongDi.Add(Temp);
                //sau khi thay đổi ma trận thì copy lại ma trận cha cho MaTran để xét trường hợp tiếp theo
                Temp = new Node();
                Temp.MaTran = new int[n, n];
                copy(tSo.MaTran, Temp.MaTran, n);
            }
            if (c > 0 && c <= n - 1)            // trái
            {
                // thay đổi hướng đi của ma trận
                Temp.MaTran[h, c] = Temp.MaTran[h, c - 1];
                Temp.MaTran[h, c - 1] = 0;

                //cập nhật lại thông số của node
                Temp.hn = Somiengsai(Temp.MaTran, n);
                Temp.MoveDirection = move.Left;
                Temp.Parent = tSo;
                lstHuongDi.Add(Temp);

                //sau khi thay đổi ma trận thì copy lại ma trận cha cho MaTran để xét trường hợp tiếp theo
                Temp = new Node();
                Temp.MaTran = new int[n, n];
                copy(tSo.MaTran, Temp.MaTran, n);
            }
            if (c < n - 1 && c >= 0)            // phải
            {
                // thay đổi hướng đi của ma trận
                Temp.MaTran[h, c] = Temp.MaTran[h, c + 1];
                Temp.MaTran[h, c + 1] = 0;
                //cập nhật lại thông số của node
                Temp.hn = Somiengsai(Temp.MaTran, n);
                Temp.MoveDirection = move.Right;
                Temp.Parent = tSo;
                lstHuongDi.Add(Temp);

                //đến đây đã xết hết hướng đi nên không cần copy lại ma trận
            }
            #endregion
            return lstHuongDi;
        }
        int viTriTotNhatOpen(List<Node> Open)
        {
            if (Open.Count > 0)
            {
                Node min = new Node();
                min = Open[0];
                int vt = 0;

                for (int i = 1; i < Open.Count; i++)
                    if (min.hn > Open[i].hn)
                    {
                        min = Open[i];
                        vt = i;
                    }
                return vt;
            }
            return 0;
        }
    }
}

