using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Properties.Settings.Default.WindowWidth, Properties.Settings.Default.WindowHeight);
            chbSizeMode.Checked = Properties.Settings.Default.AutoSize;
            chbSizeMode_CheckedChanged();

            try
            {
                pbViewer.Load(Properties.Settings.Default.PicturePath);
                btnClear.Enabled = true;
            }
            catch
            {
                pbViewer.Image = new Bitmap(pbViewer.Size.Width,pbViewer.Size.Height);
                btnClear.Enabled = false;
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.RestoreDirectory = true;
                dialog.Filter = "all files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pbViewer.Load(dialog.FileName);
                        Properties.Settings.Default.PicturePath = dialog.FileName;
                        Properties.Settings.Default.Save();
                        btnClear.Enabled = true;
                    }
                    catch
                    {
                        if (pbViewer.Image != null)
                        {
                            pbViewer.Image.Dispose();
                        }
                        pbViewer.Image = new Bitmap(pbViewer.Size.Width, pbViewer.Size.Height);
                        btnClear.Enabled = false;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (pbViewer.Image != null)
            {
                pbViewer.Image.Dispose();
            }
            pbViewer.Image = new Bitmap(pbViewer.Size.Width, pbViewer.Size.Height);
            btnClear.Enabled = false;
            Properties.Settings.Default.PicturePath = "";
            Properties.Settings.Default.Save();
        }

        private void chbSizeMode_CheckedChanged(object sender = null, EventArgs e = null)
        {
            if (chbSizeMode.Checked)
            {
                pbViewer.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pbViewer.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.AutoSize = chbSizeMode.Checked;
            Properties.Settings.Default.WindowWidth = this.Size.Width;
            Properties.Settings.Default.WindowHeight = this.Size.Height;
            Properties.Settings.Default.Save();
        }
    }
}
