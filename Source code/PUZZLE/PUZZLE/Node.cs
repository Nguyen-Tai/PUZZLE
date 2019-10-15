using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUZZLE
{
    class Node
    {
        public int[,] MaTran;// ma trận                          
        public move MoveDirection;// cha của node, để truy vét kết quả , cha move thành con
        public Node Parent;
        public int hn;  // chi phí từ nút hiện tại tới đích 
    }
}
