using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class Snake:BaseObject
    {
        protected int step;
        public Point prevPos;
        public Snake(Point pos, Point dir):base(pos, dir)
        {

        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X * Game.Scale, Pos.Y * Game.Scale, Game.Scale, Game.Scale);
            Game.Field[Pos.X, Pos.Y] = -2; //обозначить себя на клетке - для расчета коллизий
        }
        public virtual void Update(Point nextPosition)
        {
            prevPos = new Point(Pos.X, Pos.Y);
                
            if (nextPosition.X == -1 && nextPosition.Y == -1) //если змейка не знает куда идти, прокладываем новый путь
            {
                Random r = new Random();
                int r_fix = r.Next(100);
                if (r_fix < 3) //поворот на 90 градусов ВЛЕВО с вероятностью 10/100 
                {
                    if (Dir.X != 0)
                    {
                        Dir.X = 0;
                        Dir.Y = 1;
                    }
                    else
                    {
                        Dir.X = 1;
                        Dir.Y = 0;
                    }
                }
                else if (r_fix < 5) //поворот на 90 градусов ВПРАВО с вероятностью 10/100
                {
                    if (Dir.X != 0)
                    {
                        Dir.X = 0;
                        Dir.Y = -1;
                    }
                    else
                    {
                        Dir.X = -1;
                        Dir.Y = 0;
                    }
                }
                //с вероятностью 80% продолжит движение в том же направлении
                Pos.X += Dir.X ;
                Pos.Y += Dir.Y ;
                //при выходе за границы - вернуть с другой стороны доски
                if (Pos.X < 0) Pos.X += Game.Width / Game.Scale;
                if (Pos.X >= Game.Width / Game.Scale) Pos.X -=Game.Width/ Game.Scale;
                if (Pos.Y < 0) Pos.Y += Game.Height / Game.Scale;
                if (Pos.Y >= Game.Height / Game.Scale) Pos.Y -= Game.Height / Game.Scale;
            }
            else //туловище получает координаты головы
            {
                Pos.X = nextPosition.X;
                Pos.Y = nextPosition.Y;
            }
            Game.Field[prevPos.X, prevPos.Y] = 0; //очистить клетку после ухода с него

        }

    }
}
