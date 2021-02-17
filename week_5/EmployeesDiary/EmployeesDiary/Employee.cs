using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeesDiary
{
    [Serializable]
    public class Employee
    {
        public uint EmployeeID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PESEL { get; set; }
        public string Department { get; set; }
        public string Salary { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime? DateOfRelease { get; set; } = null;
        public bool Dismissal { get; set; }
        public string Comments { get; set; }
        [XmlArrayItem(typeof(StringModulToDGV))]
        public List<StringModulToDGV> Skills { get; set; }

        public string FakeIdentity()
        {
            return $"{Name} {Surname} {PESEL}";
        }

        public void ValidateDismissal()
        {
            if (DateOfRelease == null)
            {
                Dismissal = false;
            }
            else
            {
                Dismissal = DateOfRelease < DateTime.Now ? true : false;
            }
        }
    }
}
