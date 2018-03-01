﻿using System;
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
        
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos; 
            Dir = dir; 
            Size = size; 
        }
        public abstract void Draw();
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        //описываемый прямоугольник для расчета коллизий
        public Rectangle Rect => new Rectangle(Pos, Size); 

    }
}
