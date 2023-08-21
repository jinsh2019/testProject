using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace DesignPatternStudy.Patterns
{
    /// <summary>
    /// https://www.runoob.com/design-pattern/composite-pattern.html
    /// </summary>
    public class Employee
    {
        private String name;
        private String dept;
        private int salary;
        private List<Employee> subordinates;

        //构造函数
        public Employee(String name, String dept, int sal)
        {
            this.name = name;
            this.dept = dept;
            this.salary = sal;
            subordinates = new List<Employee>();
        }

        public void add(Employee e){
            subordinates.Add(e);
        }

        public void remove(Employee e){
            subordinates.Remove(e);
        }

        public List<Employee> getSubordinates(){
            return subordinates;
        }
         
        public override string ToString(){
            return ("Employee :[ Name : " + name
            + ", dept : " + dept + ", salary :"
            + salary + " ]");
        }
    }
}
