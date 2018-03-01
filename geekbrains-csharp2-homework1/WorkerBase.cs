using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geekbrains_csharp2_homework1
{
    abstract class WorkerBase : IComparable
    {
        public String Name { get; set; }
        public WorkerBase(string name)
        {
            this.Name = name;
        }
        public abstract double GetSalary();

        public int CompareTo(object obj)
        {
            return Name.CompareTo(((WorkerBase)obj).Name);
        }
    }
}
