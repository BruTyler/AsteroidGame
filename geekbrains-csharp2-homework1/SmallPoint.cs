using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class SmallPoint:BaseObject
    {
        public SmallPoint(Point pos, Point dir, Size size):base(pos, dir, size)
        {

        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X > Game.Width) Pos.X = 0;
            if (Pos.X < 0) Pos.X = Game.Width;
        }
    }
}
