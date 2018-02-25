using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class BadApple:BaseObject
    {
        public BadApple(Point pos, Point dir):base(pos, dir)
        {
            Life = Game.LifeCount;
            Game.Field[Pos.X , Pos.Y ] = -1;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Red, Pos.X * Game.Scale, Pos.Y * Game.Scale, Game.Scale, Game.Scale);
        }

        public override void Update()
        {
            Life--;
            if (Life == 0)
                Game.Field[Pos.X, Pos.Y] = 0;
        }
    }
}
