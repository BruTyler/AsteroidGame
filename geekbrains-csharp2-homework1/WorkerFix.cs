using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geekbrains_csharp2_homework1
{
    class WorkerFix:WorkerBase
    {
        
        protected double MonthSalary { get; set; }

        public WorkerFix(string name, double monthSalary):base(name)
        {
            MonthSalary = monthSalary;
        }

        public override double GetSalary()
        {
            return MonthSalary;
        }
    }
}
