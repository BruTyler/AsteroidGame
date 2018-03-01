using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geekbrains_csharp2_homework1
{
    //Работник с повременной оплатой (в час)
    class WorkerTimeShift : WorkerBase
    {
        protected double HourAmnt { get; set; }

        public WorkerTimeShift(string name, double hourAmnt) : base(name)
        {
            HourAmnt = hourAmnt;
        }

        public override double GetSalary()
        {
            return HourAmnt * 20.8 * 8; 
        }
    }
}
