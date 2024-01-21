using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFCマージツール
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            //コントロール内にドラッグされたとき実行される
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
                e.Effect = DragDropEffects.Copy;
            else
                //ファイル以外は受け付けない
                e.Effect = DragDropEffects.None;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            //コントロール内にドロップされたとき実行される
            //ドロップされたすべてのファイル名を取得する
            string[] fileName =
                (string[])e.Data.GetData(DataFormats.FileDrop, false);
            //ListBoxに追加する
            listBox1.Items.AddRange(fileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] mfc = new string[4];
            for (var i = 0; i < 4; i++)
            {
                mfc[i] = listBox1.Items[i].ToString();
            }

            string[] mfcs = new string[4];
            for (var k = 0; k < 4; k++)
            {
                if (Regex.IsMatch(mfc[k], "GOODS.csv"))
                {
                    mfcs[0] = mfc[k];
                }
                if (Regex.IsMatch(mfc[k], "GOODS_UNI.csv"))
                {
                    mfcs[1] = mfc[k];
                }
                if (Regex.IsMatch(mfc[k], "GOODS_MAI.csv"))
                {
                    mfcs[2] = mfc[k];
                }
                if (Regex.IsMatch(mfc[k], "GOODS_ONG.csv"))
                {
                    mfcs[3] = mfc[k];
                }
            }

            string mfc_base = mfcs[0];
            string mfc_hg = mfcs[1];//uni
            string mfc_new = mfcs[2];//mai
            string mfc_std = mfcs[3];//ong
            string MFC = "";
            int count = 0;
            int counthg = 0;
            int countnew = 0;
            int countstd = 0;
            List<string> lists = new List<string>();
            List<string> listshg = new List<string>();
            List<string> listsnew = new List<string>();
            List<string> listsstd = new List<string>();

            StreamReader sr = new StreamReader(mfcs[0]);
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    lists.Add(line);
                    MFC = MFC + line + "\r\n";
                    count++;
                }


            }
            StreamReader srr = new StreamReader(mfcs[1]);
            {
                while (!srr.EndOfStream)
                {
                    string line = srr.ReadLine();
                    listshg.Add(line);
                    counthg++;
                }


            }
            StreamReader srrr = new StreamReader(mfcs[2]);
            {
                while (!srrr.EndOfStream)
                {
                    string line = srrr.ReadLine();
                    listsnew.Add(line);
                    countnew++;
                }


            }
            StreamReader srrrr = new StreamReader(mfcs[3]);
            {
                while (!srrrr.EndOfStream)
                {
                    string line = srrrr.ReadLine();
                    listsstd.Add(line);
                    countstd++;
                }


            }

            for(var i = 1; i < count; i++)
            {
                string temp = lists[i];
                string tmp = lists[i].Replace("+","\\+");


                for (var k = 1; k < counthg; k++)
                {
                    string tmphg = listshg[k];
                    if (Regex.IsMatch(tmphg,tmp))
                    {
                        MFC = MFC.Replace(temp, tmphg+" UNI");

                    }



                }
                for (var k = 1; k < countnew; k++)
                {
                    string tmphg = listsnew[k];
                    if (Regex.IsMatch(tmphg, tmp))
                    {
                        MFC = MFC.Replace(temp, tmphg + " MAI");

                    }



                }
                for (var k = 1; k < countstd; k++)
                {
                    string tmphg = listsstd[k];
                    if (Regex.IsMatch(tmphg, tmp))
                    {
                        MFC = MFC.Replace(temp, tmphg + " ONG");

                    }



                }

                for (var k = 1; k < count; k++)
                {
                    string tmphg = lists[k];
                    if (Regex.IsMatch(tmphg, tmp))
                    {
                        MFC = MFC.Replace(temp, tmphg + ",");

                    }



                }


            }

            textBox1.Text = MFC.Replace("data-longitude,", "data-longitude,cabinet");


            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string data = "";
            string name = listBox1.Items[0].ToString();
            MatchCollection mc = Regex.Matches(name, @"\d+MFC");

            if (mc.Count > 0) { }
            //data = mc[0].Value;


            File.WriteAllText(@data+"_merged.csv", textBox1.Text);
        }
        }
    }
