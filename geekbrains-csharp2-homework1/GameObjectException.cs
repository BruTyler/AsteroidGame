using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geekbrains_csharp2_homework1
{
    class GameObjectException: Exception
    {
        //Собственный класс исключения
        public GameObjectException()
        {
            Console.WriteLine(base.Message);
        }
    }
}
