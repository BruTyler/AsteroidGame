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
        Point fixPos;
        int step;
        
        public BlinkStar(Point pos, Point dir, Size size):base(pos, dir, size)
        {
            step = 2;
        }

        public override void Draw()
        {
            if (step == 16) step = 2;
            fixPos.X = Pos.X / step;
            fixPos.Y = Pos.Y / step;
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

    }
}
