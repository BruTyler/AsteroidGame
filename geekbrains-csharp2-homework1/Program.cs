using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geekbrains_csharp2_homework1
{

    #region ListWorkerClass : IEnumerable
    //класс содержащий массив сотрудников
    class ListWorkerClass : IEnumerable
    {
        Random rnd = new Random();
        protected List<WorkerBase> _listEmployee;
        public ListWorkerClass()
        {
            //заполнение списка работников в классе ListWorkerClass
            _listEmployee = new List<WorkerBase>
                { 
                    new WorkerFix("Shamsutdinov", rnd.Next(1000, 25000)),
                    new WorkerFix("Flagov", rnd.Next(1000, 25000)),
                    new WorkerFix("Normalev", rnd.Next(1000, 25000)),
                    new WorkerTimeShift("Zbruev", rnd.Next(12, 250)),
                    new WorkerTimeShift("Kalugin", rnd.Next(12, 250)),
                    new WorkerTimeShift("Kotov", rnd.Next(12, 250))
                };
        }

        //возможность вывода данных с использованием foreach
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _listEmployee.Count; i++)
                yield return _listEmployee.ElementAt(i);
        }
    }
    #endregion

    #region Main()
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("Тестировние WorkerBase : IComparable");
            Console.WriteLine("----------------------");

            Random rnd = new Random();
            //заполнение списка работников в классе Main
            List<WorkerBase> listEmployee = new List<WorkerBase>
                { 
                    new WorkerFix("Ivanov", rnd.Next(10000, 25000)),
                    new WorkerFix("Selivanov", rnd.Next(10000, 25000)),
                    new WorkerFix("Petrov", rnd.Next(10000, 25000)),
                    new WorkerFix("Sidorov", rnd.Next(10000, 25000)),
                    new WorkerFix("Shmidt", rnd.Next(10000, 25000)),
                    new WorkerTimeShift("Arbuzov", rnd.Next(12, 250)),
                    new WorkerTimeShift("Kelivanov", rnd.Next(12, 250)),
                    new WorkerTimeShift("Vetrov", rnd.Next(12, 250)),
                    new WorkerTimeShift("Zidorov", rnd.Next(12, 250)),
                    new WorkerTimeShift("Oleinik", rnd.Next(12, 250))
                };

            Console.WriteLine("Список работников до сортировки: ");
            foreach (WorkerBase w in listEmployee)
                Console.WriteLine(@"{0,-15} {1,10:f1}", w.Name, w.GetSalary());
            
            listEmployee.Sort(); //Сортировка по фамилии

            Console.WriteLine();
            Console.WriteLine("Список работников после сортировки <Фамилия>: ");
            foreach (WorkerBase w in listEmployee)
                Console.WriteLine(@"{0,-15} {1,10:f1}", w.Name, w.GetSalary());

            Console.WriteLine();
            Console.WriteLine("----------------------");
            Console.WriteLine("Тестировние ListWorkerClass: IEnumerable");
            Console.WriteLine("----------------------");

            ListWorkerClass listEmployee2 = new ListWorkerClass();

            Console.WriteLine("Список работников foreach: ");
            foreach (WorkerBase w in listEmployee2)
                Console.WriteLine(@"{0,-15} {1,10:f1}", w.Name, w.GetSalary());

            Console.ReadKey();
        }
    }
    #endregion

}
