using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    static class Program
    {
        public static string FilePathStudents =
            Path.Combine(Environment.CurrentDirectory, "students.txt");

        public static string FilePathGroups =
           Path.Combine(Environment.CurrentDirectory, "groups.txt");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
