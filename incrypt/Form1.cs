#pragma warning disable IDE0044 // Add readonly modifier
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace incrypt
{
    public partial class Form1 : Form
    {
        // golbal vars
        bool incrypt = true, success = false;
        Incrypt E = new Incrypt();
        // switch
        //SwitchVert sit = new SwitchVert();

        public Form1()
        {
            InitializeComponent();
            f.listView1.DoubleClick += ListView1_DoubleClick;
            // make switch
            //sit.Location = new Point(130, 200);
            //sit.SWidth = 40;
            //sit.SHeight = 60;
            //panel1.Controls.Add(sit);
        }
        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (btnSelect.Text == ">")
            {
                incrypt = false;
                btnSelect.Location = new Point(btnSelect.Location.X + btnSelect.Size.Width, btnSelect.Location.Y);
                btnSelect.Text = "<";
            }
            else if (btnSelect.Text == "<")
            {
                incrypt = true;
                btnSelect.Location = new Point(btnSelect.Location.X - btnSelect.Size.Width, btnSelect.Location.Y);
                btnSelect.Text = ">";
            }
        }
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (txtIn.Text.Length > 0)
            {
                string mess = txtIn.Text;
                string newMess = "";
                if (incrypt)
                {
                    newMess += "M" + E.Encrypt(mess) + "M";
                    txtIn.Text = newMess;
                }
                else
                {
                    if (mess[0] == 'M' && mess[mess.Length - 1] == 'M')
                    {
                        newMess = E.Decrypt(mess.Substring(mess.IndexOf("M") + 1, mess.LastIndexOf("M") - 1));
                        txtIn.Text = newMess;
                    }
                }
            }
        }
        FilesForm f = new FilesForm();
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            f.Show();
            f.LoadFiles();
        }
        // password load stuff
        MessageForm messy = new MessageForm();
        string loadedPass, loadedMess;
        private void Openfile(string name)
        {
            StreamReader FR = new StreamReader(Directory.GetCurrentDirectory() + "\\" + name);
            string txt = FR.ReadLine();
            loadedPass = txt.Substring(txt.IndexOf("P") + 1, txt.LastIndexOf("P") - 1);
            txt = FR.ReadLine();
            loadedMess = txt.Substring(txt.IndexOf("M") + 1, txt.LastIndexOf("M") - 1);
            FR.Close();

            messy = new MessageForm();
            messy.button1.Click += Button1_Click;
            messy.Show();
            messy.textBox1.Clear();
            messy.textBox1.PasswordChar = '*';

        }
        // password stuff
        MessageForm m = new MessageForm();
        System.Timers.Timer tmrPass;
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtIn.Text.Length > 0 && txtIn.Text.Contains("M"))
            {
                m = new MessageForm();
                m.Show();
                m.textBox1.Clear();
                m.textBox1.PasswordChar = '*';
                tmrPass = new System.Timers.Timer { Interval = 500 };
                tmrPass.Elapsed += TmrPass_Elapsed;
                tmrPass.Start();
            }
        }
        private void TmrPass_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (m.Entered == true)
            {
                tmrPass.Stop();
                string pass = "P" + E.EncryptLevel2(m.textBox1.Text) + "P";
                bool fileNew = false;
                int index = 0;
                while (!fileNew)
                {
                    try
                    {
                        if (index == 0) new StreamReader(txtTitle.Text + ".txt");
                        else new StreamReader(txtTitle.Text + index.ToString() + ".txt");
                        index++;
                    }
                    catch (FileNotFoundException)
                    {
                        if (index == 0) f.SaveFile(txtTitle.Text + ".txt", pass + "\n" + txtIn.Text);
                        else f.SaveFile(txtTitle.Text + index.ToString() + ".txt", pass + "\n" + txtIn.Text);
                        fileNew = true;
                    }
                }
            }
        }
        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            f.Hide();
            try { Openfile(f.listView1.SelectedItems[0].Text); }
            catch (FileNotFoundException) { }

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            loadedPass = E.DecryptLevel2(loadedPass);
            loadedMess = E.Decrypt(loadedMess);
            if (loadedPass == messy.textBox1.Text) txtIn.Text = loadedMess;
            else MessageBox.Show("Incorect password!");
        }
        private void BtnGet_Click(object sender, EventArgs e)
        {
            txtR.Text = E.DecryptLevel2(txtR.Text);
        }

        private void BtnDelete_Click_1(object sender, EventArgs e)
        {
            var inp = MessageBox.Show("Ok to delete", "window", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            if (inp == DialogResult.OK) f.DeleteAll();
        }

        private void TxtIn_Enter(object sender, EventArgs e)
        {
            if (success) { txtIn.Text = loadedMess; success = false; }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtIn.Text = "";
        }
    }
}
#pragma warning restore IDE0044 // Add readonly modifier
