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
    public partial class AddEditInfoForm : Form
    {
        enum Mode
        {
            add,
            edit,
            info
        }

        private Mode _mode;
        private Employee _employee = null;
        private List<StringModulToDGV> _employeeSkillsList = null;

        public AddEditInfoForm()
        {
            InitializeComponent();
            this.Text = "ADD";

            _mode = Mode.add;
            _employeeSkillsList = new List<StringModulToDGV>();

            SetElements();
        }

        public AddEditInfoForm(Employee employee, bool editMode = false)
        {
            InitializeComponent();
            this.Text = editMode ? "EDIT" : "INFO";

            _employee = employee;
            _mode = editMode ? Mode.edit : Mode.info;
            _employeeSkillsList = _employee.Skills;

            SetElements();
        }

        private void SetElements()
        {
            cbxDepartment.Items.AddRange(
                Array.ConvertAll(
                    Array.FindAll<StringModulToDGV>(
                        LogicCore.CoreHook.departments.ToArray(),
                        x => x.Text != LogicCore.CoreHook.departments[0].Text), x => x.ToString()));


            switch (_mode)
            {
                case Mode.info:
                    tbID.Enabled = false;
                    tbName.Enabled = false;
                    tbSurname.Enabled = false;
                    tbPESEL.Enabled = false;
                    cbxDepartment.Enabled = false;
                    tbSalary.Enabled = false;
                    dtpEmployment.Enabled = false;
                    dtpRelease.Enabled = false;
                    rtbComments.Enabled = false;
                    dgvSkills.Enabled = false;

                    btAdd.Visible = false;
                    btDelete.Visible = false;
                    btCheck.Visible = false;
                    btAccept.Visible = false;

                    if (_employee.DateOfRelease == null)
                    {
                        dtpRelease.Visible = false;
                        lbDataRelease.Visible = false;
                    }

                    goto case Mode.edit;
                    //break;

                case Mode.edit:
                    tbID.Text = _employee.EmployeeID.ToString();
                    tbName.Text = _employee.Name;
                    tbSurname.Text = _employee.Surname;
                    tbPESEL.Text = _employee.PESEL;

                    cbxDepartment.Text = _employee.Department == "" ? LogicCore.CoreHook.departments[1].ToString() : _employee.Department;

                    tbSalary.Text = _employee.Salary;
                    dtpEmployment.Value = _employee.DateOfEmployment.Date;

                    dtpRelease.Value = _employee.DateOfRelease == null ? _employee.DateOfEmployment.Date : _employee.DateOfRelease.GetValueOrDefault();

                    rtbComments.Text = _employee.Comments;
                    RefreshDGV();
                    break;

                case Mode.add:
                    tbID.Text = LogicCore.CoreHook.employees.Count == 0 ? "0" : (LogicCore.CoreHook.employees.OrderByDescending(x => x.EmployeeID).FirstOrDefault().EmployeeID + 1).ToString();
                    cbxDepartment.Text = LogicCore.CoreHook.departments[1].ToString();
                    dtpEmployment.Value = DateTime.Now;
                    dtpRelease.Value = dtpEmployment.Value;
                    RefreshDGV();
                    break;
            }
        }

        private void KeyPressFromNumber(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void KeyPressFromNumberFloat(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && (e.KeyChar != '\b') && (e.KeyChar != ',') && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void KeyPressFromLetter(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && (e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                e.Handled = true;
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            _employeeSkillsList.Add(new StringModulToDGV());
            RefreshDGV();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dgvSkills.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select the group to be deleted", "Please select", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _employeeSkillsList.RemoveAt(_employeeSkillsList.FindIndex(x => x == ((StringModulToDGV)dgvSkills.SelectedRows[0].DataBoundItem)));
            RefreshDGV();
        }

        private void btCheck_Click(object sender, EventArgs e)
        {
            bool isOk = true;

            if (tbName.Text == "")
            {
                isOk = false;
                lbName.ForeColor = Color.Red;
            }
            else lbName.ForeColor = Color.Black;


            if (tbSurname.Text == "")
            {
                isOk = false;
                lbSurname.ForeColor = Color.Red;
            }
            else lbSurname.ForeColor = Color.Black;


            if (tbPESEL.Text.Length != 11)
            {
                isOk = false;
                lbPESEL.ForeColor = Color.Red;
            }
            else lbPESEL.ForeColor = Color.Black;


            if (!isOk)
            {
                MessageBox.Show("No required data", "No required data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (dtpEmployment.Value > dtpRelease.Value)
            {
                isOk = false;
                MessageBox.Show("Release before employment", "Wrong date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (isOk && btAccept.Enabled == false)
            {
                btAccept.Enabled = true;

                tbID.Enabled = false;
                tbName.Enabled = false;
                tbSurname.Enabled = false;
                tbPESEL.Enabled = false;
                cbxDepartment.Enabled = false;
                tbSalary.Enabled = false;
                dtpEmployment.Enabled = false;
                dtpRelease.Enabled = false;
                rtbComments.Enabled = false;
                dgvSkills.Enabled = false;

                btAdd.Visible = false;
                btDelete.Visible = false;
            }
            else
            {
                btAccept.Enabled = false;

                tbID.Enabled = true;
                tbName.Enabled = true;
                tbSurname.Enabled = true;
                tbPESEL.Enabled = true;
                cbxDepartment.Enabled = true;
                tbSalary.Enabled = true;
                dtpEmployment.Enabled = true;
                dtpRelease.Enabled = true;
                rtbComments.Enabled = true;
                dgvSkills.Enabled = true;

                btAdd.Visible = true;
                btDelete.Visible = true;
            }
        }

        private void btAccept_Click(object sender, EventArgs e)
        {
            switch (_mode)
            {
                case Mode.add:
                    LogicCore.CoreHook.employees.Add(new Employee());
                    int indexBuff = LogicCore.CoreHook.employees.Count - 1;

                    LogicCore.CoreHook.employees[indexBuff].EmployeeID = uint.Parse(tbID.Text);
                    LogicCore.CoreHook.employees[indexBuff].Name = tbName.Text;
                    LogicCore.CoreHook.employees[indexBuff].Surname = tbSurname.Text;
                    LogicCore.CoreHook.employees[indexBuff].PESEL = tbPESEL.Text;

                    if (cbxDepartment.SelectedItem.ToString() == LogicCore.CoreHook.departments[1].ToString())
                    {
                        LogicCore.CoreHook.employees[indexBuff].Department = "";
                    }
                    else
                    {
                        LogicCore.CoreHook.employees[indexBuff].Department = cbxDepartment.SelectedItem.ToString();
                    }

                    LogicCore.CoreHook.employees[indexBuff].Salary = tbSalary.Text;
                    LogicCore.CoreHook.employees[indexBuff].DateOfEmployment = dtpEmployment.Value;

                    if (dtpEmployment.Value == dtpRelease.Value)
                    {
                        LogicCore.CoreHook.employees[indexBuff].DateOfRelease = null;
                    }
                    else
                    {
                        LogicCore.CoreHook.employees[indexBuff].DateOfRelease = dtpRelease.Value;
                    }

                    LogicCore.CoreHook.employees[indexBuff].Comments = rtbComments.Text;
                    LogicCore.CoreHook.employees[indexBuff].Skills = _employeeSkillsList;
                    LogicCore.CoreHook.employees[indexBuff].ValidateDismissal();
                    break;

                case Mode.edit:
                    _employee.Name = tbName.Text;
                    _employee.Surname = tbSurname.Text;
                    _employee.PESEL = tbPESEL.Text;

                    if (cbxDepartment.SelectedItem.ToString() == LogicCore.CoreHook.departments[1].ToString())
                    {
                        _employee.Department = "";
                    }
                    else
                    {
                        _employee.Department = cbxDepartment.SelectedItem.ToString();
                    }
                   
                    _employee.Salary = tbSalary.Text;
                    _employee.DateOfEmployment = dtpEmployment.Value;
                    //_employee.DateOfRelease = dtpEmployment.Value == dtpRelease.Value ? null : dtpRelease.Value;

                    if (dtpEmployment.Value == dtpRelease.Value)
                    {
                        _employee.DateOfRelease = null;
                    }
                    else
                    {
                        _employee.DateOfRelease = dtpRelease.Value;
                    }

                    _employee.Comments = rtbComments.Text;
                    _employee.Skills = _employeeSkillsList;
                    _employee.ValidateDismissal();
                    break;
            }
            this.Close();
        }

        private void RefreshDGV()
        {
            dgvSkills.DataSource = _employeeSkillsList.ToArray();
            dgvSkills.Columns[0].HeaderText = "Skills";
        }

        private void dtpEmployment_ValueChanged(object sender, EventArgs e)
        {
            dtpRelease.Value = dtpEmployment.Value;
        }
    }

}
