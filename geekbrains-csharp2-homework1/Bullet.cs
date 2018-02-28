using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class Bullet: BaseObject
    {
        public Bullet(Point pos, Point dir, Size size):base(pos, dir, size)
        {

        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(new Pen(Color.Blue, 3), new Rectangle(Pos.X, Pos.Y, Size.Width, Size.Height));
        }

        public override void Update()
        {
            if (Pos.X != -1) Pos.X -= Dir.X;
            if (Pos.X > Game.Width || Pos.X < 0) Pos.X = -1;
        }
    }
}
