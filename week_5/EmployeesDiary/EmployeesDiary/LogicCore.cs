using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeesDiary
{
    [Serializable]
    public class LogicCore
    {
        public static LogicCore CoreHook;
        private static string _path;

        public static string Path
        {
            get
            {
                return _path;
            }

            set
            {
                if (value != "")
                {
                    Properties.Settings.Default.DataPath = value;
                    _path = value;
                }
            }
        }



        [XmlArrayItem(typeof(Employee))]
        public List<Employee> employees;
        [XmlArrayItem(typeof(StringModulToDGV))]
        public List<StringModulToDGV> departments;

        public LogicCore()
        {
            //this.employees = new List<Employee>();
            //this.departments = new List<StringModulToDGV>() { new StringModulToDGV("All"), new StringModulToDGV("---") };
        }

        private static LogicCore InitDefoult()
        {
            LogicCore buff = new LogicCore();
            buff.employees = new List<Employee>();
            buff.departments = new List<StringModulToDGV>() { new StringModulToDGV("All"), new StringModulToDGV("---") };
            return buff;
        }

        public static void Init()
        {
            _path = Properties.Settings.Default.DataPath;

            CoreHook = DeserializeFromFile();

            for (int i = 0; i < CoreHook.employees.Count; i++)
            {
                CoreHook.employees[i].ValidateDismissal();
            }
        }

        static public void SerializeToFile(LogicCore students)
        {
            var serializer = new XmlSerializer(typeof(LogicCore));

            using (var streamWriter = new StreamWriter(_path))
            {
                serializer.Serialize(streamWriter, students);
                streamWriter.Close();
            }
        }

        static public LogicCore DeserializeFromFile()
        {
            if (!File.Exists(_path))
                return InitDefoult();

            var serializer = new XmlSerializer(typeof(LogicCore));

            using (var streamReader = new StreamReader(_path))
            {
                var students = (LogicCore)serializer.Deserialize(streamReader);
                streamReader.Close();
                return students;
            }
        }
    }
}
