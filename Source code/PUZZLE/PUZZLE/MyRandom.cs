using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUZZLE
{
    class MyRandom
    {     
        static Random r = new Random();
        public static int Next(int n1, int n2) { return r.Next(n1, n2); }
    }
}
