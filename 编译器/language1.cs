using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 编译器
{
    class language1
    {
        public List<token> Tokens;
        public List<symble> FHB;
        public string error = "";
        int i = 0;
        public void CSH(cifa m)
        {
            Tokens = m.tokenList;
            FHB = m.symbles;
            M_function();
        }
        public language1()
        {

        }
        private void next()//下一项
        {
            if (i < Tokens.Count - 2)
            {
                i++;
            }
        }
        private void Before()//前一项
        {
            if (i > 0)
            {
                i--;
            }
        }
        //"主函数";
        public void M_function()
        {
            if (Tokens[i].code == 12)//有关键字 program
            {
                next();
                if (Tokens[i].code == 18)//下一个是标识符
                {
                    next();
                    P_body();
                }
                else
                {
                    error = "该程序program没有标识符/程序体";
                }
            }
            else
            {
                error = "该程序缺少program关键字";
            }
        }
        //程序体
        public void P_body()
        {
            if (Tokens[i].code == 16) //是var
            {
                next();
                V_define();
            }
            else if (Tokens[i].code == 2)
            {
                next();
                C_S();
            }
            else
            {
                error = "程序缺少var/begin关键字";
            }
        }
        //判断单词是否是标识符
        private bool IsTAG()
        {
            if (Tokens[i].code == 18)//如果是标识符
            {
                next();
                if (Tokens[i].code == 28)//下一个是逗号,则继续判断
                {
                    next();
                    return IsTAG();//递归调用
                }
                else
                {
                    Before();//i-1;指向前面一项
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        //变量定义
        private void V_define()
        {
            if (IsTAG())//该字是标识符
            {
                next();
                if (Tokens[i].code == 29)//冒号
                {
                    next();
                    if (Tokens[i].code == 3 || Tokens[i].code == 9 || Tokens[i].code == 13)//判断是否是三种类型之一
                    {
                        int j = i;
                        j = j - 2;
                        FHB[Tokens[j].addr].type = Tokens[i].code;//定义正确，在符号表中标记该标识符类型;
                        j--;
                        while (Tokens[j].code == 28)//同时定义多个标识符
                        {
                            j--;
                            FHB[Tokens[j].addr].type = Tokens[i].code;
                        }
                        next();
                        if (Tokens[i].code == 30)//如果是分号
                        {
                            if (Tokens[i].code == 2)//如果是begin，则执行复合语句，否则，继续变量定义部分
                            {
                                next();
                                C_S();
                            }
                            else
                            {
                                V_define();
                            }
                        }
                        else
                        {
                            error = "该语句缺少分号 ；";
                        }
                    }
                    else
                    {
                        error = "该变量缺少类型说明";
                    }
                }
                else
                {
                    error = "应当输入冒号  ：";
                }
            }
            else
            {
                error = "该变量不是合法标识符";
            }
        }
        //复合语句
        private void C_S()
        {
            I_list();//执行语句表
            next();
            if (error == "")
            {
                if (Tokens[i].code == 30)  //end前必须有一个分号 ；
                {
                    next();
                    if (Tokens[i].code == 2 || Tokens[i].code == 8 || Tokens[i].code == 17)
                    {
                        I_S();
                    }
                    else if (Tokens[i].code == 6) //end
                    {
                        return;
                    }
                    else
                    {
                        error = "必须以end结尾";
                    }
                }
                else
                {
                    error = "end前缺少一个分号 ；";
                }
            }
            else
            {
                error = "语句表中有错误";
            }
        }
        //《语句表》
        private void I_list()
        {
            I_H();//执行句
            if (error == "")
            {
                next();
                if (Tokens[i].code == 30)//如果下一个是 分号 ；
                {
                    next();
                    I_list();
                }
                else
                {
                    Before();
                }
            }
            else
            {
                error = "执行句中有问题";
            }
        }
        //《执行句》
        private void I_H()
        {
            if (Tokens[i].code == 18)//如果是标识符，则是简单句
            {
                next();
                Simple_S();//执行赋值语句
            }
            else if (Tokens[i].code == 2 || Tokens[i].code == 8 || Tokens[i].code == 17)
            {//就执行结构句
                I_S();
            }
            else
            {
                Before();//回退
            }
        }
        //<赋值句>
        private void Simple_S()
        {
            if (Tokens[i].code == 31)//如果是 ":="符号
            {
                next();
                expression();//表达式
            }
            else
            {
                error = "赋值句变量后缺少：=";
            }
        }
        //<结构句>
        private void I_S()
        {
            if (Tokens[i].code == 2) //以 begin 开头 执行复合句
            {
                next();
                C_S();
            }
            else if (Tokens[i].code == 8)//以 if 开头 执行if语句
            {
                next();
                IF_S();
            }
            else if (Tokens[i].code == 17) //以 while 开头 执行while语句
            {
                next();
                While_S();
            }
            else
            {
                error = "结构体出错";
            }
        }
        //<if>语句
        private void IF_S()
        {
            Boolexp();//先执行布尔表达式
            if (error == "")
            {
                next();
                if (Tokens[i].code == 14)//如果后面是then 
                {
                    next();
                    I_H();//再执行 执行句
                    next();
                    if (Tokens[i].code == 5)//如果后面是 else
                    {
                        next();
                        I_H(); //又执行  执行句
                    }
                    else
                    {
                        Before();
                        return;
                    }
                }
                else
                {
                    error = "if语句中缺少 then";
                }
            }
            else
            {
                error = "if语句中的布尔表达式出现错误";
            }
        }
        //<while>语句
        private void While_S()
        {
            Boolexp();//先执行布尔表达式
            if (error == "")
            {
                next();
                if (Tokens[i].code == 4) //如果下一项是 do
                {
                    next();
                    I_H();
                }
                else
                {
                    error = "while表达式缺少do";
                }
            }
            else
            {
                error = "while表达式中布尔表达式出现错误";
            }
        }
        //<表达式>
        private void expression()
        {
            if (Tokens[i].code == 7 || Tokens[i].code == 15 || (Tokens[i].code != -1 && FHB[Tokens[i].addr].type == 3))//不为关键字且为bool型
            {
                Boolexp();//执行布尔表达式
            }
            else
            {
                Aritexp();//执行算术表达式
            }
        }
        //《布尔表达式》
        private void Boolexp()
        {
            boolitem();//布尔项
            if (error == "")
            {
                next();
                if (Tokens[i].code == 11)//如果是 or
                {
                    next();
                    Boolexp();
                }
                else
                {
                    Before();
                }
            }
            else
            {
                return;
            }

        }
        //<布尔项>
        private void boolitem()
        {
            bool_factor();
            if (error == "")
            {
                if (Tokens[i].code == 1) //如果是 and 
                {
                    next();
                    boolitem();
                }
                else
                {
                    Before();
                }
            }
        }
        //《布尔因子》
        private void bool_factor()
        {
            if (Tokens[i].code == 10)//如果是not
            {
                next();
                bool_factor();//布尔因子
            }
            else
            {
                boolQ();
            }
        }
        //<布尔量>
        private void boolQ()
        {
            if (Tokens[i].code == 7 || Tokens[i].code == 15)//布尔常数：true 或者 false
            {
                return;
            }
            else if (Tokens[i].code == 18)  //标识符
            {
                next();
                if (Tokens[i].code == 32 || Tokens[i].code == 33 || Tokens[i].code == 34 || Tokens[i].code == 35 || Tokens[i].code == 36 || Tokens[i].code == 37)
                {//分别为6种关系运算符
                    next();
                    if (Tokens[i].code != 18)
                    {
                        error = "关系运算符后面缺少标识符";
                    }
                }
                else
                {
                    Before();
                }
            }
            else if (Tokens[i].code == 21)//如果是 （  表示为布尔表达式
            {
                Boolexp();//执行布尔表达式
                if (Tokens[i].code == 22)  //字符是 ）
                {
                    return;
                }
                else
                {
                    error = "应当输入“）”";
                }
            }
            else
            {
                error = "布尔量出错";
            }
        }
        //<算术表达式>
        private void Aritexp()
        {
            Item();//执行项
            if (error == "")
            {
                next();
                if (Tokens[i].code == 23 || Tokens[i].code == 24)//运算符为+或者-
                {
                    next();
                    Aritexp();
                }
                else
                {
                    Before();
                }
            }
            else
            {
                return;
            }
        }
        //<项>
        private void Item()
        {
            factor();//执行因子
            if (error == "")
            {
                next();
                if (Tokens[i].code == 25 || Tokens[i].code == 26)//运算符为*或/
                {
                    next();
                    Item();
                }
                else
                {
                    Before();
                }
            }
            else
            {
                return;
            }
        }
        //<因子>
        private void factor()
        {
            if (Tokens[i].code == 21) //出现 （  执行算术表达式
            {
                next();
                Aritexp();
                next();
                if (Tokens[i].code == 22)//右括号 ） 与前面匹配
                {
                    return;
                }
                else
                {
                    error = "算术表达式中缺少“）”";
                }
            }
            else
            {
                Aritamount();//执行算术量
            }
        }
        //<算术量>
        private void Aritamount()
        {
            if (Tokens[i].code == 18 || Tokens[i].code == 19 || Tokens[i].code == 20)//为标识符，常数，实数
            {
                return;
            }
            else
            {
                error = "算术量出错！";
            }
        }
    }
}
