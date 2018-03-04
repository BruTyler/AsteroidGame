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
        public Boolean isIntersected; //признак пересечения с другим объектом
        public Bullet(Point pos, Point dir, Size size):base(pos, dir, size)
        {
            Life = 1; //время отображения при встрече с другим объектом
            isIntersected = false;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            if (Pos.X != -10) Pos.X -= Dir.X;
            if (Pos.X > Game.Width || Pos.X < 0) Pos.X = -10;
            if (isIntersected) Life = 0;

        }
    }
}
