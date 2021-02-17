using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesDiary
{
    [Serializable]
    public class StringModulToDGV
    {
        public string Text { get; set; }

        public StringModulToDGV()
        {
            this.Text = "";
        }

        public StringModulToDGV(string text)
        {
            this.Text = text;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
