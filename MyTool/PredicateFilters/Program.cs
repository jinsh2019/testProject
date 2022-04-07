using System;
using System.Collections.Generic;
using System.Linq;

namespace PredicateFilters
{

    class DynamicPredicates : StudentClass
    {
        static void Main(string[] args)
        {
            string[] ids = { "111", "114", "112" };
            QueryByID(ids);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void QueryByID(string[] ids)
        {
            var queryNames =
                from student in students
                let i = student.Id.ToString()
                where ids.Contains(i)
                select new { student.LastName, student.Id };

            foreach (var name in queryNames)
            {
                Console.WriteLine($"{name.LastName}: {name.Id}");
            }
        }
    }

    public class StudentClass
    {
        #region data
        public enum GradeLevel { FirstYear = 1, SecondYear, ThirdYear, FourthYear };

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public GradeLevel Year;
        public List<int> ExamScores;

        protected static List<StudentClass> students = new List<StudentClass>
    {
        new StudentClass {FirstName = "Terry", LastName = "Adams", Id = 120,
            Year = GradeLevel.SecondYear,
            ExamScores = new List<int> { 99, 82, 81, 79}},
        new StudentClass {FirstName = "Fadi", LastName = "Fakhouri", Id = 116,
            Year = GradeLevel.ThirdYear,
            ExamScores = new List<int> { 99, 86, 90, 94}},
        new StudentClass {FirstName = "Hanying", LastName = "Feng", Id = 117,
            Year = GradeLevel.FirstYear,
            ExamScores = new List<int> { 93, 92, 80, 87}},
        new StudentClass {FirstName = "Cesar", LastName = "Garcia", Id = 114,
            Year = GradeLevel.FourthYear,
            ExamScores = new List<int> { 97, 89, 85, 82}},
        new StudentClass {FirstName = "Debra", LastName = "Garcia", Id = 115,
            Year = GradeLevel.ThirdYear,
            ExamScores = new List<int> { 35, 72, 91, 70}},
        new StudentClass {FirstName = "Hugo", LastName = "Garcia", Id = 118,
            Year = GradeLevel.SecondYear,
            ExamScores = new List<int> { 92, 90, 83, 78}},
        new StudentClass {FirstName = "Sven", LastName = "Mortensen", Id = 113,
            Year = GradeLevel.FirstYear,
            ExamScores = new List<int> { 88, 94, 65, 91}},
        new StudentClass {FirstName = "Claire", LastName = "O'Donnell", Id = 112,
            Year = GradeLevel.FourthYear,
            ExamScores = new List<int> { 75, 84, 91, 39}},
        new StudentClass {FirstName = "Svetlana", LastName = "Omelchenko", Id = 111,
            Year = GradeLevel.SecondYear,
            ExamScores = new List<int> { 97, 92, 81, 60}},
        new StudentClass {FirstName = "Lance", LastName = "Tucker", Id = 119,
            Year = GradeLevel.ThirdYear,
            ExamScores = new List<int> { 68, 79, 88, 92}},
        new StudentClass {FirstName = "Michael", LastName = "Tucker", Id = 122,
            Year = GradeLevel.FirstYear,
            ExamScores = new List<int> { 94, 92, 91, 91}},
        new StudentClass {FirstName = "Eugene", LastName = "Zabokritski", Id = 121,
            Year = GradeLevel.FourthYear,
            ExamScores = new List<int> { 96, 85, 91, 60}}
    };
        #endregion

    }
}
