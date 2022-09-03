using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBProgramming_Class_i.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AvgGrade { get; set; }
        public string Comments { get; set; }

        public Student(string name, decimal avgGrade, string comments)
        {
            this.Name = name;
            this.AvgGrade = avgGrade;
            this.Comments = comments;
            this.Id = new Random().Next(1, 1000000);
        }
    }
}