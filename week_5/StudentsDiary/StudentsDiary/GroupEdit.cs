using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class GroupEdit : Form
    {
        public GroupEdit()
        {
            InitializeComponent();
            RefreshDGV();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student.Group.Add(new StringModulToDGV("---"));
            RefreshDGV();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvGroupList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę wybrać daną grupę do usunięcia");
                return;
            }

            Student.Group.RemoveAt(Student.Group.FindIndex(x => x == ((StringModulToDGV)dgvGroupList.SelectedRows[0].DataBoundItem)));
            RefreshDGV();
        }

        private void RefreshDGV()
        {
            dgvGroupList.DataSource = Array.FindAll<StringModulToDGV>(Student.Group.ToArray(), x => x.Pole != Student.Group[0].Pole);
        }
    }
}
