using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class MedKit : BaseObject
    {
        public MedKit(Point pos, Point dir, Size size):base(pos, dir, size)
        {
            Life = 1;
        }


         
        public override void Draw()
        {
            if (Life>0)
            {
                Game.Buffer.Graphics.DrawImage(Properties.Resources.medkit, Pos.X, Pos.Y, Size.Width, Size.Height);
                PrintMessage($"{DateTime.Now} Отрисовка {this.GetType().Name} координаты X={Pos.X} Y={Pos.Y} Скороcть {Dir.X}\r");
            }
            
        }

        public override void Update()
        {
            Pos.X -= Dir.X;
            if (Pos.X < 0) Pos.X += Game.Width; //астероид вышел за пределы экрана
        }
    }
}
