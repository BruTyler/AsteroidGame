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
        
        #region Init(Form form)
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
            if (Width < 0 || Width > 1000 || Height < 0 || Height > 1000)
                throw new ArgumentOutOfRangeException();

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();

            form.KeyDown += Form_KeyDown;


            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }
        #endregion
        
        #region Load()
        private static BaseObject[] _objs; //все объекты на экране
        private static SpaceShip _ship; //корабль

        public static void Load()
        {
            Random r = new Random();
            int radius; //радиус больших планет
                       
            _objs = new BaseObject[84];
            for (int i = 0; i < 10; i++) //мигающие звезды
                _objs[i] = new BlinkStar(new Point(r.Next(0, Game.Width), r.Next(0, Game.Height)), new Point(r.Next(4), 0), new Size(7, 7));

            for (int i = 10 ; i < 20; i++) //маленькие звезды
                _objs[i] = new Star(new Point(r.Next(Game.Width), r.Next(Game.Height)), new Point(r.Next(5), r.Next(-1, 1)), new Size(5, 5));

            for (int i = 20; i < _objs.Length-4; i++) //точки
                _objs[i] = new SmallPoint(new Point(r.Next(Game.Width), r.Next(Game.Height)), new Point(r.Next(3), 0), new Size(2, 2));
            
            radius = r.Next(20, 50); //Средняя планета
            _objs[80] = new BigPoint(new Point(r.Next(20, Game.Width-20), r.Next(20, Game.Height-20)), new Point(r.Next(3), 0), new Size(radius, radius), r.Next(5));
            _objs[81] = new BigPoint(new Point(r.Next(20, Game.Width - 20), r.Next(20, Game.Height - 20)), new Point(r.Next(3), 0), new Size(radius, radius), r.Next(5));
            //Корабль
            
            
            _ship = new SpaceShip(new Point(50, Game.Height/2), new Point(0, 2), new Size(30, 30));
            _ship.RegisterDelegate(Console.WriteLine);
            _ship.RegisterDelegate(PrintFile.Print);

            //Пуля
            _objs[82] = new Bullet(new Point(75, Game.Height / 2), new Point(-4, 0), new Size(5, 3));
            _objs[82].RegisterDelegate(Console.WriteLine);
            _objs[82].RegisterDelegate(PrintFile.Print);
            
            //Астероид
            _objs[83] = new Asteroid(new Point(650, Game.Height/2 - 25), new Point(3, 0), new Size(50, 50), 20);
            //Астероид для проверки собственного Exception - слишком большой размер обьекта
            //_objs[83] = new Asteroid(new Point(650, Game.Height / 2 - 25), new Point(3, 0), new Size(171, 171), 20);
        }
        #endregion

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4),
            new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        #region Timer_Tick
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        #endregion

        #region Draw()
        /// <summary>
        /// отрисовка объектов для текущего шага
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
            {
                obj.Draw();
            }
            Buffer.Render();
        }
        #endregion

        #region Update()
        /// <summary>
        /// пересчет координат объектов для следующего шага
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
            {
                obj.Update();
                //проверка столкновения астероида с пулей
                if (obj is Asteroid && obj.Collision(_objs[82]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    ((Bullet)_objs[82]).isIntersected = true;
                    ((Asteroid)_objs[83]).isIntersected = true;
                }

                //убираем с экрана взорвавшиеся объекты с истекшим таймером отображения
                if (obj.Life==0)
                {
                    //и тут же перерисовываем эти объекты с дефолтными координатами
                    if (obj is Bullet)
                    {
                        _objs[82] = new Bullet(new Point(75, Game.Height / 2), new Point(-4, 0), new Size(5, 3));
                        _objs[82].RegisterDelegate(Console.WriteLine);
                        _objs[82].RegisterDelegate(PrintFile.Print);
                    }
                    if (obj is Asteroid)
                        _objs[83] = new Asteroid(new Point(650, Game.Height / 2 - 25), new Point(3, 0), new Size(50, 50), 20);
                }
            }
        }
        #endregion
     }
}
