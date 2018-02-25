using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_csharp2_homework1
{
    class BannerObject:BaseObject
    {
        protected String Msg;
        protected Color MsgColor;
        public BannerObject(Point pos, Point dir, String msg, Color color):base(pos, dir)
        {
            Life = Game.LifeCount / 2;
            Msg = msg;
            MsgColor = color;
            Draw();
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawString(Msg, new Font("Arial", 32), new SolidBrush(MsgColor), new PointF ((float)Pos.X * Game.Scale, (float) Pos.Y * Game.Scale));
        }
    }
}
