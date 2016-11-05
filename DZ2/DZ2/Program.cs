using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            string[] strings = integers.Distinct().Select(s => $"Broj {s} ponavlja se {integers.Where(i => s == i).Count()} puta").ToArray();
            foreach (string item in strings)
            {
                Console.WriteLine(item);
            }
            
            string[] strings2 = integers.GroupBy(j=>j).Select(s =>$"Broj {s.Key} ponavlja se {s.Count()}puta").ToArray();
            foreach (string item in strings2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(Example2());
            Console.WriteLine(Example1());

            //University[] universities = GetAllCroatianUniversities();
            //Students[] allCroatianStudents = universities.SelectMany(s => s.Students).Distinct().ToArray();
            //Students[] croatianStudentsOnMultipleUniversities = univeristies.SelectMany(s => s.Students).GroupBy(stu => stu.Jmbag).Where(s => s.Count() > 1).Select(s=>s.Key).ToArray();
            //Students[] studentsOnMaleOnlyUniversities = universities.Where(s=> (s.Students.Where(j => j.Gender == Gender.Female).Count() == 0)).SelectMany(s=>s.Students).ToArray();

        }

        private static University[] GetAllCroatianUniversities()
        {
            throw new NotImplementedException();
        }

        public static bool Example1()
        {
            var list = new List<Student>()
                {
                new Student (" Ivan ", jmbag :" 001234567 ")
                };
            var ivan = new Student(" Ivan ", jmbag: " 001234567 ");
            // false :(
            return list.Any(s => s.Equals(ivan));
        }
        public static int Example2()
        {
            var list = new List<Student>()
            {
            new Student (" Ivan ", jmbag :" 001234567 "),
            new Student (" Ivan ", jmbag :" 001234567 ")
            };
            // 2 :(
            return list.Distinct().Count();
        }
    }
}
   

