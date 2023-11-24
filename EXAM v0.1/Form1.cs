using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace EXAM_v0._1
{
    public partial class Form1 : Form
    {
        private string CurrentDate;
        NameValueCollection MyCol = new NameValueCollection();
        private string path = @"C:\Users\Public\Preparing for exams v0.1\ColSave.txt";
        private string folder = @"C:\Users\Public\Preparing for exams v0.1";
        private bool flag;
 
        public Form1()
        {
            InitializeComponent();
            if (!(flag = File.Exists(path)))
            {
                Directory.CreateDirectory(folder);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            MyCol.Add(CurrentDate, textBox1.Text);
            textBox1.Clear();
            FileSave();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int k = 0;
            if (MyCol.Count != 0)
            {
                for (int i = 0; i < MyCol.Count; ++i)
                {
                    if (CurrentDate == MyCol.GetKey(i))
                    {
                        string St = MyCol[i];
                        string NewSt = "",DopSt = "";
                        int n = 1;
                        for (int j = 0; j < St.Length; j++)
                        {
                            if (j == St.Length - 1)
                            {
                                NewSt += n.ToString() + ") " + DopSt+ St[j];
                            }
                            if (St[j] == ',')
                            {
                                NewSt += n.ToString()+") "+ DopSt + "\n";
                                n++; DopSt = "";
                                continue;
                            }
                            DopSt += St[j];
                        }
                        MessageBox.Show(MyCol.GetKey(i) + "\n"+ Environment.NewLine  + NewSt);
                        k++;
                    }
                }
                if (k == 0) MessageBox.Show("На указанную дату не намечено никаких событий");
            }
            else MessageBox.Show("На указанную дату не намечено никаких событий");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(
           "Вы уверены, что хотите удалить все события всех дат?!",
           "ПРЕДУПРЕЖДЕНИЕ",
           MessageBoxButtons.YesNo
           );
            if (res == DialogResult.Yes)
            {
                MyCol.Clear();
                File.Create(path).Close();
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(
           "Вы уверены, что хотите удалить все события выбранной даты??",
           "ПРЕДУПРЕЖДЕНИЕ",
           MessageBoxButtons.YesNo
           );
            if (res == DialogResult.Yes)
            {
                MyCol.Remove(CurrentDate);
                FileSave();
            }
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            CurrentDate = e.Start.ToLongDateString();
        }
        private void FileSave()
        {
            using (FileStream file = new FileStream(path,FileMode.Create))
            {
                using (StreamWriter Sw = new StreamWriter(file))
                {
                    for (int i = 0; i < MyCol.Count; ++i)
                    {
                        Sw.WriteLine(MyCol.GetKey(i) + "*" + MyCol[i]);
                    }
                }
            }    
        }
        private void FileLoad()
        {
            string key = "", value = "", line;
            int n = 0;
            if(!flag) File.Create(path).Close();
            string[] mas = File.ReadAllLines(path);
            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[0] == "" || mas[0] == null)
                {
                    break;
                }
                line = mas[i];
                for (int j = 0; j < line.Length-1; j++)
                {
                    if(j != 0)
                    {
                        if (line[j - 1] == '*')
                        {
                            n = j;
                            break;
                        }
                    }
                    if(line[j] != '*') key += line[j];
                }
                for (int l = n; l < line.Length; l++)
                {
                    value += line[l];
                }
                MyCol.Add(key, value);
                key = ""; value = "";
            }
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FileLoad();
        }

    } 
}
