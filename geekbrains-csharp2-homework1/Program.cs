using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geekbrains_csharp2_homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 820;
            form.Height = 640;
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);

        }
    }
}
