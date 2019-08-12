using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 编译器
{
    class symble
    {
        public int number;//序号
        public int type; //类型
        public char[] name = new char[30];//名字
        public int id;
        public symble()
        {
            number = -1;
            type = -1;
            for (int i = 0; i < 30; i++)
                name[i] = ' ';
            id = 0;
        }
        public void setid(int a)
        {
            id = a;
        }
        public void setnumber(int a)
        {
            number = a;
        }
        public void setname(char[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                name[i] = a[i];
            }
        }
        public void settype(int a)
        {
            type = a;
        }
    }
}
