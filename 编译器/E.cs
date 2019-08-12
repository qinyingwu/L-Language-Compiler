using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译器
{
    class E
    {
        public List<int> F { set; get; }
        public List<int> T { set; get; }
        public E()
        {
            this.F = new List<int>();
            this.T = new List<int>();
        }
    }
}
