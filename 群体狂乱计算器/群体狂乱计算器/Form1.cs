using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 群体狂乱计算器
{
    public partial class Form1 : Form
    {
        public void Attack(ref minion a, ref minion b)
        {

            if (a.isDivineShield == 0)
            {
                a.hp -= b.atk;
            }
            else
            {
                a.isDivineShield = 0;
            }
            if (b.isDivineShield == 0)
            {
                b.hp -= a.atk;
            }
            else
            {
                b.isDivineShield = 0;
            }
            a.hasAtk = 1;
            if (a.hp <= 0)
            {
                a.isAlive = 0;
            }
            if (b.hp <= 0)
            {
                b.isAlive = 0;
            }
        }

        int TestTimes = 1;

        TextBox[] textBoxes;
        List<minion> minions = new List<minion>();
        List<int> atkMinions = new List<int>();
        List<int> aliveMinions = new List<int>();
        Random random = new Random((int)DateTime.Now.Ticks);
        public Form1()
        {
            InitializeComponent();
        }
        private List<T> RandomSort<T>(List<T> list)
        {
            //var random = new Random((int)DateTime.Now.Ticks);
            var newList = new List<T>();
            foreach (var item in list)
            {
                newList.Insert(random.Next(newList.Count + 1), item);
            }
            return newList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String tmpCM = "";
            for (int i = 0; i < 14; i++)
            {
                if (textBoxes[i].Text.ToString().Equals("0:0"))
                {
                    tmpCM += ";";
                }
                else
                {
                    tmpCM += textBoxes[i].Text.ToString() + ";";
                }

            }
            textBox15.Text += System.Environment.NewLine + "一次实验，实验场面：" + tmpCM + System.Environment.NewLine;
            minions.Clear();
            atkMinions.Clear();
            aliveMinions.Clear();
            for (int i = 0; i < 14; i++)
            {
                minion tmp = new minion(textBoxes[i].Text.ToString(), i);
                minions.Add(tmp);
                if (tmp.hp > 0)
                {
                    atkMinions.Add(i);
                    aliveMinions.Add(i);
                }
            }
            atkMinions = RandomSort<int>(atkMinions);//随机攻击先后
            for (int i = 0; i < atkMinions.Count; i++)
            {
                if (minions[atkMinions[i]].hp <= 0) continue;//死了就不能诈尸
                aliveMinions = RandomSort<int>(aliveMinions);//被攻击的随从随机排序
                for (int j = 0; j < aliveMinions.Count; j++)
                {
                    if (aliveMinions[j] == atkMinions[i]) continue;//被攻击的不能是自己
                    if (minions[aliveMinions[j]].hp <= 0) continue;//死掉的不能被攻击
                    minion tmp1 = minions[atkMinions[i]];
                    minion tmp2 = minions[aliveMinions[j]];
                    Attack(ref tmp1, ref tmp2);//攻击
                    minions[atkMinions[i]] = tmp1;
                    minions[aliveMinions[j]] = tmp2;
                    break;//退出循环，只攻击一次
                }

            }

            for (int i = 0; i < 14; i++)
            {
                string tmp = minions[i].atk.ToString() + ":" + minions[i].hp.ToString();
                textBoxes[i].Text = tmp;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxes = new TextBox[14];
            textBoxes[0] = textBox1;
            textBoxes[1] = textBox2;
            textBoxes[2] = textBox3;
            textBoxes[3] = textBox4;
            textBoxes[4] = textBox5;
            textBoxes[5] = textBox6;
            textBoxes[6] = textBox7;
            textBoxes[7] = textBox8;
            textBoxes[8] = textBox9;
            textBoxes[9] = textBox10;
            textBoxes[10] = textBox11;
            textBoxes[11] = textBox12;
            textBoxes[12] = textBox13;
            textBoxes[13] = textBox14;
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 2;

        }

        private void button2_Click(object sender, EventArgs e)
        {



            int times = int.Parse(comboBox1.SelectedItem.ToString());
            TestTimes = times;
            int cntPoint = 0;
            int AllmyAtkF = 0, AllmyAtkS = 0, AllemAtkF = 0, AllemAtkS = 0, AllmySur = 0, AllemSur = 0;
            for (int iiiiii = 0; iiiiii < times; iiiiii++)
            {
                minions.Clear();
                atkMinions.Clear();
                aliveMinions.Clear();
                for (int i = 0; i < 14; i++)
                {
                    minion tmp = new minion(textBoxes[i].Text.ToString(), i);
                    minions.Add(tmp);
                    if (tmp.hp > 0)
                    {
                        atkMinions.Add(i);
                        aliveMinions.Add(i);
                    }
                }
                atkMinions = RandomSort<int>(atkMinions);//随机攻击先后
                for (int i = 0; i < atkMinions.Count; i++)
                {
                    if (minions[atkMinions[i]].hp <= 0) continue;//死了就不能诈尸
                    aliveMinions = RandomSort<int>(aliveMinions);//被攻击的随从随机排序
                    for (int j = 0; j < aliveMinions.Count; j++)
                    {
                        if (aliveMinions[j] == atkMinions[i]) continue;//被攻击的不能是自己
                        if (minions[aliveMinions[j]].hp <= 0) continue;//死掉的不能被攻击
                        minion tmp1 = minions[atkMinions[i]];
                        minion tmp2 = minions[aliveMinions[j]];
                        Attack(ref tmp1, ref tmp2);//攻击
                        minions[atkMinions[i]] = tmp1;
                        minions[aliveMinions[j]] = tmp2;
                        break;//退出循环，只攻击一次
                    }

                }

                int myAtkF = 0, myAtkS = 0, emAtkF = 0, emAtkS = 0, mySur = 0, emSur = 0;
                for (int i = 0; i < 14; i++)
                {

                    if (minions[i].isMine == 1)
                    {
                        if (minions[i].hp > 0)
                        {
                            mySur = 1;//我方有存活
                            myAtkF += 1;
                            myAtkS += minions[i].atk;
                        }
                    }
                    else
                    {
                        if (minions[i].hp > 0)
                        {
                            emSur = 1;
                            emAtkF += 1;
                            emAtkS += minions[i].atk;
                        }
                    }


                }
                AllmyAtkF += myAtkF;
                AllmyAtkS += myAtkS;
                AllemAtkF += emAtkF;
                AllemAtkS += emAtkS;
                AllmySur += mySur;
                AllemSur += emSur;
            }
            String tmpCM = "";
            for (int i = 0; i < 14; i++)
            {
                if (textBoxes[i].Text.ToString().Equals("0:0"))
                {
                    tmpCM += ";";
                }
                else
                {
                    tmpCM += textBoxes[i].Text.ToString() + ";";
                }

            }
            textBox15.Text += System.Environment.NewLine + "实验情况：" + tmpCM + System.Environment.NewLine;
            textBox15.Text += "实验次数：" + times.ToString() + System.Environment.NewLine + "平均敌方攻击频率：" + (float)AllemAtkF / times + System.Environment.NewLine + "平均敌方场攻：" + (float)AllemAtkS / times + System.Environment.NewLine + "平均我方攻击频率：" + (float)AllmyAtkF / times + System.Environment.NewLine + "平均我方场攻：" + (float)AllmyAtkS / times + System.Environment.NewLine + "我方留怪概率：" + (float)AllmySur/ times + System.Environment.NewLine + "敌方留怪概率：" + (float)AllemSur / times + System.Environment.NewLine + System.Environment.NewLine;
            this.textBox15.Focus();//获取焦点
            this.textBox15.Select(this.textBox15.TextLength, 0);//光标定位到文本最后
            this.textBox15.ScrollToCaret();//滚动到光标处
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String tmp = "";
            for (int i = 0; i < 14; i++)
            {
                tmp += textBoxes[i].Text.ToString() + ";";
            }
            Clipboard.SetText(tmp);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String tmp = Clipboard.GetText();
            String[] tmps = tmp.Split(';');
            if (tmps.Length < 14)
            {
                MessageBox.Show("剪切板数据错误");
                return;
            }
            for (int i = 0; i < 14; i++)
            {
                textBoxes[i].Text = tmps[i];
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                String tmp = textBoxes[i].Text;
                textBoxes[i].Text = textBoxes[i + 7].Text;
                textBoxes[i + 7].Text = tmp;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBoxes[7].Text.Equals("")) return;
            if (textBoxes[7].Text[0] == '+')
            {
                for (int i = 7; i < 14; i++)
                {
                    if (textBoxes[i].Text.Equals("")) continue;
                    if (textBoxes[i].Text[0] == '+')
                    {
                        textBoxes[i].Text = textBoxes[i].Text.Substring(1);//去掉+号
                    }
                }
            }
            else
            {
                for (int i = 7; i < 14; i++)
                {
                    if (textBoxes[i].Text.Equals("")) continue;
                    if (textBoxes[i].Text[0] != '+')
                    {
                        textBoxes[i].Text = "+" + textBoxes[i].Text;//增加+号
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String tmpCM = "";
            for (int i = 0; i < 14; i++)
            {
                if (textBoxes[i].Text.ToString().Equals("0:0"))
                {
                    tmpCM += ";";
                }
                else
                {
                    tmpCM += textBoxes[i].Text.ToString() + ";";
                }

            }
            textBox15.Text += System.Environment.NewLine + "综合实验，实验场面：" + tmpCM + System.Environment.NewLine;
            minions.Clear();
            atkMinions.Clear();
            aliveMinions.Clear();
            for (int i = 0; i < 14; i++)
            {
                minion tmp = new minion(textBoxes[i].Text.ToString(), i);
                minions.Add(tmp);
            }
            //以上读入数据

            List<minion> minions2 = new List<minion>(minions);
            maxpoint = -1000000;
            isBset = 0;bestAttck = "";
            tryAtk(0, minions2);
            if (bestAttck.Equals(";;;;;;;"))
            {
                bestAttck = "全不动或者打脸";
            }
            textBox15.Text += System.Environment.NewLine + "最优得分：" + maxpoint.ToString() + System.Environment.NewLine + "目前计算得到的最优解是：" + System.Environment.NewLine + bestAttck + System.Environment.NewLine;
            this.textBox15.Focus();//获取焦点
            this.textBox15.Select(this.textBox15.TextLength, 0);//光标定位到文本最后
            this.textBox15.ScrollToCaret();//滚动到光标处
        }
        double maxpoint = -100000;//定义最高分
        String bestAttck = "";//最优解描述
        String tmpAttack = "";
        int isBset;//是否是最优解
        void tryAtk(int mid,List<minion> minions2)
        {
            if (mid == 7)
            {
                //执行计算
                String tmpCM = "";
                for (int i = 0; i < 14; i++)
                {
                    if (minions2[i].hp == 0 && minions2[i].atk == 0)
                    {
                        tmpCM += ";";
                    }
                    else
                    {
                        tmpCM += minions2[i].atk.ToString() + ":" + minions2[i].hp.ToString() + ";";
                    }

                }
                
                int w = comboBox2.SelectedIndex;//策略选择
                double t = testGood(minions2, w);
                if (t > maxpoint)
                {
                    maxpoint = t;
                    bestAttck = tmpAttack;
                }
                else if (t == maxpoint)
                {
                    bestAttck += System.Environment.NewLine + "另一解：" + System.Environment.NewLine + tmpAttack;
                }

                textBox15.Text += System.Environment.NewLine + "实验情况：" + tmpCM + "得分：" + t.ToString() + System.Environment.NewLine;

            }
            else
            {
                
                for (int i = 0; i <=7; i++)
                {
                    if (mid == 0) tmpAttack = "";
                    List<minion> tm = new List<minion>(minions2);//记录当前的状态

                    minion tb = minions2[0], ta = minions2[0];//a是攻击者，b是被攻击者，i0为7 表示不动或打脸
                    if (i != 7 && minions2[i].hp > 0 && minions2[mid+7].hp > 0 && minions2[mid+7].canAtk==1)//如果双方存在,打一架
                    {
                        tb = minions2[i];
                        ta = minions2[mid+7];

                        Attack(ref ta, ref tb);

                        minions2[mid+7] = ta;
                        minions2[i] = tb;
                        tmpAttack = mid.ToString() + "打" + i.ToString() + ";"  + tmpAttack;
                    }
                    else
                    {
                        tmpAttack = ";" + tmpAttack;
                    }
                    tryAtk(mid + 1, minions2);//递归

                    //还原状况
                    minions2 = new List<minion>(tm);
                    int fh = tmpAttack.IndexOf(';');
                    tmpAttack = tmpAttack.Substring(fh + 1);
                    if (!(i != 7 && minions2[i].hp > 0 && minions2[mid + 7].hp > 0 && minions2[mid + 7].canAtk == 1))//如果双方不存在，则不必重复循环了
                    {
                        break;
                    }


                }

            }
        }
        private double testGood(List<minion> old_minions,int which)
        {
            int times = int.Parse(comboBox1.SelectedItem.ToString());
            TestTimes = times;
            int cntPoint = 0;
            List<minion> minions = new List<minion>();
            List<int> atkMinions = new List<int>();
            List<int> aliveMinions = new List<int>();
            int AllmyAtkF = 0, AllmyAtkS = 0, AllemAtkF = 0, AllemAtkS = 0, AllmySur = 0, AllemSur = 0;
            for (int iiiiii = 0; iiiiii < times; iiiiii++)
            {
                minions.Clear();
                atkMinions.Clear();
                aliveMinions.Clear();
                for (int i = 0; i < 14; i++)
                {
                    minion tmp = old_minions[i];
                    minions.Add(tmp);
                    if (tmp.hp > 0)
                    {
                        atkMinions.Add(i);
                        aliveMinions.Add(i);
                    }
                }
                atkMinions = RandomSort<int>(atkMinions);//随机攻击先后
                for (int i = 0; i < atkMinions.Count; i++)
                {
                    if (minions[atkMinions[i]].hp <= 0) continue;//死了就不能诈尸
                    aliveMinions = RandomSort<int>(aliveMinions);//被攻击的随从随机排序
                    for (int j = 0; j < aliveMinions.Count; j++)
                    {
                        if (aliveMinions[j] == atkMinions[i]) continue;//被攻击的不能是自己
                        if (minions[aliveMinions[j]].hp <= 0) continue;//死掉的不能被攻击
                        minion tmp1 = minions[atkMinions[i]];
                        minion tmp2 = minions[aliveMinions[j]];
                        Attack(ref tmp1, ref tmp2);//攻击
                        minions[atkMinions[i]] = tmp1;
                        minions[aliveMinions[j]] = tmp2;
                        break;//退出循环，只攻击一次
                    }

                }

                int myAtkF = 0, myAtkS = 0, emAtkF = 0, emAtkS = 0, mySur = 0, emSur = 0;
                for (int i = 0; i < 14; i++)
                {

                    if (minions[i].isMine == 1)
                    {
                        if (minions[i].hp > 0)
                        {
                            mySur = 1;//我方有存活
                            myAtkF += 1;
                            myAtkS += minions[i].atk;
                        }
                    }
                    else
                    {
                        if (minions[i].hp > 0)
                        {
                            emSur = 1;
                            emAtkF += 1;
                            emAtkS += minions[i].atk;
                        }
                    }


                }
                AllmyAtkF += myAtkF;
                AllmyAtkS += myAtkS;
                AllemAtkF += emAtkF;
                AllemAtkS += emAtkS;
                AllmySur += mySur;
                AllemSur += emSur;
            }
            //String tmpCM = "";
            //for (int i = 0; i < 14; i++)
            //{
            //    if (textBoxes[i].Text.ToString().Equals("0:0"))
            //    {
            //        tmpCM += ";";
            //    }
            //    else
            //    {
            //        tmpCM += textBoxes[i].Text.ToString() + ";";
            //    }

            //}
            //textBox15.Text += System.Environment.NewLine + "实验情况：" + tmpCM + System.Environment.NewLine;
            switch (which)
            {
                case 0:
                    return random.NextDouble();//这是随机反馈一个值，智能模式
                    break;
                case 1:
                    return (double)AllmySur / times;//我方存活
                    break;
                case 2:
                    return 1-(double)AllemSur / times;//敌方不存活
                    break;
                case 3:
                    return -(double)AllemAtkS / times;//清敌方场攻
                    break;
                case 4:
                    return -(double)AllemAtkF / times;//清敌方攻击频率
                    break;
                default:
                    break;
            }
            //textBox15.Text += "实验次数：" + times.ToString() + System.Environment.NewLine + "平均敌方攻击频率：" + (float)AllemAtkF / times + System.Environment.NewLine + "平均敌方场攻：" + (float)AllemAtkS / times + System.Environment.NewLine + "平均我方攻击频率：" + (float)AllmyAtkF / times + System.Environment.NewLine + "平均我方场攻：" + (float)AllmyAtkS / times + System.Environment.NewLine + "我方留怪概率：" + (float)AllmySur / times + System.Environment.NewLine + "敌方留怪概率：" + (float)AllemSur / times + System.Environment.NewLine + System.Environment.NewLine;
            this.textBox15.Focus();//获取焦点
            this.textBox15.Select(this.textBox15.TextLength, 0);//光标定位到文本最后
            this.textBox15.ScrollToCaret();//滚动到光标处
            return 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox15.Text = "";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox15.Text);
        }
    }
    public struct minion
    {
        public int atk;
        public int hp;
        public int hasAtk;
        public int isAlive;
        public int isMine;
        public int isDivineShield;
        public int canAtk;
        public minion(String x, int index)
        {
            isMine = index < 7 ? 0 : 1;
            canAtk = 0;
            if (!x.Equals("") && x[0] == '+')
            {
                x = x.Substring(1);
                canAtk = 1;
            }
            if (x.Equals("") || x.IndexOf(":") < 0)
            {
                atk = hp = hasAtk = isAlive = isDivineShield = 0;
            }
            else
            {
                isDivineShield = 0;
                atk = hp = hasAtk = isAlive = 0;
                if (x[x.Length - 1] == '*')
                {
                    x = x.Substring(0, x.Length - 1);
                    isDivineShield = 1;
                }
                int mh = x.IndexOf(":");
                atk = int.Parse(x.Substring(0, mh));
                hp = int.Parse(x.Substring(mh + 1));
                if (hp > 0)
                {
                    isAlive = 1;
                    hasAtk = 0;
                }

            }
        }

    }
}
