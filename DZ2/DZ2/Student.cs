using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ2
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public override bool Equals(object stud)
        {   if (stud.GetType() != typeof(Student))
                return false;

            var s = (Student)stud;

            if (this.Jmbag == s.Jmbag)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (Name == null || Jmbag == null)
            {
                return 0;
            }
            else
            {
                return (Name.GetHashCode() ^ Jmbag.GetHashCode());
            }
        }
    }
    public enum Gender
    {
       Male, Female
    }

    public class University
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }
    }



}

