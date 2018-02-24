using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class BigPoint:SmallPoint
    {
        Random r = new Random();
        Brush BrushColor;
        public BigPoint(Point pos, Point dir, Size size, int ColorNum):base(pos, dir, size)
        {
            switch (ColorNum)
            {
                case 1:
                    BrushColor = Brushes.LightBlue;
                    break;
                case 2:
                    BrushColor = Brushes.Red;
                    break;
                case 3:
                    BrushColor = Brushes.Pink;
                    break;
                case 4:
                    BrushColor = Brushes.Green;
                    break;
                default:
                    BrushColor = Brushes.Yellow;
                    break;
            }
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(BrushColor, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
    }
}
