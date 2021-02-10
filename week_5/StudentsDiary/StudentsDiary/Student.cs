using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StudentsDiary
{
    [Serializable]
    public class Student
    {
        [XmlArrayItem(typeof(StringModulToDGV))]
        public static List<StringModulToDGV> Group = null;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GroupID { get; set; }
        public string Comments { get; set; }
        public string Math { get; set; }
        public string Technology { get; set; }
        public string Physics { get; set; }
        public string PolishLang { get; set; }
        public string ForeignLang { get; set; }
        public bool ExtraActivities { get; set; }
    }

    [Serializable]
    public class StringModulToDGV
    {
        public StringModulToDGV()
        {
            this.Pole = "";
        }

        public StringModulToDGV(string text)
        {
            this.Pole = text;
        }

        public string Pole { get; set; }

        public override string ToString()
        {
            return this.Pole;
        }
    }
}
