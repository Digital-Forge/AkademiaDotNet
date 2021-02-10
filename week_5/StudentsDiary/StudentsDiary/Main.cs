using StudentsDiary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class Main : Form
    {
        private FileHelper<List<Student>> _fileHelperToStudents = 
            new FileHelper<List<Student>>(Program.FilePathStudents);

        private FileHelper<List<StringModulToDGV>> _fileHelperToGroups =
            new FileHelper<List<StringModulToDGV>>(Program.FilePathGroups);

        private List<Student> _studentsList;

        public bool IsMaximize 
        { 
            get
            {
                return Settings.Default.IsMaximize;
            }
            set
            {
                Settings.Default.IsMaximize = value;
            }
        }

        public Main()
        {
            InitializeComponent();

            InitData();
            RefreshDiary();
            SetColumnsHeader();

            if (IsMaximize)
                WindowState = FormWindowState.Maximized;
        }

        private void InitData()
        {
            _studentsList = _fileHelperToStudents.DeserializeFromFile();
            Student.Group = _fileHelperToGroups.DeserializeFromFile();

            if (Student.Group.Count == 0)
            {
                Student.Group = new List<StringModulToDGV>() { new StringModulToDGV("ALL"), 
                                                               new StringModulToDGV("A"),
                                                               new StringModulToDGV("B"),
                                                               new StringModulToDGV("C") };
            }

            GroupCB.Items.AddRange(Student.Group.ToArray());
            GroupCB.SelectedIndex = 0;
        }

        private void RefreshDiary()
        {
            if (GroupCB.Text == "ALL")
            {
                dgvDiary.DataSource = _studentsList.ToArray();
            }
            else
            {
                dgvDiary.DataSource = Array.FindAll(_studentsList.ToArray(), x => x.GroupID == GroupCB.Text);
            }  
        }

        private void SetColumnsHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imię";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Grupa";
            dgvDiary.Columns[4].HeaderText = "Uwagi";
            dgvDiary.Columns[5].HeaderText = "Matematyka";
            dgvDiary.Columns[6].HeaderText = "Technologia";
            dgvDiary.Columns[7].HeaderText = "Fizyka";
            dgvDiary.Columns[8].HeaderText = "Język polski";
            dgvDiary.Columns[9].HeaderText = "Język obcy";
            dgvDiary.Columns[10].HeaderText = "Zajęcia Dodatkowe";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent(_studentsList);
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog(this);
            _fileHelperToStudents.SerializeToFile(_studentsList);
        }

        private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshDiary();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego dane chcesz edytować");
                return;
            }

            var addEditStudent = new AddEditStudent((Student)dgvDiary.SelectedRows[0].DataBoundItem);
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog(this);
            _fileHelperToStudents.SerializeToFile(_studentsList);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego chcesz usunąć");
                return;
            }

            var selectedStudent = dgvDiary.SelectedRows[0];

            var confirmDelete =
                MessageBox.Show($"Czy na pewno chcesz usunąć ucznia {(selectedStudent.Cells[1].Value.ToString() + " " + selectedStudent.Cells[2].Value.ToString()).Trim()}",
                "Usuwanie ucznia",
                MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary();
            }
        }

        private void DeleteStudent(int id)
        {
            _studentsList.RemoveAll(x => x.Id == id);
            _fileHelperToStudents.SerializeToFile(_studentsList);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                IsMaximize = true;
            else
                IsMaximize = false;

            Settings.Default.Save();
        }

        private void GroupCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            new GroupEdit().ShowDialog(this);

            GroupCB.Items.Clear();
            GroupCB.Items.AddRange(Student.Group.ToArray());
            GroupCB.SelectedIndex = 0;
            _fileHelperToGroups.SerializeToFile(Student.Group);
        }
    }
}
