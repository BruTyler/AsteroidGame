using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class BaseObject
    {
        public Point Pos;
        protected Point Dir;
        public int Life { get; set; }
        public BaseObject(Point pos, Point dir)
        {
            Pos = pos;
            Dir = dir;
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X*Game.Scale, Pos.Y* Game.Scale, Game.Scale, Game.Scale);
        }

        public virtual void Update()
        {
            Life--;
        }
    }
}
