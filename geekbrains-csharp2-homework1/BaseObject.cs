using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    public abstract class BaseObject: ICollision
    {
        protected Point Pos; //позиция на поле
        protected Point Dir; //вектор движения
        protected Size Size; //размер объекта
        public int Life { get; protected set; } //время для показа взрыва астероида

        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
            CheckGameException();
        }
        public abstract void Draw();
        public abstract void Update();

        public virtual void CheckGameException()
        {
            if (   Pos.X < 0
                || Pos.Y < 0
                || Math.Abs(Dir.X) > 5
                || Math.Abs(Dir.Y) > 5
                || Size.Width < 0
                || Size.Height < 0
                || Size.Width > 100
                || Size.Height > 100 )
                throw new GameObjectException();
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        //описываемый прямоугольник для расчета коллизий
        public Rectangle Rect => new Rectangle(Pos, Size); 

    }
}
