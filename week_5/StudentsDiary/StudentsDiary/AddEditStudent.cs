using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        private Student _student = null;
        private List<Student> _studentsList = null;

        public AddEditStudent(Student student)
        {
            InitializeComponent();

            _student = student;
            GetStudentData();
            SetGroupCombobox();
            tbFirstName.Select();
        }

        public AddEditStudent(List<Student> studentsList)
        {
            InitializeComponent();

            _studentsList = studentsList;
            tbId.Text = AssignIdToNewStudent().ToString();
            SetGroupCombobox();
            tbFirstName.Select();
        }

        private void SetGroupCombobox()
        {
            GroupCB.Items.AddRange(Array.FindAll(Student.Group.ToArray(), x => x.Pole != Student.Group[0].Pole));
            GroupCB.SelectedIndex = -1;
        }

        private void GetStudentData()
        {
            if (_student != null)
            {
                Text = "Edytowanie danych ucznia";
                FillTextBoxes();
            }
        }

        private void FillTextBoxes()
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastName.Text = _student.LastName;
            GroupCB.SelectedItem = _student.GroupID;
            tbMath.Text = _student.Math;
            tbPhysics.Text = _student.Physics;
            tbTechnology.Text = _student.Technology;
            tbPolishLang.Text = _student.PolishLang;
            tbForeignLang.Text = _student.ForeignLang;
            rtbComments.Text = _student.Comments;
            ExtraActivitiCHB.Checked = _student.ExtraActivities;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (_studentsList != null)
            {
                AddNewUserToList();
            }
            else
            {
                EditUserUpdate();
            }

            Close();
        }

        private void AddNewUserToList()
        {
            var student = new Student
            {
                Id = int.Parse(tbId.Text),
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                GroupID = GroupCB.Text,
                Comments = rtbComments.Text,
                ForeignLang = tbForeignLang.Text,
                Math = tbMath.Text,
                Physics = tbPhysics.Text,
                PolishLang = tbPolishLang.Text,
                Technology = tbTechnology.Text,
                ExtraActivities = ExtraActivitiCHB.Checked
            };

            _studentsList.Add(student);
        }

        private void EditUserUpdate()
        {

            _student.Id = int.Parse(tbId.Text);
            _student.FirstName = tbFirstName.Text;
            _student.LastName = tbLastName.Text;
            _student.GroupID = GroupCB.Text;
            _student.Comments = rtbComments.Text;
            _student.ForeignLang = tbForeignLang.Text;
            _student.Math = tbMath.Text;
            _student.Physics = tbPhysics.Text;
            _student.PolishLang = tbPolishLang.Text;
            _student.Technology = tbTechnology.Text;
            _student.ExtraActivities = ExtraActivitiCHB.Checked;
        }

        private int AssignIdToNewStudent()
        {
            var studentWithHighestId = _studentsList.OrderByDescending(x => x.Id).FirstOrDefault();

            return studentWithHighestId == null ?
                1 : studentWithHighestId.Id + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
