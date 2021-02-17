using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeesDiary
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            // page 1
            InitializeComponent();
            cbxDepartmentRefresh();
            cbxDismissal.SelectedIndex = 0;
            RefreshDGV();

            // page 2
            RefreshDGVDepartments();
            lbPathToSaveData.Text = LogicCore.Path;
        }

        private void cbxDepartmentRefresh()
        {
            cbxDepartment.Items.Clear();
            cbxDepartment.Items.AddRange(LogicCore.CoreHook.departments.ConvertAll(x => x.ToString()).ToArray());
            cbxDepartment.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new AddEditInfoForm().ShowDialog(this);
            LogicCore.SerializeToFile(LogicCore.CoreHook);
            RefreshDGV();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select the person to edit", "Please select", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new AddEditInfoForm((Employee)dgvEmployees.SelectedRows[0].DataBoundItem, true).ShowDialog(this);
            LogicCore.SerializeToFile(LogicCore.CoreHook);
            RefreshDGV();
        }

        private void btnDismissal_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select the person to dismissal", "Please select", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new DismissalForm((Employee)dgvEmployees.SelectedRows[0].DataBoundItem).ShowDialog(this);
            LogicCore.SerializeToFile(LogicCore.CoreHook);
            RefreshDGV();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a person to display the information", "Please select", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new AddEditInfoForm((Employee)dgvEmployees.SelectedRows[0].DataBoundItem).Show();
            RefreshDGV();
        }

        private void RefreshDGV(object sender = null, EventArgs e = null)
        {
            lbWaitInfo.Enabled = true;

            Employee[] buffEmployeesList = LogicCore.CoreHook.employees.ToArray();

            switch (cbxDismissal.Text)
            {
                case "All":
                    break;
                case "Employed":
                    buffEmployeesList = Array.FindAll(buffEmployeesList, x => x.DateOfRelease == null || x.DateOfRelease >= DateTime.Now);
                    break;
                case "Dismissal":
                    buffEmployeesList = Array.FindAll(buffEmployeesList, x => x.DateOfRelease < DateTime.Now);
                    break;
                case "To Dismissal":
                    buffEmployeesList = Array.FindAll(buffEmployeesList, x => x.DateOfRelease >= DateTime.Now);
                    break;
            }

            if (cbxDepartment.Text == LogicCore.CoreHook.departments[0].Text)
            {

            }
            else if (cbxDepartment.Text == LogicCore.CoreHook.departments[1].Text)
            {
                buffEmployeesList = Array.FindAll(buffEmployeesList, x => x.Department == "");
            }
            else
            {
                buffEmployeesList = Array.FindAll(buffEmployeesList, x => x.Department == cbxDepartment.Text);
            }

            dgvEmployees.DataSource = buffEmployeesList;
            dgvEmployees.Columns["PESEL"].Visible = false;
            dgvEmployees.Columns["Comments"].Visible = false;

            lbWaitInfo.Enabled = false;
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            LogicCore.CoreHook.departments.Add(new StringModulToDGV());
            RefreshDGVDepartments();
        }

        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select the group to be deleted", "Please select", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LogicCore.CoreHook.departments.RemoveAt(LogicCore.CoreHook.departments.FindIndex(x => x == ((StringModulToDGV)dgvDepartments.SelectedRows[0].DataBoundItem)));
            RefreshDGVDepartments();
            LogicCore.SerializeToFile(LogicCore.CoreHook);
        }

        private void RefreshDGVDepartments()
        {
            dgvDepartments.DataSource = Array.FindAll<StringModulToDGV>(
                LogicCore.CoreHook.departments.ToArray(),
                    x => !(x.Text == LogicCore.CoreHook.departments[0].ToString()
                        || x.Text == LogicCore.CoreHook.departments[1].ToString()));

            dgvDepartments.Columns[0].HeaderText = "Departments";

            cbxDepartmentRefresh();
        }

        private void dgvDepartments_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            cbxDepartmentRefresh();
            LogicCore.SerializeToFile(LogicCore.CoreHook);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.FileName = LogicCore.Path;
                dialog.RestoreDirectory = true;
                dialog.Filter = "*.xml|*.xml| all files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LogicCore.Path = dialog.FileName;
                    lbPathToSaveData.Text = LogicCore.Path;
                    LogicCore.SerializeToFile(LogicCore.CoreHook);
                }
            }
        }

        private void btnResetPath_Click(object sender, EventArgs e)
        {
            LogicCore.Path = Properties.Settings.Default.DataPathDefault;
            lbPathToSaveData.Text = LogicCore.Path;
            LogicCore.SerializeToFile(LogicCore.CoreHook);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.FileName = LogicCore.Path;
                dialog.RestoreDirectory = true;
                dialog.Filter = "*.xml|*.xml| all files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string buffPath = LogicCore.Path;
                    LogicCore.Path = dialog.FileName;

                    LogicCore.SerializeToFile(LogicCore.CoreHook);

                    LogicCore.Path = buffPath;
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (chbJoinData.Checked)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.RestoreDirectory = true;
                    dialog.Filter = "*.xml|*.xml| all files (*.*)|*.*";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string buffPath = LogicCore.Path;
                        LogicCore.Path = dialog.FileName;

                        LogicCore buffLogicCore = LogicCore.DeserializeFromFile();

                        LogicCore.Path = buffPath;

                        for (int i = 0; i < buffLogicCore.departments.Count; i++)
                        {
                            if (!LogicCore.CoreHook.departments.Exists(x => x.Text == buffLogicCore.departments[i].Text))
                            {
                                LogicCore.CoreHook.departments.Add(new StringModulToDGV(buffLogicCore.departments[i].Text));
                            }
                        }

                        uint buffIndex = ((List<Employee>)(LogicCore.CoreHook.employees.OrderByDescending(x => x.EmployeeID)))[0].EmployeeID + 1;

                        for (int i = 0; i < buffLogicCore.employees.Count; i++)
                        {
                            if (!LogicCore.CoreHook.employees.Exists(x => x.FakeIdentity() == buffLogicCore.employees[i].FakeIdentity()))
                            {
                                buffLogicCore.employees[i].EmployeeID = buffIndex;
                                buffIndex++;
                                LogicCore.CoreHook.employees.Add(buffLogicCore.employees[i]);
                            }
                        }
                    }
                }
            }
            else
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.RestoreDirectory = true;
                    dialog.Filter = "*.xml|*.xml| all files (*.*)|*.*";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string buffPath = LogicCore.Path;
                        LogicCore.Path = dialog.FileName;

                        LogicCore.Init();

                        LogicCore.Path = buffPath;
                    }
                }
            }
        }
    }
}
