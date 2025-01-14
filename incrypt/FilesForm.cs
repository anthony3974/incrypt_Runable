using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace incrypt
{
    public partial class FilesForm : Form
    {
        public FilesForm()
        {
            InitializeComponent();
            del = new ToolStripMenuItem();
            del.Name = "Del";
            del.Size = new Size(155, 22);
            del.Text = "Delete";
            del.Click += new EventHandler(Del);
            //cm.ImageScalingSize = new Size(20, 20);
            cm = new ContextMenuStrip();
            cm.Items.AddRange(new ToolStripItem[] { del, });
            cm.Name = "contextMenuStrip";
            cm.ShowImageMargin = false;
            cm.Size = new Size(156, 92);
        }

        // vars
        List<string> files;
        ContextMenuStrip cm;
        ToolStripMenuItem del;

        private void Form2_Load(object sender, EventArgs e)
        {
            //files = new List<string>();
            //for (int i = 0; i < 50; i++)
            //{
            //    files.Add("day1.png");
            //    listView1.Items.Add(files[i]);
            //}
        }
        public void LoadFiles()
        {
            files = new List<string>();
            string[] flyes = Directory.GetFiles(Directory.GetCurrentDirectory() + "/");
            for (int i = 0; i < flyes.Length; i++) if (flyes[i].EndsWith(".txt")) files.Add(flyes[i]);
            listView1.Items.Clear();
            for (int i = 0; i < files.Count; i++) listView1.Items.Add(files[i].Substring(files[i].LastIndexOf("/") + 1));
        }
        public void SaveFile(string name, string data)
        {
            StreamWriter file = new StreamWriter(name);
            file.WriteLine(data);
            file.Close();
        }
        public void DeleteAll()
        {
            // Delete all files in a directory    
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach (string file in files)
                if (file.EndsWith(".txt")) File.Delete(file);
        }

        private void FilesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                cm.Show(Location.X+12 + listView1.Location.X + e.X, Location.Y+35 + listView1.Location.Y + e.Y);
            }
        }
        private void Del(object sender, EventArgs e)
        {
            File.Delete(listView1.SelectedItems[0].Text);
            LoadFiles();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
