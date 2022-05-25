using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace hashfile
{
    public partial class Form1 : Form
    {
        private byte[] _dstr1;
        private byte[] _dstr2;
        private byte[] _dstr3;
        private byte[] _dstr4;

        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // throw new System.NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == @"Compute Hash")
            {
                var resule3 = BitConverter.ToString(_dstr3);
                var resule4 = BitConverter.ToString(_dstr4);
                resule3 = resule3.Replace("-", "");
                resule4 = resule4.Replace("-", "");
                textBox1.Text = resule3;
                textBox2.Text = resule4;
                button1.Text = @"Select file";
            }
            else
            {
                openFileDialog1.Title = @"请选择文件";
                // openFileDialog1.ShowDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var file = openFileDialog1.FileName; //返回文件的完整路径  
                    textBox3.Text = file;
                    MD5 md5 = new MD5CryptoServiceProvider();
                    SHA256 sha256 = new SHA256CryptoServiceProvider();
                    _dstr1 = md5.ComputeHash(openFileDialog1.OpenFile());
                    _dstr2 = sha256.ComputeHash(openFileDialog1.OpenFile());
                }

                var sb1 = new StringBuilder();
                foreach (var s in _dstr1) sb1.Append(s.ToString("x2"));
                var sb2 = new StringBuilder();
                foreach (var s in _dstr2) sb2.Append(s.ToString("x2"));

                textBox1.Text = sb1.ToString();
                textBox2.Text = sb2.ToString();
            }
        }


        private void textBox3_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            textBox3.Text = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
            var s = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
            Stream fst = File.OpenRead(s);
            MD5 md5 = new MD5CryptoServiceProvider();
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            _dstr3 = md5.ComputeHash(fst);
            _dstr4 = sha256.ComputeHash(fst);
            textBox1.Text = null;
            textBox2.Text = null;
            button1.Text = @"Compute Hash";
            fst.Close();
            md5.Clear();
            sha256.Clear();
        }

        private void textBox3_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Text = @"Select a file";
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = @"Drop a file here.";
        }
    }
}