using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shadow_Tactics_Scan_File
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Reload();
        }
        private void Reload()
        {
            DirectoryInfo d = new DirectoryInfo(@"E:\Game\Shadow Tactics\Shadow Tactics_Data\Unity_Assets_Files\resources1");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            foreach (FileInfo file in Files)
            {
                string[] lines = File.ReadAllLines(file.FullName);
                if (lines[0].IndexOf(",") >= 0)
                {
                    var number = lines[0].Substring(lines[0].IndexOf(",") + 1);
                    number = number.Substring(number.IndexOf(",") + 1);
                    if (number.Length >= 2)
                    {
                        number = number.Substring(0, 2);
                    }
                    number = number.Trim();
                    int intNumber = Int32.Parse(number);
                    int count = 0;
                    var strTmp = "";
                    foreach (string line in lines)
                    {
                        string[] split = line.Split('\t');
                        
                        if (split.Length > intNumber)
                        {

                            if (split[intNumber].Trim().Length > 20)
                            {
                                strTmp += split[intNumber]+ "||";
                                count++;
                                
                            }
                            if (count == 3)
                            {
                                if (IsASCII(strTmp))
                                
                                   dataGridView1.Rows.Add(file.Name, strTmp);
                                else

                                dataGridView1.Rows.Add(file.Name, "@@"+ strTmp);
                                break;
                            }
                        }
                    }



                }
                else
                {
                    continue;
                }
                // Process line
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Reload();
        }
        public Boolean IsASCII(string value)
        {
            // ASCII encoding replaces non-ascii with question marks, so we use UTF8 to see if multi-byte sequences are there
            return Encoding.UTF8.GetByteCount(value) == value.Length;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 2)
                return;

            var strTemp = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            File.Delete(@"E:\Game\Shadow Tactics\Shadow Tactics_Data\Unity_Assets_Files\resources1\" + strTemp);
            
                dataGridView1.Rows.RemoveAt(e.RowIndex);
        }
    }
}
