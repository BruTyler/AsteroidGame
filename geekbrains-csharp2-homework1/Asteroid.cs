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
        public Boolean isIntersected; //признак пересечения с другим объектом
        
        public Asteroid(Point pos, Point dir, Size size, int life):base(pos, dir, size)
        {
            Life = life; //время отображения взрыва
            r = new Random();
            isIntersected = false;
        }

        public override void Draw()
        {
            if (isIntersected) //проверка состоявшегося пересечения
                //отрисовка взрыва 
                Game.Buffer.Graphics.DrawImage(Properties.Resources.explode, Pos.X, Pos.Y, Size.Width, Size.Height);     
            else    
                //отрисовка целого астероида
                Game.Buffer.Graphics.DrawImage(Properties.Resources.asteroid, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            if(!isIntersected) //проверка состоявшегося пересечения
            {
                //пересения не было - сдвинуть астероид
                Pos.X -= Dir.X; 
                if (Pos.X < 0) Pos.X += Game.Width; //астероид вышел за пределы экрана
            }
            else
            {
                //Пересечение - уменьшить время отображения взрыва
                Life--; 
            }
        }
    }
}
