using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace geekbrains_csharp2_homework1
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Scale { get; set; }
        public static int LifeCount { get; set; }
        private static Point TargetField { get; set; }
        public static int[,] Field { get; set; }
        private static bool isGameStarted;

        static Game()
        { }

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width-20;
            Height = form.Height-40;
            Scale = 10;
            LifeCount = 100;
            isGameStarted = false;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Field = new int[Width / Scale, Height / Scale];
            for (int i = 0; i < Width/ Scale; i++)
            {
                for (int j = 0; j < Height / Scale; j++)
                {
                    Field[i, j] = 0;
                }
            }

            Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        public static List<Snake> _objs_snake;
        public static List<BaseObject> _objs;
        public static List<int> _objs_deleted;
        public static void Load()
        {
            Random r = new Random();
            _objs = new List<BaseObject>();
            _objs_snake = new List<Snake>();
            for (int i = 0; i < 60; i++) 
            {
                _objs.Add(new Apple(new Point(r.Next(Game.Width/ Scale), r.Next(Game.Height/ Scale)), new Point(r.Next(4), 0)));
            }
            
            for (int i = 1; i <= 10; i++)
            {
                _objs_snake.Add(new Snake(new Point(i+30, 30), new Point(-1, 0)));
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            DeleteOld();
            CreateNew();
            if (isGameStarted)
                CalculateCollision();
            Draw();
            Update();
        }

        public static void DeleteOld()
        {
            _objs_deleted = new List<int>();
            foreach (BaseObject currentObject in _objs)
            {
                if (currentObject.Life == 0)
                    _objs_deleted.Add(_objs.IndexOf(currentObject));
            }
            _objs_deleted.Reverse();
            foreach (int i in _objs_deleted)
            {
                _objs.RemoveAt(i);
            }
        }

        public static void CreateNew()
        {
            Random r = new Random();
            int rTakeObject = r.Next(7);
            int rTypeObject = r.Next(7);

            if (rTakeObject == 5)
            {
                bool checkFreeField=false;
                do
                {
                    Point newPoint = new Point(r.Next(Game.Width / Scale), r.Next(Game.Height / Scale));
                    if (Game.Field[newPoint.X, newPoint.Y] != 0)
                    { continue; }
                    else
                    {
                        if (rTypeObject == 5) //1 к 7 что выпадет гнилое яблоко
                            _objs.Add(new BadApple(newPoint, new Point(0, 0)));
                        else
                            _objs.Add(new Apple(newPoint, new Point(0, 0)));
                        checkFreeField = true;
                    }
                }
                while (!checkFreeField);
            }
        }

        public static void CalculateCollision()
        {
            if(_objs_snake.ElementAt(0).prevPos.X > 0 )
            TargetField = new Point(_objs_snake.ElementAt(0).Pos.X, _objs_snake.ElementAt(0).Pos.Y);
            switch (Game.Field[TargetField.X, TargetField.Y])
            {
                case -1:
                    _objs.Add(new BannerObject(TargetField, new Point(0, 0), "Ooops", Color.Red));
                    break;
                case 1:
                    _objs.Add(new BannerObject(TargetField, new Point(0, 0), "+1", Color.Aqua));
                    break;
                default:
                    break;
            }
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            //for (int i = 0; i < Width / Scale; i++)
            //{
            //    for (int j = 0; j < Height / Scale; j++)
            //    {
            //        Game.Buffer.Graphics.DrawEllipse(Pens.White, i * Game.Scale, j * Game.Scale, Game.Scale, Game.Scale);
            //    }
            //}

            foreach (BaseObject obj in _objs)
            {
                obj.Draw();
            }

            foreach (Snake obj in _objs_snake)
            {
                obj.Draw();
            }
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            
            _objs_deleted = new List<int>();
            foreach (BaseObject currentObject in _objs)
            {
                if (currentObject.Life == 0)
                    _objs_deleted.Add(_objs.IndexOf(currentObject));
            }
            _objs_deleted.Reverse();
            foreach (int i in _objs_deleted)
            {
                _objs.RemoveAt(i);
            }
            

            foreach (Snake obj in _objs_snake)
            {
                int currentIndex = _objs_snake.IndexOf(obj);
                //Console.WriteLine($"obj obj.")

                if (currentIndex == 0)
                {
                    obj.Update(new Point(-1, -1));
                    //Console.WriteLine($"+++");
                    //Console.Write($"currentIndex = {currentIndex} ;");

                }
                else 
                {
                    obj.Update(new Point(_objs_snake.ElementAt(currentIndex - 1).prevPos.X, _objs_snake.ElementAt(currentIndex - 1).prevPos.Y));
                    //Console.WriteLine($"---");
                    //Console.Write($"currentIndex = {currentIndex} ;");
                    //Console.Write($" Pos.X = {_objs_snake.ElementAt(currentIndex - 1).prevPos.X} ");
                    //Console.Write($" Pos.Y = {_objs_snake.ElementAt(currentIndex - 1).prevPos.Y} ");

                }
            }

            foreach (BaseObject obj in _objs)
                obj.Update();
            if (!isGameStarted)
                isGameStarted = true;
        }

     }
}
