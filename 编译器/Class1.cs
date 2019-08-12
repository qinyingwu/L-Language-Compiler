using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译器
{
    class Class1
    {
        public static List<token> s = new List<token>();
        public static List<symble> s2 = new List<symble>();
        public static List<string> s1 = new List<string>();
        public static void settype()
        {
            for (int i = 0; i < s2.Count; i++)
            {
                if (s2[i].type == -1)
                {
                    if (s[s2[i].id + 1].code == 28 || s[s2[i].id + 1].code == 29)
                    {
                        if (s[s2[i].id + 1].code == 29)
                        {
                            if (s[s2[i].id + 2].code == 3 || s[s2[i].id + 2].code == 9 || s[s2[i].id + 2].code == 13)
                            {
                                s2[i].type = s[s2[i].id + 2].code;
                                for(int j=i+1;j<s2.Count;j++)
                                {
                                    if(string.Join("",s2[i].name).Equals(string.Join("",s2[j].name)))
                                        s2[j].type=s2[i].type;
                                }
                            }
                        }
                        else if (s[s2[i].id + 1].code == 28)
                        {
                            int j = s2[i].id + 2;
                            while(s[j].code == 18)
                            {
                                j = j + 1;
                                if (s[j].code == 28)
                                {
                                    j = j + 1;
                                }
                                else if (s[j].code == 29)
                                {
                                    j = j + 1;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (s[j].code == 3 || s[j].code == 9 || s[j].code == 13)
                            {
                                for (int h = i; h < s[j - 2].addr; h++)
                                {
                                    s2[h].type = s[j].code;
                                    for (int j1 = i + 1; j1 < s2.Count; j1++)
                                    {
                                        if (string.Join("", s2[h].name).Trim().Equals(string.Join("", s2[j1].name).Trim()))
                                            s2[j1].type = s2[h].type;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
