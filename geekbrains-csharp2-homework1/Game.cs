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
        /// <summary>
        /// Инициализация обьектов и игрового поля
        /// </summary>
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        private static Timer _timer = new Timer();
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

            SpaceShip.MessageDie += Finish; 
            form.KeyDown += Form_KeyDown;

            _timer = new Timer { Interval = 100 };
            _timer.Start();
            _timer.Tick += Timer_Tick;
        }
        #endregion

        #region Load()
        /// <summary>
        /// Первоначальная загрузка объектов
        /// </summary>
        private static BaseObject[] _objs; //все объекты на экране
        private static SpaceShip _ship; //корабль
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        private static MedKit[] _medkit;

        public static void Load()
        {
            Random r = new Random();
            int radius; //радиус больших планет
            _objs = new BaseObject[82];
            _asteroids = new Asteroid[3];
            _medkit = new MedKit[2];



            for (int i = 0; i < 10; i++) //мигающие звезды
                _objs[i] = new BlinkStar(new Point(r.Next(0, Game.Width), r.Next(0, Game.Height)), new Point(r.Next(4), 0), new Size(7, 7));
            for (int i = 10; i < 20; i++) //маленькие звезды
                _objs[i] = new Star(new Point(r.Next(Game.Width), r.Next(Game.Height)), new Point(r.Next(5), r.Next(-1, 1)), new Size(5, 5));
            for (int i = 20; i < 80; i++) //точки
                _objs[i] = new SmallPoint(new Point(r.Next(Game.Width), r.Next(Game.Height)), new Point(r.Next(3), 0), new Size(2, 2));
            radius = r.Next(20, 50); //Средняя планета
            _objs[80] = new BigPoint(new Point(r.Next(20, Game.Width - 20), r.Next(20, Game.Height - 20)), new Point(r.Next(3), 0), new Size(radius, radius), r.Next(5));
            _objs[81] = new BigPoint(new Point(r.Next(20, Game.Width - 20), r.Next(20, Game.Height - 20)), new Point(r.Next(3), 0), new Size(radius, radius), r.Next(5));
            
            //Корабль
            _ship = new SpaceShip(new Point(50, Game.Height / 2), new Point(0, 2), new Size(30, 30));
            _ship.RegisterDelegate(Console.WriteLine);
            _ship.RegisterDelegate(PrintFile.Print);

            _medkit[0] = new MedKit(new Point(699, r.Next(0, Game.Height)), new Point(r.Next(1, 10), 0), new Size(50, 50));
            _medkit[0].RegisterDelegate(Console.WriteLine);

            //Астероиды
            for (var i = 0; i < _asteroids.Length; i++)
            {
                _asteroids[i] = new Asteroid(new Point(799, r.Next(0, Game.Height)), new Point(r.Next(1, 10), 0), new Size(50, 50), 20);
                //_asteroids[0] = new Asteroid(new Point(650, Game.Height/2 - 25), new Point(3, 0), new Size(50, 50), 20);
                //Астероид для проверки собственного Exception - слишком большой размер обьекта
                //_asteroids[0] = new Asteroid(new Point(650, Game.Height / 2 - 25), new Point(3, 0), new Size(171, 171), 20);
            }
        }
        #endregion

        #region KeyDown
        /// <summary>
        /// Обработка нажатий кнопок
        /// </summary>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                _bullet = new Bullet(new Point(_ship.Rect.X + 30, _ship.Rect.Y + 12), new Point(-5, 0), new Size(4, 1));
                _bullet.RegisterDelegate(Console.WriteLine);
                _bullet.RegisterDelegate(PrintFile.Print);
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }
        #endregion

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
            foreach (BaseObject obj in _objs) obj.Draw();
            foreach (Asteroid obj in _asteroids) obj.Draw();
            _ship?.Draw();
            _bullet?.Draw();

            foreach (MedKit obj in _medkit) obj?.Draw();

            if (_medkit[0] !=null)             _medkit[0].Draw();

            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy: " + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                Buffer.Graphics.DrawString("Очки: " + _ship.Score, SystemFonts.DefaultFont, Brushes.White, 0, 10);
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
                obj.Update();

            Random r = new Random();

            for (var i = 0; i < _asteroids.Length; i++)
            {
                _asteroids[i].Update();
                //проверка столкновения астероида с пулей
                if (_bullet != null && _asteroids[i].Collision(_bullet))
                {
                    _bullet.isIntersected = true;
                    _asteroids[i].isIntersected = true;
                    _ship.AddScore();
                }

                //убираем с экрана взорвавшиеся объекты с истекшим таймером отображения
                if (_asteroids[i].Life == 0)
                {
                    //и тут же перерисовываем эти объекты с дефолтными координатами
                    _asteroids[i] = new Asteroid(new Point(799, r.Next(0, Game.Height)), new Point(r.Next(1, 10), 0), new Size(50, 50), 20);
                }
            }

            for (var i = 0; i < _medkit.Length; i++)
            {
                _medkit[i]?.Update();
                if (_medkit[i] != null && _medkit[i].Collision(_ship))
                {
                    _medkit[i].Life  = 0;
                    _ship.AddHealth();
                }
            }

            _bullet?.Update();
            _ship?.EnergyLow(1);
            System.Media.SystemSounds.Asterisk.Play();
            if (_ship.Energy <= 0) _ship?.Die();


        }
        #endregion

        #region Finish()
        /// <summary>
        /// действия при окончании игры
        /// </summary>
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
        #endregion

    }
}
