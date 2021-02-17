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
    public partial class DismissalForm : Form
    {
        private Employee _employee;

        public DismissalForm(Employee employee)
        {
            InitializeComponent();
            _employee = employee;

            lbWho.Text = _employee.Name + " " + _employee.Surname;

            if (_employee.DateOfRelease == null)
            {
                dtpWhen.Value = DateTime.Now;
            }
            else
            {
                dtpWhen.Value = _employee.DateOfRelease.GetValueOrDefault();
            }
        }

        private void btDismissal_Click(object sender, EventArgs e)
        {
            if (dtpWhen.Value == _employee.DateOfEmployment)
            {
                MessageBox.Show("No dismissal, becouse date before employment", "No dismissal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _employee.DateOfRelease = null;
                this.Close();
            }
            _employee.DateOfRelease = dtpWhen.Value;
            _employee.ValidateDismissal();
            this.Close();
        }

        private void dtpWhen_ValueChanged(object sender, EventArgs e)
        {
            if (dtpWhen.Value < _employee.DateOfEmployment)
            {
                dtpWhen.Value = _employee.DateOfEmployment;
                MessageBox.Show("Dismissal before employment", "Wrong date", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
