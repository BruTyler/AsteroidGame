using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class SpaceShip:BaseObject
    {
        public SpaceShip(Point pos, Point dir, Size size):base(pos, dir, size)
        {      }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.spaceship, Pos.X, Pos.Y, Size.Width, Size.Height);
            PrintMessage($"{DateTime.Now} Отрисовка {this.GetType().Name} координаты X={Pos.X} Y={Pos.Y} \r");
        }

        public override void Update()
        {
            Pos.Y = Pos.Y + Dir.Y;
            if ((Pos.Y < Game.Height / 2 - Size.Height * 2) || (Pos.Y > Game.Height / 2 + Size.Height*2))
                Dir.Y = -Dir.Y;
        }


        
    }
}
