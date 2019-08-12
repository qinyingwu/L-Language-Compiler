using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 编译器
{
    class cifa
    {
        private token b;
        private symble a;
        public List<token> tokenList = new List<token>();
        public List<symble> symbles = new List<symble>();
        public List<E> errors = new List<E>();
        public string[] machineCodes=new string[38];
        public Code code1 = new Code();
        public cifa() 
        {
            tokenList = Class1.s;
            symbles = Class1.s2;
            machineCodes = code1.a;
        }
        
    }
}
