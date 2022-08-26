namespace ab_test
{

    // Dispose and Finalize in .net
    internal class Program
    {
        //static List<student> students = new List<student>();
        static void Main(string[] args)
        {
            //BenchmarkDotNet.Running.BenchmarkRunner.Run<LinqToObject>();
            //BenchmarkDotNet.Running.BenchmarkRunner.Run<StringTest>();
            Console.ReadKey();
            getStudents();
            Console.ReadKey();
            Console.WriteLine("Hello, World!");

        }

        private static void getStudents()
        {
            List<student> students = new List<student>();
            for (int i = 0; i < 1000; i++)
            {
                students.Add(new student());
            }
        }
    }


    class student
    {
        public int id { get; set; }
    }
}