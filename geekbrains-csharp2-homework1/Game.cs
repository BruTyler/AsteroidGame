using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        static Game()
        { }

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        public static BaseObject[] _objs;
        public static void Load()
        {

            Random r = new Random();
            int radius; //радиус больших планет
                       
            _objs = new BaseObject[84];
            for (int i = 0; i < 10; i++) 
            {
                //мигающие звезды
                _objs[i] = new BlinkStar(new Point(r.Next(0, Game.Width), r.Next(0, Game.Height)), new Point(r.Next(4), 0), new Size(7, 7));
            }
            for (int i = 10 ; i < 20; i++)
            {
                //маленькие звезды
                _objs[i] = new Star(new Point(r.Next(Game.Width), r.Next(Game.Height)), new Point(r.Next(5), 0), new Size(5, 5));
            }
            for (int i = 20; i < 80; i++)
            {
                //точки
                _objs[i] = new SmallPoint(new Point(r.Next(Game.Width), r.Next(Game.Height)), new Point(r.Next(3), 0), new Size(2, 2));
            }

            //Средняя планета
            radius = r.Next(20, 100);
            _objs[81] = new BigPoint(new Point(r.Next(20, Game.Width-20), r.Next(20, Game.Height-20)), new Point(r.Next(3), 0), new Size(radius, radius), r.Next(5));

            //Корабль
            _objs[82] = new SpaceShip(new Point(50, Game.Height/2), new Point(0, 2), new Size(30, 30));

            //Пуля
            _objs[80] = new Bullet(new Point(50, Game.Height / 2), new Point(-3, 0), new Size(5, 3));

            //Астероид
            _objs[83] = new Asteroid(new Point(650, Game.Height / 2), new Point(3, 0), new Size(50, 50), 20);

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
            {
                obj.Draw();
            }
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }

     }
}
