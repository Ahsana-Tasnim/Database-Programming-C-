//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBProgramming_Class_i.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        public int Dept_Id { get; set; }
        public string Dept_Name { get; set; }
        public Nullable<int> Num_Employees { get; set; }
    
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
