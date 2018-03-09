using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class SpaceShip:BaseObject
    {
        private int _energy = 1000;
        private int _score = 0;
        public int Energy => _energy;
        public int Score => _score;
        public static event Message MessageDie;
        public SpaceShip(Point pos, Point dir, Size size):base(pos, dir, size)
        {      }

        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        public void AddScore(int n=1)
        {
            _score += n;
        }

        public void AddHealth(int n = 100)
        {
            _energy += n;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.spaceship, Pos.X, Pos.Y, Size.Width, Size.Height);
            //PrintMessage($"{DateTime.Now} Отрисовка {this.GetType().Name} координаты X={Pos.X} Y={Pos.Y} \r");
        }

        public override void Update()
        {
            //Pos.Y = Pos.Y + Dir.Y;
            //if ((Pos.Y < Game.Height / 2 - Size.Height * 2) || (Pos.Y > Game.Height / 2 + Size.Height*2))
            //    Dir.Y = -Dir.Y;
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }




    }
}
