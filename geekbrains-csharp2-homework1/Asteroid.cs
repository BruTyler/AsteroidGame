using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class Asteroid: BaseObject
    {
        protected Random r;
        protected Boolean isIntersected;
        public int Life { get; protected set; }
        public Asteroid(Point pos, Point dir, Size size, int life):base(pos, dir, size)
        {
            Life = life;
            r = new Random();
            isIntersected = false;
        }

        public override void Draw()
        {

            if (!isIntersected)
            {
                switch (r.Next(50))
                {
                    case 1:
                        isIntersected = true;
                        break;
                    default:
                        Game.Buffer.Graphics.DrawImage(Properties.Resources.asteroid, Pos.X, Pos.Y, Size.Width, Size.Height);
                        break;
                }
            }
            else
            {
                Game.Buffer.Graphics.DrawImage(Properties.Resources.explode, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
           
        }

        public override void Update()
        {
            if(!isIntersected)
            {
                if (Pos.X != -1) Pos.X -= Dir.X;
                if (Pos.X > Game.Width || Pos.X < 0) Pos.X = -1;
            }
            else
            {
                Life--;
            }

        }
    }
}
