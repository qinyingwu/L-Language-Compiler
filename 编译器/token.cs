using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 编译器
{
    class token
    {
       public int label; 
       public char[] name=new char[30]; 
       public int code; 
       public int addr;
       public int id;
       public int id1;
       public Code c=new Code();
       public token()
       {
           id = 0;
           id1 = 0;
           for (int i = 0; i < 30; i++)
               name[i] = ' ';
       }
       public int[] setname(char a,int i)
       {
           name[i]=a;
           int w = 0;
           int q = -1;
           int[] p=new int[2];
           string b = "";
           for (int k = i; k >= 0; k--)
           {
               b =name[k]+b;
           for (int j = 1; j <= 37; j++)
           {
               if (b.Equals(c.a[j]))
               {
                   q = k;
                   w = j;
                   break;
               }
           }
           }
           if (w > 0)
           {
               p[0] = q;
               p[1] = w;
               return p;
           }
           else
           {
               p[0] = -1;
               p[1] = 0;
               return p;
           }
       }
       public void setlabel(int a)
       {
           label = a;
       }
       public void setcode(int a)
       {
           code = a;
       }
       public void setaddr(int a)
       {
           addr = a;
       }
       public void set(string a)
       {
           char[] b = a.ToCharArray();
           for (int c = 0; c < b.Length; c++)
           {
               int r=(int)b[c];
               if ((r < 48 || r > 57) && r != 46)
                   id = 1;
               if (b[c] == '.')
                   id1 = id1 + 1;
               name[c] = b[c];
           }
       }
    }
}
