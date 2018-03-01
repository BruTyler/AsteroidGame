using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class BlinkStar:Star
    {
        int step; //переменная для регулировки скорости роста звезды
        
        public BlinkStar(Point pos, Point dir, Size size):base(pos, dir, size)
        {
            step = 1; //step = 1 тику таймера
        }

        public override void Draw()
        {
            if (step == 16) step = 1; //звезда плавно растет за 16 тиков таймера, затем счетчик обнуляется
            Game.Buffer.Graphics.DrawLine(Pens.White, 
                Pos.X + Size.Width - Size.Width / step, 
                Pos.Y - Size.Height + Size.Height / step, 
                Pos.X - Size.Width + Size.Width / step, 
                Pos.Y + Size.Height - Size.Height / step);
            Game.Buffer.Graphics.DrawLine(Pens.White,
                Pos.X - Size.Width + Size.Width / step,
                Pos.Y - Size.Height + Size.Height / step,
                Pos.X + Size.Width - Size.Width / step,
                Pos.Y + Size.Height - Size.Height / step);
            step++;
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

    }
}
