using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 编译器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
           for (int i = 0; i < Class1.s.Count; i++)
            {
                textBox4.Text = textBox4.Text + "(" + Class1.s[i].label.ToString() + "," + "“"+string.Join("", Class1.s[i].name).Trim()+"”" + "," + Class1.s[i].code.ToString() + "," + Class1.s[i].addr.ToString() + ")";
                textBox4.Text = textBox4.Text + "\r\n";
            }
            //textBox4.Text = textBox4.Text + "(" + Class1.s[1].label.ToString() + "," + string.Join("", Class1.s[1].name).Trim() + "," + Class1.s[1].code.ToString() + "," + Class1.s[1].addr.ToString() + ")";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cifa f = new cifa();
            language1 l = new language1();
            l.CSH(f);
            textBox2.Text = l.error;
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = "";
                this.openFileDialog1.ShowDialog();
                StreamReader sr = new StreamReader(this.openFileDialog1.FileName, Encoding.GetEncoding("GB2312"));
                String line;
                int j = 1;
                int k = 0;
                int L=0;
                int m = 0;
                token d = new token();
                symble h = new symble();
                while ((line = sr.ReadLine()) != null)
                {
                    textBox1.Text = textBox1.Text + "(" + j.ToString() + ")" + line.ToString().Trim();
                    textBox1.Text = textBox1.Text + "\r\n";
                    string a = line.ToString().ToLower().Replace(" ","");
                    for (int i = 0; i < a.Length; i++)
                    {
                        int b = (int)a[i];
                        bool y=false;
                        if (b >= 97 && b <= 122)//关键字处理
                        {
                            int[] p = d.setname(a[i],k);
                            d.id = 1;
                            if (p[1] != 0)
                                {
                                    if (p[0] == 0)
                                    {
                                        d.setlabel(L);
                                        d.setaddr(-1);
                                        d.setcode(p[1]);
                                        L++;
                                        k = 0;
                                        y = true;
                                        Class1.s.Add(d);
                                        d = new token();
                                    }
                                    else
                                    {
                                        string a1 = "";
                                        for (int i1 = 0; i1 < p[0]; i1++)
                                        {
                                            a1 = a1 + d.name[i1];
                                        }
                                        string a2 = "";
                                        for (int i1 = p[0]; i1 <=k; i1++)
                                        {
                                            a2 = a2 + d.name[i1];
                                        }
                                        d = new token();
                                        d.set(a1);
                                        if (d.id == 1)
                                        {
                                            d.setlabel(L);
                                            d.setaddr(m);
                                            d.setcode(18);
                                            h.setnumber(m);
                                            h.setname(d.name);
                                            h.setid(L);
                                            Class1.s2.Add(h);
                                            h = new symble();
                                            L++;
                                            k = 0;
                                            m++;
                                            Class1.s.Add(d);
                                            d = new token();
                                        }
                                        else
                                        {
                                            if (d.id1 == 0)//整数处理
                                    {
                                        d.setlabel(L);
                                        d.setaddr(m);
                                        d.setcode(19);
                                        h.setnumber(m);
                                        h.settype(9);
                                        h.setid(L);
                                        h.setname(d.name);
                                        Class1.s2.Add(h);
                                        h = new symble();
                                        L++;
                                        k = 0;
                                        m++;
                                        Class1.s.Add(d);
                                        d = new token();
                                    }
                                    else if(d.id1==1)
                                    {
                                        d = new token();
                                        d.set(a1);
                                        d.setlabel(L);
                                        d.setaddr(m);
                                        d.setcode(20);
                                        h.setnumber(m);
                                        h.settype(13);
                                        h.setid(L);
                                        h.setname(d.name);
                                        Class1.s2.Add(h);
                                        h = new symble();
                                        L++;
                                        k = 0;
                                        m++;
                                        Class1.s.Add(d);
                                        d = new token();
                                    }
                                        }
                                        d = new token();
                                        d.set(a2);
                                        d.setlabel(L);
                                        d.setaddr(-1);
                                        d.setcode(p[1]);
                                        L++;
                                        k = 0;
                                        y = true;
                                        Class1.s.Add(d);
                                        d = new token();
                                    }
                                }
                            if(!y)
                              k++;
                        }
                        else if ((b >= 48 && b <= 57)||b==46)//数字输入
                        {
                            int[] p;
                            bool b1 = false;
                            if (b == 46)
                            {
                                if (d.id != 1 && d.name[0]!=' ')
                                    d.id1 = d.id1 + 1;
                                else
                                {
                                    if (d.name[0] != ' ')
                                    {
                                        b1 = true;
                                        d.setlabel(L);
                                        d.setaddr(m);
                                        d.setcode(18);
                                        h.setnumber(m);
                                        h.setid(L);
                                        h.setname(d.name);
                                        Class1.s2.Add(h);
                                        h = new symble();
                                        L++;
                                        k = 0;
                                        m++;
                                        Class1.s.Add(d);
                                        d = new token();
                                    }
                                }
                            }
                            if (!b1)
                            {
                                p = d.setname(a[i], k);
                                k++;
                            }
                        }
                        else if((b>=40 && b<=45)|| b==47 || (b>=58 && b<=62))
                        {
                            char t1=a[i];
                            char t=' ';
                            int t2 = 0;
                            switch (t1)
                            {
                                case ':':
                                    if (i < a.Length - 1)
                                    {
                                        if (a[i + 1] == '=')
                                        {
                                            t = '=';
                                            t2 = 1;
                                        }
                                    }
                                    break;
                                case '<':
                                    if (i < a.Length - 1)
                                    {
                                        if (a[i + 1] == '=')
                                        {
                                            t = '=';
                                            t2 = 1;
                                        }
                                        else if (a[i + 1] == '>')
                                        {
                                            t2 = 1;
                                            t = '>';
                                        }
                                    }
                                    break;
                                case '>':
                                    if (i < a.Length - 1)
                                    {
                                        if (a[i + 1] == '=')
                                        {
                                            t = '=';
                                            t2 = 1;
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                            if (d.id == 1)//标识符处理
                            {
                                if (d.name[0] != ' ')
                                {
                                    d.setlabel(L);
                                    d.setaddr(m);
                                    d.setcode(18);
                                    h.setnumber(m);
                                    h.setname(d.name);
                                    h.setid(L);
                                    Class1.s2.Add(h);
                                    h = new symble();
                                    L++;
                                    k = 0;
                                    m++;
                                    Class1.s.Add(d);
                                    d = new token();
                                    int[] p = d.setname(a[i], k);
                                    if (t != ' ')
                                    {
                                        i = i + 1;
                                        k++;
                                        p = d.setname(t, k);
                                    }
                                    d.setlabel(L);
                                    d.setaddr(-1);
                                    d.setcode(p[1]);
                                    L++;
                                    k = 0;
                                    Class1.s.Add(d);
                                    d = new token();
                                }
                                else
                                {
                                    int[] p = d.setname(a[i], k);
                                    if (t != ' ')
                                    {
                                        i = i + 1;
                                        k++;
                                        p = d.setname(t, k);
                                    }
                                    d.setlabel(L);
                                    d.setaddr(-1);
                                    d.setcode(p[1]);
                                    L++;
                                    k = 0;
                                    Class1.s.Add(d);
                                    d = new token();
                                }
                            }
                            else
                            {
                                if (d.id1 == 0)//整数处理
                                {
                                    if (d.name[0] != ' ')
                                    {
                                        d.setlabel(L);
                                        d.setaddr(m);
                                        d.setcode(19);
                                        h.setnumber(m);
                                        h.settype(9);
                                        h.setid(L);
                                        h.setname(d.name);
                                        Class1.s2.Add(h);
                                        h = new symble();
                                        L++;
                                        k = 0;
                                        m++;
                                        Class1.s.Add(d);
                                        d = new token();
                                        int[] p = d.setname(a[i], k);
                                        if (t != ' ')
                                        {
                                            i = i + 1;
                                            k++;
                                            p = d.setname(t, k);
                                        }
                                        d.setlabel(L);
                                        d.setaddr(-1);
                                        d.setcode(p[1]);
                                        L++;
                                        k = 0;
                                        Class1.s.Add(d);
                                        d = new token();
                                    }
                                    else
                                    {
                                        int[] p = d.setname(a[i], k);
                                        if (t != ' ')
                                        {
                                            i = i + 1;
                                            k++;
                                            p = d.setname(t, k);
                                        }
                                        d.setlabel(L);
                                        d.setaddr(-1);
                                        d.setcode(p[1]);
                                        L++;
                                        k = 0;
                                        Class1.s.Add(d);
                                        d = new token();
                                    }
                                }
                                else if (d.id1 == 1)//实数处理
                                {
                                    if (d.name[0] != ' ')
                                    {
                                        d.setlabel(L);
                                        d.setaddr(m);
                                        d.setcode(20);
                                        h.setnumber(m);
                                        h.settype(13);
                                        h.setid(L);
                                        h.setname(d.name);
                                        Class1.s2.Add(h);
                                        h = new symble();
                                        L++;
                                        k = 0;
                                        m++;
                                        Class1.s.Add(d);
                                        d = new token();
                                        int[] p = d.setname(a[i], k);
                                        if (t != ' ')
                                        {
                                            i = i + 1;
                                            k++;
                                            p = d.setname(t, k);
                                        }
                                        d.setlabel(L);
                                        d.setaddr(-1);
                                        d.setcode(p[1]);
                                        L++;
                                        k = 0;
                                        Class1.s.Add(d);
                                        d = new token();
                                    }
                                    else
                                    {
                                        int[] p = d.setname(a[i], k);
                                        if (t != ' ')
                                        {
                                            i = i + 1;
                                            k++;
                                            p = d.setname(t, k);
                                        }
                                        d.setlabel(L);
                                        d.setaddr(-1);
                                        d.setcode(p[1]);
                                        L++;
                                        k = 0;
                                        Class1.s.Add(d);
                                        d = new token();
                                    }
                                }
                            }
                        }
                    }
                    j++;
                }
            }
            catch (Exception ex)
            {
                textBox2.Text = "";
                textBox2.Text = ex.ToString();
            }
            Class1.settype();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //a~z的ASCII值为97~122，A~Z的ASCII值为65~90，0~9的ASCII值为48~57,(~,的ASCII值为40~47,:~>的ASCII值为58~62,:为58，>62,<60,=61
            char[] a=new char[5];
            a[0]='a';
            a[1]='b';
            string b = string.Join("",a);
            a=new char[5];
            a[0] = (char)62;
            a[0]=new char();
            a[0]='=';
            int f=(int)a[0];
            string f1 = "JOHN";
            Class1.s1.Add(f1);
            f1 = "hy";
            Class1.s1.Add(f1);
            textBox2.Text = f.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Class1.s2.Count; i++)
            {
                textBox3.Text = textBox3.Text + "(" + Class1.s2[i].number.ToString() + "," +"“"+ string.Join("", Class1.s2[i].name).Trim() +"”"+ "," + Class1.s2[i].type.ToString() + ")";
                textBox3.Text = textBox3.Text + "\r\n";
            }
        }
    }
}
